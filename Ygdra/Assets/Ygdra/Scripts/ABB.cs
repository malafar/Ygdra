using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

public class ABB {

    private Nodo _raiz;
    private int _lastIndex = 0;

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
}
