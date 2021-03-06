using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo {
    private Nodo _hi;
    private Nodo _hd;
    private Nodo _padre;
    private int _id;
    private List<int> _descendencia;
    private List<string> _texto;
    private Sprite _img;

    public Nodo() {
        _hi = null;
        _hd = null;
        _padre = null;
        _id = 0;
        _descendencia = new List<int>();
        _texto = new List<string>();
        _img = null;
    }

    public void addHi(Nodo padre) {
        _hi = new Nodo();
        _hi.setPadre(padre);
    }

    public Nodo getHi() {
        return _hi;
    }

    public void addHd(Nodo padre) {
        _hd = new Nodo();
        _hd.setPadre(padre);
    }

    public Nodo getHd() {
        return _hd;
    }

    public Nodo getPadre() {
        return _padre;
    }

    public void setPadre(Nodo padre) {
        _padre = padre;
    }

    public int getID() {
        return _id;
    }

    public void incrementID(int idAnterior) {
        _id = idAnterior + 1;
    }

    public void addDescendencia(int indexHijo) {
        _descendencia.Add(indexHijo);

        Nodo recorrer = getPadre();

        while (recorrer != null) {
            recorrer.addDescendencia(indexHijo);
            recorrer = recorrer.getPadre();
        }
    }

    public bool hijoEnRama(int indexHijo) {
        return _descendencia.Contains(indexHijo);
    }

    public List<string> getTexto() {
        return _texto;
    }

    public void setTexto(string cadena) {
        string[] lineas = cadena.Split('_');

        for (int i = 0; i < lineas.Length; i++) {
            _texto.Add(lineas[i]);
        }
    }

    public Sprite getImg() {
        return _img;
    }

    public void setImg(Sprite imagen) {
        _img = imagen;
    }

    public bool dosHijos() {
        return _hd != null;
    }

    public bool ultimoTexto(int indexTexto) {
        return indexTexto == (_texto.Count - 1);
    }

    public string nextText(int currentIndexText) {
        return _texto[currentIndexText + 1];
    }

    public bool nodoFinal() {
        return (_hi == null && _hd == null);
    }
}
