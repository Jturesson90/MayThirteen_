using UnityEngine;
using OnePF;
using System.Collections.Generic;

public class InApp : MonoBehaviour
{
		public static InApp inApp;
		public string inappLabel = "";
		const string NO_ADS = "no_ads";
		bool _isInitialized = false;
		// Use this for initialization
	#pragma warning disable 0414
		string _label = "";
	#pragma warning restore 0414
		void Awake ()
		{
				if (Application.isEditor)
						return;	
				if (inApp == null) {
						DontDestroyOnLoad (this.gameObject);
						inApp = this;
				} else if (inApp != this) {
						Destroy (gameObject);
				}
			
		}

		void Start ()
		{
				
				
				OpenIAB.mapSku (NO_ADS, OpenIAB_Android.STORE_GOOGLE, "no_ads");
				OpenIAB.mapSku (NO_ADS, OpenIAB_iOS.STORE, "no_ads");

				
				init ();

		}

		void init ()
		{
				Debug.Log ("INIT");

				var public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgED1o3jgkaOy8DCSAd4UucidG5/0i4FwrTc/fKr74VAPwtn1tY6OgNG1X2jrjOwkvQthXD7oMCK7rMk6il3vBC0Q2j07rSRJHY3e2rRc1ejxVq6OM0YZXd9OxyRHl1iz0FuhWUfXQavFiPn6MWqxtk/t/gXORCjYguoCOHFvGFYrAvQzH99eJ+OyeRv6SmS7F799vTIaqjIfClT/ZgGGxdGdgkLCVLKDkt8hAdLvf690jsUmTsDaEWrrSz/cvD3uLBxiSewyS+Eb4DqVuYMHBmVn+tNtR+yLrrm7sBYx34xlxSIF3XIk8dEKV7grhDx+9iD4SvokwJEftC1bYlJWzwIDAQAB";
				var options = new Options ();
				options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
				options.prefferedStoreNames = new string[] {
						OpenIAB_Android.STORE_GOOGLE,
						OpenIAB_iOS.STORE
				};
				options.storeKeys = new Dictionary<string, string> {
					{OpenIAB_Android.STORE_GOOGLE, public_key}
				};
		
				// Transmit options and start the service
				OpenIAB.init (options);
				

		}

		public void BuyNoAds ()
		{			
				if (!_isInitialized)
						return;
				
				
				OpenIAB.purchaseProduct (NO_ADS);	
				//OpenIAB.purchaseProduct ("android.test.purchased");
		}

		public string GetLabel ()
		{
				string label = inappLabel;
				if (inappLabel == "") {
						return label;
				} else {
						inappLabel = "";
						return label;
				}
				
		}

		public bool GetInit ()
		{
				return _isInitialized;
		}

		private void OnEnable ()
		{
				// Listen to all events for illustration purposes
				OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
				OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
				OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
				OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
				OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
				OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
				OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
				OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		}

		private void OnDisable ()
		{
				// Remove all event handlers
				OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
				OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
				OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
				OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
				OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
				OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
				OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
				OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
		}

		private void billingSupportedEvent ()
		{
				_isInitialized = true;
				
				Debug.Log ("billingSupportedEvent");
				OpenIAB.queryInventory ();

		}

		private void billingNotSupportedEvent (string error)
		{
				Debug.Log ("billingNotSupportedEvent: " + error);
				
		}
		
		private void queryInventorySucceededEvent (Inventory inventory)
		{
				Debug.Log ("queryInventorySucceededEvent: " + inventory);
				if (inventory != null) {
						_label = inventory.ToString ();
						List<Purchase> skuss = inventory.GetAllPurchases ();
						for (int i = 0; i < skuss.Count; i++) {
								if (skuss [i].Sku == "android.test.purchased") {
										OpenIAB.consumeProduct (skuss [i]);
								}
						}
						List<string> prods = inventory.GetAllOwnedSkus ();
						bool foundNoAds = false;
						for (int i = 0; i < prods.Count; i++) {
								Debug.Log (prods [i]);
								if (prods [i] == NO_ADS || prods [i] == "android.test.purchased") {
										foundNoAds = true;
										
								}
						}
						if (!foundNoAds) {
								PlayerPrefs.SetInt ("NoAds", 0);
						} else {
								PlayerPrefs.SetInt ("NoAds", 1);
						}
				}
				
		}

		private void queryInventoryFailedEvent (string error)
		{
				Debug.Log ("queryInventoryFailedEvent: " + error);
				_label = error;
		}

		private void purchaseSucceededEvent (Purchase purchase)
		{
				//Debug.Log ("purchaseSucceededEvent: " + purchase);
				PurchaseIDHandler (purchase.Sku);
				_label = "PURCHASED:" + purchase.ToString ();
				inappLabel = "Purchase completed";
				
		}

		private void purchaseFailedEvent (int errorCode, string errorMessage)
		{
				Debug.Log ("errorCode " + errorCode);
				inappLabel = "Purchase failed";
		}

		private void consumePurchaseSucceededEvent (Purchase purchase)
		{
				Debug.Log ("consumePurchaseSucceededEvent: " + purchase);
				_label = "CONSUMED: " + purchase.ToString ();
		}

		private void consumePurchaseFailedEvent (string error)
		{
				Debug.Log ("consumePurchaseFailedEvent: " + error);
				_label = "Consume Failed: " + error;
				
		}

		private void PurchaseIDHandler (string id)
		{
				switch (id) {
				case NO_ADS:
						PlayerPrefs.SetInt ("NoAds", 1);
						break;
				case "android.test.purchased":
						PlayerPrefs.SetInt ("NoAds", 1);
						break;
				default:
						break;
				}
		}
}
