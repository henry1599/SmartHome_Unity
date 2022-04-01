using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HomeFooterTab : MonoBehaviour
{
    public TMP_Text title;
    public Image icon;
    public Color activeColor;
    public Color deactiveColor;

    public void SetStatus(bool active)
    {
        if (active == true)
        {
            title.color = activeColor;
            icon.color = activeColor;
        }
        else
        {
            title.color = deactiveColor;
            icon.color = deactiveColor;
        }
    }
}
