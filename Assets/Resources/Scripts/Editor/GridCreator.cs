using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class GridCreator : EditorWindow
{
    private float desiredSize = 8f;
    private GameObject parent;
    private int rows, columns;

    [MenuItem("Custom/Grid Creator")]
    public static void ShowWindow()
    {
        GetWindow<GridCreator>("Grid Creator");
    }

    private void OnEnable()
    {
        if (Selection.activeGameObject)
        {
            parent = Selection.activeGameObject;
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Instantiation Settings", EditorStyles.boldLabel);
        
        parent = EditorGUILayout.ObjectField("Parent Object", parent, typeof(GameObject), true) as GameObject;
        rows = EditorGUILayout.IntField("Number Of Rows", rows);
        columns = EditorGUILayout.IntField("Number Of Columns", columns);
        

        GUILayout.Space(10);

        if (GUILayout.Button("Instantiate Prefabs"))
        {
            if (parent != null && rows > 0 && columns > 0)
            {
                InstantiatePrefabs();
            }
            else
            {
                Debug.Log("Please select a parent object and ensure rows and columns are not 0");
                //Debug.Log($"There were {rows} rows selected and {columns} columns selected");
            }
        }

        if (GUILayout.Button("Clear Children"))
        {
            ClearChildren();
        }
    }

    private void ClearChildren()
    {
        foreach (var child in parent.GetComponentsInChildren<Transform>())
        {
            if (child != parent.transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }

    private void InstantiatePrefabs()
    {
        //we have an n x m grid, where n is rows and m is columns
        //Our default size for 1x1 will be a certain amount (say 10 units)
        //Then we scale the prefab to the 10 units
        //if it is 1x2, we make each square 10/2 units
        //therefore, we are scaling by baseSize/maxDivisions for prefab scale and spacing
        int maxDivs = rows > columns ? rows : columns;
        float scale = desiredSize/maxDivs;
        float incrementSize = 1f * scale;
        float posTrackX = -incrementSize*(columns-1)/2f, posTrackY = incrementSize*(rows-1)/2f, nodePosX = -incrementSize*columns/2f, nodePosY = incrementSize*rows/2;
        Vector2 parentPos = parent.transform.position;
        bool black = false;
        for (int i = 0; i < rows; i++)
        {
            //placing nodes for intersections
            for (int j = 0; j <= columns; j++)
            {
                Object node = Instantiate(UnityEngine.Resources.Load("Prefabs/intersectionPrefab"), new Vector3(parentPos.x + nodePosX,parentPos.y + nodePosY), Quaternion.identity, parent.transform);
                nodePosX += incrementSize;
            }
            
            //placing nodes for tiling squares
            for (int j = 0; j < columns; j++)
            {
                Object squareNode = Instantiate(UnityEngine.Resources.Load("Prefabs/TilePrefab"),
                    new Vector3(parentPos.x + posTrackX, parentPos.y + posTrackY), Quaternion.identity, parent.transform);
                squareNode.GameObject().transform.localScale = new Vector3(scale,scale);
                squareNode.GameObject().GetComponent<SpriteRenderer>().color = black ? Color.black : Color.white;
                black = !black;
                posTrackX += incrementSize;
            }

            if (columns % 2 == 0)
            {
                black = !black;
            }
            posTrackX = -incrementSize*(columns-1)/2f;
            nodePosX = -(incrementSize * columns) / 2f;
            posTrackY -= incrementSize;
            nodePosY -= incrementSize;
            
            //placing closing node intersections
            if (i == rows - 1)
            {
                for (int j = 0; j <= columns; j++)
                {
                    Object node = Instantiate(UnityEngine.Resources.Load("Prefabs/intersectionPrefab"), new Vector3(parentPos.x + nodePosX, parentPos.y + nodePosY), Quaternion.identity, parent.transform);
                    nodePosX += incrementSize;
                }
            }
        }
    }
}