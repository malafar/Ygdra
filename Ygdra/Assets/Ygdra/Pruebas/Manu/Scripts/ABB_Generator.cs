using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ABB_Generator : MonoBehaviour {
    private ABB _abb;
    public UnityEngine.Object abbData;
    public SpriteRenderer pruebaImg;

    void Start() {
        _abb = new ABB(AssetDatabase.GetAssetPath(abbData));
        //muestraInfoABB();
    }

    private void muestraInfoABB() {
        Nodo recorrer = _abb.getRaiz();
        List<int> visitados = new List<int>();
        while (recorrer != null) {
            string info = "Hola soy el nodo " + recorrer.getID() + ".\n";

            if (recorrer.getPadre() == null) {
                info += "Soy el nodo raíz.\n";
            } else {
                info += ("Mi padre es " + recorrer.getPadre().getID() + ".\n");
            }

            if (recorrer.getHi() != null && recorrer.getHd() != null) {
                info += ("Tengo dos hijos, " + recorrer.getHi().getID() + " y " + recorrer.getHd().getID() + ".\n");
            } else if (recorrer.getHi() == null && recorrer.getHd() == null) {
                info += "No tengo hijos.\n";
            } else {
                info += ("Tengo un hijo, " + recorrer.getHi().getID() + ".\n");
            }

            Debug.Log(info);

            Debug.Log("Tengo que decir:\n");
            for (int i = 0; i < recorrer.getTexto().Count; i++) {
                Debug.Log(recorrer.getTexto()[i]);
            }

            pruebaImg.sprite = recorrer.getImg();
            // Lógica de navegación entre nodos
            // Si tengo hijo izq, paso a hijo izq
            if (recorrer.getHi() != null) {
                recorrer = recorrer.getHi();
            } else {
                // En caso contrario, veo si tengo un hermano. Si lo tengo, paso a hijo dch de mi padre.
                if (recorrer.getPadre().getHd() != null) {
                    recorrer = recorrer.getHd();
                } else {
                    Nodo anterior = new Nodo();
                    // Si no tengo hermano, subo hasta que haya un hijo dch o llegue al primer antepasado.
                    while (recorrer != null && ((recorrer.getHd() == null  
                        || (recorrer.getHd() != null && recorrer.getHd() == anterior)))) {
                        anterior = recorrer;
                        recorrer = recorrer.getPadre();
                    }

                    if (recorrer != null) {
                        anterior = recorrer;
                        recorrer = recorrer.getHd();
                    }
                }
            }
        }
    }
}
