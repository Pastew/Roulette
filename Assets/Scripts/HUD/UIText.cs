using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{

    public static UIText instance;

    protected Text text;

    private void Awake()
    {
        instance = this;
        text = GetComponent<Text>();
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }
}
