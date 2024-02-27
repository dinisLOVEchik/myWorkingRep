using PersonalFinance.Services;
namespace PersonalFinance.Tests
{
    public class CalcTests
    {
        private readonly CalculatorService _calculatorService;
        public CalcTests()
        {
            _calculatorService = new CalculatorService();
        }

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
            decimal expected = 12;

            decimal actual = _calculatorService.Calc(x, y, op);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void SubstructCalcTest()
        {
            int x = 10;
            int y = 2;
            string op = "substruct";
            decimal expected = 8;

            decimal actual = _calculatorService.Calc(x, y, op);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void MultiplyCalcTest()
        {
            int x = 10;
            int y = 2;
            string op = "multiply";
            decimal expected = 20;

            decimal actual = _calculatorService.Calc(x, y, op);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void DevideCalcTest()
        {
            int x = 10;
            int y = 2;
            string op = "devide";
            decimal expected = 5;

            decimal actual = _calculatorService.Calc(x, y, op);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}