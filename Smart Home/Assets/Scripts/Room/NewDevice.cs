using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class NewDevice : MonoBehaviour
{
    public TMP_Text portField;
    public TMP_InputField nameField;
    public Animator anim;
    public DeviceType deviceType;
    public int port;
    public string nameDevice;
    public static event System.Action<NewDevice> OnNewDeviceClicked;
    public void Setup(int port, string name)
    {
        this.port = port;
        this.nameDevice = name;
        portField.text = $"Port : {port.ToString()}";
        nameField.text = name;
        anim.SetBool("containsText", true);
        deviceType.deviceTypeTab[0].Active = true;
    }
    public void Submit()
    {
        if (nameField.text == "")
        {
            Debug.Log("Cannot add an empty-name-device");
            return;
        }
        OnNewDeviceClicked?.Invoke(this);
        RoomManager.Instance.AddNewDevice(port, nameField.text);
    }
}
