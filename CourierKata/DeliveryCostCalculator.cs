using CourierKata.Consts;

namespace CourierKata
{
    public class DeliveryCostCalculator
    {
        //private const decimal SmallSizeCost = 3m;
        //private const decimal MediumSizeCost = 8m;
        //private const decimal LargeSizeCost = 15m;
        //private const decimal XLSizeCost = 25m;
        //private const decimal HeavySizeCost = 50m;
        //private const decimal SmallWeightLimit = 1m;
        //private const decimal MediumWeightLimit = 3m;
        //private const decimal LargeWeightLimit = 6m;
        //private const decimal XLWeightLimit = 10m;
        //private const decimal HeavyWeightLimit = 50m;

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
            // parcel= DefineParcel(parcel);

            if (IsParcelOverweight(parcel))
            {
                //TODO: weight limit for XL parcels

                parcel.OverweightCost = (parcel.ParcelWeight - parcel.ParcelWeightLimit) * 2m;

                parcel.ParcelCost += parcel.OverweightCost;
            }

            return parcel.ParcelCost;
        }

        //private static decimal CalculateParcelCosts(DeliveryCart delivery)
        //{
        //    foreach (var parcel in delivery.Parcels)
        //    {
        //        Delivery delivery = new();
        //        var prc = DefineParcel(parcel);

        //        if (IsParcelOverweight(parcel))
        //        {
        //            prc.OverweightCost = (parcel.ParcelWeight - parcel.ParcelWeightLimit) * 2m;
        //            prc.ParcelCost += parcel.OverweightCost;
        //        }

        //        delivery.TotalCost += parcel.ParcelCost;
        //    }
        //    return 0;

        //    //return parcel.ParcelCost;
        //}

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

            if (parcel.ParcelHeight < 10 && parcel.ParcelHeight < 10 && parcel.ParcelDepth < 10)
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

        //private static ParcelType DefineParcelType(Parcel parcel)
        //{
        //    if (parcel.ParcelHeight <= 0 || parcel.ParcelHeight <= 0 || parcel.ParcelDepth <= 0)
        //    {
        //        throw new ArgumentException("Invalid parcel size");
        //    }

        //    if (parcel.ParcelWeight >= 50m)
        //    {
        //        return ParcelType.Heavy;
        //    }

        //    if (parcel.ParcelHeight < 10 && parcel.ParcelHeight < 10 && parcel.ParcelDepth < 10)
        //    {
        //        return ParcelType.Small;
        //    }
        //    else if (parcel.ParcelHeight < 50 && parcel.ParcelWidth < 50 && parcel.ParcelDepth < 50)
        //    {
        //        return ParcelType.Medium;
        //    }
        //    else if (parcel.ParcelHeight < 100 && parcel.ParcelWidth < 100 && parcel.ParcelDepth < 100)
        //    {
        //        return ParcelType.Large;
        //    }
        //    else if (parcel.ParcelHeight >= 100 || parcel.ParcelWidth >= 100 || parcel.ParcelDepth >= 100)
        //    {
        //        return ParcelType.XL;
        //    }

        //    throw new ArgumentException("Invalid parcel size");
        //}

        //private static decimal GetParcelCost(Parcel parcel)
        //{
        //    switch (parcel.ParcelType)
        //    {
        //        case ParcelType.Small:
        //            return SmallSizeCost;

        //        case ParcelType.Medium:
        //            return MediumSizeCost;

        //        case ParcelType.Large:
        //            return LargeSizeCost;

        //        case ParcelType.XL:
        //            return XLSizeCost;

        //        case ParcelType.Heavy:
        //            return HeavySizeCost;

        //        default:
        //            throw new ArgumentException("Invalid parcel type");
        //    }
        //}
        private static decimal CalculateSpeedyShippingParcelCost(Delivery delivery)
        {
            delivery.SpeedyShippingCost = delivery.TotalCost;

            delivery.TotalCost += delivery.SpeedyShippingCost;

            return delivery.TotalCost;
        }

        private static bool IsParcelOverweight(Parcel parcel)
        {
            //parcel.ParcelWeightLimit = GetWeightLimit(parcel.ParcelType);

            parcel.IsOverweight = parcel.ParcelWeight > parcel.ParcelWeightLimit;

            return parcel.IsOverweight;
        }

