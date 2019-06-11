using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TableFieldsGeneratorWindow : EditorWindow
{
    Vector3 straightsOffset; // -1.01, -1.15 works OK.
    Vector3 splitsOffset;
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
        straightsOffset = EditorGUILayout.Vector2Field("Straights offset", straightsOffset);
        splitsOffset = EditorGUILayout.Vector2Field("Splits offset", Vector2.one / 2);

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
                float fieldPosX = rootPosition.x + straightsOffset.x * column;
                float fieldPosY = rootPosition.y + straightsOffset.y * row;
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
                float fieldPosX = rootPosition.x + straightsOffset.x * column;
                float fieldPosY = rootPosition.y + straightsOffset.y / 2 + straightsOffset.y * row;
                Vector3 betPosition = new Vector3(fieldPosX, fieldPosY, 0);

                GameObject betFieldGO = Instantiate(betFieldPrefab, splitsContainer.transform) as GameObject;
                betFieldGO.name = String.Format("Split_{0},{1}", number, number + 1);
                betFieldGO.transform.position = betPosition;

                BetField betField = betFieldGO.GetComponent<BetField>();
                betField.number = -1;
                betField.betType = BetDef.BetType.Split;
                betField.relatedFields = new List<BetField>();
                betField.relatedFields.Add(GameObject.Find("Straight_" + number).GetComponent<BetField>());
                betField.relatedFields.Add(GameObject.Find("Straight_" + (number+1)).GetComponent<BetField>());

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
                float fieldPosX = rootPosition.x + straightsOffset.x/2 + straightsOffset.x * column;
                float fieldPosY = rootPosition.y + straightsOffset.y * row;
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

    private static void RemoveAllBetFields()
    {
        foreach (string gameObjectName in new string[] { "Straights", "HorizontalSplits", "VerticalSplits" })
        {
            Transform transformToDestroy = GameObject.Find("Table").transform.Find(gameObjectName);
            if (transformToDestroy != null)
                DestroyImmediate(transformToDestroy.gameObject);
        }
        //List<GameObject> betFields = new List<GameObject>();
        //foreach (BetField betField in FindObjectsOfType<BetField>()) betFields.Add(betField.gameObject);
        //betFields.ForEach(child => DestroyImmediate(child));
    }
}