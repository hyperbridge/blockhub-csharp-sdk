namespace Hyperbridge.Services
{
    public class AccountBalance
    {
        public decimal Amount { get; set; }
        public string Unit { get; set; }

        public override string ToString()
        {
            return $"{Amount} {Unit}";
        }
    }
}
