using Moq;
using RabbitRegister.MockData;
using RabbitRegister.Model;
using RabbitRegister.Services;
using RabbitRegister.Services.RabbitService;
using RabbitRegister.Services.TrimmingService;

namespace Test1
{
    [TestClass]
    public class UnitTest1
    {
        private RabbitService _rabbitService;
        private Mock<DbGenericService<Rabbit>> _mockDbService;

        [TestInitialize]
        public void Initialize()
        {
            _mockDbService = new Mock<DbGenericService<Rabbit>>();
            var mockRabbits = MockRabbit.GetMockRabbits();
            _mockDbService.Setup(
                x => x.GetObjectsAsync()).ReturnsAsync(mockRabbits);

            _rabbitService = new RabbitService(_mockDbService.Object);
        }

        [TestMethod]
        public void Test_GetRabbit()
        {
            // Arrange
            int RabbitRegNo = 1;
            int BreederRegNo = 5095;

            // Act
            Rabbit rabbit = _rabbitService.GetRabbit(RabbitRegNo, BreederRegNo);

            // Assert
            Assert.IsNotNull(rabbit);
            var expected = "Kaliba";
            Assert.AreEqual(expected, rabbit.Name);
        }

        [TestMethod]
        public void Test_GetOwnedAliveRabbits()
        {

            // Arrange
            int breederRegNo = 5095;

            // Act
            List<Rabbit> ownedAliveRabbit = _rabbitService.GetOwnedAliveRabbits(breederRegNo);

            // Assert
            var expected = 4;
            Assert.AreEqual(expected, ownedAliveRabbit.Count);
        }
    }
}