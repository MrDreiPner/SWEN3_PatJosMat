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

        public string OcrPdf(MemoryStream pdfStream)
        {
            pdfStream.Seek(0, SeekOrigin.Begin);
            _logger.Info("Starting OCR operation with PDF file (length)"+pdfStream.Length);
            string tessDataPath = _configuration["TesseractOCR:Path"];
            string language = _configuration["TesseractOCR:Language"];

            var stringBuilder = new StringBuilder();
            try
            {
                using (var magickImages = new MagickImageCollection())
                {

                    _logger.Info("Converting PDF to images");
                    magickImages.Read(pdfStream);

                    _logger.Info("Performing OCR on each image");
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
                _logger.Info("Extracted string: " + stringBuilder.ToString());
                
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return stringBuilder.ToString();
        }
    }
}