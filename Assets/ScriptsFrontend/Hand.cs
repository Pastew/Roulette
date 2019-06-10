using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private GameObject heldChip;
    private BetField closestBetField;

    private List<BetField> betFields;

    private void Start()
    {
        betFields = FindObjectsOfType<BetField>().ToList();
    }

    void Update()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        transform.position = newPos;

        if(heldChip != null)
        {
            BetField newClosestBetField = FindClosestBetField();
            print(newClosestBetField.name);
            if (newClosestBetField != closestBetField)
            {
                if(closestBetField != null)
                    closestBetField.TurnLights(false);

                closestBetField = newClosestBetField;
                closestBetField.TurnLights(true);
            }
        }

        if (Input.GetMouseButtonUp(0) && heldChip != null)
        {
            PlaceChip();
        }
    }

    private BetField FindClosestBetField()
    {
        float minDistance = float.PositiveInfinity;
        BetField currentClosestBetField = null;

        foreach (BetField betField in betFields)
        {
            float distance = Vector2.Distance(betField.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                currentClosestBetField = betField;
            }
        }
        return currentClosestBetField;
    }

    private void PlaceChip()
    {
        heldChip.transform.parent = null;
    }

    public void SpawnChip(GameObject chipPreset)
    {
        heldChip = Instantiate(chipPreset, transform);
    }
}
