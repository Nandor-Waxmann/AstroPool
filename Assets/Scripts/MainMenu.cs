using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public string secondLevel;
    public string thirdLevel;
    public string fourthLevel;
    public string fifthLevel;

    public void takeTest()
    {
        if (firstLevel == "MainMenuScene")
        {
            GameObject.Find("AppControllerObject").GetComponent<AppController>().test_bonus = false;
            GameObject.Find("AppControllerObject").GetComponent<AppController>().test_bonus_amount = 0;
        }
        SceneManager.LoadScene(firstLevel);
    }

    public void playGame()
    {
        SceneManager.LoadScene(secondLevel);
    }

    public void openCustomization()
    {
        SceneManager.LoadScene(thirdLevel);
    }

    public void openSimulation()
    {
        SceneManager.LoadScene(fourthLevel);
    }

    public void openInfo()
    {
        SceneManager.LoadScene(fifthLevel);
    }
}
