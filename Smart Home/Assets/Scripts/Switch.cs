using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switch : MonoBehaviour
{
    public SwitchState state;
    public Animator anim;
    public TMP_Text text;
    public void OnToggle()
    {
        state = state == SwitchState.On ? SwitchState.Off : SwitchState.On;
        anim.SetBool("isOn", state == SwitchState.On);
        if (text != null)
        {
            text.text = state == SwitchState.On ? "On" : "Off";
        }
    }
    public void SetStatus(bool active)
    {
        if (active == true)
        {
            state = SwitchState.On;
            anim.SetBool("isOn", true);
            text.text = "On";
        }
        else
        {
            state = SwitchState.Off;
            anim.SetBool("isOn", false);
            text.text = "Off";
        }
    }
}

public enum SwitchState
{
    Off,
    On
}
