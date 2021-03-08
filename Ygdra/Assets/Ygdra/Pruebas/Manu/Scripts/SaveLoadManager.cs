using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour {

    private Dictionary<string, List< Tuple<int, State> > > _dataABB;
    private string _saveFilePath;
    private BinaryFormatter _bf;
    private FileStream _saveFile;

    void Awake() {
        _saveFilePath = Application.persistentDataPath + "save.dat";
    }

    void Start() {
        cargar();    
    }

    public void guardar(string nombreABB = null, List< Tuple <int, State> > estadosABB = null) {
        _bf = new BinaryFormatter();
        _saveFile = File.Create(_saveFilePath);

        // Guardado de datos
        // Datos Abb
        guardarDatosAbb(nombreABB, estadosABB);

        _saveFile.Close();
        cargar();
    }

    public void cargar() {
        if (File.Exists(_saveFilePath)) {
            _bf = new BinaryFormatter();
            _saveFile = File.Open(_saveFilePath, FileMode.Open);

            // Guardado de datos
            // Datos Abb
            cargarDatosAbb();

            _saveFile.Close();
        }
    }

    private void guardarDatosAbb(string nombreABB, List<Tuple<int, State>> estadosABB) {
        AbbData savedData = new AbbData();
        // Primero se le pasan los datos actuales, al ser muchos ABB
        savedData.saveABB(_dataABB);
        // Ahora, se guardan únicamente los datos nuevos
        if (nombreABB != null && estadosABB != null) {
            savedData.saveABB(nombreABB, estadosABB);
        }

        _bf.Serialize(_saveFile, savedData);
    }

    private void cargarDatosAbb() {
        AbbData savedData = (AbbData)_bf.Deserialize(_saveFile);

        _dataABB = savedData.loadABB();
    }

    public List<Tuple<int, State>> getAbbData(string nombreABB) {
        if (_dataABB.ContainsKey(nombreABB)) {
            return _dataABB[nombreABB];
        }

        return new List<Tuple<int, State>>();
    }
}
