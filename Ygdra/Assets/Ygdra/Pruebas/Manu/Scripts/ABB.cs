using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABB {

    public Nodo raiz;
    private int _lastIndex = 0;

    public ABB() {
        raiz = null;
    }

    public ABB(string rutaFichero) {
        // Por determinar
    }

    public void insertar(bool decision, int indexPadre) {
        if (decision) {
            if (raiz == null) {
                raiz = new Nodo();
                addDosHijos(raiz);
            } else {
                Nodo recorrer = buscarNodo(indexPadre);
                addDosHijos(recorrer);
            }
        } else {
            if (raiz == null) {
                raiz = new Nodo();
                addUnHijo(raiz);
            } else {
                Nodo recorrer = buscarNodo(indexPadre);
                addUnHijo(recorrer);
            }
        }
    }

    private void addDosHijos(Nodo padre) {
        padre.addHi();
        padre.getHi().incrementID(_lastIndex);
        _lastIndex++;
        padre.addHijoToList(_lastIndex);

        padre.addHd();
        padre.getHd().incrementID(_lastIndex);
        _lastIndex++;
        padre.addHijoToList(_lastIndex);
    }

    private void addUnHijo(Nodo padre) {
        padre.addHi();
        padre.getHi().incrementID(_lastIndex);
        _lastIndex++;
        padre.addHijoToList(_lastIndex);
    }

    private Nodo buscarNodo(int indexPadre) {
        Nodo recorrer = raiz;

        while (recorrer.getID() != indexPadre) {
            if (recorrer.getHi().hijoEnRama(indexPadre)) {
                recorrer = recorrer.getHi();
            } else {
                recorrer = recorrer.getHd();
            }
        }

        return recorrer;
    }

    public void ajustarPagina(int indexNodo, string texto, Sprite ilustracion) {
        Nodo nodo = buscarNodo(indexNodo);

        nodo.setTexto(texto);
        nodo.setIlustracion(ilustracion);
    }
}
