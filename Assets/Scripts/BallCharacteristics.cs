using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCharacteristics : MonoBehaviour
{
    public enum Type { Rocky, GasGiant, Moon, Star, Liquid };

    public string[] ball_name;
    public Type[] ball_type;
    public int[] ball_size;
    public float[] ball_temp;
    public float[] ball_mass;
    public bool[] has_liquid_water;
}
