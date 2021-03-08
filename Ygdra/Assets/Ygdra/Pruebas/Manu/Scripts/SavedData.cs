using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SavedData { }

[Serializable]
class AbbData {
    private Dictionary<string, List< Tuple<int, State> > > _dataABB;

    public void saveABB(string nombreABB, List< Tuple<int, State> > estadosABB) {
        // Lógica: si existe se actualiza, si no , se crea al ser la primera vez que se guarda
        if (_dataABB.ContainsKey(nombreABB)) {
            _dataABB[nombreABB] = estadosABB;
        } else {
            _dataABB.Add(nombreABB, estadosABB);
        }
    }

    public void saveABB(Dictionary<string, List<Tuple<int, State>>> datos) {
        _dataABB = datos;
    }

    public Dictionary<string, List<Tuple<int, State>>> loadABB() {
        return _dataABB;
    } 
}