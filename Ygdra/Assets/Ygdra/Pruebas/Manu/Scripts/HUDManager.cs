using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour{

    public Text cntHojas;
    void Start(){
        updateCntHojas();
    }

    public void updateCntHojas() {
        cntHojas.text = Player.getCurrentHojas() + "/" + Player.getMaxHojas();
    }
}
