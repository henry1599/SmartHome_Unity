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

public class MyMqttClient : M2MqttUnityClient
{
    [Tooltip("Set this to true to perform a testing cycle automatically on startup")]
    public bool autoTest = false;
    [Header("User Interface")]
    public TMP_Text tempText;
    public TMP_Text humiText;

    private List<string> eventMessages = new List<string>();
    private bool updateUI = false;

    public void TestPublish()
    {
        client.Publish(this.publishTopic, System.Text.Encoding.UTF8.GetBytes("Hello"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
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

    protected override void DecodeMessage(string topic, byte[] message)
    {
        string msg = System.Text.Encoding.UTF8.GetString(message);
        Debug.Log("Received: " + msg);
        if (msg.Contains("TempHumi"))
        {
            TempHumi tempHumi = TempHumi.FromJsonToObject(msg);
            List<string> tempHumiValues = tempHumi.ExtractParas();
            tempText.text = tempHumiValues[0].ToString();
            humiText.text = tempHumiValues[1].ToString();
        }
        // print(TempHumi.FromJsonToObject(msg).name);
        StoreMessage(msg);
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
        // if (updateUI)
        // {
        //     UpdateUI();
        // }
    }

    private void OnDestroy()
    {
        Disconnect();
    }

    private void OnValidate()
    {
    }

}
