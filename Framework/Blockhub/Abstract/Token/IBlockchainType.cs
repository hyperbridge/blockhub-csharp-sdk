namespace Blockhub
{
    public interface IBlockchainType
    {
        string Code { get; }
        string Name { get; }
        string Description { get; }
    }
}