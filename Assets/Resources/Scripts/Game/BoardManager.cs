using System.Collections.Generic;
using Resources.Scripts.Game;
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
    [SerializeField] private int levelNum;

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
            GameObject victoryScreen = Instantiate((GameObject)UnityEngine.Resources.Load("Prefabs/StarDisplay"), Vector3.zero,
                Quaternion.identity);
            victoryScreen.GetComponent<VictoryScoreController>().ShowVictory(moveCount);
            victoryScreen.GetComponent<LoadShopScene>().LevelDoneFlag = levelNum;
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
