using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddRoomIconChoose : MonoBehaviour
{
    public TMP_Text roomText;
    public Image background;
    public Image icon;
    public Color activeRoomTextColor;
    public Color deactiveRoomTextColor;
    public Color activeBackgroundColor;
    public Color deactiveBackgroundColor;
    public Color activeIconColor;
    public Color deactiveIconColor;
    public void SetStatus(bool active)
    {
        if (active == true)
        {
            roomText.color = activeRoomTextColor;
            background.color = activeBackgroundColor;
            icon.color = activeIconColor;
        }
        else
        {
            roomText.color = deactiveRoomTextColor;
            background.color = deactiveBackgroundColor;
            icon.color = deactiveIconColor;
        }
    }
}
