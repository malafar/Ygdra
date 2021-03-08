using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager {
    public static SaveLoadManager _saveLoadManager;

    public static SaveLoadManager getSaveLoadManagaer() {
        return _saveLoadManager;
    }
}
