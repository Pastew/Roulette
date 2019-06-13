using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BetField : MonoBehaviour
{
    private Vector3 chipStackOffset = new Vector3(0.08f, 0.06f, -0.1f);

    [SerializeField]
    private BetDef.BetType betType;
    [SerializeField]
    private int number;
    [SerializeField]
    private List<BetField> relatedFields;

    private GameObject light;

    public BetDef.BetType BetType { get => betType; set => betType = value; }
    public int Number { get => number; set => number = value; }
    public List<BetField> RelatedFields { get => relatedFields; set => relatedFields = value; }

    private void Awake()
    {
        light = transform.Find("Light").gameObject; // TODO: Optimize finding method.
    }

    public int[] GetRelatedNumbers()
    {
        return RelatedFields.Select(field => field.Number).ToArray<int>();
    }

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
        foreach(BetField field in RelatedFields)
        {
            field.TurnHighlight(turnedOn);
        }
    }

    public void TurnHighlight(bool turnedOn)
    {
        light.gameObject.SetActive(turnedOn);
    }
}
