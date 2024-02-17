using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyUI : MonoBehaviour
{
    public static KeyUI instance;

    [SerializeField]
    private TMP_Text KeyNumText;

    private string textPrefix = "x ";

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateKeyNum();
    }

    public void UpdateKeyNum()
    {
        KeyNumText.text = textPrefix + KeyHandler.instance.GetKey().ToString("D2");
    }
}
