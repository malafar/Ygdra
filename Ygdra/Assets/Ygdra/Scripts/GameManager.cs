using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    private static SaveLoadManager _saveLoadManager = null;

    public static SaveLoadManager getSaveLoadManagaer() {
        if (_saveLoadManager == null) {
            _saveLoadManager = GameObject.FindGameObjectWithTag("SaveLoadManager").GetComponent<SaveLoadManager>();
        }
        return _saveLoadManager;
    }
}
