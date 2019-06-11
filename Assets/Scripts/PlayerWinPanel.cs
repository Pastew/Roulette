using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWinPanel : MonoBehaviour
{
    internal void SetText(string newText)
    {
        GetComponentInChildren<Text>().text = newText;
    }
}
