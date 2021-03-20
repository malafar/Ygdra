﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;
using UnityEngine.UI;

public class ABB {

    private Nodo _raiz;
    private int _lastIndex = 0;
    private string _nombre;

    public ABB() {
        _raiz = null;
    }

    public ABB(string rutaFichero) {
        string data;
        string[] splitData;

        _raiz = null;

        try {
            StreamReader sr = new StreamReader(rutaFichero);
            data = sr.ReadLine();
            _nombre = data;

            data = sr.ReadLine();

            while (data != null) {
                splitData = data.Split(';');

                if (int.Parse(splitData[1]) > 0) {
                    insertar(int.Parse(splitData[1]) > 1, int.Parse(splitData[0]));
                }
                    
                ajustarPagina(int.Parse(splitData[0]), splitData[2], splitData[3]);

                data = sr.ReadLine();
            }

            sr.Close();
        } catch (Exception e) {
            Debug.LogError("Excepción: " + e.Message);
        } finally {
            Debug.Log("Árbol cargado correctamente.");
        }

        if (GameManager.getSaveLoadManagaer().getAbbData(_nombre) != null) {
            cargarDatos();
        }
    }

    public Nodo getRaiz() {
        return _raiz;
    }

    public void insertar(bool decision, int indexPadre) {
        if (decision) {
            if (_raiz == null) {
                _raiz = new Nodo();
                addDosHijos(_raiz);
                _raiz.setPadre(null);
            } else {
                Nodo recorrer = buscarNodo(indexPadre);
                addDosHijos(recorrer);
            }
        } else {
            if (_raiz == null) {
                _raiz = new Nodo();
                addUnHijo(_raiz);
            } else {
                Nodo recorrer = buscarNodo(indexPadre);
                addUnHijo(recorrer);
            }
        }
    }

    private void addDosHijos(Nodo padre) {
        padre.addHi(padre);
        padre.getHi().incrementID(_lastIndex);
        _lastIndex++;
        padre.addDescendencia(_lastIndex);

        padre.addHd(padre);
        padre.getHd().incrementID(_lastIndex);
        _lastIndex++;
        padre.addDescendencia(_lastIndex);
    }

    private void addUnHijo(Nodo padre) {
        padre.addHi(padre);
        padre.getHi().incrementID(_lastIndex);
        _lastIndex++;
        padre.addDescendencia(_lastIndex);
    }

    private Nodo buscarNodo(int indexPadre) {
        Nodo recorrer = _raiz;

        while (recorrer.getID() != indexPadre) {
            if (recorrer.getHd() != null && 
                (recorrer.getHd().getID() == indexPadre || recorrer.getHd().hijoEnRama(indexPadre))) {
                recorrer = recorrer.getHd();
            } else {
                recorrer = recorrer.getHi();
            }
        }

        return recorrer;
    }

    public void ajustarPagina(int indexNodo, string texto, string  pathIlustracion) {
        Nodo nodo = buscarNodo(indexNodo);
        Sprite imagen = (Sprite)AssetDatabase.LoadAssetAtPath(pathIlustracion, typeof(Sprite));

        nodo.setTexto(texto);
        nodo.setImg(imagen);
    }

    private void cargarDatos() {
        List<Tuple<int, State>> datos = GameManager.getSaveLoadManagaer().getAbbData(_nombre);

        if (datos.Count > 0) {
            Nodo recorrer = _raiz;
            while (recorrer != null) {
                for(int i = 0; i < datos.Count - 1; i++) {
                    if (recorrer.getID() == datos[i].Item1) {
                        recorrer.setState(datos[i].Item2);
                        i = datos.Count - 1;
                        datos.Remove(datos[i]);
                    }
                }

                recorrer = nextNodo(recorrer);
            }
        }
    }

    public void guardarDatos() {
        List<Tuple<int, State>> datos = new List<Tuple<int, State>>();
        Tuple<int, State> info;

        Nodo recorrer = _raiz;
        while (recorrer != null) {
            info = new Tuple<int, State>(recorrer.getID(), recorrer.getState());
            datos.Add(info);

            recorrer = nextNodo(recorrer);
        }

        GameManager.getSaveLoadManagaer().guardar(_nombre, datos);
    }

    private Nodo nextNodo(Nodo current) {
        // Lógica de navegación entre nodos
        // Si tengo hijo izq, paso a hijo izq
        if (current.getHi() != null) {
            current = current.getHi();
        } else {
            // En caso contrario, veo si tengo un hermano. Si lo tengo, paso a hijo dch de mi padre.
            if (current.getPadre().getHd() != null) {
                current = current.getHd();
            } else {
                Nodo anterior = new Nodo();
                // Si no tengo hermano, subo hasta que haya un hijo dch o llegue al primer antepasado.
                while (current != null && ((current.getHd() == null
                    || (current.getHd() != null && current.getHd() == anterior)))) {
                    anterior = current;
                    current = current.getPadre();
                }

                if (current != null) {
                    anterior = current;
                    current = current.getHd();
                }
            }
        }

        return current;
    }
}
