namespace Blockhub
{
    public interface ITokenSource
    {
        string Code { get; }
        string Name { get; }
        string Description { get; }
    }
}