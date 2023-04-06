namespace CourierKata
{
    public class DeliveryCostCalculator
    {
        public Delivery CalculateDeliveryCost(Delivery delivery)
        {
            var totalCost = 0;

            if (delivery.Parcels != null)
            {
                foreach (var parcel in delivery.Parcels)
                {
                    if (parcel.ParcelHeight < 10 && parcel.ParcelHeight < 10 && parcel.ParcelDepth < 10)
                    {
                        parcel.ParcelType = ParcelType.Small;
                    }

                    if (parcel.ParcelType == ParcelType.Small)
                    {
                        parcel.ParcelCost = 3m;
                    }

                    delivery.TotalCost = totalCost + parcel.ParcelCost;
                }
            }

            return delivery;
        }
    }
}
