using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BetField : MonoBehaviour
{
    private Vector3 chipStackOffset = new Vector3(0.08f, 0.06f, -0.1f);

    public BetDef.BetType betType;
    public int number;
    public List<BetField> relatedFields;

    private GameObject light;

    private void Awake()
    {
        light = transform.Find("Light").gameObject; // TODO: Modify to find light GO in a better way.
    }

    internal int[] GetRelatedNumbers()
    {
        return relatedFields.Select(field => field.number).ToArray<int>();
    }

    // Placing chips
    public void PlaceChip(Chip chip)
    {
        int chipsNumberAlreadyPlacedHere = transform.GetComponentsInChildren<Chip>().Length;

        chip.transform.parent = transform;
        chip.transform.localPosition = Vector3.zero + chipStackOffset * chipsNumberAlreadyPlacedHere;
        GameManager.instance.AddBet(this, chip);
    }

    // Highlights
    public void TurnHighlightsForRelatedFields(bool turnedOn)
    {
        foreach(BetField field in relatedFields)
        {
            field.TurnHighlight(turnedOn);
        }
    }

    public void TurnHighlight(bool turnedOn)
    {
        light.gameObject.SetActive(turnedOn);
    }
}
