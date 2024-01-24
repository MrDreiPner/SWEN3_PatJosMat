using ImageMagick;
using log4net;
using Microsoft.Extensions.Configuration;
using NPaperless.BusinessLogic.Interfaces;
using NPaperless.BusinessLogic.Services;
using System.Text;
using Tesseract;

namespace NPaperless.BusinessLogic.TesseractOCR
{
    public class OcrClient : IOcrClient
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(OcrBackgroundService));
        private readonly IConfiguration _configuration;

        public OcrClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> OcrPdf(MemoryStream pdfStream)
        {
            _logger.Info("Starting OCR operation, over");
            string tessDataPath = _configuration["TesseractOCR:Path"];
            string language = _configuration["TesseractOCR:Language"];

            var stringBuilder = new StringBuilder();

            pdfStream.Position = 0;
            try
            {
                using (var magickImages = new MagickImageCollection())
                {
                    _logger.Info("In MagicImages and doing magic");
                    await magickImages.ReadAsync(pdfStream); //We fucky wucky here fyi
                    _logger.Info("Magic is read");
                    foreach (var magickImage in magickImages)
                    {
                        _logger.Info("ForEaching");
                        // Set the resolution and format of the image (adjust as needed)
                        magickImage.Density = new Density(300, 300);
                        magickImage.Format = MagickFormat.Png;

                        // Perform OCR on the image
                        using (var tesseractEngine = new TesseractEngine(tessDataPath, language, EngineMode.Default))
                        {
                            using (var page = tesseractEngine.Process(Pix.LoadFromMemory(magickImage.ToByteArray())))
                            {

                                var extractedText = page.GetText();
                                stringBuilder.Append(extractedText);
                                _logger.Info("Text extracted");
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                _logger.Error("Error in OCR: " + ex);
            }
            _logger.Info("Extracted string: "+stringBuilder.ToString());
            return stringBuilder.ToString();
        }
    }
}