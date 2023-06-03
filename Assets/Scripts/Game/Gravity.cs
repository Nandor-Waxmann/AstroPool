using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float StrengthOfAttraction;
    public float AttractionRadius;
    public GameObject hole;

    void FixedUpdate()
    {
        float magsqr;
        Vector3 offset;

        offset = hole.transform.position - transform.position;
        offset.z = 0;

        magsqr = offset.sqrMagnitude;

        if (magsqr > 0.0001f && magsqr < AttractionRadius)
        {
            GetComponent<Rigidbody2D>().AddForce((StrengthOfAttraction * offset.normalized / magsqr) * GetComponent<Rigidbody2D>().mass);
        }
    }
}
