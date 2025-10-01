using ITS.Day2.API.Controllers;
using ITS.Day2.BL;
using ITS.Day2.BL.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework.Internal;

namespace ITS.Day2.API.Test
{
    [TestFixture, Category("Class_Controller")]
    public class ProductsControllerTest
    {
        private Randomizer randomizer;

        private Mock<IProductService> svcMock;
        private ProductsController sut;


        [SetUp]
        public void Setup()
        {
            randomizer = TestContext.CurrentContext.Random;

            svcMock = new Mock<IProductService>(MockBehavior.Strict);

            sut = new ProductsController(svcMock.Object)
            {
                ControllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() },
            };
        }

        [TearDown]
        public void Teardown()
        {
            svcMock.Verify();
        }


        [Test]
        public async Task GetById_Found_ReturnsOk()
        {
            int id = randomizer.Next();

            ProductDto p = new ProductDto();
            svcMock
                .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(p)
                .Verifiable();

            IActionResult result = await sut.GetById(id);

            OkObjectResult ok = result as OkObjectResult;
            Assert.IsNotNull(ok);

            ProductDto dto = ok.Value as ProductDto;
            Assert.IsNotNull(dto);
            Assert.That(p, Is.EqualTo(dto));
        }
    }
}