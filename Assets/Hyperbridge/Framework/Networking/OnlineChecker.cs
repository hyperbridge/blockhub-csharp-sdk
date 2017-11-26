using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using System;

namespace Hyperbridge.Networking
{
    public class OnlineChecker
    {
        private const string URL = "https://httpbin.org/headers";
        private string status = "Waiting...";
        private bool downloading;

        public IEnumerator Check()
        {
            downloading = true;

            Debug.Log("[OnlineChecker] Checking connection with " + URL);

            // Create and send our request
            var request = new HTTPRequest(new Uri(URL));
            request.Timeout = TimeSpan.FromSeconds(10);
            request.Send();

            status = "Download started";

            // Wait while it's finishes and add some fancy dots to display something while the user waits for it.
            // A simple "yield return StartCoroutine(request);" would do the job too.
            while (request.State < HTTPRequestStates.Finished)
            {
                yield return new WaitForSeconds(0.1f);

                status += ".";
            }

            bool success = false;

            // Check the outcome of our request.
            switch (request.State)
            {
                // The request finished without any problem.
                case HTTPRequestStates.Finished:
                    if (request.Response.IsSuccess)
                    {
                        status = "AssetBundle downloaded!";

                        if (request.Response.DataAsText.Contains("BestHTTP"))
                        {
                            success = true;
                        }
                    }
                    else
                    {
                        status = string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
                                                        request.Response.StatusCode,
                                                        request.Response.Message,
                                                        request.Response.DataAsText);
                        Debug.LogWarning(status);
                    }

                    break;

                // The request finished with an unexpected error. The request's Exception property may contain more info about the error.
                case HTTPRequestStates.Error:
                    status = "Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception");
                    Debug.LogError(status);
                    break;

                // The request aborted, initiated by the user.
                case HTTPRequestStates.Aborted:
                    status = "Request Aborted!";
                    Debug.LogWarning(status);
                    break;

                // Connecting to the server is timed out.
                case HTTPRequestStates.ConnectionTimedOut:
                    status = "Connection Timed Out!";
                    Debug.LogError(status);
                    break;

                // The request didn't finished in the given time.
                case HTTPRequestStates.TimedOut:
                    status = "Processing the request Timed Out!";
                    Debug.LogError(status);
                    break;
            }

            downloading = false;

            if (success)
            {
                CodeControl.Message.Send<InternetConnectionEvent>(new InternetConnectionEvent { connected = true });
            }
            else
            {
                CodeControl.Message.Send<InternetConnectionEvent>(new InternetConnectionEvent { connected = false });
            }
        }
    }
}