namespace Hyperbridge.Wallet
{
    public interface ICoinCurrency
    {
        string Code { get; }

        string Name { get; }
        string Description { get; }
    }
}