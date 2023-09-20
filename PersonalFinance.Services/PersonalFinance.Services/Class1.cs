using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Interfaces.Streaming;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PersonalFinance.Services
{
    public class CalculatorService
    {
        public string Calc(int arg1, int arg2, string op)
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

            return $"{res}";
        }
    }
}