using Moq;
using RabbitRegister.MockData;
using RabbitRegister.Model;
using RabbitRegister.Services;
using RabbitRegister.Services.RabbitService;
using RabbitRegister.Services.TrimmingService;

namespace Test2
{
    [TestClass]
    public class UnitTest2
    {
        private Mock<RabbitService> _mockRabbitService;
        private Mock<DbGenericService<Trimming>> _mockDbTrimmingService;
        private Mock<DbGenericService<Rabbit>> _mockDbRabbitService;
        private TrimmingService _trimmingService;

        [TestInitialize]
        public void Initialize()
        {
            _mockDbRabbitService = new Mock<DbGenericService<Rabbit>>();

            _mockRabbitService = new Mock<RabbitService>(_mockDbRabbitService.Object);
            var mockRabbits = MockRabbit.GetMockRabbits();
            _mockRabbitService.Setup(
                x => x.GetAllRabbitsWithOwner(It.IsAny<int>())).Returns(mockRabbits);

            _mockDbTrimmingService = new Mock<DbGenericService<Trimming>>();
            var mockTrimmings = MockTrimming.GetMockTrimming();
            _mockDbTrimmingService.Setup(
                x => x.GetObjectsAsync()).ReturnsAsync(mockTrimmings);

            _mockDbTrimmingService.Setup(
                x => x.AddObjectAsync(It.IsAny<Trimming>()));

            _trimmingService = new TrimmingService(_mockDbTrimmingService.Object, _mockRabbitService.Object);
        }


        [TestMethod]
        public void Test_GetTrimmings()
        {

            // Arrange

            // Act
            List<Trimming> GetAllTrimmings = _trimmingService.GetTrimmings();

            // Assert
            var expected = 10;
            Assert.AreEqual(expected, GetAllTrimmings.Count);
        }

        [TestMethod]
        public void Test_GetTrimmingByRabbitRegNoAndBreederRegNo()
        {
            // Arrange
            int RabbitRegNo = 1;
            int BreederRegNo = 5095;

            // Act
            List<Trimming> trimmingsByRabbitRegNoAndBreederRegNo = _trimmingService.GetTrimmingByRabbitRegNoAndBreederRegNo(RabbitRegNo, BreederRegNo);

            // Assert
            var expected = 4;
            Assert.AreEqual(expected, trimmingsByRabbitRegNoAndBreederRegNo.Count);
        }

        [TestMethod]
        public void Test_GetTrimmingsByOwnerId()
        {
            // Arrange
            int OwnerId = 5095;

            // Act 
            List<Trimming> trimmingsByOwnerId = _trimmingService.GetTrimmingsByOwnerId(OwnerId);

            // Assert
            var expected = 10;
            Assert.AreEqual(expected, trimmingsByOwnerId.Count);
        }

        [TestMethod]
        public void Test_AddTrimmingAsync()
        {
            // Arrange
            var trimming = new Trimming(1, 5095, "Fnugi", new DateTime(), 65, 6, 18, 85, 75, 20);

            // Act 
            _trimmingService.AddTrimmingAsync(trimming);

            // Assert
            _mockDbTrimmingService.Verify(x => x.AddObjectAsync(trimming), Times.Once());
        }
    }
}