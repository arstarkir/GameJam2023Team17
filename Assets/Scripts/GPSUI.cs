using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSUI : MonoBehaviour
{
    public GameObject GPS;
    public void ShowGPSsystem(bool gpssystem)
    {
        GPS.SetActive(gpssystem);
    }
}
