using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject credits;
    public GameObject manu;
    public void ShowCredits()
    {
        manu.SetActive(false);
        credits.SetActive(true);
    }
    public void HideCredits()
    {
        manu.SetActive(true);
        credits.SetActive(false);
    }
}
