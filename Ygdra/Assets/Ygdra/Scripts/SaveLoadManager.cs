using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour {

    private Dictionary<string, List< Tuple<int, State> > > _dataABB;
    private int _currentHojas;
    private int _maxHojas;

    private string _saveFilePath;
    private BinaryFormatter _bf;
    private FileStream _saveFile;

    void Awake() {
        _saveFilePath = Application.persistentDataPath + "/save.dat";
    }

    void Start() {
        _dataABB = new Dictionary<string, List<Tuple<int, State>>>();
        cargar(LoadType.ALL);    
    }

    public void guardarABB(string nombreABB = null, List< Tuple <int, State> > estadosABB = null) {
        _bf = new BinaryFormatter();
        _saveFile = File.Create(_saveFilePath);

        // Guardado de datos
        // Datos Abb
        guardarDatosAbb(nombreABB, estadosABB);

        _saveFile.Close();
        cargar(LoadType.ABB);
    }

    public void guardarPlayerData() {
        _bf = new BinaryFormatter();
        _saveFile = File.Create(_saveFilePath);

        // Guardado de datos
        // Datos de player
        guardarDatosPlayer();

        _saveFile.Close();
        cargar(LoadType.PLAYER);
    }

    public void cargar(LoadType tipo) {
        if (File.Exists(_saveFilePath)) {
            _bf = new BinaryFormatter();
            _saveFile = File.Open(_saveFilePath, FileMode.Open);

            // Cargado de datos

            switch (tipo) {
                case LoadType.ABB:
                    // Datos Abb
                    cargarDatosAbb();
                    break;

                case LoadType.PLAYER:
                    // Datos de jugador
                    cargarDatosPlayer();
                    break;

                case LoadType.ALL:
                    cargarDatosAbb();
                    _saveFile.Position = 0;
                    cargarDatosPlayer();
                    break;
            }

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

    private void guardarDatosPlayer() {
        PlayerData savedData = new PlayerData();
        
        savedData.savePlayerData();

        _bf.Serialize(_saveFile, savedData);
    }

    private void cargarDatosAbb() {
        AbbData savedData = (AbbData)_bf.Deserialize(_saveFile);

        _dataABB = savedData.loadABB();
    }

    private void cargarDatosPlayer() {
        PlayerData savedData = (PlayerData)_bf.Deserialize(_saveFile);

        _currentHojas = savedData.loadCurrentHojas();
        _maxHojas = savedData.loadMaxHojas();
    }

    public List<Tuple<int, State>> getAbbData(string nombreABB) {
        if (_dataABB.ContainsKey(nombreABB)) {
            return _dataABB[nombreABB];
        }

        return new List<Tuple<int, State>>();
    }

    public int getCurrentHojas() {
        return _currentHojas;
    }

    public int getMaxHojas() {
        return _maxHojas;
    }
}
