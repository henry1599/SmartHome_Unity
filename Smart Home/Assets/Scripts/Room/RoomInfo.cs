using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class RoomInfo
{
    public string roomName;
    public RoomCategory category;
    public List<DeviceInfo> devices;
    public RoomInfo(string _roomName, RoomCategory _category, List<DeviceInfo> _devices)
    {
        roomName = _roomName;
        category = _category;
        devices = _devices;
    }
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public static RoomInfo FromJson(string json)
    {
        return JsonUtility.FromJson<RoomInfo>(json);
    }
}

public enum RoomCategory
{
    LivingRoom,
    BathRoom,
    BedRoom,
    Garage,
    Kitchen,
    Office
}
