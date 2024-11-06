using NUnit.Framework;

namespace ArraySumService.UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(34, new [] { 1, 2, 10, 90, 58, 34}, true)]
        [TestCase(13, new [] { 6, 4, 2, 8, 7, 9}, true)]
        [TestCase(49, new int[] { 20, 303, 3, 4, 25 }, true)]
        [TestCase(73, new int[] {8, 33, 23, 40, 19}, true)]
        public void ArrayContainsSumOfK_ReturnsExpected(int k, int[] array, bool expected)
        {
            var arraySumService = new ArraySumService();

            var actual = arraySumService.ArrayContainsSumOfK(k, array);

            Assert.AreEqual(expected, actual);
        }
    }
}