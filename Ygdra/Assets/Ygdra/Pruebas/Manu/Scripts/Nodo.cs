using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo {
    private Nodo _hi;
    private Nodo _hd;
    private int _id;
    private List<int> _nodosHijos;
    private string _texto;
    private Sprite _ilustracion;

    public Nodo() {
        _hi = null;
        _hd = null;
        _id = 0;
        _texto = "";
        _ilustracion = null;
    }

    public void addHi() {
        _hi = new Nodo();
    }

    public Nodo getHi() {
        return _hi;
    }

    public void addHd() {
        _hd = new Nodo();
    }

    public Nodo getHd() {
        return _hd;
    }

    public int getID() {
        return _id;
    }

    public void incrementID(int idAnterior) {
        _id = idAnterior + 1;
    }

    public void addHijoToList(int indexHijo) {
        _nodosHijos.Add(indexHijo);
    }

    public bool hijoEnRama(int indexHijo) {
        return _nodosHijos.Contains(indexHijo);
    }

    public void setTexto(string cadena) {
        _texto = cadena;
    }

    public void setIlustracion(Sprite imagen) {
        _ilustracion = imagen;
    }
}
