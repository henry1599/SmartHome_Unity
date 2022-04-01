using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticHeaderTab : MonoBehaviour
{
    public GameObject border;
    public void SetStatus(bool active)
    {
        border.SetActive(active);
    }
}
