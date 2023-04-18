using CourierKata.Consts;

namespace CourierKata
{
    public class DeliveryCostCalculator
    {

        public Delivery CalculateDeliveryCost(Delivery delivery)
        {
            if (!delivery.Parcels.Any())
            {

                return null;
            }
            else
            {
                foreach (var parcel in delivery.Parcels)
                {
                    DefineParcel(parcel);

                    parcel.ParcelCost = CalculateParcelCost(parcel);

                    delivery.TotalCost += parcel.ParcelCost;
                }

                var discountedShipping = IsDiscountedShipping(delivery);

                if (discountedShipping)
                {
                    delivery.TotalShippingDiscount = GetDiscounts(delivery);
                    delivery.TotalCost += delivery.TotalShippingDiscount;
                }

                if (delivery.SpeedyShipping)
                {
                    delivery.SpeedyShippingCost = CalculateSpeedyShippingParcelCost(delivery);
                    delivery.TotalCost += delivery.SpeedyShippingCost;

                }
                return delivery;
            }
        }

        private static decimal CalculateParcelCost(Parcel parcel)
        {

            if (IsParcelOverweight(parcel))
            {
                //TODO: weight limit for XL parcels

                parcel.ParcelCost += (parcel.ParcelWeight - parcel.ParcelWeightLimit) * 2m;
            }

            return parcel.ParcelCost;
        }


        private static Parcel DefineParcel(Parcel parcel)
        {
            if (parcel.ParcelHeight <= 0 || parcel.ParcelHeight <= 0 || parcel.ParcelDepth <= 0)
            {
                throw new ArgumentException("Invalid parcel size");
            }

            if (parcel.ParcelWeight >= 50m)
            {
                parcel.ParcelType = ParcelType.Heavy;
                parcel.ParcelCost = ParcelConstants.HeavyParcelCost;
                parcel.ParcelWeightLimit = ParcelConstants.HeavyParcelWeightLimit;
            }
            else if (parcel.ParcelHeight < 10 && parcel.ParcelHeight < 10 && parcel.ParcelDepth < 10)
            {
                parcel.ParcelType = ParcelType.Small;
                parcel.ParcelCost = ParcelConstants.SmallSizeCost;
                parcel.ParcelWeightLimit = ParcelConstants.SmallSizeWeightLimit;

            }
            else if (parcel.ParcelHeight < 50 && parcel.ParcelWidth < 50 && parcel.ParcelDepth < 50)
            {
                parcel.ParcelType = ParcelType.Medium;
                parcel.ParcelCost = ParcelConstants.MediumSizeCost;
                parcel.ParcelWeightLimit = ParcelConstants.MediumSizeWeightLimit;
            }
            else if (parcel.ParcelHeight < 100 && parcel.ParcelWidth < 100 && parcel.ParcelDepth < 100)
            {
                parcel.ParcelType = ParcelType.Large;
                parcel.ParcelCost = ParcelConstants.LargeSizeCost;
                parcel.ParcelWeightLimit = ParcelConstants.LargeSizeWeightLimit;
            }
            else if (parcel.ParcelHeight >= 100 || parcel.ParcelWidth >= 100 || parcel.ParcelDepth >= 100)
            {
                parcel.ParcelType = ParcelType.XL;
                parcel.ParcelCost = ParcelConstants.XLSizeCost;
                parcel.ParcelWeightLimit = ParcelConstants.XLWeightParcelLimit;
            }
            else
            {
                throw new ArgumentException("Invalid parcel size");
            }

            return parcel;
        }

        private static decimal CalculateSpeedyShippingParcelCost(Delivery delivery)
        {
            delivery.SpeedyShippingCost = delivery.TotalCost;

            return delivery.SpeedyShippingCost;
        }

        private static bool IsParcelOverweight(Parcel parcel)
        {
            //parcel.ParcelWeightLimit = GetWeightLimit(parcel.ParcelType);

            parcel.IsOverweight = parcel.ParcelWeight > parcel.ParcelWeightLimit;

            return parcel.IsOverweight;
        }

        private static bool IsDiscountedShipping(Delivery delivery)
        {
            if (HasSmallParcelDiscount(delivery) || HasParcelMediumDiscount(delivery) || HasMixedParcelDiscount(delivery))
            {
                return true;
            }

            return false;
        }

        private static bool HasSmallParcelDiscount(Delivery delivery)
        {
            var isDiscounted = delivery.Parcels.Where(p => p.ParcelType == ParcelType.Small).Count() > 4;

            return isDiscounted;
        }

        private static bool HasParcelMediumDiscount(Delivery delivery)
        {
            var isDiscounted = delivery.Parcels.Where(p => p.ParcelType == ParcelType.Medium).Count() > 3;

            return isDiscounted;
        }

        private static bool HasMixedParcelDiscount(Delivery delivery)
        {
            var isDiscounted = delivery.Parcels.Count > 5;

            return isDiscounted;
        }


        private static decimal GetDiscounts(Delivery delivery)
        {
            var smallParcels = delivery.Parcels
               .Where(p => p.ParcelType == ParcelType.Small)
               .OrderByDescending(p => p.ParcelCost).ToList();

            decimal discountTotal = 0;

            var discountedQuantitySmall = smallParcels.Count / 4;


            for (int i = 0; i < discountedQuantitySmall; i++)
            {

                var discountedSmallParcels = smallParcels.Skip(i * 4).ToList();
                decimal smallDiscount = smallParcels.ElementAt(4-1).ParcelCost;
                discountTotal += smallDiscount;
            }
            List<Parcel> discountAppliedSmallParcels = smallParcels.Take(discountedQuantitySmall * 4).ToList();

            var mediumParcels = delivery.Parcels
              .Where(p => p.ParcelType == ParcelType.Medium)
              .OrderByDescending(p => p.ParcelCost).ToList();

            var discountedQuantityMedium = mediumParcels.Count / 3;


            for (int i = 0; i < discountedQuantityMedium; i++)
            {
                var discountedMediumParcels = mediumParcels.Skip(i*3).ToList();
                decimal mediumDiscount = discountedMediumParcels.ElementAt(3-1).ParcelCost;
                discountTotal += mediumDiscount;
            }

    
            List<Parcel> discountAppliedMediumParcels = mediumParcels.Take(discountedQuantityMedium * 3).ToList();

            List<Parcel> remainingParcels = delivery.Parcels
                .Except(discountAppliedSmallParcels)
                .Except(discountAppliedMediumParcels)
                .OrderByDescending(p => p.ParcelCost).ToList();

            var discountedQuantityMixed = remainingParcels.Count / 5;


            for (int i = 0; i < discountedQuantityMixed; i++)
            {
                decimal mixedParcelDiscount = remainingParcels.ElementAt(5).ParcelCost;
                discountTotal += mixedParcelDiscount;
            }

            return -discountTotal;

        }

    }
}
