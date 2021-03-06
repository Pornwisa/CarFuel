﻿using CarFuel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CarFuel.Utils;

namespace CarFuel.Facts
{
    public class FillUpFacts
    {

        public class GeneralUsage:IDisposable
        {

            [Fact]
            public void Basic()
            {
                // Arrange
                var f1 = new FillUp();
                f1.Odometer = 1000;
                f1.IsFull = true;
                f1.Liters = 50.0;

                // Act

                // Assert
                Assert.Equal(1000, f1.Odometer);
                Assert.Equal(true, f1.IsFull);
                Assert.Equal(50.0, f1.Liters);
            }

            [Fact]
            public void DefaultDateShouldBeNow()
            {
                var now = DateTime.Now;
                SystemTime.SetDateTime(now);

                var f = new FillUp();
                f.Odometer = 1000;
                f.IsFull = true;
                f.Liters = 50.0;

                DateTime dt = f.Date;

                Assert.Equal(expected: now, actual: dt);
            }

            public void Dispose()
            {
                SystemTime.ResetDateTime();
            }
        }

        public class KilometerPerLiterProperty
        {
            private FillUp f1, f2, f3;

            public KilometerPerLiterProperty() {
                f1 = new FillUp();
                f2 = new FillUp();
                f3 = new FillUp();
            }
            [Fact]
            public void FirstFillUp()
            {
               // var f1 = new FillUp();
                f1.Odometer = 1000;
                f1.IsFull = true;
                f1.Liters = 50.0;

                double? kml = f1.KilometerPerLiter;

                Assert.Null(kml);
            }

            [Fact]
            public void SecondFillUp()
            {
                //var f1 = new FillUp();
                f1.Odometer = 1000;
                f1.IsFull = true;
                f1.Liters = 50.0;

                //var f2 = new FillUp();
                f2.Odometer = 1500;
                f2.IsFull = true;
                f2.Liters = 40.0;

                f1.NextFillUp = f2;

                double? kml = f1.KilometerPerLiter;

                Assert.Equal(12.5, kml);
            }

            [Fact]
            public void ThirdFillUp()
            {
               // var f1 = new FillUp();
                f1.Odometer = 1000;
                f1.IsFull = true;
                f1.Liters = 50.0;

                //var f2 = new FillUp();
                f2.Odometer = 1500;
                f2.IsFull = true;
                f2.Liters = 40.0;

               // var f3 = new FillUp();
                f3.Odometer = 2100;
                f3.IsFull = true;
                f3.Liters = 50.0;

                f1.NextFillUp = f2;
                f2.NextFillUp = f3;

                double? kml = f2.KilometerPerLiter;

                Assert.Equal(12.0, kml);
            }
        }

        public class TheNextFillUpProperty
        {
            [Fact]
            public void การเติมน้ำมันครั้งถัดไปต้องมีodometerไม่น้อยกว่าครั้งที่แล้ว()
            {
                // NextFillUpOdometer_ShouldNotLessThanPreviousFillUp() {
                var f1 = new FillUp();
                f1.Odometer = 1000;

                var f2 = new FillUp();
                f2.Odometer = 500;

                Assert.ThrowsAny<Exception>(() =>
                {
                    f1.NextFillUp = f2;
                });

            }
        }
    }
}
