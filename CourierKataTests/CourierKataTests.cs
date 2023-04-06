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
    }
}
