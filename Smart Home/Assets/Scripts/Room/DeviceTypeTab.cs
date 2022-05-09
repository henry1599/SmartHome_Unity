using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeviceTypeTab : MonoBehaviour
{
    public TMP_Text title;
    public Color activeColor;
    public Color deActiveColor;
    private bool m_Active;
    public string type;
    public bool Active 
    {
        get => m_Active;
        set => m_Active = value;
    }
    public void SetStatus(bool active)
    {
        Active = active;
        if (active == true)
        {
            title.color = activeColor;
        }
        else
        {
            title.color = deActiveColor;
        }
    }
}
