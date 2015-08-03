using UnityEngine;
using OnePF;
using System.Collections.Generic;
using UnityEngine.UI;
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
				Debug.Log ("OpenIab INIT");
				
				var public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA3R9IacWrtjDTNU7m9OVuQqKDrqIQ7izEZvKi9cPWVup4rrR71SuxLRV2owmPQX/NTlMWBuKKaQ08aR84iGJQ3UM0h8KcLK70pXOTD8qzb55eiTR6VuUirWFqqy5LDt6neZAOPBYv9nyyLyfDH1LCgNb6izB8LL3Qbw2nhX531UyT3aPVe2LXZUL6240IFETo7I6dW7nLaSTYElehhyH23q1G66cnIDJlTkup0qYVQ17ucGsX8i5f3VXk1hCoDfgYGlBSAiIakWYSymR5n1XXi27AlQPKYHwzLLd0oTgXEQFgdpPikfDYxRGmHK6oXpV2C7HQBFj/9WCdf6k26pqimwIDAQAB";
				var options = new Options ();
				options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
				options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
				options.checkInventory = false;
				options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
				options.prefferedStoreNames = new string[] {
						OpenIAB_Android.STORE_GOOGLE,
						OpenIAB_iOS.STORE
				};
				options.storeSearchStrategy = SearchStrategy.BEST_FIT;
				options.availableStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE,
			OpenIAB_iOS.STORE };
				options.storeKeys = new Dictionary<string, string> {
					{OpenIAB_Android.STORE_GOOGLE, public_key}
				};
		
				// Transmit options and start the service
				OpenIAB.init (options);
				

		}

		public void BuyNoAds ()
		{			
				print ("BUYNOADS");
			
				if (!_isInitialized)
						return;
				print ("BUY is initialized");
				
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
				//	Debug.Log ("billingNotSupportedEvent: " + error);
				
		}

		private bool JespersPhone ()
		{
				bool isJespersPhone = false; 
				#if UNITY_ANDROID
				if (!Application.isEditor) {
						AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject> ("currentActivity");
						AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject> ("getContentResolver");  
						AndroidJavaClass secure = new AndroidJavaClass ("android.provider.Settings$Secure");
						string id = secure.CallStatic<string> ("getString", contentResolver, "android_id");
						if ("5c40728084cb5806" == id) {
								isJespersPhone = true;
						}
				}
				#endif
				return isJespersPhone;
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
						
						if (JespersPhone ()) {
								foundNoAds = true;
						}
						if (!foundNoAds) {
								PlayerPrefsManager.ShowAds ();
						} else {
								PlayerPrefsManager.RemoveAds ();
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
						PlayerPrefsManager.RemoveAds ();
						break;
				case "android.test.purchased":
						PlayerPrefsManager.RemoveAds ();
						break;
				default:
						break;
				}
		}
}
