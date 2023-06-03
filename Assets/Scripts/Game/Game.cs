using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Sprite options_1;
    public Sprite options_2;
    public Sprite options_3;
    public Sprite options_4;
    public Sprite options_5;
    public Sprite options_6;

    public GameObject game_end_panel;

    public bool are_balls_moving = false;

    public GameObject[] balls;
    public GameObject black_hole;

    AppController appController;
    GameObject mainBall;
    GameObject star;
    TMPro.TextMeshProUGUI stats;

    int condition_type;
    int condition;
    int condition_sprite;

    int earned_stardust = 0;
    int earned_knowledge = 0;
    int correct_balls = 0;

    float bh_scale = 0.5f;
    float bh_radius = 1f;

    bool show_end_panel = false;

    void Start()
    {
        mainBall = GameObject.Find("Main Ball");
        appController = GameObject.Find("AppControllerObject").GetComponent<AppController>();
        stats = GameObject.Find("StatsText").GetComponent<TMPro.TextMeshProUGUI>();

        game_end_panel.SetActive(false);

        switch (appController.selectedSprite)
        {
            case 0:
                mainBall.GetComponent<SpriteRenderer>().sprite = options_1;
                mainBall.GetComponent<Rigidbody2D>().mass = 5;
                GameObject.Find("UI").GetComponent<DragNShoot>().power = 7;
                break;
            case 1:
                mainBall.GetComponent<SpriteRenderer>().sprite = options_2;
                mainBall.GetComponent<Rigidbody2D>().mass = 3;
                GameObject.Find("UI").GetComponent<DragNShoot>().power = 5;
                break;
            case 2:
                mainBall.GetComponent<SpriteRenderer>().sprite = options_3;
                mainBall.GetComponent<Rigidbody2D>().mass = 13;
                GameObject.Find("UI").GetComponent<DragNShoot>().power = 15;
                break;
            case 3:
                mainBall.GetComponent<SpriteRenderer>().sprite = options_4;
                mainBall.GetComponent<Rigidbody2D>().mass = 10;
                GameObject.Find("UI").GetComponent<DragNShoot>().power = 12;
                break;
            case 4:
                mainBall.GetComponent<SpriteRenderer>().sprite = options_5;
                mainBall.GetComponent<Rigidbody2D>().mass = 1;
                GameObject.Find("UI").GetComponent<DragNShoot>().power = 2;
                break;
            case 5:
                mainBall.GetComponent<SpriteRenderer>().sprite = options_6;
                mainBall.GetComponent<Rigidbody2D>().mass = 20;
                GameObject.Find("UI").GetComponent<DragNShoot>().power = 24;
                break;
        }

        balls = new GameObject[10];
        balls[0] = GameObject.Find("Ball 1");
        balls[1] = GameObject.Find("Ball 2");
        balls[2] = GameObject.Find("Ball 3");
        balls[3] = GameObject.Find("Ball 4");
        balls[4] = GameObject.Find("Ball 5");
        balls[5] = GameObject.Find("Ball 6");
        balls[6] = GameObject.Find("Ball 7");
        balls[7] = GameObject.Find("Ball 8");
        balls[8] = GameObject.Find("Ball 9");
        balls[9] = GameObject.Find("Ball 10");

        int random;
        for (int i = 0; i < 10; i++)
        {
            random = Random.Range(0, 100) % 5;
            switch (random)
            {
                case 0:
                    balls[i].GetComponent<SpriteRenderer>().sprite = options_1;
                    balls[i].GetComponent<Rigidbody2D>().mass = 5;
                    break;
                case 1:
                    balls[i].GetComponent<SpriteRenderer>().sprite = options_2;
                    balls[i].GetComponent<Rigidbody2D>().mass = 3;
                    break;
                case 2:
                    balls[i].GetComponent<SpriteRenderer>().sprite = options_3;
                    balls[i].GetComponent<Rigidbody2D>().mass = 13;
                    break;
                case 3:
                    balls[i].GetComponent<SpriteRenderer>().sprite = options_4;
                    balls[i].GetComponent<Rigidbody2D>().mass = 10;
                    break;
                case 4:
                    balls[i].GetComponent<SpriteRenderer>().sprite = options_5;
                    balls[i].GetComponent<Rigidbody2D>().mass = 1;
                    break;
                /*case 5:
                    balls[i].GetComponent<SpriteRenderer>().sprite = options_6;
                    break;*/
            }
            balls[i].GetComponent<Gravity>().AttractionRadius = bh_radius;
        }
        random = Random.Range(0, 100) % 10;
        balls[random].GetComponent<SpriteRenderer>().sprite = options_6;
        //balls[random].GetComponent<Gravity>().AttractionRadius = bh_radius;
        star = balls[random];
        star.GetComponent<Rigidbody2D>().mass = 20;

        mainBall.GetComponent<Gravity>().AttractionRadius = bh_radius;
        black_hole.transform.localScale = new Vector3(bh_scale, bh_scale, 0);
    }

    void Update()
    {
        if ((mainBall.activeSelf && mainBall.GetComponent<Rigidbody2D>().velocity.magnitude > 0.0001) || are_balls_moving)
        {
            var temp = true;
            for (int i = 0; i < 10; i++)
            {
                if (balls[i].GetComponent<Rigidbody2D>().velocity.magnitude < 0.0001) temp = false;
                else { temp = true; break; }
            }
            are_balls_moving = temp;
        }
        if (show_end_panel) ShowGameEndInfo();

        if (bh_scale > black_hole.transform.localScale.x)
        {
            if (star.activeSelf) black_hole.transform.localScale = new Vector3(black_hole.transform.localScale.x + 0.001f, black_hole.transform.localScale.y + 0.001f, 0);
            else
            {
                black_hole.transform.localScale = new Vector3(black_hole.transform.localScale.x + 0.01f, black_hole.transform.localScale.y + 0.01f, 0);
                for (int i = 0; i < 10; i++)
                {
                    balls[i].GetComponent<Gravity>().AttractionRadius = balls[i].GetComponent<Gravity>().AttractionRadius + 0.5f;
                    balls[i].GetComponent<Gravity>().StrengthOfAttraction = balls[i].GetComponent<Gravity>().StrengthOfAttraction + 0.5f;
                }
                mainBall.GetComponent<Gravity>().AttractionRadius = mainBall.GetComponent<Gravity>().AttractionRadius + 0.5f;
                mainBall.GetComponent<Gravity>().StrengthOfAttraction = mainBall.GetComponent<Gravity>().StrengthOfAttraction + 0.5f;
            }
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ShowGameEndInfo()
    {
        if (!show_end_panel) show_end_panel = true;
        else if (!are_balls_moving && !game_end_panel.activeSelf)
        {
            var temp = "";
            if (GameObject.Find("Black Hole").GetComponent<BlackHoleCollision>().remaining_balls == 0 && star.activeSelf) temp = "Congratulations!\n\n";
            else temp = "Game Over\n\n";
            game_end_panel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = temp + "You managed to feed the Black Hole " + (9 - GameObject.Find("Black Hole").GetComponent<BlackHoleCollision>().remaining_balls) + " celestial objects with " + correct_balls + " of them being the correct object.\n\nYou have earned:\n" + earned_stardust + " Stardust points\n" + earned_knowledge + " Knowledge points\n\nYou can take a test for bonus points or play the game again.";
            appController.Stardust += earned_stardust;
            appController.Knowledge += earned_knowledge;
            game_end_panel.SetActive(true);
            SaveGame();
        }
    }

    public void NextBallCondition(int remaining_balls)
    {
        if (remaining_balls == 0)
        {
            stats.text = stats.text.Remove(stats.text.Length - 1) + "0";
            ShowGameEndInfo();
            return;
        }
        int random_ball = Random.Range(0, 1000) % 10;
        while (!balls[random_ball].activeSelf || balls[random_ball].name == star.name) random_ball = Random.Range(0, 1000) % 10;
        int random_condition = Random.Range(0, 100) % 5;
        switch (balls[random_ball].GetComponent<SpriteRenderer>().sprite.name)
        {
            case "earth_ball":
                condition_sprite = 0;
                break;
            case "mars_ball":
                condition_sprite = 1;
                break;
            case "jupiter_ball":
                condition_sprite = 2;
                break;
            case "saturn_ball":
                condition_sprite = 3;
                break;
            case "moon_ball":
                condition_sprite = 4;
                break;
            /*case "sun_ball":
                condition_sprite = 5;
                break;*/
        }
        switch (random_condition)
        {
            case 0:     // Ball Name
                condition_type = 0;
                condition = -1;
                stats.text = "Feed the Black Hole the " + appController.GetComponent<BallCharacteristics>().ball_name[condition_sprite] + " celestial object.\nRemaining objects: " + remaining_balls;
                break;
            case 1:     // Ball Type
                condition_type = 1;
                condition = -1;
                stats.text = "Feed the Black Hole a celestial object of type " + appController.GetComponent<BallCharacteristics>().ball_type[condition_sprite] + ".\nRemaining objects: " + remaining_balls;
                break;
            case 2:     // Ball Size
                condition_type = 2;
                condition = Random.Range(0, 1000) % 3;
                switch (condition)
                {
                    case 0:
                        stats.text = "Feed the Black Hole a celestial object that has a diameter smaller than " + appController.GetComponent<BallCharacteristics>().ball_size[condition_sprite] * 1.2 + " km.\nRemaining objects: " + remaining_balls;
                        break;
                    case 1:
                        stats.text = "Feed the Black Hole a celestial object that has a diameter of " + appController.GetComponent<BallCharacteristics>().ball_size[condition_sprite] + " km.\nRemaining objects: " + remaining_balls;
                        break;
                    case 2:
                        stats.text = "Feed the Black Hole a celestial object that has a diameter larger than " + appController.GetComponent<BallCharacteristics>().ball_size[condition_sprite] * 0.8 + " km.\nRemaining objects: " + remaining_balls;
                        break;
                }
                break;
            case 3:     // Ball Temp
                condition_type = 3;
                condition = Random.Range(0, 1000) % 3;
                switch (condition)
                {
                    case 0:
                        stats.text = "Feed the Black Hole a celestial object that has a temperature cooler than " + System.Math.Round(appController.GetComponent<BallCharacteristics>().ball_temp[condition_sprite] * 1.4, 2) + " degrees Celsius.\nRemaining objects: " + remaining_balls;
                        break;
                    case 1:
                        stats.text = "Feed the Black Hole a celestial object that has a temperature of " + System.Math.Round(appController.GetComponent<BallCharacteristics>().ball_temp[condition_sprite], 2) + " degrees Celsius.\nRemaining objects: " + remaining_balls;
                        break;
                    case 2:
                        stats.text = "Feed the Black Hole a celestial object that has a temperature hotter than " + System.Math.Round(appController.GetComponent<BallCharacteristics>().ball_temp[condition_sprite] * 0.6, 2) + " degrees Celsius.\nRemaining objects: " + remaining_balls;
                        break;
                }
                break;
            case 4:     // Ball Mass
                condition_type = 4;
                condition = Random.Range(0, 1000) % 3;
                switch (condition)
                {
                    case 0:
                        stats.text = "Feed the Black Hole a celestial object that has a mass smaller than " + System.Math.Round(appController.GetComponent<BallCharacteristics>().ball_mass[condition_sprite] * 1.4, 2) + " Earths.\nRemaining objects: " + remaining_balls;
                        break;
                    case 1:
                        stats.text = "Feed the Black Hole a celestial object that has a mass of " + System.Math.Round(appController.GetComponent<BallCharacteristics>().ball_mass[condition_sprite], 2) + " Earths.\nRemaining objects: " + remaining_balls;
                        break;
                    case 2:
                        stats.text = "Feed the Black Hole a celestial object that has a mass larger than " + System.Math.Round(appController.GetComponent<BallCharacteristics>().ball_mass[condition_sprite] * 0.6, 2) + " Earths.\nRemaining objects: " + remaining_balls;
                        break;
                }
                break;
        }
    }

    public void CheckCondition(Collision2D collision, int remaining_balls)
    {
        if (collision.gameObject.name == star.name)
        {
            bh_scale += 20;
            star.SetActive(false);
        }
        if (!star.activeSelf) return;
        //for (int i = 0; i < 10; i++) if (collision.gameObject.name == balls[i].name) { balls[i].SetActive(false); break; }
        int temp;
        switch (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name)
        {
            case "earth_ball":
                temp = 0;
                break;
            case "mars_ball":
                temp = 1;
                break;
            case "jupiter_ball":
                temp = 2;
                break;
            case "saturn_ball":
                temp = 3;
                break;
            case "moon_ball":
                temp = 4;
                break;
            /*case "sun_ball":
                temp = 5;
                break;*/
            default:
                temp = 0;
                break;
        }
        switch (condition_type)
        {
            case 0:     // Ball Name
                if (temp == condition_sprite)
                {
                    earned_stardust += 90;
                    earned_knowledge += 5;
                    correct_balls++;
                    NextBallCondition(remaining_balls);
                }
                else
                {
                    if (remaining_balls < 9) stats.text = stats.text.Remove(stats.text.Length - 1) + remaining_balls;
                    else stats.text = stats.text.Remove(stats.text.Length - 2) + remaining_balls;
                }
                break;
            case 1:     // Ball Type
                if (appController.GetComponent<BallCharacteristics>().ball_type[temp] == appController.GetComponent<BallCharacteristics>().ball_type[condition_sprite])
                {
                    earned_stardust += 120;
                    earned_knowledge += 10;
                    correct_balls++;
                    NextBallCondition(remaining_balls);
                }
                else
                {
                    if (remaining_balls < 9) stats.text = stats.text.Remove(stats.text.Length - 1) + remaining_balls;
                    else stats.text = stats.text.Remove(stats.text.Length - 2) + remaining_balls;
                }
                break;
            case 2:     // Ball Size
                switch (condition)
                {
                    case 0:
                        if (appController.GetComponent<BallCharacteristics>().ball_size[temp] < appController.GetComponent<BallCharacteristics>().ball_size[condition_sprite] * 1.2)
                        {
                            earned_stardust += 150;
                            earned_knowledge += 15;
                            correct_balls++;
                            NextBallCondition(remaining_balls);
                        }
                        else
                        {
                            if (remaining_balls < 9) stats.text = stats.text.Remove(stats.text.Length - 1) + remaining_balls;
                            else stats.text = stats.text.Remove(stats.text.Length - 2) + remaining_balls;
                        }
                        break;
                    case 1:
                        if (appController.GetComponent<BallCharacteristics>().ball_size[temp] == appController.GetComponent<BallCharacteristics>().ball_size[condition_sprite])
                        {
                            earned_stardust += 150;
                            earned_knowledge += 15;
                            correct_balls++;
                            NextBallCondition(remaining_balls);
                        }
                        else
                        {
                            if (remaining_balls < 9) stats.text = stats.text.Remove(stats.text.Length - 1) + remaining_balls;
                            else stats.text = stats.text.Remove(stats.text.Length - 2) + remaining_balls;
                        }
                        break;
                    case 2:
                        if (appController.GetComponent<BallCharacteristics>().ball_size[temp] > appController.GetComponent<BallCharacteristics>().ball_size[condition_sprite] * 0.8)
                        {
                            earned_stardust += 150;
                            earned_knowledge += 15;
                            correct_balls++;
                            NextBallCondition(remaining_balls);
                        }
                        else
                        {
                            if (remaining_balls < 9) stats.text = stats.text.Remove(stats.text.Length - 1) + remaining_balls;
                            else stats.text = stats.text.Remove(stats.text.Length - 2) + remaining_balls;
                        }
                        break;
                }
                break;
            case 3:     // Ball Temp
                switch (condition)
                {
                    case 0:
                        if (appController.GetComponent<BallCharacteristics>().ball_temp[temp] < appController.GetComponent<BallCharacteristics>().ball_temp[condition_sprite] * 1.2)
                        {
                            earned_stardust += 170;
                            earned_knowledge += 20;
                            correct_balls++;
                            NextBallCondition(remaining_balls);
                        }
                        else
                        {
                            if (remaining_balls < 9) stats.text = stats.text.Remove(stats.text.Length - 1) + remaining_balls;
                            else stats.text = stats.text.Remove(stats.text.Length - 2) + remaining_balls;
                        }
                        break;
                    case 1:
                        if (appController.GetComponent<BallCharacteristics>().ball_temp[temp] == appController.GetComponent<BallCharacteristics>().ball_temp[condition_sprite])
                        {
                            earned_stardust += 170;
                            earned_knowledge += 20;
                            correct_balls++;
                            NextBallCondition(remaining_balls);
                        }
                        else
                        {
                            if (remaining_balls < 9) stats.text = stats.text.Remove(stats.text.Length - 1) + remaining_balls;
                            else stats.text = stats.text.Remove(stats.text.Length - 2) + remaining_balls;
                        }
                        break;
                    case 2:
                        if (appController.GetComponent<BallCharacteristics>().ball_temp[temp] > appController.GetComponent<BallCharacteristics>().ball_temp[condition_sprite] * 0.8)
                        {
                            earned_stardust += 170;
                            earned_knowledge += 20;
                            correct_balls++;
                            NextBallCondition(remaining_balls);
                        }
                        else
                        {
                            if (remaining_balls < 9) stats.text = stats.text.Remove(stats.text.Length - 1) + remaining_balls;
                            else stats.text = stats.text.Remove(stats.text.Length - 2) + remaining_balls;
                        }
                        break;
                }
                break;
            case 4:     // Ball Mass
                switch (condition)
                {
                    case 0:
                        if (appController.GetComponent<BallCharacteristics>().ball_mass[temp] < appController.GetComponent<BallCharacteristics>().ball_mass[condition_sprite] * 1.2)
                        {
                            earned_stardust += 200;
                            earned_knowledge += 25;
                            correct_balls++;
                            NextBallCondition(remaining_balls);
                        }
                        else
                        {
                            if (remaining_balls < 9) stats.text = stats.text.Remove(stats.text.Length - 1) + remaining_balls;
                            else stats.text = stats.text.Remove(stats.text.Length - 2) + remaining_balls;
                        }
                        break;
                    case 1:
                        if (appController.GetComponent<BallCharacteristics>().ball_mass[temp] == appController.GetComponent<BallCharacteristics>().ball_mass[condition_sprite])
                        {
                            earned_stardust += 200;
                            earned_knowledge += 25;
                            correct_balls++;
                            NextBallCondition(remaining_balls);
                        }
                        else
                        {
                            if (remaining_balls < 9) stats.text = stats.text.Remove(stats.text.Length - 1) + remaining_balls;
                            else stats.text = stats.text.Remove(stats.text.Length - 2) + remaining_balls;
                        }
                        break;
                    case 2:
                        if (appController.GetComponent<BallCharacteristics>().ball_mass[temp] > appController.GetComponent<BallCharacteristics>().ball_mass[condition_sprite] * 0.8)
                        {
                            earned_stardust += 200;
                            earned_knowledge += 25;
                            correct_balls++;
                            NextBallCondition(remaining_balls);
                        }
                        else
                        {
                            if (remaining_balls < 9) stats.text = stats.text.Remove(stats.text.Length - 1) + remaining_balls;
                            else stats.text = stats.text.Remove(stats.text.Length - 2) + remaining_balls;
                        }
                        break;
                }
                break;
            default:
                stats.text = "Unknown condition\nRemaining objects: " + remaining_balls;
                break;
        }

        bh_radius += 0.44f;
        bh_scale += 0.16f;

        for (int i = 0; i < 10; i++) balls[i].GetComponent<Gravity>().AttractionRadius = bh_radius;

        mainBall.GetComponent<Gravity>().AttractionRadius = bh_radius;
    }

    public void TakeBonusTest()
    {
        appController.test_bonus = true;
        appController.test_bonus_amount = correct_balls;
        SceneManager.LoadScene("TestScene");
    }
    void SaveGame()
    {
        PlayerPrefs.SetInt("stardust", appController.Stardust);
        PlayerPrefs.SetInt("knowledge", appController.Knowledge);
    }

}