        //private static decimal GetWeightLimit(ParcelType ParcelType)
        //{
        //    switch (ParcelType)
        //    {
        //        case ParcelType.Small:
        //            return SmallWeightLimit;

        //        case ParcelType.Medium:
        //            return MediumWeightLimit;

        //        case ParcelType.Large:
        //            return LargeWeightLimit;

        //        case ParcelType.XL:
        //            return XLWeightLimit;

        //        case ParcelType.Heavy:
        //            return HeavyWeightLimit;

        //        default:
        //            throw new Exception("Invalid parcel type");
        //    }
        //}

        //private static decimal CalculateShippingDiscounts(Delivery delivery)
        //{
        //    var orderedParcels = delivery.Parcels.OrderBy(p => (int)p.ParcelType).OrderByDescending(c => c.ParcelCost);

        //    var smallParcels = orderedParcels.Where(s => s.ParcelType == ParcelType.Small).Count();
        //    var mediumParcels = orderedParcels.Where(s => s.ParcelType == ParcelType.Medium).Count();
        //    var largeParcels = orderedParcels.Where(s => s.ParcelType == ParcelType.Large).Count();


        //    var discountedSmallParcelnumber = 0;
        //    var discountedMediumParcelnumber = 0;
        //    var discountedMixedParcelnumber = 0;
        //    var discountSmallParcel = 0m;
        //    var discountMediumParcel = 0m;
        //    var discountMixedParcel = 0m;

        //    if (smallParcels > 3)
        //    {
        //        discountedSmallParcelnumber = delivery.Parcels.Count / 4;

        //        discountSmallParcel = -delivery.Parcels.OrderBy(x => x.ParcelCost).Take(discountedSmallParcelnumber).Sum(y => y.ParcelCost);

        //        //delivery.ShippingDiscounts += discountSmallParcel;
        //    }

        //    if (mediumParcels > 2)
        //    {
        //        discountedMediumParcelnumber = delivery.Parcels.Count / 3;

        //        discountMediumParcel = -delivery.Parcels.OrderBy(x => x.ParcelCost).Take(discountedMediumParcelnumber).Sum(y => y.ParcelCost);

        //        // delivery.ShippingDiscounts += discountMediumParcel;
        //    }

        //    if (delivery.Parcels.Count > 4)
        //    {
        //        discountedMixedParcelnumber = (delivery.Parcels.Count - (discountedSmallParcelnumber * 4 + discountedMediumParcelnumber * 3)) / 5;

        //        if (discountedMixedParcelnumber > 0)
        //        {
        //            discountMixedParcel = -delivery.Parcels.OrderBy(x => x.ParcelCost).Take(discountedMixedParcelnumber).Sum(y => y.ParcelCost);
        //        }

        //        // delivery.ShippingDiscounts += discountMixedParcel;
        //    }

        //    return delivery.TotalCost += delivery.TotalShippingDiscount;
        //}

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


        //private static (List<Parcel> remainingParcels, decimal discountTotal) CalculateDiscount(List<Parcel> parcels, int discountedParcelNo)
        //{
        //    decimal discountTotal = 0;
        //    //parcels.OrderByDescending(p => p.ParcelCost);
        //    var discountedQuantity = parcels.Count / discountedParcelNo;

        //    for (int i = 0; i < discountedQuantity; i++)
        //    {
        //        decimal discount = parcels.ElementAt(discountedParcelNo).ParcelCost;
        //        discountTotal += discount;
        //        parcels = parcels.Skip(discountedParcelNo).ToList();
        //    }
        //    return (parcels, discountTotal);
        //}
        //use as
        //(List<int> remainingList, int discountTotal) myTuple = GetDiscountedParcels(parcelCosts);
        //List<int> myList = myTuple.Item1;
        //int myTotal = myTuple.Item2;

