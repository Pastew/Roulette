using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetField : MonoBehaviour
{
    public BetDef.BetType betType;
    public List<BetField> relatedFields;

    private GameObject light;

    //private List<Chip> chipsOnThisField;


    private void Awake()
    {
        light = transform.Find("Light").gameObject; // TODO: Modify to find light GO in a better way.
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // Placing chips
    public void PlaceChip(Chip chip)
    {
        chip.transform.parent = transform;
        chip.transform.localPosition = Vector3.zero;
    }

    // Lights
    public void TurnLights(bool turnedOn)
    {
        foreach(BetField field in relatedFields)
        {
            field.TurnLight(turnedOn);
        }
    }

    private void TurnLight(bool turnedOn)
    {
        light.gameObject.SetActive(turnedOn);
    }

}
