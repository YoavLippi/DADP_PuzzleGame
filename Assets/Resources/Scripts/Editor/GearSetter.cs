using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GearSetter : EditorWindow
{
    private GameObject parent;

    private List<GearController> childGears;

    [MenuItem("Custom/Gear Setter")]
    public static void ShowWindow()
    {
        GetWindow<GearSetter>("Gear Setter");
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
        GUILayout.Label("Gear Setter Settings", EditorStyles.boldLabel);
        
        parent = EditorGUILayout.ObjectField("Parent Object", parent, typeof(GameObject), true) as GameObject;
        
        GUILayout.Space(10);

        if (GUILayout.Button("Randomise gears") && parent)
        {
            childGears = new List<GearController>();
            Randomise();
        }
    }

    private void Randomise()
    {
        foreach (var gear in parent.GetComponentsInChildren<GearController>())
        {
            childGears.Add(gear);
        }

        childGears = Shuffle(childGears);
        
        float incrementSize = GameObject.FindWithTag("BoardController").GetComponent<BoardManager>().NodeDistance;
        float xPos = parent.transform.position.x- 7.5f;
        float yPos = parent.transform.position.y + 4.2f;
        for (int i = 0; i<childGears.Count; i++)
        {
            if (yPos <= -4.2f)
            {
                yPos = parent.transform.position.y + 4.2f;
                xPos += incrementSize;
            }

            if (childGears[i].transform.GetComponent<GearMovement>().Movable)
            {
                childGears[i].transform.position = new Vector3(xPos, yPos);
                yPos -= incrementSize;
            }
        }
    }

    private List<GearController> Shuffle(List<GearController> inputList)
    {
        List<GearController> temp = new List<GearController>(inputList);
        int[] usedIndices = new int[inputList.Count];
        for (int i = 0; i < inputList.Count; i++)
        {
            bool randoFlag = false;
            int index = Random.Range(0, inputList.Count);
            while (!randoFlag)
            {
                index = Random.Range(0, inputList.Count);
                if (usedIndices[index] != 1)
                {
                    randoFlag = true;
                    usedIndices[index] = 1;
                }
            }

            temp[index] = inputList[i];
        }

        return temp;
    }
}
