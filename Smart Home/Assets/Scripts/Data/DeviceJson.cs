using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using JSONExtension;
using System;

public enum PostType
{
    ADD_NEW_ROOM,
    OTHER
}
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
#region RESPONSE CLASS
public class R_DataDevice
{
    public string _id { get; set; }
    public object id { get; set; }
    public string name { get; set; }
    public bool status { get; set; }
    public string type { get; set; }
    public object roomId { get; set; }
    public int capacity { get; set; }
    public int duration { get; set; }
    public DateTime lastUse { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
}

public class R_DataRoom
{
    public string _id { get; set; }
    public long id { get; set; }
    public string name { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
}
/// <summary>
/// * Use for all devices => Response for get all device
/// </summary>
public class R_AllDevices
{
    public List<R_DataDevice> data { get; set; }
}
/// <summary>
/// * Use for add new device => Response for add new device
/// </summary>
public class R_Device
{
    public int status { get; set; }
    public R_DataDevice data { get; set; }
}
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

/// <summary>
/// * Use for add and delete room => Response for add or delete a room
/// </summary>
public class R_Room
{
    public int status { get; set; }
    public R_DataRoom data { get; set; }
}
public class R_Room_Id
{
    public R_Room_Id(long _id)
    {
        this.id = _id;
    }
    public long id;
}

/// <summary>
/// * Use for all rooms => Response for get all rooms
/// </summary>
public class R_AllRooms
{
    public List<R_DataRoom> data { get; set; }
}
#endregion

#region GET CLASS
/// <summary>
/// * Use for update device status => Get request to server
/// </summary>
public class P_UpdateDeviceStatus
{
    public long id { get; set; }
    public bool status { get; set; }
    public P_UpdateDeviceStatus(long _id, bool _status)
    {
        this.id = _id;
        this.status = _status;
    }
}
public class P_DeviceStatusResponse
{
    public int status { get; set; }
    public R_DataDevice data { get; set; }
}
public class P_DeleteDevice
{
    public int id {get; set;}
    public P_DeleteDevice(int id)
    {
        this.id = id;
    }
}
public class R_FeedData
{
    public string id { get; set; }
    public string value { get; set; }
    public int feed_id { get; set; }
    public string feed_key { get; set; }
    public DateTime created_at { get; set; }
    public int created_epoch { get; set; }
    public DateTime expiration { get; set; }
}
public class R_FeedDatas
{
    public List<R_FeedData> data { get; set; }
}
public class R_Port
{
    public string _id { get; set; }
    public int port { get; set; }
    public bool status { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int __v { get; set; }
}
public class R_AllPorts
{
    public List<R_Port> data { get; set; }
}
/// <summary>
/// * Use for add new device => Get request to server
/// </summary>
public class P_AddDevice
{
    public string name { get; set; }
    public long id { get; set; }
    public string type { get; set; }
    public long roomId { get; set; }
    public P_AddDevice(string _name, long _id, string _type, long _roomId)
    {
        this.name = _name;
        this.id = _id;
        this.type = _type;
        this.roomId = _roomId;
    }
}
/// <summary>
/// * Use for delete a single room => Get request to the server
/// </summary>
public class P_DeleteRoom
{
    public long id { get; set; }
}
/// <summary>
/// * Use for add a new room => Get request to the server
/// </summary>
public class P_AddRoom
{
    public string name { get; set; }
}
#endregion

#region URL CLASS
public static class P_URL
{
    public static string UpdateDeviceStatus = "http://localhost:5000/device/toggle";
    public static string AddNewDevice = "http://localhost:5000/device/add";
    public static string DeleteRoom = "http://localhost:5000/room/delete";
    public static string AddNewRoom = "http://localhost:5000/room/add";
    public static string ContentTypeForAddNewRoom = "application/json";
    public static string GetAllDevicesInRoom = "http://localhost:5000/room/device/all";
    public static string DeleteDevice = "http://localhost:5000/device/delete";
    // public static string UpdateDeviceStatus = "http://192.168.1.16:5000/device/toggle";
    // public static string AddNewDevice = "http://192.168.1.16:5000/device/add";
    // public static string DeleteRoom = "http://192.168.1.16:5000/room/delete";
    // public static string AddNewRoom = "http://192.168.1.16:5000/room/add";
    // public static string ContentTypeForAddNewRoom = "application/json";
    // public static string GetAllDevicesInRoom = "http://192.168.1.16:5000/room/device/all";
    // public static string DeleteDevice = "http://192.168.1.16:5000/device/delete";
}
public static class G_URL
{
    public static string GetAllRooms = "http://localhost:5000/room/all";
    public static string GetAllDevices = "http://localhost:5000/device/all";
    public static string GetAllFeedDatas = "http://localhost:5000/adafruit/hardware/all";
    public static string GetAllPorts = "http://localhost:5000/port";
}
#endregion
#region MQTT PUBLISH
public class MqttJsonClass
{
    public long id { get; set; }
    public string cmd { get; set; }
    public string name { get; set; }
    public string paras { get; set; }
    public MqttJsonClass(long _id, string _cmd, string _name, string _paras)
    {
        this.id = _id;
        this.cmd = _cmd;
        this.name = _name;
        this.paras = _paras;
    }
}
#endregion