using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampSlowMo_TriggerScript : MonoBehaviour
{
    private bool rampTriggered = false;

    private void Start()
    {
        rampTriggered = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rampTriggered = true;
            StartCoroutine("ActivateSlowMotion");
        }
    }

    private IEnumerator ActivateSlowMotion()
    {
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1.8f);

        Time.timeScale = 1f;
        rampTriggered = false;
    }


}
