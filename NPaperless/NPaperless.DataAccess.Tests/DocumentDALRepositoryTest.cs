using Microsoft.EntityFrameworkCore;
using NPaperless.DataAccess.Entities;
using NPaperless.DataAccess.SQL;

namespace NPaperless.DataAccess.Tests
{
    internal class DocumentDALRepositoryTest
    {

        private DocumentDALRepository repository;
        DocumentDAL document1 = new DocumentDAL()
        {
            Id = 1,
            OriginalFileName = "Test",
            Content = "Test content",
        };

        DocumentDAL document2 = new DocumentDAL()
        {
            Id = 2,
            OriginalFileName = "Test2",
            Content = "Test content2",
        };

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<NPaperlessDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using (var context = new NPaperlessDbContext(options))
            {
                context.Documents.Add(document1);

                context.SaveChanges();
            }
        }

        [TearDown]
        public void TearDown()
        {
            var options = new DbContextOptionsBuilder<NPaperlessDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDb")
            .Options;

            using (var context = new NPaperlessDbContext(options))
            {
                context.Documents.RemoveRange(context.Documents);
                context.SaveChanges();
            }
        }

        [Test]
        public void CreateDocument_WhenCalled_CreatesDocumentInDatabase()
        {
            var options = new DbContextOptionsBuilder<NPaperlessDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using (var context = new NPaperlessDbContext(options))
            {
                repository = new DocumentDALRepository(context);
                repository.CreateDocument(document2);


                Assert.That(repository.GetAllDocuments().Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void GetAllDocuments_WhenCalled_ReturnsListOfAllDocuments()
        {
            var options = new DbContextOptionsBuilder<NPaperlessDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using (var context = new NPaperlessDbContext(options))
            {
                repository = new DocumentDALRepository(context);
                var documents = repository.GetAllDocuments();
                Assert.That(documents.Count, Is.EqualTo(1));
            }
        }

        [Test]
        public void UpdateDocuments_WhenCalled_UpdatesDocuments()
        {
            string content = "New Content";

            var options = new DbContextOptionsBuilder<NPaperlessDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            using (var context = new NPaperlessDbContext(options))
            {
                repository = new DocumentDALRepository(context);
                repository.UpdateDocument(document1.Id, content);
                var result = repository.GetDocument(document1.Id);
                Assert.That(result.Content, Is.EqualTo(content));
            }
        }
    }
}