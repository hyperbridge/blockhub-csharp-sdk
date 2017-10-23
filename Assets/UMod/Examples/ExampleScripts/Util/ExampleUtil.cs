using UnityEngine;
using System.Collections;

namespace UMod.Example
{
    /// <summary>
    /// Simple helper class used by the example scripts.
    /// </summary>
    public static class ExampleUtil
    {
        // Methods
        public static void Log(object sender, string message)
        {
            // Format the message
            string msg = FormattedMessage(sender, message);

            // Log to console
            Debug.Log(msg);
        }

        public static void LogError(object sender, string message)
        {
            // Format the message
            string msg = FormattedMessage(sender, message);

            // Log to console
            Debug.LogError(msg);
        }

        private static string FormattedMessage(object sender, string message)
        {
            // Get the name of the class that sent the message
            string typeName = sender.GetType().Name;

            // Format the message
            return string.Format("[{0}]: {1}", typeName, message);
        }
    }
}