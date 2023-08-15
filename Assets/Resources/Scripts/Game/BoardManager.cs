using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    //Serialized for debugging, nothing else
    [SerializeField] private float nodeDistance;
    [SerializeField] private float prefabScale;
    [SerializeField] private List<NodeValidator> allIntersections;

    public List<NodeValidator> AllIntersections
    {
        get => allIntersections;
        set => allIntersections = value;
    }

    public float NodeDistance
    {
        get => nodeDistance;
        set => nodeDistance = value;
    }

    public float PrefabScale
    {
        get => prefabScale;
        set => prefabScale = value;
    }

    private void Start()
    {
        Random.InitState(42);
    }

    public void EndLevel()
    {
        if (ValidateJunctions())
        {
            //todo victory implementation
            Debug.Log("YOU WIN!!!");
        }
        else
        {
            Debug.Log("Incorrect solution");
        }
    }

    private bool ValidateJunctions()
    {
        foreach (var node in GetComponentsInChildren<NodeValidator>())
        {
            if (!node.ValidateJunction()) return false;
        }

        return true;
    }
}
