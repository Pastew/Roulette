using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetField : MonoBehaviour
{
    public List<BetField> relatedFields;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TurnLights(bool turnedOn)
    {
        foreach(BetField field in relatedFields)
        {
            field.TurnLight(turnedOn);
        }
    }

    private void TurnLight(bool turnedOn)
    {
        transform.GetChild(0).gameObject.SetActive(turnedOn);
    }
}
