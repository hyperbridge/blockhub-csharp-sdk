using UnityEngine;
using System.Collections;

namespace Hyperbridge.Core
{
    public class AppStateChangeEvent : CodeControl.Message
    {
        public AppState state;
    }
}