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
                    else if (parcel.ParcelHeight < 50 && parcel.ParcelWidth < 50 && parcel.ParcelDepth < 50)
                    {
                        parcel.ParcelType = ParcelType.Medium;
                    }
                    else if (parcel.ParcelHeight < 100 && parcel.ParcelWidth < 100 && parcel.ParcelDepth < 100)
                    {
                        parcel.ParcelType = ParcelType.Large;
                    }
                    else if (parcel.ParcelHeight >= 100 || parcel.ParcelWidth >= 100 || parcel.ParcelDepth >= 100)
                    {
                        parcel.ParcelType = ParcelType.XL;
                    }

                    if (parcel.ParcelType == ParcelType.Small)
                    {
                        parcel.ParcelCost = 3m;
                    }
                    if (parcel.ParcelType == ParcelType.Medium)
                    {
                        parcel.ParcelCost = 8m;
                    }
                    if (parcel.ParcelType == ParcelType.Large)
                    {
                        parcel.ParcelCost = 15m;
                    }
                    if (parcel.ParcelType == ParcelType.XL)
                    {
                        parcel.ParcelCost = 25m;
                    }

                    delivery.TotalCost += totalCost + parcel.ParcelCost;
                }
            }

            return delivery;
        }
    }
}
