namespace CourierKata
{
    public class DeliveryCostCalculator
    {
        private const decimal SmallSizeCost = 3m;
        private const decimal MediumSizeCost = 8m;
        private const decimal LargeSizeCost = 15m;
        private const decimal XLSizeCost = 25m;

        public Delivery CalculateDeliveryCost(Delivery delivery)
        {
            var totalCost = 0;

            if (delivery.Parcels != null)
            {
                foreach (var parcel in delivery.Parcels)
                {
                    parcel.ParcelType = DefineParcelType(parcel);

                    parcel.ParcelCost = GetParcelCost(parcel);

                    delivery.TotalCost += totalCost + parcel.ParcelCost;
                }
            }

            if (delivery.SpeedyShipping)
            {
                delivery.SpeedyShippingCost = delivery.TotalCost;
                delivery.TotalCost += delivery.SpeedyShippingCost;
            }

            return delivery;
        }

        private static ParcelType DefineParcelType(Parcel parcel)
        {
            if (parcel.ParcelHeight <= 0 || parcel.ParcelHeight <= 0 || parcel.ParcelDepth <= 0)
            {
                throw new ArgumentException("Invalid parcel size");
            }

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

            return parcel.ParcelType;
        }

        private static decimal GetParcelCost(Parcel parcel)
        {
            switch (parcel.ParcelType)
            {
                case ParcelType.Small:
                    parcel.ParcelCost = SmallSizeCost;
                    break;

                case ParcelType.Medium:
                    parcel.ParcelCost = MediumSizeCost;
                    break;

                case ParcelType.Large:
                    parcel.ParcelCost = LargeSizeCost;
                    break;

                case ParcelType.XL:
                    parcel.ParcelCost = XLSizeCost;
                    break;
            }

            return parcel.ParcelCost;
        }
    }
}
