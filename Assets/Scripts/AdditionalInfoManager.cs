using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalInfoManager : MonoBehaviour
{
    public string[] nasa_links;
    public string[] esa_links;
    public void openLink(int x)
    {
        if (x < 0) Application.OpenURL(esa_links[(x + 1) * (-1)]);
        else Application.OpenURL(nasa_links[x - 1]);
    }
}
