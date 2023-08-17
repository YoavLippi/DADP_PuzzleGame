using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


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

    [SerializeField] private TextMeshProUGUI txt;

    private int levelDoneFlag;

    private void Awake()
    {
        txt.text = "Enter";
        FlagHandler f = GameObject.FindWithTag("FlagHandler").GetComponent<FlagHandler>();
        if (this.gameObject.CompareTag("EditorOnly"))
        {
            done0 = f.clearFlags[0];
            done01 = f.clearFlags[1];
            done012 = f.clearFlags[2];
            done0123 = f.clearFlags[3];
            done01234 = f.clearFlags[4];

            Level1Button.gameObject.SetActive(false);
            Level2Button.gameObject.SetActive(false);
            Level4Button.gameObject.SetActive(false);
            Level3Button.gameObject.SetActive(false);
            Level5Button.gameObject.SetActive(false);

            if (done0)
            {
                Level1Button.gameObject.SetActive(true);

                if (done01)
                {
                    Level2Button.gameObject.SetActive(true);

                    if (done012)
                    {
                        Level3Button.gameObject.SetActive(true);

                        if (done0123)
                        {
                            Level4Button.gameObject.SetActive(true);

                            if (done01234)
                            {
                                Level5Button.gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
    }
    public int LevelDoneFlag
    {
        get => levelDoneFlag;
        set => levelDoneFlag = value;
    }

    public void ViewTutorialScene()
    {
        SceneManager.LoadScene("Scenes/0 - Tutorial");
    }
    public void ViewLevelOneScene()
    {
        SceneManager.LoadScene("Scenes/1 - Very Easy");
    }
    public void ViewLevelTwoScene()
    {
        SceneManager.LoadScene("Scenes/2 - Easy");
    }
    public void ViewLevelThreeScene()
    {
        SceneManager.LoadScene("Scenes/3 - Medium");
    }
    public void ViewLevelFourScene()
    {
        SceneManager.LoadScene("Scenes/4 - Hard");
    }
    public void ViewLevelFiveScene()
    {
        SceneManager.LoadScene("Scenes/5 - Expert");
    }
   
    
    public void ReturnToMenu()
    {
        if (GameObject.FindWithTag("FlagHandler").GetComponent<FlagHandler>())
        {
            FlagHandler f = GameObject.FindWithTag("FlagHandler").GetComponent<FlagHandler>();
            f.clearFlags[levelDoneFlag] = true;
        }
        SceneManager.LoadScene("Main Screen");
    }

    private bool IntToBool(int i)
    {
        //returns false if i=0, true otherwise
        return i != 0;
    }
}

