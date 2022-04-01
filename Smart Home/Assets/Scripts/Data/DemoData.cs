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
