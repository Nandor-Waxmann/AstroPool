using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCollision : MonoBehaviour
{


    public int remaining_balls = 9;


    private void Start()
    {
        GameObject.Find("UI").GetComponent<Game>().NextBallCondition(remaining_balls);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Main Ball")
        {
            remaining_balls--;
            collision.gameObject.SetActive(false);
            GameObject.Find("UI").GetComponent<Game>().CheckCondition(collision, remaining_balls);
        }
        else
        {
            collision.gameObject.SetActive(false);
            GameObject.Find("UI").GetComponent<Game>().ShowGameEndInfo();
        }
    }
}
