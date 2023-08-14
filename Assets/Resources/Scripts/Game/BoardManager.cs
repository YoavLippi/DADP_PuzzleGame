using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    //Serialized for debugging, nothing else
    [SerializeField] private float nodeDistance;
    [SerializeField] private float prefabScale;

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
}
