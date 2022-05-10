using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatisticFragment : MonoBehaviour
{
    [SerializeField] TMP_Text m_Value;
    [SerializeField] TMP_Text m_RoomName;
    public void Setup(string roomName, long value)
    {
        m_Value.text = ((double)value / 3600).ToString() + " kWh";
        m_RoomName.text = roomName;
    } 
}
