using System;

public class IDGenerator
{

    public string GenerateID()
    {
        return Guid.NewGuid().ToString();
    }


}
