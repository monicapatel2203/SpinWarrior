using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, 
// one of the existing Survival Shooter scripts.
using UnityEngine.UI;


namespace CompleteProject
{
		// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
	public class Purchaser : MonoBehaviour, IStoreListener
	{
		private static IStoreController m_StoreController;          // The Unity Purchasing system.
		private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

		// Product identifiers for all products capable of being purchased: 
		// "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
		// counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
		// also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

		// General product identifiers for the consumable, non-consumable, and subscription products.
		// Use these handles in the code to reference which product to purchase. Also use these values 
		// when defining the Product Identifiers on the store. Except, for illustration purposes, the 
		// kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
		// specific mapping to Unity Purchasing's AddProduct, below.
		/*public static string kProductIDConsumable =    "consumable";   
		public static string kProductIDNonConsumable = "nonconsumable";
		public static string kProductIDSubscription =  "subscription"; */

		//	public static string k_testpurchase100_NonConsumable = "testpurchase100";

		public static string swStars1Consumable = "com.if.spinwarrior.swstars1";
		public static string swStars2Consumable = "com.if.spinwarrior.swstars2";
		public static string swStars3Consumable = "com.if.spinwarrior.swstars3";
		public static string removeAds = "com.if.spinwarrior.removeads";

		// Apple App Store-specific product identifier for the subscription product.
		private static string kProductNameAppleSubscription =  "com.unity3d.subscription.new";

		// Google Play Store-specific product identifier subscription product.
		private static string kProductNameGooglePlaySubscription =  "com.unity3d.subscription.original"; 


		//public GameObject Notificationtxt;
		public GameObject InternetPanel,RemoveAdPanel;
	
		void Start()
		{
			// If we haven't set up the Unity Purchasing reference

			//Debug.LogError ("Installer....IAP!"+m_StoreController);

			if (m_StoreController == null)
			{
				//Debug.LogError ("Installer....IAP!");
					// Begin to configure our connection to Purchasing
					InitializePurchasing();
			}
		}

		public void InitializePurchasing() 
		{
			// If we have already connected to Purchasing ...
			if (IsInitialized())
			{
					Debug.Log ("Proud  init  ");
					// ... we are done here.
					return;
			}
			// Create a builder, first passing in a suite of Unity provided stores.
			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

			// Add a product to sell / restore by way of its identifier, associating the general identifier
			// with its store-specific identifiers.
			//builder.AddProduct(kProductIDConsumable, ProductType.Consumable);
			// Continue adding the non-consumable product.

			//builder.AddProduct(k_testpurchase100_NonConsumable, ProductType.NonConsumable);


			builder.AddProduct(swStars1Consumable, ProductType.Consumable);
			builder.AddProduct(swStars2Consumable, ProductType.Consumable);
			builder.AddProduct(swStars3Consumable, ProductType.Consumable);
			builder.AddProduct(removeAds,ProductType.NonConsumable);

			// And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
			// if the Product ID was configured differently between Apple and Google stores. Also note that
			// one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
			// must only be referenced here. 
			/*builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
					{ kProductNameAppleSubscription, AppleAppStore.Name },
					{ kProductNameGooglePlaySubscription, GooglePlay.Name },
			});*/

			// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
			// and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
			UnityPurchasing.Initialize(this, builder);
		}

		private bool IsInitialized()
		{
			// Only say we are initialized if both the Purchasing references are set.
			Debug.Log(m_StoreController+"    aaa  "+m_StoreExtensionProvider);
			return m_StoreController != null && m_StoreExtensionProvider != null;
		}

		public void Buyswgems1_Consumable()
		{
			// Buy the non-consumable product using its general identifier. Expect a response either 
			// through ProcessPurchase or OnPurchaseFailed asynchronously.

			if (Application.internetReachability == NetworkReachability.NotReachable) 
			{
				InternetPanel.SetActive(true);
				//StartCoroutine(InternetClose_Panel(3.0f));
				PlayerPrefs.SetInt ("isNetNotAvail",1);
			} 

			else 
			{
				BuyProductID (swStars1Consumable);
			}
		}

		public void Buyswgems2_Consumable()
		{
			// Buy the non-consumable product using its general identifier. Expect a response either 
			// through ProcessPurchase or OnPurchaseFailed asynchronously.
		
			if (Application.internetReachability == NetworkReachability.NotReachable) 
			{
				InternetPanel.SetActive(true);
				//StartCoroutine(InternetClose_Panel(3.0f));
				PlayerPrefs.SetInt ("isNetNotAvail",1);
			} 

			else 
			{
				BuyProductID (swStars2Consumable);
			}
		}

		public void Buyswgems3_Consumable()
		{
			// Buy the non-consumable product using its general identifier. Expect a response either 
			// through ProcessPurchase or OnPurchaseFailed asynchronously.

			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				InternetPanel.SetActive(true);
				//StartCoroutine(InternetClose_Panel(3.0f));
				PlayerPrefs.SetInt ("isNetNotAvail",1);
			} 

			else 
			{
				BuyProductID (swStars3Consumable);
			} 
		}

		public void RemoveAds()
		{
			if(Application.internetReachability == NetworkReachability.NotReachable)
			{
				GameControllerScript _gcSCript = FindObjectOfType<GameControllerScript>();
				_gcSCript.InternetCheckPanel_home.SetActive(true);
				//StartCoroutine(InternetClose_PanelHome(3.0f));
				//PlayerPrefs.SetInt ("isNetNotAvail",1);
			}
			else
			{
				if( PlayerPrefs.GetInt("NoAd") == 1 )
				{
					RemoveAdPanel.SetActive(true);
					//StartCoroutine(RemoveAds_Panel(3.0f));
				}
				else
				{
					BuyProductID(removeAds);
				}				
			}
		}
	
