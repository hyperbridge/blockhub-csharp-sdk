using UnityEngine;
using System.Collections;

namespace Hyperbridge.Networking
{
    public class InternetConnectionEvent : CodeControl.Message
    {
        public bool connected;
    }
}