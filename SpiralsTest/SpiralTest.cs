using System.Drawing;
using NUnit.Framework;
using Spirals;

namespace SpiralsTest
{
    // The problem as given is slightly underspecified.  The provided example involves an "odd-width"
    // spiral, where the origin ("0") is naturally in the middle of the spiral.  Without an example 
    // involving an "even-width" spiral, it's impossible to tell whether the desired output for an even-width
    // spiral is contained within the minimal bounding square, or within the slightly larger
    // square in which the origin is in the middle.  E.g. whether, for the input "3", the result should be
    // ---
    // 0 1
    // 3 2
    // ---
    // or
    // -----
    // 
    //   0 1
    //   3 2
    // -----
    //
    // I believe that the result will look more like a spiral with the latter assumption, so until
    // I get an answer back from Sheri, I'm going to assume the latter.
    [TestFixture]
    public class SpiralTest
    {
        [Test]
        public void SpiralToZeroHasOneElement()
        {
            Assert.That(new Spiral(0).Numbers, Is.EqualTo(new[,] { { 0 } }));
        }

        [Test]
        public void SpiralToZeroHasOriginInCenter()
        {
            Assert.That(new Spiral(0).Origin, Is.EqualTo(new Point(0, 0)));
        }

        [Test]
        public void SpiralToZeroHasCorrectMaxElement()
        {
            Assert.That(new Spiral(0).SpiralTo, Is.EqualTo(0));            
        }

        [Test]
        public void SpiralToOneHasNineElements()
        {
            Assert.That(new Spiral(1).Numbers, Is.EqualTo(new[,]
            {
                { 0, 0, 0 }, 
                { 0, 0, 0 }, 
                { 0, 0, 0 }
            }));
        }

        [Test]
        public void SpiralToOneHasOriginInCenter()
        {
            Assert.That(new Spiral(1).Origin, Is.EqualTo(new Point(1, 1)));
        }

        [Test]
        public void SpiralToOneHasCorrectMaxElement()
        {
            Assert.That(new Spiral(1).SpiralTo, Is.EqualTo(1));
        }

        [Test]
        public void SpiralAlwaysHasTheSmallestOddSizeSufficientToContainIt()
        {
            Assert.That(new Spiral(3).Size, Is.EqualTo(3));
            Assert.That(new Spiral(8).Size, Is.EqualTo(3));
            Assert.That(new Spiral(9).Size, Is.EqualTo(5));
        }
    }
}