        private static decimal GetDiscounts(Delivery delivery)
        {
            var smallParcels = delivery.Parcels
               .Where(p => p.ParcelType == ParcelType.Small)
               .OrderByDescending(p => p.ParcelCost).ToList();

            decimal discountTotal = 0;
            var discountedQuantitySmall = smallParcels.Count / 4;


            for (int i = 0; i < discountedQuantitySmall; i++)
            {
                decimal smallDiscount = smallParcels.ElementAt(4).ParcelCost;
                discountTotal += smallDiscount;
            }
            List<Parcel> discountedSmallParcels = smallParcels.Take(discountedQuantitySmall * 4).ToList();

            var smallParcelDiscounts = discountTotal;

            var mediumParcels = delivery.Parcels
              .Where(p => p.ParcelType == ParcelType.Medium)
              .OrderByDescending(p => p.ParcelCost).ToList();

            var discountedQuantityMedium = mediumParcels.Count / 3;

            for (int i = 0; i < discountedQuantityMedium; i++)
            {
                decimal mediumDiscount = mediumParcels.Skip(i*3).ElementAt(3).ParcelCost;
                discountTotal += mediumDiscount;
            }
            List<Parcel> discountedMediumParcels = mediumParcels.Take(discountedQuantityMedium * 3).ToList();
            //var discounts = discountTotal;

            List<Parcel> remainingParcels = delivery.Parcels
                .Except(discountedSmallParcels)
                .Except(discountedMediumParcels)
                .OrderByDescending(p => p.ParcelCost).ToList();

            var discountedQuantityMixed = remainingParcels.Count / 5;


            for (int i = 0; i < discountedQuantityMixed; i++)
            {
                decimal mixedParcelDiscount = remainingParcels.ElementAt(5).ParcelCost;
                discountTotal += mixedParcelDiscount;
            }

            return - discountTotal;

            //var smallParcels = delivery.Parcels
            //   .Where(p => p.ParcelType == ParcelType.Small)
            //   .OrderByDescending(p => p.ParcelCost);

            //var smallQuantity = smallParcels.Count() / 4;

            //var smalldiscount = 0;
            //do
            //{
            //    smallParcels.Skip(4).ToList();
            //    //smalldiscount += smallParcels.ElementAt(4);
            //} while (smallParcels.Count() / 4 == 0);


            //var x = smallParcels.Skip(smallQuantity).ToList();//.Repeat(2);


            //// smallParcels.Repeat(1);

            //var smallDiscountedParcels = smallParcels
            //   .Where((p, i) => i % 4 == 0);

            //var smallParcelDiscount = smallParcels
            //    .Where((p, i) => i % 4 == 3)
            //    .Sum(p => p.ParcelCost);

            //var remainingSmallParcels = smallParcels.Except(smallDiscountedParcels).ToList();

            //var mediumParcels = delivery.Parcels
            //  .Where(p => p.ParcelType == ParcelType.Medium)
            //  .OrderByDescending(p => p.ParcelCost);

            //var mediumDiscountedParcels = mediumParcels
            //   .Where((p, i) => i % 3 == 0);

            //var mediumParcelDiscount = smallParcels
            //    .Where((p, i) => i % 3 == 2)
            //    .Sum(p => p.ParcelCost);

            //var remainingMediumParcels = mediumParcels.Except(mediumDiscountedParcels).ToList();

            ////var mediumParcelDiscount = delivery.Parcels
            ////    .Where(p => p.ParcelType == ParcelType.Medium)
            ////    .OrderByDescending(p => p.ParcelCost)
            ////    .Where((p, i) => i % 3 == 2)
            ////    .Sum(p => p.ParcelCost);

            //var smallParcelsList = delivery.Parcels
            //    .GroupBy(keySelector: x => x.ParcelType, elementSelector: y => new { y.ParcelType, y.ParcelCost }).SelectMany(parcels => parcels.Where(z => z.ParcelType == ParcelType.Small)).OrderByDescending(y => y.ParcelCost).ToList();


            //var smallprcDiscount = smallParcelsList.TakeEvery(5).ToList();

            //while (smallParcelsList.Any()) ;

            //var mediumParcelsList = delivery.Parcels
            //     .GroupBy(keySelector: x => x.ParcelType, elementSelector: y => new { y.ParcelType, y.ParcelCost }).SelectMany(parcels => parcels.Where(z => z.ParcelType == ParcelType.Medium)).OrderByDescending(y => y.ParcelCost).ToList();

            ////var mediumParcels = delivery.Parcels
            ////   .OrderByDescending(c => c.ParcelCost)
            ////   .GroupBy(p => p.ParcelType == ParcelType.Medium);

            //var mixedParcels = delivery.Parcels
            //   .OrderByDescending(c => c.ParcelCost);



            //if (delivery.Parcels.Where(p => p.ParcelType == ParcelType.Small).Count() > 3)
            //{
            //    delivery.Parcels.OrderByDescending(p => p.ParcelCost).Select(p => p.ParcelType == ParcelType.Small);
            //}
        }

    }
}
