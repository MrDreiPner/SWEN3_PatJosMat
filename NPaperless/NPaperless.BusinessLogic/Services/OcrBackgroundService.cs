﻿using AutoMapper;
using EasyNetQ.Consumer;
using FluentValidation;
using ImageMagick;
using log4net;
using Microsoft.Extensions.Hosting;
using Minio;
using Minio.Exceptions;
using NPaperless.BusinessLogic.Entities;
using NPaperless.BusinessLogic.Interfaces;
using NPaperless.BusinessLogic.RabbitMQ;
using NPaperless.BusinessLogic.TesseractOCR;
using NPaperless.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NPaperless.BusinessLogic.Services
{
    public class OcrBackgroundService : IHostedService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(OcrBackgroundService));
        private readonly IDocumentDALRepository _repository;
        private readonly IMinioClient _minio;
        private readonly IMessageReceiver _messageReceiver;
        private readonly IOcrClient _ocrClient;
        private readonly IElastic _elastic;

        public OcrBackgroundService(IDocumentDALRepository repository, IMinioClient minio, IMessageReceiver messageReceiver, IOcrClient ocrClient, IElastic elastic)
        {
            _repository = repository;
            _minio = minio;
            _ocrClient = ocrClient;
            _messageReceiver = messageReceiver;
            _elastic = elastic;

            _messageReceiver.OnReceived += (sender, e) =>
            {
                HandleOcrJob(e.Message);
            };
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _messageReceiver.Receive();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }

        private async Task HandleOcrJob(string message)
        {
            _logger.Info("We received the message -> "+message);
            int indexOfSeparator = message.IndexOf(',');
            string fileName = message.Substring(indexOfSeparator+2);
            var pdf = await GetFileFromMinIO(fileName);
            int id = int.Parse(message.Substring(0, indexOfSeparator));
            _logger.Info("We tried getting the pdf >" +fileName+" < from MinIO, lets see if its here.");
            if (pdf != null)
            {
                _logger.Info("We got something! ");
                string result = _ocrClient.OcrPdf(pdf);
                _logger.Info("OCR RESULT: "+ result);
                UploadOcrText(id, result);
                _logger.Info("updated db entry with ocr result");
                ElasticDocument doc = new ElasticDocument();
                doc.Id = id;
                doc.Title = fileName;
                doc.Content = result;
                _logger.Info("adding doc to Elastic Search");
                _elastic.AddDocumentAsync(doc);
                _logger.Info("Job's done");
            }
            else
            {
                _logger.Info("We got NULL-thing!");
            }
            
        }
 
        private void UploadOcrText(int id, string text)
        {
            try
            {
                _logger.Info("stepping into update");
                _repository.UpdateDocument(id, text);
                _logger.Info("Update complete");
            }
            catch (Exception ex) { }
            return;
        }

        protected async Task<MemoryStream> GetFileFromMinIO(string fileName)
        {
            MemoryStream? pdf = new MemoryStream();
            _logger.Info("Initial PDF Length: " + pdf.Length);
            try
            {
                StatObjectArgs statObjectArgs = new StatObjectArgs()
                                    .WithBucket("npaperless-bucket")
                                    .WithObject(fileName);
                await _minio.StatObjectAsync(statObjectArgs);

                var getObjectArgs = new GetObjectArgs()
                        .WithBucket("npaperless-bucket")
                        .WithObject(fileName)
                        .WithCallbackStream((stream) =>
                        {
                            stream.CopyTo(pdf); 
                        });
                await _minio.GetObjectAsync(getObjectArgs);
                if (pdf != null)
                {
                    _logger.Info("SUCCEEDED: We have copied a file successfully. PDF Length: " + pdf.Length);

                    //_ocrClient.OcrPdf(pdf);
                }
                else
                {
                    _logger.Info("FAILED: No file copied");
                    throw new Exception("No file copied");
                }
            }
            catch (MinioException e)
            {
                _logger.Error($"Minio Error: {e.Message}");
            }
            return pdf;
        }

    }
}
