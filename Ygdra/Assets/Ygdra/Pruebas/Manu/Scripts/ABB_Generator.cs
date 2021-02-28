using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ABB_Generator : MonoBehaviour {
    private ABB _abb;
    public string abbData = "Assets/Ygdra/Pruebas/Manu/Data/dataABBPruebas.txt";

    void Start() {
        _abb = new ABB(abbData);
        muestraInfoABB();
    }

    private void muestraInfoABB() {
        Nodo recorrer = _abb.getRaiz();

        //while (recorrer != null) {
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

            Console.WriteLine(info);
            
            Console.WriteLine("Tengo que decir:\n");
            for (int i = 0; i < recorrer.getTexto().Count; i++) {
                Console.WriteLine(recorrer.getTexto()[i]);
            }
        //}
    }
}
