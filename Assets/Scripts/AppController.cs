using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    public int Stardust = 0;
    public int Knowledge = 0;
    public int selectedSprite = 0;
    public bool[] unlock_status = { true, false, false, false, false, false };
    public bool test_bonus = false;
    public int test_bonus_amount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("stardust"))
        {
            Stardust = PlayerPrefs.GetInt("stardust");
            Knowledge = PlayerPrefs.GetInt("knowledge");
        }
        if (PlayerPrefs.HasKey("sprite"))
        {
            selectedSprite = PlayerPrefs.GetInt("sprite");
            unlock_status[0] = PlayerPrefs.GetInt("unlock_1") != 0;
            unlock_status[1] = PlayerPrefs.GetInt("unlock_2") != 0;
            unlock_status[2] = PlayerPrefs.GetInt("unlock_3") != 0;
            unlock_status[3] = PlayerPrefs.GetInt("unlock_4") != 0;
            unlock_status[4] = PlayerPrefs.GetInt("unlock_5") != 0;
            unlock_status[5] = PlayerPrefs.GetInt("unlock_6") != 0;
        }
        DontDestroyOnLoad(GameObject.Find("AppControllerObject"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
