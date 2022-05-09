using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;
using TMPro;
using JSONExtension;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
public class MyMqttClient : M2MqttUnityClient
{
    public static MyMqttClient Instance {get; set;}
    [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
    public bool autoTest = false;
    [Header("User Interface")]
    public TMP_Text tempText;
    public TMP_Text humiText;

    private List<string> eventMessages = new List<string>();
    private bool updateUI = false;
    public static event Action<bool> OnUpdateDeviceStatus;
    public static event Action<bool> OnAddDeviceSuccessfully;
    public static event Action<bool> OnRemoveDeviceSuccessfully;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void Publish(string json)
    {
        client.Publish(this.publishTopic, System.Text.Encoding.UTF8.GetBytes(json), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
        // AddUiMessage("Test message published.");
    }

    public void SetBrokerAddress(string brokerAddress)
    {
    }

    public void SetBrokerPort(string brokerPort)
    {
    }

    public void SetEncrypted(bool isEncrypted)
    {
        this.isEncrypted = isEncrypted;
    }


    public void SetUiMessage(string msg)
    {
    }

    public void AddUiMessage(string msg)
    {
    }

    protected override void OnConnecting()
    {
        base.OnConnecting();
        SetUiMessage("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
    }

    protected override void OnConnected()
    {
        base.OnConnected();
        SetUiMessage("Connected to broker on " + brokerAddress + "\n");

        // if (autoTest)
        // {
        //     TestPublish();
        // }
    }

    protected override void SubscribeTopics()
    {
        client.Subscribe(new string[] { this.subscriptionTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    protected override void UnsubscribeTopics()
    {
        client.Unsubscribe(new string[] { this.subscriptionTopic });
    }

    protected override void OnConnectionFailed(string errorMessage)
    {
        // AddUiMessage("CONNECTION FAILED! " + errorMessage);
    }

    protected override void OnDisconnected()
    {
        // AddUiMessage("Disconnected.");
    }

    protected override void OnConnectionLost()
    {
        // AddUiMessage("CONNECTION LOST!");
    }

    private void UpdateUI()
    {
        
    }

    protected override void Start()
    {
        SetUiMessage("Ready.");
        updateUI = true;
        base.Start();
        base.Connect();
    }

    /// <summary>
    /// * Receive message from mqtt server
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="message"></param>
    protected override void DecodeMessage(string topic, byte[] message)
    {
        string msg = System.Text.Encoding.UTF8.GetString(message);
        Debug.Log("Received: " + msg);
        if (msg.Contains("TempHumi") && SceneManager.GetActiveScene().name == "Home Scene")
        {
            HandleTempHumi(msg);
        }
        else if (msg.Contains("open") || msg.Contains("close"))
        {
            HandleUpdateDeviceStatus(msg);
        }
        else if (msg.Contains("add"))
        {
            HandleAddNewDevice(msg);
        }
        else if (msg.Contains("del"))
        {
            HandleRemoveDevice(msg);
        }
        // print(TempHumi.FromJsonToObject(msg).name);
        StoreMessage(msg);
    }
    void HandleTempHumi(string msg)
    {
        // if (tempText == null || humiText == null)
        // {
        //     return;
        // }
        TempHumi tempHumi = JsonConvert.DeserializeObject<TempHumi>(msg);
        print(tempHumi.paras[0].ToString());
        print(tempHumi.paras[1].ToString());
        tempText.text = ((int)tempHumi.paras[0]).ToString() + "°C";
        humiText.text = ((int)tempHumi.paras[1]).ToString() + "%";
    }
    void HandleUpdateDeviceStatus(string msg)
    {
        // print("Invoke successfully");
        MqttJsonClass mqttJsonClass = JsonConvert.DeserializeObject<MqttJsonClass>(msg);
        // print(RoomManager.Instance.clickedSwitch);
        if (RoomManager.Instance.clickedSwitch == null)
        {
            RoomManager.Instance.UpdateDeviceListener(mqttJsonClass.id, mqttJsonClass.cmd == "open" ? true : false, mqttJsonClass.name);
        }
        else
        {
            OnUpdateDeviceStatus?.Invoke(msg.Contains("success"));
        }
    }
    void HandleAddNewDevice(string msg)
    {
        OnAddDeviceSuccessfully?.Invoke(msg.Contains("success"));
    }
    void HandleRemoveDevice(string msg)
    {
        MqttJsonClass mqttJsonClass = JsonConvert.DeserializeObject<MqttJsonClass>(msg);
        if (RoomManager.Instance.deviceFragmentRemoveButtonClick == null)
        {
            RoomManager.Instance.DeleteDeviceListener((int)mqttJsonClass.id);
        }
        else
        {
            OnRemoveDeviceSuccessfully?.Invoke(msg.Contains("success"));
        }
        // print("$ Remove messge : {msg}");
    }

    private void StoreMessage(string eventMsg)
    {
        eventMessages.Add(eventMsg);
    }

    private void ProcessMessage(string msg)
    {
        // AddUiMessage("Received: " + msg);
    }

    protected override void Update()
    {
        base.Update(); // call ProcessMqttEvents()

        if (eventMessages.Count > 0)
        {
            foreach (string msg in eventMessages)
            {
                ProcessMessage(msg);
            }
            eventMessages.Clear();
        }
    }

    private void OnDestroy()
    {
        Disconnect();
    }

    private void OnValidate()
    {
    }

}
