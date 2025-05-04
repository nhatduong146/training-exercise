using ConcurrencyLab.Infrastructure.Helper.Parser;
using Moq;

namespace ConcurrencyLab.Test.Infrastructure.Helper.Parser
{
    [TestClass]
    public class JsonParserTests
    {
        [TestMethod]
        public void Test_DeserializeJson()
        {
            // Arrange
            var jsonParserMock = new Mock<IJsonParser>();
            string json = "{\"Name\":\"Phone\",\"Price\":300000}";
            var expectedProduct = new Product { Name = "Phone", Price = 300000 };

            jsonParserMock
                .Setup(p => p.DeserializeJson<Product>(json))
                .Returns(expectedProduct);

            // Act
            var result = jsonParserMock.Object.DeserializeJson<Product>(json);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, "Phone");
            Assert.AreEqual(result.Price, 300000);

            jsonParserMock.Verify(p => p.DeserializeJson<Product>(json), Times.Once);
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
    }
}
