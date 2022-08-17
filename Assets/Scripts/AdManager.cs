using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private InterstitialAd interstitial;
    public static AdManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }
    void Start()
    {
        MobileAds.Initialize(InitializationStatus => { });
        this.RequestInterstitial();
    }



    public void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-6663924389402728/4999565022";
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }
        this.interstitial = new InterstitialAd(adUnitId);


        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;




        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
        OyuncuManager.isGameStarted = true;

    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
       /* MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);*/
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
      
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        OyuncuManager.isGameStarted = false;

    }
    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            interstitial.Show();

        }else
        {
            Debug.Log("Henuz Yuklenmedi");
        }
    }

}
