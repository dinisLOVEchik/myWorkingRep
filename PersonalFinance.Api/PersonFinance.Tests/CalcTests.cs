using Microsoft.VisualStudio.TestPlatform;
using PersonalFinance.Services;

namespace PersonFinance.Tests
{
    public class CalcTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddCalcTest()
        {
            int x = 10;
            int y = 2;
            string op = "add";
            string expected = "12";

            CalculatorService calculator = new CalculatorService();
            string actual = calculator.Calc(x, y, op);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SubstructCalcTest()
        {
            int x = 10;
            int y = 8;
            string op = "substruct";
            string expected = "2";

            CalculatorService calculator = new CalculatorService();
            string actual = calculator.Calc(x, y, op);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultiplyCalcTest()
        {
            int x = 10;
            int y = 2;
            string op = "multiply";
            string expected = "20";

            CalculatorService calculator = new CalculatorService();
            string actual = calculator.Calc(x, y, op);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void devideCalcTest()
        {
            int x = 10;
            int y = 2;
            string op = "devide";
            string expected = "5";

            CalculatorService calculator = new CalculatorService();
            string actual = calculator.Calc(x, y, op);

            Assert.AreEqual(expected, actual);
        }
    }
}