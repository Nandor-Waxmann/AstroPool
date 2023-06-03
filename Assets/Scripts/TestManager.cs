using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    public string[] earth_questions;
    public string[] earth_answers;
    public string[] mars_questions;
    public string[] mars_answers;
    public string[] jupiter_questions;
    public string[] jupiter_answers;
    public string[] saturn_questions;
    public string[] saturn_answers;
    public string[] moon_questions;
    public string[] moon_answers;
    public string[] sun_questions;
    public string[] sun_answers;

    string[] questions = { };
    string[] answers = { };

    static TMPro.TextMeshProUGUI new_question;
    static TMPro.TextMeshProUGUI[] answers_text;
    static TMPro.TextMeshProUGUI stats_text_left;
    static TMPro.TextMeshProUGUI stats_text_right;
    AppController appController;

    TMPro.TextMeshProUGUI selected_option;

    List<int> used_questions = new List<int>();

    int rnd = -1;
    int correct_answers = 0;
    int earned_stardust = 0;
    int earned_knowledge = 0;

    public GameObject test_end_panel;
    public GameObject next_question;
    public GameObject finnish_test;

    // Start is called before the first frame update
    void Start()
    {
        questions = questions.Concat(earth_questions).ToArray();
        questions = questions.Concat(mars_questions).ToArray();
        questions = questions.Concat(jupiter_questions).ToArray();
        questions = questions.Concat(saturn_questions).ToArray();
        questions = questions.Concat(moon_questions).ToArray();
        questions = questions.Concat(sun_questions).ToArray();

        answers = answers.Concat(earth_answers).ToArray();
        answers = answers.Concat(mars_answers).ToArray();
        answers = answers.Concat(jupiter_answers).ToArray();
        answers = answers.Concat(saturn_answers).ToArray();
        answers = answers.Concat(moon_answers).ToArray();
        answers = answers.Concat(sun_answers).ToArray();

        appController = GameObject.Find("AppControllerObject").GetComponent<AppController>();
        new_question = GameObject.Find("QuestionText").GetComponent<TMPro.TextMeshProUGUI>();
        answers_text = new TMPro.TextMeshProUGUI[4];
        answers_text[0] = GameObject.Find("Option 1 Text").GetComponent<TMPro.TextMeshProUGUI>();
        answers_text[1] = GameObject.Find("Option 2 Text").GetComponent<TMPro.TextMeshProUGUI>();
        answers_text[2] = GameObject.Find("Option 3 Text").GetComponent<TMPro.TextMeshProUGUI>();
        answers_text[3] = GameObject.Find("Option 4 Text").GetComponent<TMPro.TextMeshProUGUI>();
        stats_text_left = GameObject.Find("StatsTextLeft").GetComponent<TMPro.TextMeshProUGUI>();
        stats_text_right = GameObject.Find("StatsTextRight").GetComponent<TMPro.TextMeshProUGUI>();
        stats_text_left.text = "10 questions remaining.";
        stats_text_right.text = "Bonus: " + appController.test_bonus;

        test_end_panel.SetActive(false);

        selected_option = next_question.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        NextQuestion(next_question.GetComponentInChildren<TMPro.TextMeshProUGUI>());
    }

    public void NextQuestion(TMPro.TextMeshProUGUI pressed_button)
    {
        if ((pressed_button.name != "NQ" && pressed_button.name != "FT") && (!next_question.activeSelf && !finnish_test.activeSelf))
        {
            selected_option = pressed_button;
            if (pressed_button.text == answers[rnd * 4])
            {
                pressed_button.color = new Color32(40, 100, 25, 255);
            }
            else pressed_button.color = new Color32(255, 0, 0, 255);
            if (used_questions.Count > 8) finnish_test.SetActive(true);
            else next_question.SetActive(true);
            return;
        }
        else if ((pressed_button.name != "NQ" && pressed_button.name != "FT") && (next_question.activeSelf || finnish_test.activeSelf)) return;
        if (test_end_panel.activeSelf) return;
        if (used_questions.Count > 8)
        {
            if (selected_option.text == answers[rnd * 4])
            {
                correct_answers++;
            }
            stats_text_left.text = "Test completed.";
            test_end_panel.SetActive(true);
            var temp1 = "";
            var temp2 = "";
            if (appController.test_bonus)
            {
                temp1 = "You have an active bonus of " + appController.test_bonus_amount * 5 + "%\n\n";
                temp2 = earned_stardust + " + " + (int)earned_stardust * ((float)(appController.test_bonus_amount * 5) / 100) + " Stardust points.\n" + earned_knowledge + " + " + (int)earned_knowledge * ((float)(appController.test_bonus_amount * 5) / 100) + " Knowledge points.\n";
            }
            else
            {
                temp1 = "You don't have an active bonus.\n\n";
                temp2 = earned_stardust + " Stardust points.\n" + earned_knowledge + " Knowledge points.\n";
            }
            test_end_panel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Test completed.\nYou have answered " + correct_answers + " questions correctly.\n\n" + temp1 + "You have earned:\n" + temp2;
            if (correct_answers < 6)
            {
                test_end_panel.GetComponentInChildren<TMPro.TextMeshProUGUI>().text += "\nUnfortunately you have answered less than 60% of questions correctly and your rewards have been lost in space.";
            }
            else
            {
                appController.Stardust += earned_stardust + (int)(earned_stardust * ((float)(appController.test_bonus_amount * 5) / 100));
                appController.Knowledge += earned_knowledge + (int)(earned_knowledge * ((float)(appController.test_bonus_amount * 5) / 100));
            }
            SaveGame();
            return;
        }
        else if ((rnd != -1) && (selected_option.text == answers[rnd * 4]))
        {
            correct_answers++;

            switch (rnd % 5)
            {
                case 0:
                    earned_stardust += 5;
                    earned_knowledge += 30;
                    break;
                case 1:
                    earned_stardust += 10;
                    earned_knowledge += 60;
                    break;
                case 2:
                    earned_stardust += 15;
                    earned_knowledge += 90;
                    break;
                case 3:
                    earned_stardust += 20;
                    earned_knowledge += 120;
                    break;
                case 4:
                    earned_stardust += 25;
                    earned_knowledge += 150;
                    break;
            }
        }

        if (rnd != -1)
        {
            used_questions.Add(rnd);
            stats_text_left.text = (10 - used_questions.Count) + " questions remaining.";
        }

        rnd = Random.Range(0, 1000) % 30;

        while (used_questions.Contains(rnd))
        {
            rnd = Random.Range(0, 1000) % 30;
        }

        System.Random rnd_temp = new System.Random();
        int[] temp = Enumerable.Range(0, 4).OrderBy(c => rnd_temp.Next()).ToArray();
        new_question.text = questions[rnd];
        answers_text[temp[0]].text = answers[rnd * 4];
        answers_text[temp[1]].text = answers[rnd * 4 + 1];
        answers_text[temp[2]].text = answers[rnd * 4 + 2];
        answers_text[temp[3]].text = answers[rnd * 4 + 3];

        next_question.SetActive(false);
        selected_option.color = new Color(0, 0, 0);
    }
    public void ResetTest()
    {
        appController.test_bonus = false;
        appController.test_bonus_amount = 0;
        SceneManager.LoadScene("TestScene");
    }

    void SaveGame()
    {
        PlayerPrefs.SetInt("stardust", appController.Stardust);
        PlayerPrefs.SetInt("knowledge", appController.Knowledge);
    }
}
