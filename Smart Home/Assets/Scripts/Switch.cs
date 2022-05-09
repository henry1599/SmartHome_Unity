using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switch : MonoBehaviour
{
    public DeviceFragment deviceFragment;
    public SwitchState state;
    public Animator anim;
    public TMP_Text text;
    public static event System.Action<Switch> OnSwitchClick;
    public void OnToggle()
    {
        RoomManager.Instance.UpdateDeviceStatus(deviceFragment.id, state == SwitchState.On ? false : true, deviceFragment.type);
        OnSwitchClick?.Invoke(this);
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
