using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SettingTab : MonoBehaviour
{
    public Image icon;
    public Image background;
    public TMP_Text title;
    public Color backgroundActiveColor;
    public Color backgroundDeactiveColor;
    public Color iconActiveColor;
    public Color iconDeactiveColor;
    public Color titleActiveColor;
    public Color titleDeactiveColor;
    public void SetStatus(bool active)
    {
        if (active == true)
        {
            icon.color = iconActiveColor;
            title.color = titleActiveColor;
            background.color = backgroundActiveColor;
        }
        else
        {
            icon.color = iconDeactiveColor;
            title.color = titleDeactiveColor;
            background.color = backgroundDeactiveColor;
        }
    }
    public void BackHomeButton()
    {
        SceneManager.LoadScene("Login Scene");
    }
}
