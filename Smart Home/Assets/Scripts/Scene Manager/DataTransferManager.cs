using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class DataTransferManager : MonoBehaviour
{
    public static DataTransferManager Instance {get; set;}
    public static string whatRoomToBeChanged;
    public static event Action OnRoomChangedFromHome;
    public string ChangedRoom
    {
        get {return whatRoomToBeChanged;}
        set {
            whatRoomToBeChanged = value;
            OnRoomChangedFromHome?.Invoke();
        }
    }
    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Login Scene")
        {
            Destroy(gameObject);
            return;
        }
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
