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
                CalculateSpeedyShippingParcelCost(delivery);
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
                return ParcelType.Small;
            }
            else if (parcel.ParcelHeight < 50 && parcel.ParcelWidth < 50 && parcel.ParcelDepth < 50)
            {
                return ParcelType.Medium;
            }
            else if (parcel.ParcelHeight < 100 && parcel.ParcelWidth < 100 && parcel.ParcelDepth < 100)
            {
                return ParcelType.Large;
            }
            else if (parcel.ParcelHeight >= 100 || parcel.ParcelWidth >= 100 || parcel.ParcelDepth >= 100)
            {
                return ParcelType.XL;
            }

            throw new ArgumentException("Invalid parcel size");
        }

        private static decimal GetParcelCost(Parcel parcel)
        {
            switch (parcel.ParcelType)
            {
                case ParcelType.Small:
                    return SmallSizeCost;

                case ParcelType.Medium:
                    return MediumSizeCost;

                case ParcelType.Large:
                    return LargeSizeCost;

                case ParcelType.XL:
                    return XLSizeCost;

                default:
                    throw new ArgumentException("Invalid parcel type");
            }
        }

        private static decimal CalculateSpeedyShippingParcelCost(Delivery delivery)
        {
            delivery.SpeedyShippingCost = delivery.TotalCost;

            delivery.TotalCost += delivery.SpeedyShippingCost;

            return delivery.TotalCost;
        }
    }
}
