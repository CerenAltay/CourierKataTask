using AutoFixture;
using CourierKata;
using System;
using System.IO;

namespace CourierKataTests
{
    public class TestFixture : IDisposable
    {
        private readonly Fixture _fixture;
        private readonly MemoryStream _stream;

        public TestFixture()
        {
            _fixture = new Fixture();
            _stream = new MemoryStream();
        }

        public Parcel SmallParcel => _fixture.Build<Parcel>()
           .With(p => p.ParcelHeight, 9)
           .With(p => p.ParcelWidth, 9)
           .With(p => p.ParcelDepth, 9)
           .With(p => p.ParcelWeight, 1)
           .Create();

        public Parcel MediumParcel => _fixture.Build<Parcel>()
           .With(p => p.ParcelHeight, 40)
           .With(p => p.ParcelWidth, 40)
           .With(p => p.ParcelDepth, 40)
           .With(p => p.ParcelWeight, 3)
           .Create();

        public Parcel LargeParcel => _fixture.Build<Parcel>()
          .With(p => p.ParcelHeight, 90)
          .With(p => p.ParcelWidth, 90)
          .With(p => p.ParcelDepth, 90)
          .With(p => p.ParcelWeight, 6)
          .Create();

        public Parcel XLParcel => _fixture.Build<Parcel>()
          .With(p => p.ParcelHeight, 100)
          .With(p => p.ParcelWidth, 100)
          .With(p => p.ParcelDepth, 100)
          .With(p => p.ParcelWeight, 10)
          .Create();

        public Parcel SmallOverweightParcel => _fixture.Build<Parcel>()
          .With(p => p.ParcelHeight, 9)
          .With(p => p.ParcelWidth, 9)
          .With(p => p.ParcelDepth, 9)
          .With(p => p.ParcelWeight, 2)
          .Create();

        public Parcel MediumOverweightParcel => _fixture.Build<Parcel>()
          .With(p => p.ParcelHeight, 40)
          .With(p => p.ParcelWidth, 40)
          .With(p => p.ParcelDepth, 40)
          .With(p => p.ParcelWeight, 4)
          .Create();
 
        public Parcel LargeOverweightParcel => _fixture.Build<Parcel>()
          .With(p => p.ParcelHeight, 90)
          .With(p => p.ParcelWidth, 90)
          .With(p => p.ParcelDepth, 90)
          .With(p => p.ParcelWeight, 7)
          .Create();

        public Parcel XLOverweightParcel => _fixture.Build<Parcel>()
           .With(p => p.ParcelHeight, 100)
           .With(p => p.ParcelWidth, 100)
           .With(p => p.ParcelDepth, 100)
           .With(p => p.ParcelWeight, 11)
           .Create();

        public Parcel HeavyParcel => _fixture.Build<Parcel>()
           .With(p => p.ParcelHeight, 50)
           .With(p => p.ParcelWidth, 50)
           .With(p => p.ParcelDepth, 50)
           .With(p => p.ParcelWeight, 50)
           .Create();

        public Parcel HeavyOverweightParcel => _fixture.Build<Parcel>()
           .With(p => p.ParcelHeight, 50)
           .With(p => p.ParcelWidth, 50)
           .With(p => p.ParcelDepth, 50)
           .With(p => p.ParcelWeight, 51)
           .Create();

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}