﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [Tooltip("Offset from finger to chip. It can be used to make it more visible where you're placing chip. Set to (0,0) if you want to show chip just below the finger.")]
    public Vector3 chipOffset;

    private GameObject heldChip;
    private BetField closestBetField;

    private List<BetField> betFields;
    private PlayerWallet playerWallet;

    private void Start()
    {
        betFields = FindObjectsOfType<BetField>().ToList();
        playerWallet = FindObjectOfType<PlayerWallet>();
    }

    void Update()
    {
        if(heldChip != null)
        {
            UpdateHeldChipPosition();
            LightUpClosestBetField();
        }

        if (Input.GetMouseButtonUp(0) && heldChip != null)
        {
            PlaceChip();
        }
    }

    private void UpdateHeldChipPosition()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        heldChip.transform.position = newPos + chipOffset;
    }

    private void LightUpClosestBetField()
    {
        BetField newClosestBetField = FindClosestBetField();

        if (newClosestBetField != closestBetField)
        {
            if (closestBetField != null)
                closestBetField.TurnHighlightsForRelatedFields(false);

            closestBetField = newClosestBetField;
            closestBetField.TurnHighlightsForRelatedFields(true);
        }
    }

    private BetField FindClosestBetField()
    {
        float minDistance = float.PositiveInfinity;
        BetField currentClosestBetField = null;

        foreach (BetField betField in betFields)
        {
            float distance = Vector2.Distance(betField.transform.position, heldChip.transform.position);
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
        closestBetField.PlaceChip(heldChip.GetComponent<Chip>());
        heldChip = null;
    }

    public void SpawnChip(GameObject chipPreset)
    {
        if(chipPreset.GetComponent<Chip>().value <= playerWallet.PlayerBalance)
            heldChip = Instantiate(chipPreset, transform);
    }
}
