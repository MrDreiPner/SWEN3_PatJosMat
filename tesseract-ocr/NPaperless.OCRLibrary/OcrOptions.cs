using System;

namespace NPaperless.OCRLibrary;

public class OcrOptions
{
    public const string OCR = "OCR";

    public string Language { get; set; } = "en";
    public string TessDataPath { get; set; } = "./tessdata";
}