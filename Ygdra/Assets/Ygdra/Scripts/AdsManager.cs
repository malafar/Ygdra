using UnityEngine.Events;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class AdsManager : MonoBehaviour {

    private RewardedAd _reward;
    private string _rewardID = "ca-app-pub-3940256099942544/5224354917";
    // TODO: cuando se vaya a publicar, hay que poner el ID bueno, éste es de prueba

    void Awake() {
        GameManager.inicializarAds();
    }

    private void Start() {
        _reward = new RewardedAd(_rewardID);

        // Cargamos los eventos
        // Cuando falla en cargarse el anuncio
        _reward.OnAdFailedToLoad += HandleFalloCargaAnuncio;

        // Cuando se empieza a mostrar el anuncio
        _reward.OnAdOpening += HandleAperturaAnuncio;

        // Cuando falla en mostrarse el anuncio
        _reward.OnAdFailedToShow += HandleFalloMuestraAnuncio;

        // Cuando se recompensa al jugador/a
        _reward.OnUserEarnedReward += HandleRecompensar;

        // Cuando se cierra el anuncio
        _reward.OnAdClosed += HandleAnuncioCerrado;

        cargarAnuncio();
    }

    private void cargarAnuncio() {
        // Creamos anuncio vacío
        AdRequest request = new AdRequest.Builder().Build();
        // Cargamos el anuncio en la request
        _reward.LoadAd(request);
    }

    public void mostrarAnuncio() {
        if (_reward.IsLoaded()) {
            _reward.Show();
        }
    }

    public void HandleFalloCargaAnuncio(object sender, AdErrorEventArgs args) {
        // Informamos de fallo de carga y que se vuelva a intentar
        // TODO: pendiente de UI

        // Recargamos el anuncio para el próximo intento
        cargarAnuncio();
    }

    public void HandleAperturaAnuncio(object sender, EventArgs args) {
        // Detenemos el audio del juego totalmente
        // TODO: pendiente de sonido
    }

    public void HandleFalloMuestraAnuncio(object sender, AdErrorEventArgs args) {
        // Intentamos volver a mostrar el anuncio
        mostrarAnuncio();
    }

    public void HandleAnuncioCerrado(object sender, EventArgs args) {
        // Devolvemos el adudio del juego
        // TODO: pendiente de sonido

        // Precargamos el siguiente anuncio
        cargarAnuncio();
    }

    public void HandleRecompensar(object sender, Reward args) {
        /*
         Como la recompensa es incrementar el número de hojas
        actual en uno, no hace falta cargar una cantidad,
        directamente incrementamos el número con la funcionalidad
        creada.
         */
        GameManager.updateCntHojas(true);

        /* Código ejemplo
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);*/
    }
}
