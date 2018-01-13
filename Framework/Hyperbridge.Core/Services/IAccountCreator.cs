namespace Hyperbridge.Services.Abstract
{
    public interface IAccountCreator
    {
        Account CreateAccount(Wallet wallet, string name);
    }
}
