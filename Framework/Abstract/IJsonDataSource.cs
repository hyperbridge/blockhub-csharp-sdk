using System;

namespace Hyperbridge {
    // Returns Json data from a given data source
    public interface IJsonDataSource {
        string GetData();
    }
}