using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavedDataClass { }

[Serializable]
class SavedData {

    // ABB
    private Dictionary<string, List< Tuple<int, State> > > _dataABB;

    // Datos contador hojas
    private int _currentHojas;
    private int _maxHojas;

    // Datos anuncios
    private int _anunciosVistos;

    public void saveABB(Dictionary<string, List<Tuple<int, State>>> datos) {
        _dataABB = datos;
    }

    public Dictionary<string, List<Tuple<int, State>>> loadABB() {
        return _dataABB;
    }

    public void savePlayerData() {
        _currentHojas = Player.getCurrentHojas();
        _maxHojas = Player.getMaxHojas();
    }

    public int loadCurrentHojas() {
        return _currentHojas;
    }

    public int loadMaxHojas() {
        return _maxHojas;
    }

    public void saveAnunciosVistos(int numVistos) {
        _anunciosVistos = numVistos;
    }

    public int loadAnunciosVistos() {
        return _anunciosVistos;
    }
}