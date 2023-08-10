using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadShopScene : MonoBehaviour
{
    public void ViewTutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void ViewLevelOneScene()
    {
        SceneManager.LoadScene("Level 1");

    }
    public void ViewLevelTwoScene()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void ViewLevelThreeScene()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void ViewLevelFourScene()
    {
        SceneManager.LoadScene("Level 4");
    }
}

