using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSUI : MonoBehaviour
{
    public GameObject GPS;
    public List<int> togs = new List<int>();
    public void ShowGPSsystem(bool gpssystem)
    {
        GPS.SetActive(gpssystem);
    }
    public void LocationChosen(int i)
    {
        WasOn(i);
    }
    bool WasOn(int id)
    {
        for (int i = 0; i < togs.Count; i++)
        {
            if (togs[i] == id) 
            {
                togs.RemoveAt(i);
                return false;
            }
        }
        togs.Add(id);
        return false;
    }
}
