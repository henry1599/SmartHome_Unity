using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance {get; set;}
    public RoomInfo roomInfo;
    public TMP_Text roomNameText;
    public RoomState state;
    public RoomTabButton lightButton, fanButton;
    public DeviceTypeButton lightTypeButton, fanTypeButton;
    public DeviceFragment device;
    public Transform deviceField;
    public Animator mainAnim;
    private bool isLoaded = false;
    private DeviceInfo demoDeviceInfo;
    private void Awake() 
    {
        Instance = this;    
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadRoom();
        lightTypeButton.SetStatus(true);
        fanTypeButton.SetStatus(false);
        StartCoroutine(Upload(roomInfo.devices.Find(d => d.category == DeviceCategory.Light).ToJson()));
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case RoomState.LightView:
                HandleLightView();
                break;
            case RoomState.FanView:
                HandleFanView();
                break;
            case RoomState.AddDevice:
                HandleAddDevice();
                break;
        }
        mainAnim.SetBool("isMain", state == RoomState.LightView || state == RoomState.FanView);
        mainAnim.SetBool("isAdd", state == RoomState.AddDevice);
    }
    void HandleLightView()
    {
        if (isLoaded)
        {
            return;
        }
        LoadDevice(DeviceCategory.Light);
    }
    void HandleFanView()
    {
        if (isLoaded)
        {
            return;
        }
        LoadDevice(DeviceCategory.Fan);
    }
    void HandleAddDevice()
    {
        ResetLoaded();
    }
    public void UpdateState(RoomState newState)
    {
        if (state == newState)
        {
            return;
        }
        state = newState;
    }
    public void UpdateState(int newStateid)
    {
        if (state == (RoomState)newStateid)
        {
            return;
        }
        state = (RoomState)newStateid;
    }
    void LoadDevice(DeviceCategory category)
    {
        isLoaded = true;
        List<DeviceInfo> deviceInfos = roomInfo.devices.FindAll(d => d.category == category);
        switch (category)
        {
            case DeviceCategory.Light:
                if (deviceInfos.Count == 0)
                {
                    return;
                }
                foreach (DeviceInfo deviceInfo in deviceInfos)
                {
                    DeviceFragment deviceInstance = Instantiate(device.gameObject, deviceField).GetComponent<DeviceFragment>();
                    deviceInstance.Setup(deviceInfo.deviceName, deviceInfo.status == DeviceStatus.On);
                }
                break;
            case DeviceCategory.Fan:
                if (deviceInfos.Count == 0)
                {
                    return;
                }
                foreach (DeviceInfo deviceInfo in deviceInfos)
                {
                    DeviceFragment deviceInstance = Instantiate(device.gameObject, deviceField).GetComponent<DeviceFragment>();
                    deviceInstance.Setup(deviceInfo.deviceName, deviceInfo.status == DeviceStatus.On);
                }
                break;
        }
        // print(deviceInfos[0].ToJson());
    }
    void LoadRoom()
    {
        string currentRoom = DataTransferManager.Instance.ChangedRoom;
        if (currentRoom == "LivingRoom")
        {
            roomInfo = DemoData.ROOMS[(int)RoomCategory.LivingRoom];
        }
        else if (currentRoom == "BathRoom")
        {
            roomInfo = DemoData.ROOMS[(int)RoomCategory.BathRoom];
        }
        else if (currentRoom == "BedRoom")
        {
            roomInfo = DemoData.ROOMS[(int)RoomCategory.BedRoom];
        }
        else if (currentRoom == "Garage")
        {
            roomInfo = DemoData.ROOMS[(int)RoomCategory.Garage];
        }
        else if (currentRoom == "Kitchen")
        {
            roomInfo = DemoData.ROOMS[(int)RoomCategory.Kitchen];
        }
        else if (currentRoom == "Office")
        {
            roomInfo = DemoData.ROOMS[(int)RoomCategory.Office];
        }

        roomNameText.text = roomInfo.roomName;
        UpdateState(RoomState.LightView);
        lightButton.SetStatus(true);
        fanButton.SetStatus(false);
    }
    public void ResetLoaded()
    {
        isLoaded = false;
        Helper.DeletChildren(deviceField);
    }
    IEnumerator Upload(string profile/*, System.Action<bool> callback = null*/)
    {
        // unyNuvp0W98xzyPD
        // mongodb+srv://henry1599:unyNuvp0W98xzyPD@smarthome.mjg1s.mongodb.net/smarthome?retryWrites=true&w=majority
        
        using (UnityWebRequest request = new UnityWebRequest("mongodb://henry1599:unyNuvp0W98xzyPD@smarthome.mjg1s.mongodb.net/smarthome?retryWrites=true&w=majority", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(profile);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                // if (callback != null)
                // {
                //     callback.Invoke(false);
                // }
            }
            else
            {
                // if (callback != null)
                // {
                //     callback.Invoke(request.downloadHandler.text != "{}");
                // }
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}

public enum RoomState 
{
    LightView,
    FanView,
    AddDevice
}
