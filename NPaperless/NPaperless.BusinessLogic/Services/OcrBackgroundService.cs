using AutoMapper;
using FluentValidation;
using log4net;
using Microsoft.Extensions.Hosting;
using Minio;
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
            LoopDiLoop();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }

        private async Task LoopDiLoop()
        {
            _logger.Info("starting ocr background service");
            while (true)
            {
                _messageReceiver.Receive();
                await Task.Delay(1000);
            }
        }

        private async Task HandleOcrJob(string message)
        {
            _logger.Info(message);
        }
    }
}
