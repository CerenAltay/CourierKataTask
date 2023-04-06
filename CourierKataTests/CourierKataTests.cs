using CourierKata;
using System;
using System.Collections.Generic;
using Xunit;

namespace CourierKataTests
{
    public class CourierKataTests
    {
        [Fact]
        public void CalculateDeliveryCost_Parcel_ReturnsDeliveryWithCosts()
        {
            //Arrange
            var parcels = new List<Parcel> {
                new Parcel { ParcelHeight = 1, ParcelWidth = 1, ParcelDepth = 1 },
            };
            var delivery = new Delivery { Parcels = parcels };
            var deliveryCalculator = new DeliveryCostCalculator();

            //Act
            var result = deliveryCalculator.CalculateDeliveryCost(delivery);

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
            var parcels = new List<Parcel> {
                new Parcel { ParcelHeight = 1, ParcelWidth = 1, ParcelDepth = 1 },
                new Parcel { ParcelHeight = 15, ParcelWidth = 15, ParcelDepth = 15 },
                new Parcel { ParcelHeight = 15, ParcelWidth = 45, ParcelDepth = 95 },
                new Parcel { ParcelHeight = 9, ParcelWidth = 15, ParcelDepth = 101 },
            };

            var delivery = new Delivery { Parcels = parcels };
            var deliveryCalculator = new DeliveryCostCalculator();

            //Act
            var result = deliveryCalculator.CalculateDeliveryCost(delivery);

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
                }
                );
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
            var deliveryCalculator = new DeliveryCostCalculator();

            //Assert
            var exception = Assert.Throws<ArgumentException>(() => deliveryCalculator.CalculateDeliveryCost(delivery));
            Assert.Equal("Invalid parcel size", exception.Message);
        }


        //STEP 2

        [Fact]
        public void CalculateDeliveryCost_SpeedyShipping_ReturnsDeliveryWithSpeedyShippingCost()
        {
            //Arrange
            var parcels = new List<Parcel> {
                new Parcel { ParcelHeight = 1, ParcelWidth = 1, ParcelDepth = 1 },
                new Parcel { ParcelHeight = 15, ParcelWidth = 15, ParcelDepth = 15 },
                new Parcel { ParcelHeight = 15, ParcelWidth = 45, ParcelDepth = 95 },
                new Parcel { ParcelHeight = 9, ParcelWidth = 15, ParcelDepth = 101 },
            };

            var delivery = new Delivery { Parcels = parcels, SpeedyShipping = true };
            var deliveryCalculator = new DeliveryCostCalculator();

            //Act
            var result = deliveryCalculator.CalculateDeliveryCost(delivery);

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

            Assert.True(result.SpeedyShipping);
            Assert.Equal(51, result.SpeedyShippingCost);
            Assert.Equal(102, result.TotalCost);
        }


        //STEP 3

        [Fact]
        public void CalculateDeliveryCost_OverweightParcel_ReturnsDeliveryWithOverweightCosts()
        {
            //Arrange
            var parcels = new List<Parcel> {
                new Parcel { ParcelHeight = 1, ParcelWidth = 1, ParcelDepth = 1, ParcelWeight = 2 },
                new Parcel { ParcelHeight = 15, ParcelWidth = 15, ParcelDepth = 15, ParcelWeight = 4 },
                new Parcel { ParcelHeight = 15, ParcelWidth = 45, ParcelDepth = 95, ParcelWeight = 7},
                new Parcel { ParcelHeight = 9, ParcelWidth = 15, ParcelDepth = 101, ParcelWeight = 11 }
            };

            var delivery = new Delivery { Parcels = parcels };
            var deliveryCalculator = new DeliveryCostCalculator();

            //Act
            var result = deliveryCalculator.CalculateDeliveryCost(delivery);

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
            var parcels = new List<Parcel> {
                new Parcel { ParcelHeight = 1, ParcelWidth = 1, ParcelDepth = 1, ParcelWeight = 2 },
                new Parcel { ParcelHeight = 1, ParcelWidth = 1, ParcelDepth = 1, ParcelWeight = 1 },
                new Parcel { ParcelHeight = 25, ParcelWidth = 10, ParcelDepth = 18, ParcelWeight = 55 }
            };

            var delivery = new Delivery { Parcels = parcels };
            var deliveryCalculator = new DeliveryCostCalculator();

            //Act
            var result = deliveryCalculator.CalculateDeliveryCost(delivery);

            //Assert
            Assert.Collection(delivery.Parcels,
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
                    Assert.Equal(ParcelType.Heavy, item.ParcelType);
                    Assert.Equal(60, item.ParcelCost);
                });

            Assert.Equal(68, result.TotalCost);
        }
    }
}
