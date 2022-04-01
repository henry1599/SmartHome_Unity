using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginSceneManager : MonoBehaviour
{
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public string defaultUsername = "admin";
    public string defaultPassword = "admin";
    public void OnGoToHomeButtonClick()
    {
        if (defaultUsername == usernameInputField.text && defaultPassword == passwordInputField.text)
        {
            SceneManager.LoadScene("Home Scene");
        }
        else
        {
            Debug.Log("Invalid username or password");
        }
    }
}
