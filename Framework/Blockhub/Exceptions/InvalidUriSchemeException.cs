using System;

namespace Blockhub
{
    public class InvalidUriSchemeException : Exception
    {
        public string Scheme { get; }
        public InvalidUriSchemeException(string scheme) : base($"Invalid uri scheme [{scheme}].")
        {
            Scheme = scheme;
        }
    }
}
