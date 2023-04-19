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
                    delivery.TotalShippingDiscount = CalculateDiscounts(delivery);
                    delivery.TotalCost += delivery.TotalShippingDiscount;
                }

                if (delivery.SpeedyShipping)
                {
                    delivery.SpeedyShippingCost = CalculateSpeedyShippingCost(delivery);
                    delivery.TotalCost += delivery.SpeedyShippingCost;

                }
                return delivery;
            }
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
                parcel.ParcelCost = Constants.HeavyParcelCost;
                parcel.ParcelWeightLimit = Constants.HeavyParcelWeightLimit;
            }
            else if (parcel.ParcelHeight < 10 && parcel.ParcelHeight < 10 && parcel.ParcelDepth < 10)
            {
                parcel.ParcelType = ParcelType.Small;
                parcel.ParcelCost = Constants.SmallSizeCost;
                parcel.ParcelWeightLimit = Constants.SmallSizeWeightLimit;

            }
            else if (parcel.ParcelHeight < 50 && parcel.ParcelWidth < 50 && parcel.ParcelDepth < 50)
            {
                parcel.ParcelType = ParcelType.Medium;
                parcel.ParcelCost = Constants.MediumSizeCost;
                parcel.ParcelWeightLimit = Constants.MediumSizeWeightLimit;
            }
            else if (parcel.ParcelHeight < 100 && parcel.ParcelWidth < 100 && parcel.ParcelDepth < 100)
            {
                parcel.ParcelType = ParcelType.Large;
                parcel.ParcelCost = Constants.LargeSizeCost;
                parcel.ParcelWeightLimit = Constants.LargeSizeWeightLimit;
            }
            else if (parcel.ParcelHeight >= 100 || parcel.ParcelWidth >= 100 || parcel.ParcelDepth >= 100)
            {
                parcel.ParcelType = ParcelType.XL;
                parcel.ParcelCost = Constants.XLSizeCost;
                parcel.ParcelWeightLimit = Constants.XLWeightParcelLimit;
            }
            else
            {
                throw new ArgumentException("Invalid parcel size");
            }

            return parcel;
        }

        private static decimal CalculateParcelCost(Parcel parcel)
        {
            if (IsParcelOverweight(parcel))
            {
                decimal excessWeight = (parcel.ParcelWeight - parcel.ParcelWeightLimit);

                //if (parcel.ParcelType == ParcelType.XL)
                //{
                //    parcel.ParcelCost = Math.Min(excessWeight * 2m, 50m) + Math.Max(excessWeight - 50m / 2m, 0m) * 1m;
                //}

                parcel.ParcelCost += excessWeight * 2m;
            }

            return parcel.ParcelCost;
        }

        private static bool IsParcelOverweight(Parcel parcel)
        {

            parcel.IsOverweight = parcel.ParcelWeight > parcel.ParcelWeightLimit;

            return parcel.IsOverweight;
        }

        private static decimal CalculateSpeedyShippingCost(Delivery delivery)
        {
            delivery.SpeedyShippingCost = delivery.TotalCost;

            return delivery.SpeedyShippingCost;
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

        private static decimal CalculateDiscounts(Delivery delivery)
        {
            var smallParcelDiscount = 0m;
            var mediumParcelDiscount = 0m;
            var mixedParcelDiscount = 0m;
            List<Parcel> discountApplied = new();
            List<Parcel> discountAppliedSmallParcels = new();
            List<Parcel> discountAppliedMediumParcels = new();

            if (HasSmallParcelDiscount(delivery))
            {
                smallParcelDiscount = GetDiscount(delivery.Parcels, ParcelType.Small, Constants.DiscountedSmallParcelNo, out discountApplied);

                discountAppliedSmallParcels = discountApplied;
            }
            if (HasParcelMediumDiscount(delivery))
            {
                mediumParcelDiscount = GetDiscount(delivery.Parcels, ParcelType.Medium, Constants.DiscountedMediumParcelNo, out discountApplied);

                discountAppliedMediumParcels = discountApplied;
            }

            List<Parcel> remainingParcels = delivery.Parcels
                .Except(discountAppliedSmallParcels)
                .Except(discountAppliedMediumParcels)
                .OrderByDescending(p => p.ParcelCost).ToList();

            if (HasMixedParcelDiscount(delivery))
            {
                mixedParcelDiscount = GetDiscount(remainingParcels, null, Constants.DiscountedMixedParcelNo, out discountApplied);
            }

            decimal discount = smallParcelDiscount + mediumParcelDiscount + mixedParcelDiscount;

            return discount;

        }

        private static decimal GetDiscount(List<Parcel> parcels, ParcelType? parcelType, int discountedParcelNo, out List<Parcel> discountedParcels)
        {
            decimal discountTotal = 0;

            if (parcelType != null && parcels.Any())
            {
                parcels = parcels.Where(p => p.ParcelType == parcelType).ToList();
            }

            parcels = parcels.OrderByDescending(p => p.ParcelCost).ToList();

            var discountedQuantity = parcels.Count / discountedParcelNo;

            for (int i = 0; i < discountedQuantity; i++)
            {
                var remainingParcels = parcels.Skip(i * discountedParcelNo).ToList();

                decimal discount = -remainingParcels.ElementAt(discountedParcelNo - 1).ParcelCost;

                discountTotal += discount;
            }

            discountedParcels = parcels.Take(discountedQuantity * discountedParcelNo).ToList();

            return discountTotal;
        }
    }
}
