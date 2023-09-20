namespace PersonalFinance.Api
{
    public class CalcRequest
    {
        public int arg1 { get; set; }
        public int arg2 { get; set; }
        public string op { get; set; }
        public CalcRequest(int arg1, int arg2, string op)
        {
            this.arg1 = arg1;
            this.arg2 = arg2;
            this.op = op;
        }
    }
}
