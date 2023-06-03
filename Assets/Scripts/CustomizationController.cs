using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationController : MonoBehaviour
{
    static GameObject[] options_check;
    static TMPro.TextMeshProUGUI[] stats_text;
    static AppController appController;
    static GameObject[] panels;
    static GameObject unlock;

    // Start is called before the first frame update
    void Start()
    {
        options_check = new GameObject[6];
        options_check[0] = GameObject.Find("Option 1 Check");
        options_check[1] = GameObject.Find("Option 2 Check");
        options_check[2] = GameObject.Find("Option 3 Check");
        options_check[3] = GameObject.Find("Option 4 Check");
        options_check[4] = GameObject.Find("Option 5 Check");
        options_check[5] = GameObject.Find("Option 6 Check");

        panels = new GameObject[6];
        panels[0] = GameObject.Find("Panel 1");
        panels[1] = GameObject.Find("Panel 2");
        panels[2] = GameObject.Find("Panel 3");
        panels[3] = GameObject.Find("Panel 4");
        panels[4] = GameObject.Find("Panel 5");
        panels[5] = GameObject.Find("Panel 6");

        unlock = GameObject.Find("Unlock");

        stats_text = new TMPro.TextMeshProUGUI[2];
        stats_text[0] = GameObject.Find("StatsTextLeft").GetComponent<TMPro.TextMeshProUGUI>();
        stats_text[1] = GameObject.Find("StatsTextRight").GetComponent<TMPro.TextMeshProUGUI>();

        appController = GameObject.Find("AppControllerObject").GetComponent<AppController>();

        stats_text[0].text = "Stardust: " + appController.Stardust;
        stats_text[1].text = "Knowledge: " + appController.Knowledge;

        closePanels();
        changeSprite(appController.selectedSprite);
        unlock.SetActive(false);
    }


    public void closePanels()
    {
        for (int i = 0; i < 6; i++)
        {
            panels[i].SetActive(false);
        }
        unlock.SetActive(false);
    }

    public void openPanels(int x)
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == x)
            {
                if (appController.unlock_status[i] && !unlock.activeSelf) panels[i].SetActive(true);
                else if (!appController.unlock_status[i]) show_unlock(i);
            }
        }
    }

    void show_unlock(int x)
    {
        string text;
        switch (x)
        {
            case 1:
                text = "Would you like to unlock Mars for 2000 Stardust points? You will need 4000 Knowledge points to be able to unlock it.";
                break;
            case 2:
                text = "Would you like to unlock Jupiter for 3500 Stardust points? You will need 8000 Knowledge points to be able to unlock it.";
                break;
            case 3:
                text = "Would you like to unlock Saturn for 5500 Stardust points? You will need 12000 Knowledge points to be able to unlock it.";
                break;
            case 4:
                text = "Would you like to unlock The Moon for 8000 Stardust points? You will need 16000 Knowledge points to be able to unlock it.";
                break;
            case 5:
                text = "Would you like to unlock The Sun for 12000 Stardust points? You will need 20000 Knowledge points to be able to unlock it.";
                break;
            default:
                text = "Unknown option.";
                   break;
        }
        unlock.SetActive(true);
        unlock.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = text;
    }

    public void unlockOption()
    {
        string text = unlock.GetComponentInChildren<TMPro.TextMeshProUGUI>().text;
        int points = 0, know_points = 0, x = 0;
        switch (text)
        {
            case "Would you like to unlock Mars for 2000 Stardust points? You will need 4000 Knowledge points to be able to unlock it.":
                points = 2000;
                know_points = 4000;
                x = 1;
                break;
            case "Would you like to unlock Jupiter for 3500 Stardust points? You will need 8000 Knowledge points to be able to unlock it.":
                points = 3500;
                know_points = 8000;
                x = 2;
                break;
            case "Would you like to unlock Saturn for 5500 Stardust points? You will need 12000 Knowledge points to be able to unlock it.":
                points = 5500;
                know_points = 12000;
                x = 3;
                break;
            case "Would you like to unlock The Moon for 8000 Stardust points? You will need 16000 Knowledge points to be able to unlock it.":
                points = 8000;
                know_points = 16000;
                x = 4;
                break;
            case "Would you like to unlock The Sun for 12000 Stardust points? You will need 20000 Knowledge points to be able to unlock it.":
                points = 12000;
                know_points = 20000;
                x = 5;
                break;
        }
        if (appController.Knowledge >= know_points && appController.Stardust >= points)
        {
            appController.Stardust -= points;
            stats_text[0].text = "Stardust: " + appController.Stardust;
            appController.unlock_status[x] = true;
            unlock.SetActive(false);
            panels[x].SetActive(true);
        }
        SaveGame();
    }

    public void changeSprite(int x)
    {
        appController.selectedSprite = x;

        for (int i = 0; i < 6; i++)
        {
            if (i == appController.selectedSprite) options_check[i].SetActive(true);
            else options_check[i].SetActive(false);
        }
        closePanels();
        SaveGame();
    }

    void SaveGame()
    {
        PlayerPrefs.SetInt("stardust", appController.Stardust);
        PlayerPrefs.SetInt("knowledge", appController.Knowledge);
        PlayerPrefs.SetInt("sprite", appController.selectedSprite);
        PlayerPrefs.SetInt("unlock_1", appController.unlock_status[0] ? 1 : 0);
        PlayerPrefs.SetInt("unlock_2", appController.unlock_status[1] ? 1 : 0);
        PlayerPrefs.SetInt("unlock_3", appController.unlock_status[2] ? 1 : 0);
        PlayerPrefs.SetInt("unlock_4", appController.unlock_status[3] ? 1 : 0);
        PlayerPrefs.SetInt("unlock_5", appController.unlock_status[4] ? 1 : 0);
        PlayerPrefs.SetInt("unlock_6", appController.unlock_status[5] ? 1 : 0);
    }
}
