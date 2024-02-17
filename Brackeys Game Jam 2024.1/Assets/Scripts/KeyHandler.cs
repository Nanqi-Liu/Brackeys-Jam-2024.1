using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHandler : MonoBehaviour
{
    public static KeyHandler instance;
    private int currKey = 0;
    void Start()
    {
        instance = this;
        currKey = 0;
    }

    public void IncreaseKey()
    {
        currKey += 1;
        KeyUI.instance.UpdateKeyNum();
    }

    public void DecreaseKey()
    {
        currKey -= 1;
        KeyUI.instance.UpdateKeyNum();
    }

    public int GetKey()
    {
        return currKey;
    }
    
}
