using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AdsManager : MonoBehaviour {

    private RewardBasedVideoAd _reward;
    [SerializeField] private string rewardID = "ca-app-pub-3940256099942544/5224354917";
    // TODO: cuando se vaya a publicar, hay que poner el ID bueno, éste es de prueba

    void Awake() {
        GameManager.inicializarAds();
    }

    private void Start() {
        pedirReward();
    }

    private void pedirReward() {
        _reward = RewardBasedVideoAd.Instance;
        AdRequest request = new AdRequest.Builder().Build();
        _reward.LoadAd(request, rewardID);
    }

    public void mostrarAnuncio() {
        _reward.Show();
        pedirReward();
    }
}
