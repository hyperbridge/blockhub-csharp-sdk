namespace Blockhub.Wallet
{
    /// <summary>
    /// Generates a new seed based on an algorithm.
    /// </summary>
    /// <typeparam name="T">The type of the seed to generate (i.e. string)</typeparam>
    public interface ISeedGenerator<T>
    {
        T Generate();
    }
}