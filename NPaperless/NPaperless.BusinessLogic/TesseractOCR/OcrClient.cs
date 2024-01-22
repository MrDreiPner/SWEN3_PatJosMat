using ImageMagick;
using Microsoft.Extensions.Configuration;
using NPaperless.BusinessLogic.Interfaces;
using System.Text;
using Tesseract;

namespace NPaperless.BusinessLogic.TesseractOCR
{
    public class OcrClient : IOcrClient
    {
        private readonly IConfiguration _configuration;

        public OcrClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string OcrPdf(Stream pdfStream)
        {
            string tessDataPath = _configuration["TesseractOCR:Path"];
            string language = _configuration["TesseractOCR:Language"];

            var stringBuilder = new StringBuilder();
            using (var magickImages = new MagickImageCollection())
            {
                magickImages.Read(pdfStream);
                foreach (var magickImage in magickImages)
                {
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
                        }
                    }
                }
            }

            return stringBuilder.ToString();
        }
    }
}