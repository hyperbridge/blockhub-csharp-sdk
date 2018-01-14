using System;

namespace Blockhub
{
    public class ProfileNotSetException : Exception
    {
        public ProfileNotSetException() : base("Profile has not been set.") { }
    }
}
