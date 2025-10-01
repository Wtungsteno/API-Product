using ITS.Day2.BL.Dtos;
using ITS.Day2.Models;
using ITS.Day2.Repositories;
using Moq;
using NUnit.Framework.Internal;

namespace ITS.Day2.BL.Test
{
    [TestFixture, Category("Class_Service")]
    public class ProductsServiceTest
    {
        private Randomizer randomizer;

        private Mock<IAppRepository> repoMock;
        private ProductService sut;


        [SetUp]
        public void Setup()
        {
            randomizer = TestContext.CurrentContext.Random;

            repoMock = new Mock<IAppRepository>(MockBehavior.Strict);

            sut = new ProductService(repoMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            repoMock.Verify();
        }

        [Test]
        public void When_Product_dont_exist_then_GetById_Throws_Exception()
        {
            int id = randomizer.Next();
            repoMock
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Product)null)
                .Verifiable();

            Assert.ThrowsAsync<Exception>(async () => await sut.GetByIdAsync(id));
        }

        [Test]
        public async Task GetById_Found_ReturnsOk()
        {
            int id = randomizer.Next();

            Product p = new Product()
            {
                Id = id,
                Name = randomizer.GetString(),
                Price = randomizer.NextDecimal(),
                Stock = randomizer.Next()
            };
            repoMock
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(p)
                .Verifiable();

            ProductDto result = await sut.GetByIdAsync(id);
            Assert.IsNotNull(result);

            Assert.That(result.Id, Is.EqualTo(p.Id));
            Assert.That(result.Name, Is.EqualTo(p.Name));
            Assert.That(result.Price, Is.EqualTo(p.Price));
            Assert.That(result.Stock, Is.EqualTo(p.Stock));
        }
    }
}