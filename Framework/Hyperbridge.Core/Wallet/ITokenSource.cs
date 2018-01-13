namespace Hyperbridge.Wallet
{
    public interface ITokenSource
    {
        string Code { get; }
        string Name { get; }
        string Description { get; }
    }
}