		IEnumerator InternetClose_Panel(float delay)
		{
			yield return new WaitForSeconds(delay);
			InternetPanel.SetActive(false);
		}

		IEnumerator InternetClose_PanelHome( float delay )
		{
			GameControllerScript _gameControlScript = FindObjectOfType<GameControllerScript>();
			yield return new WaitForSeconds(delay);
			_gameControlScript.InternetCheckPanel_home.SetActive(false);
		}

		IEnumerator RemoveAds_Panel( float delay )
		{
			yield return new WaitForSeconds(delay);
			RemoveAdPanel.SetActive(false);
		}

		void BuyProductID(string productId)
		{
			// If Purchasing has been initialized ...
			if (IsInitialized())
			{
				// ... look up the Product reference with the general product identifier and the Purchasing 
				// system's products collection.
				Product product = m_StoreController.products.WithID(productId);

				// If the look up found a product for this device's store and that product is ready to be sold ... 
				if (product != null && product.availableToPurchase)
				{
					Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
					// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
					// asynchronously.

					m_StoreController.InitiatePurchase(product);
				}
				// Otherwise ...
				else
				{
					// ... report the product look-up failure situation  
					Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
			// Otherwise ...
			else
			{
				// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
				// retrying initiailization.
				Debug.Log("BuyProductID FAIL. Not initialized.");
			}
		}

		// Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
		// Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
		public void RestorePurchases()
		{
				// If Purchasing has not yet been set up ...
			if (!IsInitialized())
			{
				// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
				Debug.Log("RestorePurchases FAIL. Not initialized.");
				return;
			}

			// If we are running on an Apple device ... 
			if (Application.platform == RuntimePlatform.IPhonePlayer || 
					Application.platform == RuntimePlatform.OSXPlayer)
			{
				// ... begin restoring purchases
				Debug.Log("RestorePurchases started ...");

				// Fetch the Apple store-specific subsystem.
				IAppleExtensions apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
				// Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
				// the Action below, and ProcessPurchase if there are previously purchased products to restore.
				apple.RestoreTransactions((result) => {
						// The first phase of restoration. If no more responses are received on ProcessPurchase then 
						// no purchases are available to be restored.
						Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
				});
			}
			// Otherwise ...
			else
			{
				// We are not running on an Apple device. No work is necessary to restore purchases.
				Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
			}
		}

		// --- IStoreListener
		//

		public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
		{
				// Purchasing has succeeded initializing. Collect our Purchasing references.
			Debug.Log("OnInitialized: PASS");

			// Overall Purchasing system, configured with products for this application.
			m_StoreController = controller;
			// Store specific subsystem, for accessing device-specific store features.
			m_StoreExtensionProvider = extensions;
		}


		public void OnInitializeFailed(InitializationFailureReason error)
		{
			// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
			Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
		}

		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
		{
			Debug.LogError("Process Purchase....Inapp....");
						// A consumable product has been purchased by this user.
			Gameplay _gPlayScript = FindObjectOfType<Gameplay>();

			if (String.Equals(args.purchasedProduct.definition.id, swStars1Consumable, StringComparison.Ordinal))
			{
				Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				Debug.Log ("Buy 0 carrom board  ");

				PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") + 200);		//20
				// transform.GetComponent<Gameplay> ().PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
				_gPlayScript.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
				_gPlayScript.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
				_gPlayScript.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();

				// TODO: The non-consumable item has been successfully purchased, grant this item to the player.
			}

			else if (String.Equals(args.purchasedProduct.definition.id, swStars2Consumable, StringComparison.Ordinal))
			{
				Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				Debug.Log ("Buy 1 carrom board  ");

				PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") + 500);		//50
				// transform.GetComponent<Gameplay> ().PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
				_gPlayScript.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
				_gPlayScript.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
				_gPlayScript.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
				// TODO: The non-consumable item has been successfully purchased, grant this item to the player.
			}
			else if (String.Equals(args.purchasedProduct.definition.id, swStars3Consumable, StringComparison.Ordinal))
			{
				Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				Debug.Log ("Buy 2 carrom board  ");
				PlayerPrefs.SetInt ("PowerUpCollect", PlayerPrefs.GetInt ("PowerUpCollect") + 800);		//80
				// transform.GetComponent<Gameplay> ().PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
				_gPlayScript.PowerUpTxt.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
				_gPlayScript.SelectCharStar.GetComponent<Text> ().text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
				_gPlayScript.PowerUpTxt_InApp.text = PlayerPrefs.GetInt ("PowerUpCollect").ToString ();
				// TODO: The non-consumable item has been successfully purchased, grant this item to the player.
			}
			else if(String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
			{
				Debug.Log("Remove Ad string");
				PlayerPrefs.SetInt("NoAd",1);
			}
//						
			// Or ... a subscription product has been purchased by this user.

			// Or ... an unknown product has been purchased by this user. Fill in additional products here....
			else 
			{
				Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
			}

			// Return a flag indicating whether this product has completely been received, or if the application needs 
			// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
			// saving purchased products to the cloud, and when that save is delayed. 
			return PurchaseProcessingResult.Complete;
		}

		public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
		{
			// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
			// this reason with the user to guide their troubleshooting actions.
			Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
		}
	}
}