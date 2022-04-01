using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HomeHeaderTab : MonoBehaviour
{
    public TMP_Text title;
    public Color activeColor;
    public Color deactiveColor;
    public GameObject line;
    public void SetStatus(bool active)
    {
        if (active == true)
        {
            title.color = activeColor;
            line.SetActive(true);
        }
        else
        {
            title.color = deactiveColor;
            line.SetActive(false);
        }
    }
}
