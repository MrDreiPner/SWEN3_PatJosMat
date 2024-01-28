using Moq;
using System.Text;
using NPaperless.BusinessLogic.Interfaces;
using AutoMapper;
using FluentValidation;
using NPaperless.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NPaperless.BusinessLogic.Entities;
using NPaperless.DataAccess.Entities;
using Minio;
using NPaperless.BusinessLogic.Services;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;

namespace NPaperless.BusinessLogic.Tests
{
    public class DocumentServiceTests

    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IValidator<DocumentBL>> validatorBL = new Mock<IValidator<DocumentBL>>();
        Mock<FluentValidation.Results.ValidationResult> validationResult = new Mock<FluentValidation.Results.ValidationResult>();
        Mock<IDocumentDALRepository> repository = new Mock<IDocumentDALRepository>();
        Mock<IMinioClient> minio = new Mock<IMinioClient>();
        Mock<IMessageSender> messageSender = new Mock<IMessageSender>();

        DocumentDAL documentDAL = new DocumentDAL() { Title = "Test Title" };
        DocumentService? documentService;

        [SetUp]
        public void Setup()
        {
            validatorBL.Setup(x => x.Validate(It.IsAny<ValidationContext<DocumentBL>>())).Returns(validationResult.Object);
            repository.Setup(x => x.CreateDocument(It.IsAny<DocumentDAL>())).Returns(1);
            messageSender.Setup(x => x.SendMessage(It.IsAny<string>()));
            mapper.Setup(x => x.Map<DocumentDAL>(It.IsAny<DocumentBL>())).Returns(documentDAL);


            documentService = new DocumentService(mapper.Object, validatorBL.Object, repository.Object, minio.Object, messageSender.Object);

        }

        [Test]
        public void CreateDocument_WhenCalledWithValidDocument_ReturnsOkResult()
        {
            // Arrange
            DocumentBL document = new DocumentBL();
            document.Title = "Test Title";
            document.UploadDocument = CreateTemporaryFile("Test Title", "application/pdf");

            validationResult.Setup(x => x.IsValid).Returns(true);

            //Act
            var result = documentService.CreateDocument(document);

            //Assert
            Assert.IsInstanceOf<OkResult>(result.Result);
        }

        [Test]
        public void CreateDocument_WhenCalledWithInvalidDocument_ReturnsBadRequestResult()
        {
            // Arrange
            DocumentBL document = new DocumentBL();
            document.Title = "Test Title";
            document.UploadDocument = CreateTemporaryFile("Test Title", "application/pdf");

            validationResult.Setup(x => x.IsValid).Returns(false);

            //Act
            var result = documentService.CreateDocument(document);

            //Assert
            Assert.IsInstanceOf<BadRequestResult>(result.Result);
        }



        private IFormFile CreateTemporaryFile(string fileName, string contentType)
        {

            byte[] bytes = Encoding.UTF8.GetBytes("Test content");

            var formFile = new FormFile(
                baseStream: new MemoryStream(bytes),
                baseStreamOffset: 0,
                length: bytes.Length,
                name: "Data",
                fileName: fileName)
            {
                Headers = new HeaderDictionary()
            };

            formFile.ContentType = contentType;
            return formFile;
        }
    }
}