using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveTester : MonoBehaviour
{
    public TaleManager tm;

    public void guardar() {
        tm._abb.guardarDatos();
    }
}
