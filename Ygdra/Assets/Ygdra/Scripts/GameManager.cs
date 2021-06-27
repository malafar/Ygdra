using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public static class GameManager {
    private static SaveLoadManager _saveLoadManager = null;
    private static bool _adsInicializados = false;
    private static string _nombreABB = null;
    private static HUDManager _hudManager = null;

    public static SaveLoadManager getSaveLoadManager() {
        if (_saveLoadManager == null) {
            _saveLoadManager = GameObject.FindGameObjectWithTag("SaveLoadManager").GetComponent<SaveLoadManager>();
        }
        return _saveLoadManager;
    }

    public static void inicializarAds() {
        if (!_adsInicializados) {
            MobileAds.Initialize(initStatus => { });
            _adsInicializados = true;
        }
    }

    public static string getNombreABB() {
        return _nombreABB;
    }

    public static void setNombreABB(string nombre) {
        _nombreABB = nombre;
    }

    public static HUDManager GetHUDManager() {
        return _hudManager;
    }

    public static void setHUDManager(HUDManager hud) {
        _hudManager = hud;
    }

    public static void updateCntHojas(bool positivo) {
        if (positivo) {
            Player.incrementCurrentHojas();
        } else {
            Player.decrementCurrentHojas();
        }
        
        _hudManager.updateCntHojas();
    }
}
