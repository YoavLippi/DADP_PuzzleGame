using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    //Serialized for debugging, nothing else
    [SerializeField] private float nodeDistance;
    [SerializeField] private float prefabScale;
    [SerializeField] private List<NodeValidator> allIntersections;
    [SerializeField] private List<TileHoldChecker> allTiles;
    [SerializeField] private int moveCount;
    [SerializeField] private TextMeshProUGUI moveCounter;

    public List<TileHoldChecker> AllTiles
    {
        get => allTiles;
        set => allTiles = value;
    }

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

    public void IncrementCounter()
    {
        moveCount++;
        moveCounter.text = $"Moves : {moveCount}";
    }

    public void EndLevel()
    {
        if (ValidateJunctions())
        {
            //todo victory implementation
            Debug.Log("YOU WIN!!!");
            Object victoryScreen = Instantiate(UnityEngine.Resources.Load("Prefabs/StarDisplay"), Vector3.zero,
                Quaternion.identity);
        }
        else
        {
            Debug.Log("Incorrect solution");
        }
    }

    public bool CheckAllOccupied()
    {
        foreach (var tile in allTiles)
        {
            if (!tile.occupied)
            {
                return false;
            }
        }

        return true;
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
