using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Player {

    // TODO: Valores actuales para pruebas, actualizar a los finales una vez se calculen
    private static int _currentHojas = 3;
    private static int _maxHojas = 3;

    public static int getCurrentHojas() {
        return _currentHojas;
    }

    public static void incrementCurrentHojas() {
        _currentHojas++;
    }

    public static void decrementCurrentHojas() {
        _currentHojas--;
    }

    public static void resetCurrentHojas() {
        _currentHojas = _maxHojas;
    }

    public static int getMaxHojas() {
        return _maxHojas;
    }

    public static void incrementMaxHojas() {
        _maxHojas++;
    }
}