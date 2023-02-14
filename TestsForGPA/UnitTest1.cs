namespace TestsForGPA
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            int a = 5; int b = 6;
            int c = a+ b;
        }

        [Test]
        public void Test1()
        {
            int c = 11;
            Assert.AreEqual(c, 11);
        }
    }
}