using CourierKata;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CourierKataTests
{
    public class CourierKataTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        private readonly DeliveryCostCalculator _deliveryCalculator = new();

        public CourierKataTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CalculateDeliveryCost_Parcel_ReturnsDeliveryWithCosts()
        {

            //Arrange
            var parcels = new List<Parcel> { _fixture.SmallParcel };

            var delivery = new Delivery { Parcels = parcels };

            //Act
            var result = _deliveryCalculator.CalculateDeliveryCost(delivery);

            //Assert
            Assert.Collection(delivery.Parcels,
             item =>
             {
                 Assert.Equal(ParcelType.Small, item.ParcelType);
                 Assert.Equal(3, item.ParcelCost);
             });

            Assert.Equal(3, result.TotalCost);
        }

        [Fact]
        public void CalculateDeliveryCost_Parcels_ReturnsDeliveryWithCosts()
        {
            //Arrange
            var parcels = new List<Parcel> { _fixture.SmallParcel, _fixture.MediumParcel, _fixture.LargeParcel, _fixture.XLParcel };

            var delivery = new Delivery { Parcels = parcels };

            //Act
            var result = _deliveryCalculator.CalculateDeliveryCost(delivery);

            //Assert
            Assert.Collection(delivery.Parcels,
                item =>
                {
                    Assert.Equal(ParcelType.Small, item.ParcelType);
                    Assert.Equal(3, item.ParcelCost);
                },
                item =>
                {
                    Assert.Equal(ParcelType.Medium, item.ParcelType);
                    Assert.Equal(8, item.ParcelCost);
                },
                 item =>
                 {
                     Assert.Equal(ParcelType.Large, item.ParcelType);
                     Assert.Equal(15, item.ParcelCost);
                 },
                item =>
                {
                    Assert.Equal(ParcelType.XL, item.ParcelType);
                    Assert.Equal(25, item.ParcelCost);
                });

            Assert.Equal(51, result.TotalCost);
        }

        [Fact]
        public void CalculateDeliveryCost_InvalidParcelSize_ThrowsExceptionWithMessage()
        {
            //Arrange
            var parcels = new List<Parcel> {
                new Parcel { ParcelHeight = 1, ParcelWidth = 1, ParcelDepth = -1 },
            };
            var delivery = new Delivery { Parcels = parcels };

            //Assert
            var exception = Assert.Throws<ArgumentException>(() => _deliveryCalculator.CalculateDeliveryCost(delivery));
            Assert.Equal("Invalid parcel size", exception.Message);
        }


        //STEP 2

        [Fact]
        public void CalculateDeliveryCost_SpeedyShipping_ReturnsDeliveryWithSpeedyShippingCost()
        {
            //Arrange
            var parcels = new List<Parcel> { _fixture.SmallParcel, _fixture.MediumParcel, _fixture.LargeParcel, _fixture.XLParcel };

            var delivery = new Delivery { Parcels = parcels, SpeedyShipping = true };

            //Act
            var result = _deliveryCalculator.CalculateDeliveryCost(delivery);

            //Assert
            Assert.Collection(delivery.Parcels,
                item =>
                {
                    Assert.Equal(ParcelType.Small, item.ParcelType);
                    Assert.Equal(3, item.ParcelCost);
                },
                item =>
                {
                    Assert.Equal(ParcelType.Medium, item.ParcelType);
                    Assert.Equal(8, item.ParcelCost);
                },
                 item =>
                 {
                     Assert.Equal(ParcelType.Large, item.ParcelType);
                     Assert.Equal(15, item.ParcelCost);
                 },
                item =>
                {
                    Assert.Equal(ParcelType.XL, item.ParcelType);
                    Assert.Equal(25, item.ParcelCost);
                });

            Assert.Equal(51, result.SpeedyShippingCost);
            Assert.Equal(102, result.TotalCost);
        }


        //STEP 3

        [Fact]
        public void CalculateDeliveryCost_OverweightParcel_ReturnsDeliveryWithOverweightCosts()
        {
            //Arrange
            var parcels = new List<Parcel> { _fixture.SmallOverweightParcel, _fixture.MediumOverweightParcel, _fixture.LargeOverweightParcel, _fixture.XLOverweightParcel };

            var delivery = new Delivery { Parcels = parcels };

            //Act
            var result = _deliveryCalculator.CalculateDeliveryCost(delivery);

            //Assert
            Assert.Collection(delivery.Parcels,
                item =>
                {
                    Assert.Equal(ParcelType.Small, item.ParcelType);
                    Assert.True(item.IsOverweight);
                    Assert.Equal(5, item.ParcelCost);
                },
                item =>
                {
                    Assert.Equal(ParcelType.Medium, item.ParcelType);
                    Assert.True(item.IsOverweight);
                    Assert.Equal(10, item.ParcelCost);
                },
                 item =>
                 {
                     Assert.Equal(ParcelType.Large, item.ParcelType);
                     Assert.True(item.IsOverweight);
                     Assert.Equal(17, item.ParcelCost);
                 },
                item =>
                {
                    Assert.Equal(ParcelType.XL, item.ParcelType);
                    Assert.True(item.IsOverweight);
                    Assert.Equal(27, item.ParcelCost);
                });

            Assert.Equal(59, result.TotalCost);
        }


        //STEP 4

        [Fact]
        public void CalculateDeliveryCost_HeavyParcel_ReturnsDeliveryWithCosts()
        {
            //Arrange
            var parcels = new List<Parcel> { _fixture.HeavyParcel };


            var delivery = new Delivery { Parcels = parcels };

            //Act
            var result = _deliveryCalculator.CalculateDeliveryCost(delivery);

            //Assert
            Assert.Collection(delivery.Parcels,
                item =>
                {
                    Assert.Equal(ParcelType.Heavy, item.ParcelType);
                    Assert.Equal(60, item.ParcelCost);
                });

            Assert.Equal(50, result.TotalCost);
        }

        //Step 5

        [Fact]
        public void CalculateDeliveryCost_SmallParcelDiscounts_ReturnsDeliveryWithDiscounts()
        {
            //Arrange
            var parcels = new List<Parcel>
        {
            _fixture.SmallParcel,
            _fixture.SmallOverweightParcel,
            _fixture.SmallParcel,
            _fixture.SmallParcel,
            _fixture.SmallOverweightParcel
        };


            var delivery = new Delivery { Parcels = parcels };

            //Act
            var result = _deliveryCalculator.CalculateDeliveryCost(delivery);

            //Assert
            Assert.Collection(delivery.Parcels,
                item =>
                {
                    Assert.Equal(ParcelType.Small, item.ParcelType);
                    Assert.Equal(3, item.ParcelCost);
                },
                item =>
                {
                    Assert.Equal(ParcelType.Small, item.ParcelType);
                    Assert.Equal(5, item.ParcelCost);
                },
                 item =>
                 {
                     Assert.Equal(ParcelType.Small, item.ParcelType);
                     Assert.Equal(3, item.ParcelCost);
                 },
                   item =>
                   {
                       Assert.Equal(ParcelType.Small, item.ParcelType);
                       Assert.Equal(3, item.ParcelCost);
                   },
                item =>
                {
                    Assert.Equal(ParcelType.Small, item.ParcelType);
                    Assert.Equal(5, item.ParcelCost);
                });

            //Assert.True(result.DiscountedShipping);
            Assert.Equal(-3, result.TotalShippingDiscount);
            Assert.Equal(16, result.TotalCost);
        }


        [Fact]
        public void CalculateDeliveryCost_MediumParcelDiscounts_ReturnsDeliveryWithDiscounts()
        {
            //Arrange
            var parcels = new List<Parcel> {
              _fixture.MediumParcel,
              _fixture.MediumParcel,
              _fixture.MediumParcel,
              _fixture.MediumOverweightParcel,
              _fixture.MediumOverweightParcel,
              _fixture.MediumOverweightParcel
            };

            var delivery = new Delivery { Parcels = parcels };

            //Act
            var result = _deliveryCalculator.CalculateDeliveryCost(delivery);

            //Assert
            Assert.Collection(delivery.Parcels,
                item =>
                {
                    Assert.Equal(ParcelType.Medium, item.ParcelType);
                    Assert.Equal(8, item.ParcelCost);
                },
                item =>
                {
                    Assert.Equal(ParcelType.Medium, item.ParcelType);
                    Assert.Equal(8, item.ParcelCost);
                },
                 item =>
                 {
                     Assert.Equal(ParcelType.Medium, item.ParcelType);
                     Assert.Equal(8, item.ParcelCost);
                 },
                 item =>
                 {
                     Assert.Equal(ParcelType.Medium, item.ParcelType);
                     Assert.Equal(10, item.ParcelCost);
                 },
                 item =>
                 {
                     Assert.Equal(ParcelType.Medium, item.ParcelType);
                     Assert.Equal(10, item.ParcelCost);
                 },
                 item =>
                 {
                     Assert.Equal(ParcelType.Medium, item.ParcelType);
                     Assert.Equal(10, item.ParcelCost);
                 });

           // Assert.True(result.DiscountedShipping);
            Assert.Equal(-18, result.TotalShippingDiscount);
            Assert.Equal(36, result.TotalCost);
        }


        [Fact]
        public void CalculateDeliveryCost_MixedParcelDiscounts_ReturnsDeliveryWithDiscounts()
        {
            //Arrange

            var parcels = new List<Parcel> {
              _fixture.SmallParcel,
              _fixture.SmallParcel,
              _fixture.SmallParcel,
              _fixture.SmallParcel,
              _fixture.SmallParcel,
              _fixture.MediumParcel,
              _fixture.MediumParcel,
              _fixture.MediumOverweightParcel,
              _fixture.MediumOverweightParcel,
              _fixture.MediumOverweightParcel,
              _fixture.LargeParcel,
              _fixture.LargeOverweightParcel,
              _fixture.XLParcel
            };

            var delivery = new Delivery { Parcels = parcels };

            //Act
            var result = _deliveryCalculator.CalculateDeliveryCost(delivery);

            //Assert
            Assert.Collection(delivery.Parcels,
                item =>
                {
                    Assert.Equal(ParcelType.Small, item.ParcelType);
                    Assert.Equal(3, item.ParcelCost);
                },
                item =>
                {
                    Assert.Equal(ParcelType.Small, item.ParcelType);
                    Assert.Equal(3, item.ParcelCost);
                },
                 item =>
                 {
                     Assert.Equal(ParcelType.Small, item.ParcelType);
                     Assert.Equal(3, item.ParcelCost);
                 },
                 item =>
                {
                    Assert.Equal(ParcelType.Small, item.ParcelType);
                    Assert.Equal(3, item.ParcelCost);
                },
                 item =>
                 {
                     Assert.Equal(ParcelType.Small, item.ParcelType);
                     Assert.Equal(3, item.ParcelCost);
                 },
                item =>
                {
                    Assert.Equal(ParcelType.Medium, item.ParcelType);
                    Assert.Equal(8, item.ParcelCost);
                },
                 item =>
                 {
                     Assert.Equal(ParcelType.Medium, item.ParcelType);
                     Assert.Equal(8, item.ParcelCost);
                 },
                  item =>
                  {
                      Assert.Equal(ParcelType.Medium, item.ParcelType);
                      Assert.Equal(10, item.ParcelCost);
                  },
                 item =>
                 {
                     Assert.Equal(ParcelType.Medium, item.ParcelType);
                     Assert.Equal(10, item.ParcelCost);
                 },
                    item =>
                    {
                        Assert.Equal(ParcelType.Medium, item.ParcelType);
                        Assert.Equal(10, item.ParcelCost);
                    },
                item =>
                {
                    Assert.Equal(ParcelType.Large, item.ParcelType);
                    Assert.Equal(15, item.ParcelCost);
                },
            item =>
            {
                Assert.Equal(ParcelType.Large, item.ParcelType);
                Assert.Equal(17, item.ParcelCost);
            },
             item =>
             {
                 Assert.Equal(ParcelType.XL, item.ParcelType);
                 Assert.Equal(25, item.ParcelCost);
             }
            );

            Assert.Equal(-3, result.TotalShippingDiscount);
            Assert.Equal(37, result.TotalCost);
        }
    }
}
