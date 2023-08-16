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
    private void Awake()
    {
        if (done0 == true)
        {
            Level1Button.gameObject.SetActive(true);

            if (done01 == true)
            {
                Level2Button.gameObject.SetActive(true);

                if (done012 == true)
                {
                    Level3Button.gameObject.SetActive(true);

                    if (done0123 == true)
                    {
                        Level4Button.gameObject.SetActive(true);

                        if (done01234 == true)
                        {
                            Level5Button.gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}

