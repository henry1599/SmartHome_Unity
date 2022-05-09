using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using Newtonsoft.Json;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance {get; set;}
    public RoomState state;
    public TMP_Text roomNameText;
    public DeviceFragment device;
    public Transform deviceField;
    public Animator mainAnim;
    public bool isLoaded = false;
    public bool isLoadedPort = false;
    bool isDeviceLoaded;
    bool isCurrentRoomLoaded;
    bool isAllRoomsLoaded;
    public bool statusUpdated;
    public bool addDeviceSuccessfully;
    public bool removeDeviceSuccessfully;
    public NewDevice newDeviceTemplate;
    [HideInInspector]
    public Switch clickedSwitch = null;
    public NewDevice newDeviceClick;
    public DeviceFragment deviceFragmentRemoveButtonClick;
    public Transform newDeviceField;
    private void Awake() 
    {
        isCurrentRoomLoaded = false;
        isAllRoomsLoaded = false;
        isDeviceLoaded = false;
        isLoaded = false;
        Instance = this;    
    }
    // Start is called before the first frame update
    void Start()
    {
        // LoadAllDevice();
        roomNameText.text = DataTransferManager.Instance.ChangedRoom;
        StartCoroutine(Init());
        DeviceFragment.OnDeviceRemove += HandleRemoveDeviceClick;
        NewDevice.OnNewDeviceClicked += HandleOnDeviceClicked;
        Switch.OnSwitchClick += HandleOnSwitchClick;
        MyMqttClient.OnAddDeviceSuccessfully += HandleAddDeviceSuccessfully;
        MyMqttClient.OnUpdateDeviceStatus += HandleUpdateDeviceStatus;
        MyMqttClient.OnRemoveDeviceSuccessfully += HandleRemoveDevice;
    }
    void OnDestroy()
    {
        DeviceFragment.OnDeviceRemove -= HandleRemoveDeviceClick;
        NewDevice.OnNewDeviceClicked -= HandleOnDeviceClicked;
        Switch.OnSwitchClick -= HandleOnSwitchClick;
        MyMqttClient.OnUpdateDeviceStatus -= HandleUpdateDeviceStatus;
        MyMqttClient.OnAddDeviceSuccessfully -= HandleAddDeviceSuccessfully;
        MyMqttClient.OnRemoveDeviceSuccessfully -= HandleRemoveDevice;
    }
    void HandleRemoveDevice(bool success)
    {
        removeDeviceSuccessfully = success;
    }
    void HandleRemoveDeviceClick(DeviceFragment device)
    {
        deviceFragmentRemoveButtonClick = device;
    }
    void HandleOnDeviceClicked(NewDevice nd)
    {
        newDeviceClick = nd;
    }
    void HandleUpdateDeviceStatus(bool success)
    {
        statusUpdated = success;
    }
    void HandleOnSwitchClick(Switch s)
    {
        clickedSwitch = s;
    }
    void HandleAddDeviceSuccessfully(bool success)
    {
        addDeviceSuccessfully = success;
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case RoomState.Idle:
                HandleIdle();
                break;
            case RoomState.View:
                HandleView();
                break;
            case RoomState.AddDevice:
                HandleAddDevice();
                break;
        }
        mainAnim.SetBool("isMain", state == RoomState.View || state == RoomState.Idle);
        mainAnim.SetBool("isAdd", state == RoomState.AddDevice);
    }
    IEnumerator Init()
    {
        LoadAllRooms();
        LoadAllDevice();
        // yield return null;
        yield return new WaitUntil(() => isAllRoomsLoaded == true);
        LoadCurrentRoom();
        yield return new WaitUntil(() => isCurrentRoomLoaded == true);
        LoadAllDeviceInCurrentRoom();
        ResetLock();
    }
    void LoadAllDevice()
    {
        string allDevicesJson = ResponseFromGetRequest(G_URL.GetAllDevices);
        DATA.ALL_DEVICE = JsonConvert.DeserializeObject<R_AllDevices>(allDevicesJson);
        // print($"All devices get : {DATA.ALL_DEVICE.data.Count}");
    }
    void LoadAllRooms()
    {
        string allRoomsJson = ResponseFromGetRequest(G_URL.GetAllRooms);
        DATA.ALL_ROOMS = JsonConvert.DeserializeObject<R_AllRooms>(allRoomsJson);
        isAllRoomsLoaded = true;
    }
    void LoadCurrentRoom()
    {
        DATA.CURRENT_ROOM = DATA.ALL_ROOMS.data.Find(r => r.name == DataTransferManager.Instance.ChangedRoom);
        // print($"Request change room {DataTransferManager.Instance.ChangedRoom}");
        if (DATA.CURRENT_ROOM == null)
        {
            return;
        }
        // print($"Room get : {DATA.CURRENT_ROOM.name}");
        // print($"Room id : {DATA.CURRENT_ROOM.id}");
        isCurrentRoomLoaded = true;
    }
    public void LoadAllDeviceInCurrentRoom()
    {
        Helper.DeletChildren(deviceField);
        long currentRoomId = DATA.CURRENT_ROOM.id;
        List<R_DataDevice> devices = new List<R_DataDevice>();
        foreach (R_DataDevice device in DATA.ALL_DEVICE.data)
        {
            if ((long)device.roomId == currentRoomId)
            {
                devices.Add(device);
            }
        }

        if (devices.Count == 0)
        {
            return;
        }
        foreach (R_DataDevice deviceInfo in devices)
        {
            DeviceFragment deviceInstance = Instantiate(device.gameObject, deviceField).GetComponent<DeviceFragment>();
            // print($"Light {deviceInfo.id}, status : {deviceInfo.status}");
            deviceInstance.Setup((long)deviceInfo.id, deviceInfo.type, deviceInfo.name, deviceInfo.status);
        }
        isLoaded = true;
    }
    public void UpdateDeviceStatus(long id, bool status, string type = "")
    {
        if (status == true)
        {
            MqttJsonClass updatedDeviceStatusForAda = new MqttJsonClass(id, "open", type, "none");
            string jsonUpdatedForAda = JsonConvert.SerializeObject(updatedDeviceStatusForAda);
            MyMqttClient.Instance.Publish(jsonUpdatedForAda);
        }
        else
        {
            MqttJsonClass updatedDeviceStatusForAda = new MqttJsonClass(id, "close", type, "none");
            string jsonUpdatedForAda = JsonConvert.SerializeObject(updatedDeviceStatusForAda);
            MyMqttClient.Instance.Publish(jsonUpdatedForAda);
        }
        StartCoroutine(UpdateDeviceStatusWeb(id, status, type));
    }
    IEnumerator UpdateDeviceStatusWeb(long id, bool status, string type = "")
    {
        yield return new WaitUntil(() => statusUpdated == true);
        UpdateDeviceListener(id, status, type);
    }
    public void AddNewDevice(int port, string name)
    {
        MqttJsonClass addDeviceForAda = new MqttJsonClass(port, "add", newDeviceClick.deviceType.GetChosenDeviceType(), "none");
        string jsonUpdatedForAda = JsonConvert.SerializeObject(addDeviceForAda);
        MyMqttClient.Instance.Publish(jsonUpdatedForAda);
        // print($"Add json : {jsonUpdatedForAda}");
        StartCoroutine(AddNewDeviceWeb(port, name));
    }
    IEnumerator AddNewDeviceWeb(int port, string name)
    {
        yield return new WaitUntil(() => addDeviceSuccessfully == true);
        AddDeviceListener(port, name);
    }
    public void AddDeviceListener(int port, string name, string type = "")
    {
        P_AddDevice addDeviceForWeb = new P_AddDevice(name, port, newDeviceClick.deviceType.GetChosenDeviceType(), DATA.CURRENT_ROOM.id);
        string jsonAddDeviceForWeb = JsonConvert.SerializeObject(addDeviceForWeb);
        // print($"Add device json sent : {jsonAddDeviceForWeb}");
        string jsonResult = ResponseFromPostRequest(P_URL.AddNewDevice, jsonAddDeviceForWeb, PostType.OTHER);
        Destroy(newDeviceClick.gameObject);
        addDeviceSuccessfully = false;
    }
    public void UpdateDeviceListener(long id, bool status, string type = "")
    {
        P_UpdateDeviceStatus updatedDeviceStatusForWeb = new P_UpdateDeviceStatus(id, status);
        string jsonUpdateForWeb = JsonConvert.SerializeObject(updatedDeviceStatusForWeb);
        // print($" json sent : {jsonUpdateForWeb}");
        string jsonResult = ResponseFromPostRequest(P_URL.UpdateDeviceStatus, jsonUpdateForWeb, PostType.OTHER);
        // print($" json received : {jsonResult}");
        P_DeviceStatusResponse deviceStatusResponse = JsonConvert.DeserializeObject<P_DeviceStatusResponse>(jsonResult);
        if (clickedSwitch == null)
        {
            FindObjectsOfType<DeviceFragment>().ToList().Find(d => d.id == id)?.switchStatus?.SetStatus(status);
        }
        else
        {
            clickedSwitch?.SetStatus(status);
            clickedSwitch = null;
        }
        statusUpdated = false;
    }
    public void DeleteDevice(int port)
    {
        MqttJsonClass addDeviceForAda = new MqttJsonClass(port, "del", deviceFragmentRemoveButtonClick.type, "none");
        string jsonUpdatedForAda = JsonConvert.SerializeObject(addDeviceForAda);
        MyMqttClient.Instance.Publish(jsonUpdatedForAda);
        print($"Add json : {jsonUpdatedForAda}");
        StartCoroutine(DeleteDeviceWeb(port));
    }
    IEnumerator DeleteDeviceWeb(int port)
    {
        yield return new WaitUntil(() => removeDeviceSuccessfully == true);
        DeleteDeviceListener(port);
    }
    public void DeleteDeviceListener(int port)
    {
        P_DeleteDevice removeDevice = new P_DeleteDevice(port);
        string jsonRemoveDeviceForWeb = JsonConvert.SerializeObject(removeDevice);
        string jsonResult = ResponseFromPostRequest(P_URL.DeleteDevice, jsonRemoveDeviceForWeb, PostType.OTHER);
        // print(jsonResult);
        if (deviceFragmentRemoveButtonClick == null)
        {
            Destroy(FindObjectsOfType<DeviceFragment>().ToList().Find(d => d.id == port).gameObject);
        }
        else
        {
            Destroy(deviceFragmentRemoveButtonClick.gameObject);
        }
        removeDeviceSuccessfully = false;
    }
        
    void ResetLock()
    {
        isAllRoomsLoaded = false;
        isCurrentRoomLoaded = false;
    }
    public string ResponseFromGetRequest(string url)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

        return responseString.ToString();
    }

    public string ResponseFromPostRequest(string url, string json, PostType postType)
    {
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(json);
            // streamWriter.Flush();
            // streamWriter.Close();
        }
        HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        string result = "";
        using(StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }
        return result;
    }
    void HandleView()
    {
        if (isLoaded)
        {
            return;
        }
        StartCoroutine(Init());
        isLoadedPort = false;
        // UpdateState(RoomState.Idle);
    }
    void HandleAddDevice()
    {
        if (isLoadedPort)
        {
            return;
        }
        LoadAvailablePorts();
        isLoaded = false;
        // UpdateState(RoomState.Idle);
    }
    void HandleIdle()
    {

    }
    public void LoadAvailablePorts()
    {
        Helper.DeletChildren(newDeviceField);
        string jsonResponse = ResponseFromGetRequest(G_URL.GetAllPorts);
        R_AllPorts allPorts = JsonConvert.DeserializeObject<R_AllPorts>(jsonResponse);
        List<int> ports = allPorts.data.FindAll(p => p.status == false).Select(p => p.port).ToList();
        foreach (int port in ports)
        {
            print($"Available port : {port}");
            NewDevice newDeviceInstance = Instantiate(newDeviceTemplate.gameObject, newDeviceField).GetComponent<NewDevice>();
            newDeviceInstance.Setup(port, "Device");
        }
        isLoadedPort = true;
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
    public void ResetLoaded()
    {
        isLoaded = false;
        Helper.DeletChildren(deviceField);
    }
}

public enum RoomState 
{
    Idle,
    View,
    AddDevice
}
