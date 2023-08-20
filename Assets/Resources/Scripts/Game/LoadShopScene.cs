using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadShopScene : MonoBehaviour
{
    public bool done0;
    public bool done01;
    public bool done012;
    public bool done0123;
    public bool done01234;
    
    [SerializeField] private Button Level1Button;
    [SerializeField] private Button Level2Button;
    [SerializeField] private Button Level3Button;
    [SerializeField] private Button Level4Button;
    [SerializeField] private Button Level5Button;

    private FlagHandler f;

    private int levelDoneFlag;

    public int LevelDoneFlag
    {
        get => levelDoneFlag;
        set => levelDoneFlag = value;
    }
    private void Awake()
    {
        if (!f)
        {
            f = GameObject.FindWithTag("FlagHandler").GetComponent<FlagHandler>();
            DontDestroyOnLoad(f.gameObject);
        }
    }

    private void Start()
    {
        if (this.gameObject.CompareTag("GameController"))
        {
            done0 = f.clearFlags[0];
            done01 = f.clearFlags[1];
            done012 = f.clearFlags[2];
            done0123 = f.clearFlags[3];
            done01234 = f.clearFlags[4];
            
            Level1Button.gameObject.SetActive(done0);
            Level2Button.gameObject.SetActive(done01);
            Level3Button.gameObject.SetActive(done012);
            Level4Button.gameObject.SetActive(done0123);
            Level5Button.gameObject.SetActive(done01234);
        }
    }

    public void ReturnToMenu()
    {
        if (GameObject.FindWithTag("FlagHandler").GetComponent<FlagHandler>())
        {
            FlagHandler f = GameObject.FindWithTag("FlagHandler").GetComponent<FlagHandler>();
            if (f.clearFlags.Length < levelDoneFlag)
            {
                f.clearFlags[levelDoneFlag] = true;
            }
        }
        SceneManager.LoadScene("Main Screen");
    }
}

