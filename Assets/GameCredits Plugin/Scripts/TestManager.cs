using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameCreditsAPI))]
public class TestManager : MonoBehaviour
{
	private GameCreditsAPI apiComponent;

	public GameCreditsAPI API
	{
		get
		{
			if (apiComponent == null)
				apiComponent = GetComponent<GameCreditsAPI>();
			return apiComponent;
		}
	}

	void Start()
	{
		string ApiServerIp = "https://wallet.gamecredits.org";
		// replace with your app id
		string AppId = "25";
		// replace with one of your app's tokens
		string AppToken = "a3b60742-4d6f-4072-9eb3-8cd2c08accd7";
		// replace with the url you wish to receive notifications at
		string InvoiceNotificationUrl = "postbin.com";
		// init the API
		GameCreditsAPI.Init(ApiServerIp, AppId, AppToken, InvoiceNotificationUrl);

		// create a test invoice and print the received data
		CreateTestInvoice();

		// replace with an invoice id you wish to fetch details for
		string testInvoiceId = "1";

		// fetch details for an invoice
		// GetInvoice("1");
	}

	public void CreateTestInvoice()
	{
		System.Action<bool, string, string> createCallback = new System.Action<bool, string, string>((success, retVal, error_code) =>
		{
			if (success)
			{
				print("Invoice creation successful: " + retVal);
			}
			else
			{
				print("Invoice creation failed: " + error_code + ". Reason: " + retVal);
			}
		});
		print("Creaing invoices...");
		API.CreateInvoice("Test item #1", "Item description", "wallet.gamecredits.org", "Custom tracking data", 10, GameCreditsAPI.SupportedCurrency.GAME, true, createCallback);
	}

	public void GetInvoice(string invoiceId)
	{
		System.Action<bool, string, string> getCallback = new System.Action<bool, string, string>((success, retVal, error_code) =>
		{
			if (success)
			{
				print("Invoice details: " + retVal);
			}
			else
			{
				print("Failed to get invoice details: " + error_code);
			}
		});
		API.GetInvoice(invoiceId, getCallback);
	}

}
