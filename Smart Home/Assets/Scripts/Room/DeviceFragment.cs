using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeviceFragment : MonoBehaviour
{
    public TMP_Text deviceName;
    public Switch switchStatus;
    public void Setup(string _name, bool active)
    {
        deviceName.text = _name;
        switchStatus.SetStatus(active);
    }
}
