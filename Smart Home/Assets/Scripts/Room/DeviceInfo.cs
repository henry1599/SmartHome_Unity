using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSONExtension;

[System.Serializable]
public class DeviceInfo 
{
    public string deviceName;
    public DeviceCategory category;
    public DeviceStatus status;
    public int strength;
    public string dv;
    public string strengthDescription;
    public DeviceInfo(string _deviceName, DeviceCategory _category, DeviceStatus _status, int _strength, string _dv, string _strengthDescription)
    {
        deviceName = _deviceName;
        category = _category;
        status = _status;
        strength = _strength;
        dv = _dv;
        strengthDescription = _strengthDescription;
    }
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public static DeviceInfo FromJson(string json)
    {
        return JsonUtility.FromJson<DeviceInfo>(json);
    }
}
public enum DeviceStatus
{
    On,
    Off
}
public enum DeviceCategory
{
    Light,
    Fan
}
