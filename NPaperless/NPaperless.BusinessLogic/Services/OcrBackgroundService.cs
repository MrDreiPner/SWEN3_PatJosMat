using AutoMapper;
using EasyNetQ.Consumer;
using FluentValidation;
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

        public OcrBackgroundService(IDocumentDALRepository repository, IMinioClient minio, IMessageReceiver messageReceiver, IOcrClient ocrClient)
        {
            _repository = repository;
            _minio = minio;
            _ocrClient = ocrClient;
            _messageReceiver = messageReceiver;

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
            int indexOfId = message.IndexOf(',');
            string fileName = message.Substring(indexOfId+2);
            var pdf = await GetFileFromMinIO(fileName);
            _logger.Info("We tried getting the pdf >" +fileName+" < from MinIO, lets see if its here.");
            if (pdf != null)
            {
                _logger.Info("We got something! ");
                string yesyes = _ocrClient.OcrPdf(pdf);
                _logger.Info("OCR RESULT: "+yesyes);
            }
            else
            {
                _logger.Info("We got NULL-thing!");
            }
        }

        protected async Task<Stream> GetFileFromMinIO(string fileName)
        {
            Stream? pdf = new MemoryStream();
            try
            {
                StatObjectArgs statObjectArgs = new StatObjectArgs()
                                    .WithBucket("npaperless-bucket")
                                    .WithObject(fileName);
                var isHere = await _minio.StatObjectAsync(statObjectArgs);

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
                    _logger.Info("SUCCEEDED: We have copied a file successfully");
                    //_ocrClient.OcrPdf(pdf);
                }
                else
                {
                    _logger.Info("FAILED: No file copied");
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
