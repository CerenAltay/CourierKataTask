using CourierKata;
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
    }
}
