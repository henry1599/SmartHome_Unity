using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DemoData
{
    public static List<RoomInfo> ROOMS = new List<RoomInfo>()
    {
        new RoomInfo("Living Room", RoomCategory.LivingRoom,
            new List<DeviceInfo>()
            {
                new DeviceInfo("Light 1", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 2", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 3", DeviceCategory.Light, DeviceStatus.Off, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 4", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 5", DeviceCategory.Light, DeviceStatus.Off, 45, "lm", "Light Strength"),
                new DeviceInfo("Fan 1", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength"),
                new DeviceInfo("Fan 2", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength")
            }
        ),
        new RoomInfo("Bath Room", RoomCategory.BathRoom,
            new List<DeviceInfo>()
            {
                new DeviceInfo("Light 1", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 2", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 3", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 4", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 5", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Fan 1", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength"),
                new DeviceInfo("Fan 2", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength")
            }
        ),
        new RoomInfo("Bed Room", RoomCategory.BedRoom,
            new List<DeviceInfo>()
            {
                new DeviceInfo("Light 1", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 2", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 3", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 4", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 5", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Fan 1", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength"),
                new DeviceInfo("Fan 2", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength")
            }
        ),
        new RoomInfo("Garage", RoomCategory.Garage,
            new List<DeviceInfo>()
            {
                new DeviceInfo("Light 1", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 2", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 3", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 4", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 5", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Fan 1", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength"),
                new DeviceInfo("Fan 2", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength")
            }
        ),
        new RoomInfo("Kitchen", RoomCategory.Kitchen,
            new List<DeviceInfo>()
            {
                new DeviceInfo("Light 1", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 2", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 3", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 4", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 5", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Fan 1", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength"),
                new DeviceInfo("Fan 2", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength")
            }
        ),
        new RoomInfo("Office", RoomCategory.Office,
            new List<DeviceInfo>()
            {
                new DeviceInfo("Light 1", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 2", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 3", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 4", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Light 5", DeviceCategory.Light, DeviceStatus.On, 45, "lm", "Light Strength"),
                new DeviceInfo("Fan 1", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength"),
                new DeviceInfo("Fan 2", DeviceCategory.Fan, DeviceStatus.On, 65, "m\\s", "Fan Strength")
            }
        )
    };
}
public static class DATA
{
    public static R_AllDevices ALL_DEVICE;
    public static R_AllRooms ALL_ROOMS;
    public static R_DataRoom CURRENT_ROOM;
    public static R_AllDevices ALL_DEVICES_IN_CURRENT_ROOM;
}

public static class GET_URL 
{
    public const string GET_ALL_ROOMS = "http://localhost:5000/room/all";
    public const string GET_ALL_DEVICES = "http://localhost:5000/device/all";
    public const string DEMO_STRING = "{\"data\":[{\"_id\": \"6255325804cc8b5a55baa2fe\",\"id\": 2022412145816,\"name\": \"Đèn bếp\",\"status\": false,\"type\": \"led\",\"roomId\": 202232910475,\"capacity\": 36,\"duration\": 11316,\"lastUse\": \"2022-04-12T07:58:36.281Z\",\"createdAt\": \"2022-04-12T08:03:36.530Z\",\"updatedAt\": \"2022-04-12T08:03:48.694Z\",\"__v\": 0}]}";
}