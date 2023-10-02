using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public Inventory inv;
    public GameObject scoreScreen1;
    public GameObject scoreScreen2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreScreen1.GetComponent<TMPro.TextMeshProUGUI>().text = inv.playerPoints.ToString();
        scoreScreen2.GetComponent<TMPro.TextMeshProUGUI>().text = inv.playerPoints.ToString();
    }
}
