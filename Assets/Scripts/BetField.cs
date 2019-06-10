using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BetField : MonoBehaviour
{
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
        chip.transform.parent = transform;
        chip.transform.localPosition = Vector3.zero;
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

    private void TurnHighlight(bool turnedOn)
    {
        light.gameObject.SetActive(turnedOn);
    }
}
