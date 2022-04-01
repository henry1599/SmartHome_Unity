using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSceneButton : MonoBehaviour
{
    public void OnBackButtonClick()
    {
        if (RoomManager.Instance.state == RoomState.AddDevice)
        {
            RoomManager.Instance.UpdateState(RoomState.LightView);
        }
        else
        {
            SceneManager.LoadScene("Home Scene");
        }
    }
}
