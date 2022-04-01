using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomTabButton : MonoBehaviour
{
    public Color activeColor;
    public Color deactiveColor;
    public TMP_Text text;
    public Image icon;
    public void SetStatus(bool active)
    {
        if (active == true)
        {
            text.color = activeColor;
            icon.color = activeColor;
        }
        else
        {
            text.color = deactiveColor;
            icon.color = deactiveColor;
        }
    }
}
