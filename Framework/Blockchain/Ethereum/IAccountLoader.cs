using System;

namespace Hyperbridge.Blockchain.Ethereum 
{
    public interface IAccountLoader {
        Account Load(IJsonDataSource dataSource, string password);
    }
}