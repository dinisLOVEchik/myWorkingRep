namespace PersonalFinance.Services
{
    public class CalculatorService
    {
        public decimal Calc(int arg1, int arg2, string op)
        {
            decimal res = 0;
            if (op.Equals(Operation.add.ToString()))
                res = arg1 + arg2;
            else if (op.Equals(Operation.substruct.ToString()))
                res = arg1 - arg2;
            else if (op.Equals(Operation.multiply.ToString()))
                res = arg1 * arg2;
            else if (op.Equals(Operation.devide.ToString()))
                res = arg1 / arg2;

            return res;
        }
    }
}