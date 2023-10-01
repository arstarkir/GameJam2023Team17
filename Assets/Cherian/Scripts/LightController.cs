using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject headlightLeft;
    public GameObject headlightRight;
    public GameObject tailLight;

    // Start is called before the first frame update
    void Start()
    {
        tailLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            tailLight.SetActive(true);
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            tailLight.SetActive(false);
        }
    }
}
