using AutoFixture;
using Entities;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace StocksAppTests
{
    public class StocksServiceTests
    {
        private readonly IStockService _stocksService;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly Mock<IStockRepository> _stockRepositoryMock;
        private readonly IStockRepository _stockRepository;
        private readonly IFixture _fixture;


        public StocksServiceTests(ITestOutputHelper testOutputHelper)
        {

            _testOutputHelper = testOutputHelper;
            _fixture = new Fixture();
            _stockRepositoryMock = new Mock<IStockRepository>();
            _stockRepository = _stockRepositoryMock.Object;
            _stocksService = new StocksService(_stockRepository);
        }
        #region CreateBuyOrder
        // When you supply BuyOrderRequest as null, it should throw ArgumentNullException
        [Fact]
        public async Task CreateBuyOrder_NullBuyOrder_ThrowsArgumentNullException()
        {
            // Arrange
            BuyOrderRequest? buyOrderRequest = null;


            //act 
            Func<Task> action = async () =>
                  {
                      await _stocksService.CreateBuyOrder(buyOrderRequest);
                  };
            // Assert

            await action.Should().ThrowAsync<ArgumentNullException>();


        }



        //When you supply buyOrderQuantity as 0 (as per the specification, minimum is 1),
        //it should throw ArgumentException
        [Theory]
        [InlineData(0)]
        public async Task CreateBuyOrder_ForZeroQuantity_ThrowsArgumentException(uint buyOrderQuantity)
        {
            // Arrange

            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, buyOrderQuantity)

                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            Func<Task> action = async () =>

                // Act
                await _stocksService.CreateBuyOrder(buyOrderRequest);

            await action.Should().ThrowAsync<ArgumentException>();

        }
        //When you supply buyOrderQuantity as 100001 (as per the specification, maximum is 100000),
        //        //it should throw ArgumentException
        [Theory]
        [InlineData(100001)]
        public async Task CreateBuyOrder_GreaterThenMaxQuantity_ThrowsArgumentException(uint buyOrderQuantity)
        {
            //Arrange
           BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.Quantity, buyOrderQuantity)
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert

           await action.Should().ThrowAsync<ArgumentException>();


        }
        //When you supply stock symbol=null (as per the specification, stock symbol can't be null),
        //        //it should throw ArgumentException
        [Fact]

        public async Task CreateBuyOrder_NullStockSymbol_ThrowsArgumentException()
        {
            //Arrange

            BuyOrderRequest? buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert
            await action.Should().ThrowAsync<ArgumentException>();

        }
        // When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) -
        // (as per the specification, it should be equal or newer date than 2000-01-01),
        // it should throw ArgumentException.

        [Fact]

        public async Task CreateBuyOrder_DateBeforeMinimumYear2000_ThrowsArgumentException()
        {
            //Arrange
            BuyOrderRequest buyOrderRequest = _fixture.Build<BuyOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("1982-10-15"))
                .Create();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act

            Func<Task> action = async () =>
            {
                await _stocksService.CreateBuyOrder(buyOrderRequest);
            };

            //Assert

            await action.Should().ThrowAsync<ArgumentException>();




        }
        //If you supply all valid values, it should be successful and return
        //an object of BuyOrderResponse type with auto-generated BuyOrderID (guid).

        [Fact]

        public async Task CreateBuyOrder_FullValidValues_Success()
        {
            //Arrange

            BuyOrderRequest? buyOrderRequest = _fixture.Create<BuyOrderRequest>();

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>())).ReturnsAsync(buyOrder);

            //Act

            BuyOrderResponse buyOrderResponse = await _stocksService.CreateBuyOrder(buyOrderRequest);

            //Assert
            buyOrder.BuyOrderID =(Guid)buyOrderResponse.BuyOrderID;
            buyOrderResponse.BuyOrderID.Should().NotBe(Guid.Empty);
            BuyOrderResponse expectedResponse= buyOrder.ToBuyOrderResponse();
            buyOrderResponse.Should().Be(expectedResponse);




        }

        #endregion

        #region CreateSellOrder

        //When you supply SellOrderRequest as null, it should throw
        //ArgumentNullException

        [Fact]

        public async Task CreateSellOrder_NullSellOrder()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = null;

            //Act

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            // Assert

            await action.Should().ThrowAsync<ArgumentNullException>();



        }

        //        //When you supply sellOrderQuantity as 0 (as per the specification, minimum is 1),
        //        //it should throw ArgumentException

        [Theory]
        [InlineData(0)]

        public async Task CreateSellOrder_QuantityZero_ThrowsArgumentException(uint stockQuantity)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, stockQuantity)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert

            await action.Should().ThrowAsync<ArgumentException>();
        }

        //When you supply sellOrderQuantity as 100001 (as per the specification,
        //maximum is 100000), it should throw ArgumentException

        [Theory]
        [InlineData(100001)]

        public async Task CreateSellOrder_GreaterThenMaxQuantity_ThrowsArgumentException(uint sellOrderQuantity)
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Quantity, sellOrderQuantity)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert

            await action.Should().ThrowAsync<ArgumentException>();

        }

        //When you supply sellOrderPrice as 0 (as per the specification, minimum is 1),
        //it should throw ArgumentException.

        [Theory]
        [InlineData(0)]

        public async Task CreateSellOrder_PriceZero_ThrowsArgumentException(uint sellOrderPrice)
        {
            //Arrange

            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Price, sellOrderPrice)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert

            action.Should().ThrowAsync<ArgumentException>();


        }
        //When you supply sellOrderPrice as 10001 (as per the specification, maximum
        //is 100000), it should throw ArgumentException.

        [Theory]
        [InlineData(100001)]
        public async Task CreateSellOrder_GreaterThenMaxPrice_ThjrowsArgumentException(uint sellOrderPrice)
        {
            //Arrange
            
            SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.Price, sellOrderPrice)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            action.Should().ThrowAsync<ArgumentException>();
        }

        //When you supply stock symbol=null (as per the specification,
        //stock symbol can't be null), it should throw ArgumentException.

        [Fact]

        public async Task CreateSellOrder_NullStockSymbol_ThrowsArgumentException()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest= _fixture.Build<SellOrderRequest>()
                .With(temp => temp.StockSymbol, null as string)
                .Create();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act

            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };

            //Assert

            action.Should().ThrowAsync<ArgumentException>();

        }

        //When you supply dateAndTimeOfOrder as "1999-12-31" (YYYY-MM-DD) -
        //(as per the specification, it should be equal or newer date than 2000-01-01),
        //it should throw ArgumentException.

        [Fact]
        public async Task CreateSellOrder_DateBeforeMinimum_ThrowsArgumentException()
        {
            //Arrange

           SellOrderRequest? sellOrderRequest = _fixture.Build<SellOrderRequest>()
                .With(temp => temp.DateAndTimeOfOrder, DateTime.Parse("1982-10-15"))
                .Create();
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();
            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);
            //Act
            Func<Task> action = async () =>
            {
                await _stocksService.CreateSellOrder(sellOrderRequest);
            };
            //Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        // If you supply all valid values, it should be successful and return
        // an object of SellOrderResponse type with auto-generated SellOrderID (guid).

        [Fact]
        public async Task CreateSellOrder_FullValidValues_Success()
        {
            //Arrange
            SellOrderRequest? sellOrderRequest = _fixture.Create<SellOrderRequest>();

            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            _stockRepositoryMock.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>())).ReturnsAsync(sellOrder);

            //Act

            SellOrderResponse sellOrderResponse = await _stocksService.CreateSellOrder(sellOrderRequest);

            //Assert
            sellOrder.SellOrderID = (Guid)sellOrderResponse.SellOrderID;
            sellOrderResponse.SellOrderID.Should().NotBe(Guid.Empty);
            SellOrderResponse expectedResponse = sellOrder.ToSellOrderResponse();
            sellOrderResponse.Should().Be(expectedResponse);





        }

        #endregion

        #region GetAllBuyOrders

        //When there are no buy orders, it should return an empty list.

        [Fact]

        public async Task GetAllBuyOrders_DefaultList_ToBeEmptyList()
        {
            //Arrange
            List<BuyOrder> buyOrder = new List<BuyOrder>();

            _stockRepositoryMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(buyOrder);
            //Act
            List<BuyOrderResponse> buyOrderResponses = await _stocksService.GetBuyOrders();

            //Assert

            buyOrderResponses.Should().BeEmpty();
        }
        //When there are buy orders, it should return a list of BuyOrderResponse objects.

        [Fact]

        public async Task GetAllBuyOrders_WithFewBuyOrders_ToBeSuccessful()

        {
            //Arrange

            List<BuyOrder> buyOrders_request = new List<BuyOrder>()
            {
                _fixture.Create<BuyOrder>(),
                _fixture.Create<BuyOrder>(),
                _fixture.Create<BuyOrder>()
            };

            List<BuyOrderResponse> buyOrders_list_expected = buyOrders_request.Select(temp => temp.ToBuyOrderResponse()).ToList();
           

            _stockRepositoryMock.Setup(temp => temp.GetBuyOrders()).ReturnsAsync(buyOrders_request);

            //Act

           List<BuyOrderResponse> buyOrders_list_from_get = await _stocksService.GetBuyOrders();

            //Assert
            buyOrders_list_from_get.Should().BeEquivalentTo(buyOrders_list_expected);


        }

        #endregion

        #region GetAllSellOrders

        //When there are no sell orders, it should return an empty list.

        [Fact]

        public async Task GetAllSellOrders_DefaultList_ToBeEmptyList()
        {
            //Arrange

            List<SellOrder> sellOrders = new List<SellOrder>();

            _stockRepositoryMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(sellOrders);

            //Act

            List<SellOrderResponse> sellOrders_from_get = await _stocksService.GetSellOrders();

            //Assert

            sellOrders_from_get.Should().BeEmpty();

        }

        //When there are sell orders, it should return a list of SellOrderResponse objects.

        [Fact]

        public async Task GetAllSellOrders_WithFewSellOrders_ToBeSuccessfull()
        {
            //Arrange

            List<SellOrderRequest> sellOrderRequests = new List<SellOrderRequest>()
            {
                _fixture.Create<SellOrderRequest>(),
                _fixture.Create<SellOrderRequest>(),
                _fixture.Create<SellOrderRequest>()
            };

            List<SellOrder> sellOrders_expected = sellOrderRequests.Select(temp => temp.ToSellOrder()).ToList();

            _stockRepositoryMock.Setup(temp => temp.GetSellOrders()).ReturnsAsync(sellOrders_expected);

            //Act

            List<SellOrderResponse> sellOrders_from_get = await _stocksService.GetSellOrders();

            //Assert

            sellOrders_from_get.Should().BeEquivalentTo(sellOrders_expected.Select(temp => temp.ToSellOrderResponse()));




        }

        #endregion
    }
}