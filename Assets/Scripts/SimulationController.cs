using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    int sim = 0;

    public TMPro.TextMeshProUGUI scroll;
    public float scroll_speed = 5f;

    public string[] sim_names;
    public TMPro.TextMeshProUGUI title;
    public GameObject sun_sim_1;
    public GameObject mercury_sim_1;
    public GameObject venus_sim_1;
    public GameObject earth_sim_1;
    public GameObject mars_sim_1;
    public GameObject jupiter_sim_1;
    public GameObject saturn_sim_1;
    public GameObject uranus_sim_1;
    public GameObject neptune_sim_1;
    public GameObject earth_sim_2;
    public GameObject moon_sim_2;
    public GameObject saturn_sim_3;
    public GameObject titan_sim_3;

    GameObject[] sim_anim;
    RectTransform scroll_transform;
    Vector3 startPos;
    float width;
    float scrollPos = 0;

    void Start()
    {
        scroll_transform = scroll.GetComponent<RectTransform>();
        startPos = scroll_transform.position;
        width = scroll.preferredWidth / startPos.magnitude;

        sim_anim = new GameObject[3];
        sim_anim[0] = GameObject.Find("Simulation 1");
        sim_anim[1] = GameObject.Find("Simulation 2");
        sim_anim[2] = GameObject.Find("Simulation 3");

        for (int i = 1; i < sim_anim.Length; i++)
        {
            sim_anim[i].SetActive(false);
        }
        title.text = sim_names[0];
    }

    private void Update()
    {
        if (sim == 0)
        {
            sun_sim_1.transform.Rotate(0f, 0f, -1.3f);
            mercury_sim_1.transform.Rotate(0f, 0f, -1.2f);
            venus_sim_1.transform.Rotate(0f, 0f, -1f);
            earth_sim_1.transform.Rotate(0f, 0f, -0.8f);
            mars_sim_1.transform.Rotate(0f, 0f, -0.6f);
            jupiter_sim_1.transform.Rotate(0f, 0f, -0.4f);
            saturn_sim_1.transform.Rotate(0f, 0f, -0.35f);
            uranus_sim_1.transform.Rotate(0f, 0f, -0.25f);
            neptune_sim_1.transform.Rotate(0f, 0f, -0.12f);
        }
        else if (sim == 1)
        {
            earth_sim_2.transform.Rotate(0f, 0f, -1f);
            moon_sim_2.transform.Rotate(0f, 0f, -0.6f);
        }
        else if (sim == 2)
        {
            saturn_sim_3.transform.Rotate(0f, 0f, -1f);
            titan_sim_3.transform.Rotate(0f, 0f, -0.6f);
        }

        scroll_transform.position = new Vector3(-scrollPos % width, startPos.y, startPos.z);
        scrollPos += scroll_speed * 5 * Time.deltaTime;
    }

    public void ChangeSim(bool dir)
    {
        if (dir)
        {
            if (sim < sim_anim.Length - 1)
            {
                sim_anim[sim + 1].SetActive(true);
                sim_anim[sim].SetActive(false);
                title.text = sim_names[sim + 1];
                sim++;
            }
        }
        else
        {
            if (sim > 0)
            {
                sim_anim[sim - 1].SetActive(true);
                sim_anim[sim].SetActive(false);
                title.text = sim_names[sim - 1];
                sim--;
            }
        }
    }
}
