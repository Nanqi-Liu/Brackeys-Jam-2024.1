using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryUI : MonoBehaviour
{
    [SerializeField]
    private Image[] batteryCells;
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        // get current flashlight precentage
        int cellNum = FlashlightHandler.instance.CalcBatteryCell(batteryCells.Length);
        UpdateBatteryCells(cellNum);
    }

    private void UpdateBatteryCells(int cellNum)
    {
        for (int i = 0; i < batteryCells.Length; i++)
        {
            if (i < cellNum)
            {
                batteryCells[i].enabled = true;
            }
            else
            {
                batteryCells[i].enabled = false;
            }
        }
    }
}
