using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public static class GameManager {
    private static SaveLoadManager _saveLoadManager = null;
    private static bool _adsInicializados = false;
    private static string _nombreABB = null;

    public static SaveLoadManager getSaveLoadManagaer() {
        if (_saveLoadManager == null) {
            _saveLoadManager = GameObject.FindGameObjectWithTag("SaveLoadManager").GetComponent<SaveLoadManager>();
        }
        return _saveLoadManager;
    }

    public static void inicializarAds() {
        if (!_adsInicializados) {
            _adsInicializados = !_adsInicializados;
            MobileAds.Initialize(initStatus => { });
        }
    }

    public static string getNombreABB() {
        return _nombreABB;
    }

    public static void setNombreABB(string nombre) {
        _nombreABB = nombre;
    }
}
