﻿using System.Collections;
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
    private SavedData _savedData;

    void Awake() {
        _saveFilePath = Application.persistentDataPath + "/save.dat";
    }

    void Start() {
        _dataABB = new Dictionary<string, List<Tuple<int, State>>>();
        _currentHojas = 0;
        _maxHojas = 0;
        cargar();    
    }

    public void cargar() {
        if (File.Exists(_saveFilePath)) {
            _bf = new BinaryFormatter();
            _saveFile = File.Open(_saveFilePath, FileMode.Open);
            _savedData = (SavedData)_bf.Deserialize(_saveFile);

            // Cargado de datos
            // Datos Abb
            cargarDatosAbb();

            // Datos de jugador
            cargarDatosPlayer();

            _saveFile.Close();
        }
    }

    private void cargarDatosAbb() {
        _dataABB = _savedData.loadABB();
    }

    private void cargarDatosPlayer() {
        _currentHojas = _savedData.loadCurrentHojas();
        _maxHojas = _savedData.loadMaxHojas();
    }

    public void guardar(string nombreABB = null, List< Tuple <int, State> > estadosABB = null) {
        _bf = new BinaryFormatter();
        _saveFile = File.Create(_saveFilePath);
        _savedData = new SavedData();

        // Guardado de datos
        // Datos Abb
        guardarDatosAbb(nombreABB, estadosABB);

        // Datos de player
        guardarDatosPlayer();

        _bf.Serialize(_saveFile, _savedData);
        _saveFile.Close();
        cargar();
    }

    private void guardarDatosAbb(string nombreABB, List<Tuple<int, State>> estadosABB) {
        if (nombreABB != null && estadosABB != null) {
            // Primero se ajustan los datos del árbol concreto si hay cambios
            _dataABB[nombreABB] = estadosABB;
        }

        _savedData.saveABB(_dataABB);
    }

    private void guardarDatosPlayer() {
        _savedData.savePlayerData();
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
