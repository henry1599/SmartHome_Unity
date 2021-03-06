using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomHomeButton : MonoBehaviour
{
    public TMP_Text title;
    public Image background;
    public Image icon;
    public Color activeTitleTextColor;
    public Color deactiveTitleTextColor;
    public Color activeBackgroundColor;
    public Color deactiveBackgroundColor;
    public Color activeIconColor;
    public Color deactiveIconColor;
    public void OnRoomButtonClick()
    {
        DataTransferManager.Instance.ChangedRoom = GetComponent<Room>().roomName;
    }
    public void SetStatus(bool active)
    {
        if (active == true)
        {
            title.color = activeTitleTextColor;
            background.color = activeBackgroundColor;
            icon.color = activeIconColor;
        }
        else
        {
            title.color = deactiveTitleTextColor;
            background.color = deactiveBackgroundColor;
            icon.color = deactiveIconColor;
        }
    }
}
