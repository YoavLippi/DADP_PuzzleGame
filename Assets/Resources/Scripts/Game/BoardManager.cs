using System.Collections;
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
    
    [Header("Important stuff")]
    [SerializeField] private int moveCount;
    [SerializeField] private TextMeshProUGUI moveCounter;
    [SerializeField] private int levelNum;
    
    [Header("Change these to the move amounts required for each")]
    [SerializeField] private int threeStars;
    [SerializeField] private int twoStars, oneStar;

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
            StartCoroutine(DoDelay());
        }
    }

    private IEnumerator DoDelay()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        GameObject victoryScreen = Instantiate((GameObject)UnityEngine.Resources.Load("Prefabs/StarDisplay"), Vector3.zero,
            Quaternion.identity);
        VictoryScoreController vc = victoryScreen.GetComponent<VictoryScoreController>();
        vc.OneStar = oneStar;
        vc.TwoStars = twoStars;
        vc.ThreeStars = threeStars;
        vc.ShowVictory(moveCount);
        victoryScreen.GetComponent<LoadShopScene>().LevelDoneFlag = levelNum;
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
