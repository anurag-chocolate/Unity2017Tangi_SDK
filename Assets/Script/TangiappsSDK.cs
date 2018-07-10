using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vdopia;
using UnityEngine.UI;

public class TangiappsSDK : MonoBehaviour {

	VdopiaPlugin plugin;

	public Text myText;

	void Start()                        //Called by Unity
	{





		if (Application.platform == RuntimePlatform.Android)
		{
			plugin = VdopiaPlugin.GetInstance();       //Initialize Plugin Instance

			if (plugin != null)
			{
				//Set Delegate Receiver For Vdopia SDK Ad Event (Not compulsory)
				VdopiaListener.GetInstance().VdopiaAdDelegateEventHandler += onVdopiaEventReceiver;

				//Set USER parameter used for better ad targeting and higher yield (Not mandatory)
				//Developer can pass empty string for any Param like ""
				//Param 1 : Age
				//Param 2 : BirthDate (dd/MM/yyyy)
				//Param 3 : Gender (m/f/u)
				//Param 4 : Marital Status (single/married/unknown)
				//Param 5 : Ethinicty (example : Africans/Asian/Russians)
				//Param 6 : DMA Code (in String format)
				//Param 7 : Postal Code (in String format)
				//Param 8 : Current Postal Code (in String format)
				//Param 9 : Location latitude in string format
				//Param 10 : Location longitude in string format

				plugin.SetAdRequestUserData("23", "23/11/1990", "m", "single", "Asian", "999", "123123", "321321", "", "");

				//Set APP parameter used better Ad targeting and higher yield
				//Developer can pass empty string for any Param like ""
				//Param 1 : App Name
				//Param 2 : Publisher Name
				//Param 3 : App Domain
				//Param 4 : Publisher Domain
				//Param 5 : PlayStore URL of the App
				//Param 6 : App Category (IAB category)

				plugin.SetAdRequestAppData("UnityDemo", "Chocolate", "unity-demo.com", "chocolateplatform.com", "", "IAB9");

				//Set Test Mode parameter used for Getting Test AD (Not mandatory)
				//Param 1 : boolean : true if test mode enabled else false
				//Param 2 : Hash ID (If you are testing Facebook/Google Partner Test Ad you can get from ADB Logcat)
				//plugin.SetAdRequestTestMode(true, "XXXXXXXXXXXXXXXX");

				//New!  Optional.  Silently pre-fetch the next reward ad without making
				//any callbacks.  The pre-fetched ad will remain in cache until you call
				//the next LoadRewardAd.
				plugin.PrefetchRewardAd("JB9dhz");

			}
			else
			{
				Debug.Log("Vdopia Plugin Initialize Error.");
			}

		}

	}



	void onVdopiaEventReceiver(string adType, string eventName)     //Ad Event Receiver
	{
		Debug.Log("Ad Event Received : " + eventName + " : For Ad Type : " + adType);



	

			

			if(adType=="INTERSTITIAL"){
				
				// = When Interstitial Ad is Loaded
				if(eventName=="INTERSTITIAL_LOADED"){

				myText.text = "INTERSTITIAL_LOADED";

				}else 
				if(eventName=="INTERSTITIAL_FAILED" ){//= When Interstitial Ad Load has Failed
					
					myText.text = "INTERSTITIAL_FAILED";
					
				}else
				if(eventName=="INTERSTITIAL_SHOWN" ){//= When Interstitial Ad is Displayed
						myText.text = "INTERSTITIAL_SHOWN";
				}else
				if(eventName=="INTERSTITIAL_DISMISSED"){// = When Interstitial Ad is Dismissed
							myText.text = "INTERSTITIAL_DISMISSED";	
				}else
				if(eventName=="INTERSTITIAL_CLICKED" ){//= When Interstitial Ad is Clicked
								myText.text = "INTERSTITIAL_CLICKED";			
				}
			}



		if(adType=="REWARD"){

			// = When Interstitial Ad is Loaded
			if(eventName=="REWARD_AD_LOADED"){

				myText.text = "REWARD_AD_LOADED";

			}else 
				if(eventName=="REWARD_AD_FAILED" ){//= When Reward Ad Load has Failed

					myText.text = "REWARD_AD_FAILED";

				}else
					if(eventName=="REWARD_AD_SHOWN" ){//= When Reward Ad is Displayed
						myText.text = "REWARD_AD_SHOWN";
					}else
						if(eventName=="REWARD_AD_SHOWN_ERROR"){// = When Reward Ad is Dismissed
							myText.text = "REWARD_AD_SHOWN_ERROR";	
						}else
							if(eventName=="REWARD_AD_DISMISSED" ){//= When Reward Ad is Clicked
								myText.text = "REWARD_AD_DISMISSED";			
							}else
								if(eventName=="REWARD_AD_COMPLETED" ){//= When Reward Ad is completed
									myText.text = "REWARD_AD_COMPLETED";			
								}
		}



				





	}


	//===============Interstitial Ad Methods===============

	public void loadInterstitial()     //called when btnLoadInterstitial Clicked
	{
		Debug.Log("Load Interstitial...");
		if (Application.platform == RuntimePlatform.Android && plugin != null)
		{
			//Param 1: AdUnit Id (This is your SSP App ID you received from your account manager or obtained from the portal)
			plugin.LoadInterstitialAd("JB9dhz");
		}
	}

	public void showInterstitial()     //called when btnShowInterstitial Clicked
	{
		Debug.Log("Show Interstitial...");
		if (Application.platform == RuntimePlatform.Android && plugin != null)
		{
			//Make sure Interstitial Ad is loaded before call this method
			plugin.ShowInterstitialAd();

			//New!  Optional.  Silently pre-fetch the next interstitial ad without making
			//any callbacks.  The pre-fetched ad will remain in cache until you call
			//the next LoadInterstitialAd.
			plugin.PrefetchInterstitialAd("JB9dhz");
		}
	}

	//===============Rewarded Video Ad Methods===============

	public void requestReward()       //called when btnRequestReward Clicked
	{
		Debug.Log("Request Reward...");
		if (Application.platform == RuntimePlatform.Android && plugin != null)
		{
			//Param 1: AdUnit Id (This is your SSP App ID you received from your account manager or obtained from the portal)
			plugin.RequestRewardAd("JB9dhz");
		}
	}

	public void showReward()           //called when btnShowReward Clicked
	{
		Debug.Log("Show Reward...");

		//Make sure Ad is loaded before call this method
		if (Application.platform == RuntimePlatform.Android && plugin != null)
		{
			//Parma 1: Secret Key (Get it from Vdopia Portal: Required if server-to-server callback enabled)
			//Parma 2: User name – this is the user ID of your user account system
			//Param 3: Reward Currency Name or Measure
			//Param 4: Reward Amount
			plugin.ShowRewardAd("qj5ebyZ0F0vzW6yg", "Chocolate1", "coin", "30");

			//Pre-fetch:  Silently pre-fetch the next reward ad without making
			//any callbacks.  The pre-fetched ad will remain in cache until you call
			//the next LoadRewardAd.
			plugin.PrefetchRewardAd("JB9dhz");
		}

	}

	public void checkRewardAdAvailable() {
		Debug.Log("Check Reward Ad Available...");
		if (Application.platform == RuntimePlatform.Android && plugin != null)
		{
			bool isReady = plugin.IsRewardAdAvailableToShow();
			Debug.Log("Check Reward Ad Available..." + isReady);
		}
	}

}
