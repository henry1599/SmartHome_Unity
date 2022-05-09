using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeviceFragment : MonoBehaviour
{
    public long id;
    public string type;
    public TMP_Text deviceName;
    public Switch switchStatus;
    public static event System.Action<DeviceFragment> OnDeviceRemove;
    public void Setup(long _id, string _type, string _name, bool active)
    {
        id = _id;
        type = _type;
        deviceName.text = _name;
        switchStatus.SetStatus(active);
    }
    public void Submit()
    {
        OnDeviceRemove?.Invoke(this);
        RoomManager.Instance.DeleteDevice((int)id);
    }
}
