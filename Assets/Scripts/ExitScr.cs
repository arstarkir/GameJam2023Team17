using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScr : MonoBehaviour
{
    public void Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
