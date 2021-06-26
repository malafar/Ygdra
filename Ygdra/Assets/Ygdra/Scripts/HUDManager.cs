using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour{

    public Text cntHojas;
    void Start(){
        Player.ajustarContadoresHojas(GameManager.getSaveLoadManager().getCurrentHojas(), GameManager.getSaveLoadManager().getMaxHojas());
        updateCntHojas();

        if (GameManager.GetHUDManager() == null) {
            GameManager.setHUDManager(this);
        }
    }

    public void updateCntHojas() {
        cntHojas.text = Player.getCurrentHojas() + "/" + Player.getMaxHojas();
    }
}
