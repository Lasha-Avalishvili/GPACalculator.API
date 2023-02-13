using System.Diagnostics;
using GPACalculator.API.Db;
using NUnit.Framework;

namespace GPACalculator.API.Tests
{
    public class GPACalculatorTest
    {

        [TestFixture]
        public class ExampleTest
        {
            [Test]
            public void TestAddition()
            {
                int a = 1;
                int b = 2;
                int expectedResult = 3;

                int result = a + b;

                Debug.WriteLine($"Expected result: {expectedResult}");
                Debug.WriteLine($"Actual result: {result}");

                Assert.AreEqual(expectedResult, result);
            }
        }
    }
}
