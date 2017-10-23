using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCreditsAPI : MonoBehaviour
{
    private static string ApiServerIp;
    private static int ApiServerPort;

    private static string AppId;
    private static string AppToken;
    private static string InvoiceNotificationUrl;

    private static bool isConfigured = false;

    public enum SupportedCurrency
    {
        GAME, USD
    }

    /// <summary>
    /// Initializes the API connection settings.
    /// You <b>must</b> call this method before calling the Get/Create methods.
    /// </summary>
    /// <param name="serverIp">The server ip.</param>
    /// <param name="appId">The app ID.</param>
    /// <param name="appToken">An auth token linked to the APP.</param>
    /// <param name="notificationUrl">The notification URL where invoice notifications will be posted.</param>
    public static void Init(string serverIp, string appId, string appToken, string notificationUrl)
    {
        ApiServerIp = serverIp;
        AppId = appId;
        AppToken = appToken;
        InvoiceNotificationUrl = notificationUrl;

        isConfigured = true;
    }

    public static string GetServerUrl()
    {
		return ApiServerIp;
    }

    public static string GetInvoiceUrl(string appId, string invoiceId = "")
    {
        return GetServerUrl() + "/api/app/" + appId + "/invoices/" + invoiceId;
    }

    /// <summary>
    /// Returns details on the invoice with the given ID.
    /// </summary>
    /// <param name="invoiceId">The invoice ID.</param>
    /// <param name="callback">The callback action to call.</param>
    /// <exception cref="System.InvalidOperationException">You must call the Init method before using the Get/Create methods!</exception>
    public void GetInvoice(string invoiceId, System.Action<bool, string, string> callback)
    {
        // check if API config is done
        if (!isConfigured)
        {
            throw new System.InvalidOperationException("You must call the Init method before using the Get/Create methods!");
        }

        StartCoroutine(RequestGetInvoice(invoiceId, callback));
    }

    /// <summary>
    /// Creates and returns a new invoice with the given details.
    /// </summary>
    /// <param name="itemName">Name of the item.</param>
    /// <param name="itemDescription">The item description.</param>
    /// <param name="redirectUrl">The redirect URL. After payment, user will be redirected to this URL.</param>
    /// <param name="trackingData">Custom tracking data. Optional.</param>
    /// <param name="price">The item's price.</param>
    /// <param name="currency">The currency to use. If currency is not GameCredits, the API will calculate the price in GameCredits.</param>
    /// <param name="callback">The callback action to call.</param>
    /// <exception cref="System.InvalidOperationException">You must call the Init method before using the Get/Create methods!</exception>
    public void CreateInvoice(string itemName, string itemDescription, string redirectUrl,
           string trackingData, float price, SupportedCurrency currency, bool isTest, System.Action<bool, string, string> callback)
    {
        // check if API config is done
        if (!isConfigured)
        {
            throw new System.InvalidOperationException("You must call the Init method before using the Get/Create methods!");
        }

        StartCoroutine(RequestCreateInvoice(itemName, itemDescription, redirectUrl, trackingData, price, currency,isTest, callback));
    }

    private IEnumerator RequestGetInvoice(string invoiceId, System.Action<bool, string, string> callback)
    {
        string url = GetInvoiceUrl(AppId, invoiceId);
        // if POST data is null, unity will send a GET request
        byte[] rawData = null;
        // add auth header
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers["Authorization"] = AppToken;
        WWW respData = new WWW(url, rawData, headers);
        yield return respData;

        if (!string.IsNullOrEmpty(respData.error))
        {
            if (callback != null)
            {
                callback(false, respData.text, respData.error);
            }
        }
        else
        {
            if (callback != null)
            {
                callback(true, respData.text, "");
            }
        }

    }

    private IEnumerator RequestCreateInvoice(string itemName, string itemDescription, string redirectUrl,
            string trackingData, float price, SupportedCurrency currency,bool isTest, System.Action<bool, string, string> callback)
    {
        WWWForm postForm = new WWWForm();
        postForm.AddField("item_name", itemName);
        postForm.AddField("item_description", itemDescription);
        postForm.AddField("item_price", price.ToString());
        postForm.AddField("item_currency", currency.ToString());
        postForm.AddField("data", trackingData);
        postForm.AddField("redirect_url", redirectUrl);
        postForm.AddField("app", AppId);
        postForm.AddField("notify_url", InvoiceNotificationUrl);
        postForm.AddField("is_test", isTest.ToString());
        byte[] rawData = postForm.data;

        // add auth header
        Dictionary<string, string> headers = postForm.headers;
        headers["Authorization"] = AppToken;
        string url = GetInvoiceUrl(AppId);
		print(url);
        WWW respData = new WWW(url, rawData, headers);
        yield return respData;
        if (!string.IsNullOrEmpty(respData.error))
        {
            if (callback != null)
            {
                callback(false, respData.text, respData.error);
            }
        }
        else
        {
            if (callback != null)
            {
                callback(true, respData.text, "");
            }
        }
    }
}
