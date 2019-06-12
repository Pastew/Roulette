using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// This tool will generate  bet fields on table:
// It will generate:
// 1. Straights based on Root position. Root = game object in Table game object placed on Straight 1 field. 
// Note: I won't generate Straight_0, you have to create it manually
//
// 2. Splits
// 3. Corners
//
// 4. It will also create and fill BetField scripts for Fixed numbers bet fields (Even, Red, Odd, From1To18 etc...)
// Note: It won't generate second FirstFour field, you have to create it manually
//
// To use it:
// 1. Roulette tab (in Unity Editor) -> Roulette Table Field Generator
// 2. Select BetField Prefab
// 3. Click Generate
//
// Note: This code is not optimized, because it is used only once during development.
// Note: This code is not pretty.
public class TableFieldsGeneratorWindow : EditorWindow
{
    Vector3 offset = new Vector2(-1.01f, -1.15f); // -1.01, -1.15 works OK.
    UnityEngine.Object betFieldPrefab;

    [MenuItem("Roulette/Roulette Table Fields Generator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TableFieldsGeneratorWindow));
    }

    void OnGUI()
    {
        GUILayout.Label("BetField prefab", EditorStyles.boldLabel);
        betFieldPrefab = EditorGUILayout.ObjectField(betFieldPrefab, typeof(UnityEngine.Object), true);

        GUILayout.Label("How to place bet fields on table", EditorStyles.boldLabel);
        offset = EditorGUILayout.Vector2Field("Straights offset", offset);

        if (GUILayout.Button("Generate"))
        {
            RemoveAllBetFields();
            GenerateBetFields();
        }
    }

    private void GenerateBetFields()
    {
        GameObject table = GameObject.Find("Table");
        Vector3 rootPosition = table.transform.Find("Root").transform.position;

        GenerateStraights(rootPosition);
        GenerateSplits(rootPosition);
        GenerateCorners(rootPosition);
        GenerateStreets(rootPosition);
        GenerateSixLines(rootPosition);

        foreach (BetDef.BetType betType in BetDef.betFixedNumbers.Keys)
            GenerateFixedNumberBetFields(betType);
    }


    private void GenerateFixedNumberBetFields(BetDef.BetType betType)
    {
        GameObject table = GameObject.Find("Table");
        Vector2 betFieldPosition = new Vector2();
        try
        {
            betFieldPosition = table.transform.Find(betType.ToString()).gameObject.transform.position;
        }
        catch (NullReferenceException ex)
        {
            Debug.LogError("You need to create this BetField in Table: " + betType.ToString());
        }

        GameObject betFieldGO = Instantiate(betFieldPrefab, table.transform) as GameObject;
        betFieldGO.transform.position = betFieldPosition;
        betFieldGO.name = betType.ToString();
        DestroyImmediate(table.transform.Find(betType.ToString()).gameObject);

        foreach (BetField bf in betFieldGO.GetComponents<BetField>())
            DestroyImmediate(bf);

        BetField betField = betFieldGO.AddComponent<BetField>();
        betField.number = -1;
        betField.betType = betType;

        betField.relatedFields = new List<BetField>();

        foreach (int number in BetDef.betFixedNumbers[betType])
        {
            betField.relatedFields.Add(GameObject.Find("Straight_" + number).GetComponent<BetField>());
        }
    }

    private void GenerateStraights(Vector3 rootPosition)
    {
        GameObject table = GameObject.Find("Table");
        GameObject straightsContainer = new GameObject("Straights");
        straightsContainer.transform.parent = table.transform;

        int number = 1;

        for (int column = 0; column < 12; column++)
        {
            for (int row = 0; row < 3; row++)
            {
                float fieldPosX = rootPosition.x + offset.x * column;
                float fieldPosY = rootPosition.y + offset.y * row;
                Vector3 betPosition = new Vector3(fieldPosX, fieldPosY, 0);

                GameObject betFieldGO = Instantiate(betFieldPrefab, straightsContainer.transform) as GameObject;
                betFieldGO.name = "Straight_" + number;
                betFieldGO.transform.position = betPosition;

                BetField betField = betFieldGO.GetComponent<BetField>();
                betField.number = number;
                betField.betType = BetDef.BetType.Straight;

                number++;
            }
        }
    }

    private void GenerateSplits(Vector3 rootPosition)
    {
        GameObject table = GameObject.Find("Table");

        // Horizontal lines splits
        GameObject splitsContainer = new GameObject("HorizontalSplits");
        splitsContainer.transform.parent = table.transform;

        int number = 1;

        for (int column = 0; column < 12; column++)
        {
            for (int row = 0; row < 2; row++)
            {
                float fieldPosX = rootPosition.x + offset.x * column;
                float fieldPosY = rootPosition.y + offset.y / 2 + offset.y * row;
                Vector3 betPosition = new Vector3(fieldPosX, fieldPosY, 0);

                GameObject betFieldGO = Instantiate(betFieldPrefab, splitsContainer.transform) as GameObject;
                betFieldGO.name = String.Format("Split_{0},{1}", number, number + 1);
                betFieldGO.transform.position = betPosition;

                BetField betField = betFieldGO.GetComponent<BetField>();
                betField.number = -1;
                betField.betType = BetDef.BetType.Split;
                betField.relatedFields = new List<BetField>();
                betField.relatedFields.Add(GameObject.Find("Straight_" + number).GetComponent<BetField>());
                betField.relatedFields.Add(GameObject.Find("Straight_" + (number + 1)).GetComponent<BetField>());

                number++;
            }
            number++;
        }


        // Vertical lines splits
        GameObject verticalSplitsContainer = new GameObject("VerticalSplits");
        verticalSplitsContainer.transform.parent = table.transform;
        number = 1;

        for (int column = 0; column < 11; column++)
        {
            for (int row = 0; row < 3; row++)
            {
                float fieldPosX = rootPosition.x + offset.x / 2 + offset.x * column;
                float fieldPosY = rootPosition.y + offset.y * row;
                Vector3 betPosition = new Vector3(fieldPosX, fieldPosY, 0);

                GameObject betFieldGO = Instantiate(betFieldPrefab, verticalSplitsContainer.transform) as GameObject;
                betFieldGO.name = String.Format("Split_{0},{1}", number, number + 3);
                betFieldGO.transform.position = betPosition;

                BetField betField = betFieldGO.GetComponent<BetField>();
                betField.number = -1;
                betField.betType = BetDef.BetType.Split;

                betField.relatedFields = new List<BetField>();
                betField.relatedFields.Add(GameObject.Find("Straight_" + number).GetComponent<BetField>());
                betField.relatedFields.Add(GameObject.Find("Straight_" + (number + 3)).GetComponent<BetField>());

                number++;
            }
        }
    }

    private void GenerateStreets(Vector3 rootPosition)
    {
        GameObject table = GameObject.Find("Table");

        GameObject streetsContainer = new GameObject("Streets");
        streetsContainer.transform.parent = table.transform;

        int number = 1;

        for (int column = 0; column < 12; column++)
        {
            float fieldPosX = rootPosition.x + offset.x * column;
            float fieldPosY = rootPosition.y + offset.y / 2 + offset.y * 2;
            Vector3 betPosition = new Vector3(fieldPosX, fieldPosY, 0);

            GameObject betFieldGO = Instantiate(betFieldPrefab, streetsContainer.transform) as GameObject;
            betFieldGO.name = String.Format("Street_{0},{1},{2}", number, number + 1, number + 2);
            betFieldGO.transform.position = betPosition;

            BetField betField = betFieldGO.GetComponent<BetField>();
            betField.number = -1;
            betField.betType = BetDef.BetType.Street;

            betField.relatedFields = new List<BetField>();
            for (int i = number; i <= number + 2; i++)
                betField.relatedFields.Add(GameObject.Find("Straight_" + i).GetComponent<BetField>());

            number += 3;
        }
    }

    private void GenerateSixLines(Vector3 rootPosition)
    {
        GameObject table = GameObject.Find("Table");

        GameObject sixLinesContainer = new GameObject("SixLines");
        sixLinesContainer.transform.parent = table.transform;

        int number = 1;

        for (int column = 0; column < 11; column++)
        {
            float fieldPosX = rootPosition.x + offset.x / 2 + offset.x * column;
            float fieldPosY = rootPosition.y + offset.y / 2 + offset.y * 2;
            Vector3 betPosition = new Vector3(fieldPosX, fieldPosY, 0);

            GameObject betFieldGO = Instantiate(betFieldPrefab, sixLinesContainer.transform) as GameObject;
            betFieldGO.name = String.Format("SixLine_{0},{1},{2}", number, number + 1, number + 2);
            betFieldGO.transform.position = betPosition;

            BetField betField = betFieldGO.GetComponent<BetField>();
            betField.number = -1;
            betField.betType = BetDef.BetType.SixLine;

            betField.relatedFields = new List<BetField>();
            for (int i = number; i <= number + 5; i++)
                betField.relatedFields.Add(GameObject.Find("Straight_" + i).GetComponent<BetField>());

            number += 3;
        }
    }

    private void GenerateCorners(Vector3 rootPosition)
    {
        GameObject table = GameObject.Find("Table");

        GameObject cornersContainer = new GameObject("Corners");
        cornersContainer.transform.parent = table.transform;

        int number = 1;

        for (int column = 0; column < 11; column++)
        {
            for (int row = 0; row < 2; row++)
            {
                float fieldPosX = rootPosition.x + offset.x / 2 + offset.x * column;
                float fieldPosY = rootPosition.y + offset.y / 2 + offset.y * row;
                Vector3 betPosition = new Vector3(fieldPosX, fieldPosY, 0);

                GameObject betFieldGO = Instantiate(betFieldPrefab, cornersContainer.transform) as GameObject;
                betFieldGO.name = String.Format("Corner_{0},{1},{2},{3}", number, number + 1, number + 3, number + 4);
                betFieldGO.transform.position = betPosition;

                BetField betField = betFieldGO.GetComponent<BetField>();
                betField.number = -1;
                betField.betType = BetDef.BetType.Corner;

                betField.relatedFields = new List<BetField>();
                betField.relatedFields.Add(GameObject.Find("Straight_" + number).GetComponent<BetField>());
                betField.relatedFields.Add(GameObject.Find("Straight_" + (number + 1)).GetComponent<BetField>());
                betField.relatedFields.Add(GameObject.Find("Straight_" + (number + 3)).GetComponent<BetField>());
                betField.relatedFields.Add(GameObject.Find("Straight_" + (number + 4)).GetComponent<BetField>());

                number++;
            }
            number++;
        }
    }

    private static void RemoveAllBetFields()
    {
        foreach (string gameObjectName in new string[] { "Straights", "HorizontalSplits", "VerticalSplits", "Corners", "Streets", "SixLines" })
        {
            Transform transformToDestroy = GameObject.Find("Table").transform.Find(gameObjectName);
            if (transformToDestroy != null)
                DestroyImmediate(transformToDestroy.gameObject);
        }
    }
}