using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeviceType : MonoBehaviour
{
    public List<DeviceTypeTab> deviceTypeTab;
    public string GetChosenDeviceType()
    {
        return deviceTypeTab.Find(d => d.Active == true).type;
    }
}
