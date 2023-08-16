using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeValidator : MonoBehaviour
{
    //Serialized for debugging, nothing else
    [SerializeField] private float nodeDistance;

    public float NodeDistance
    {
        get => nodeDistance;
        set => nodeDistance = value;
    }

    private void ValidateJunctions()
    {
        
    }
}
