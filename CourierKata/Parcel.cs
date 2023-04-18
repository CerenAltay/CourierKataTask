namespace CourierKata
{
    public class Parcel
    {
        public double ParcelHeight { get; set; }
        public double ParcelWidth { get; set; }
        public double ParcelDepth { get; set; }
        //todo name
        //public string ParcelsType { get; set; }
        public decimal ParcelCost { get; set; }
        public decimal ParcelWeightLimit { get; set; }
        public bool IsOverweight { get; set; }
        public decimal ParcelWeight { get; set; }
        public ParcelType ParcelType { get; set; }
        public decimal OverweightCost { get; set; }
    }
}
