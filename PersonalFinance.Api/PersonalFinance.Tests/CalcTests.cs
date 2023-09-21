using PersonalFinance.Services;
namespace PersonalFinance.Tests
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


            CalculatorService calculatorService = new CalculatorService();
            string actual = calculatorService.Calc(x, y, op);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SubstructCalcTest()
        {
            int x = 10;
            int y = 2;
            string op = "substruct";
            string expected = "8";


            CalculatorService calculatorService = new CalculatorService();
            string actual = calculatorService.Calc(x, y, op);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MultiplyCalcTest()
        {
            int x = 10;
            int y = 2;
            string op = "multiply";
            string expected = "20";


            CalculatorService calculatorService = new CalculatorService();
            string actual = calculatorService.Calc(x, y, op);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DevideCalcTest()
        {
            int x = 10;
            int y = 2;
            string op = "devide";
            string expected = "5";


            CalculatorService calculatorService = new CalculatorService();
            string actual = calculatorService.Calc(x, y, op);

            Assert.AreEqual(expected, actual);
        }
    }
}