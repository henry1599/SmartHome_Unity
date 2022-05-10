using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Graph : MonoBehaviour
{
    public static int MAX_HEIGHT_GRAPH_UI = 140;
    [SerializeField] RectTransform m_ValueField;
    [SerializeField] TMP_Text m_RoomName;
    [SerializeField] TMP_Text m_ValueText;
    public void Setup(double percentage, string roomName, long actualValue)
    {
        double shownValue = (MAX_HEIGHT_GRAPH_UI * percentage);
        m_ValueField.sizeDelta = new Vector2(m_ValueField.sizeDelta.x, (float)shownValue);
        m_ValueText.text = ((double)actualValue / 3600).ToString() + " kWh";
        m_RoomName.text = roomName;
    }
}
