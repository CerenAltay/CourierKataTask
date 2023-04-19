namespace CourierKata
{
    public class Delivery
    {
        public List<Parcel> Parcels { get; set; }
        public bool SpeedyShipping { get; set; }
        public decimal TotalCost { get; set; }
        public decimal SpeedyShippingCost { get; set; }
        public decimal TotalShippingDiscount { get; set; }
    }
}
