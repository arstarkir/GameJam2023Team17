using UnityEngine;
using UnityEngine.UI;

public class Delivery : MonoBehaviour
{
    [SerializeField] GameObject player2;
    Inventory inv;
    bool ReCheck = false;
    int OrderNum,j, OrderSlotsNum;
    private void OnTriggerEnter(Collider other)
    {
        inv = player2.GetComponent<Inventory>();
        for (int i = 0; i < inv.DropPos.Count; i++)
        {
            Debug.Log(inv.DropPos.Count);
            if (inv.OrderPos[inv.DropPos[i]].transform.position == transform.position)
            {
                OrderSlotsNum = i;
                ReCheck = true;
                OrderNum = i;
                j = i;
                break;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(OrderNum + " " + inv.OrderSlots.Count);

        foreach (Transform childTemp in inv.OrderSlots[OrderNum].transform)
        {
            GameObject child = childTemp.gameObject;
            if (child.name == "Checkmark")
            {
                if (child.gameObject.activeInHierarchy == true)
                {
                    inv.DropPos.RemoveAt(j);
                    child.SetActive(false);

                    //inv.markers[OrderNum].gameObject.SetActive(false);
                    inv.OrderIcon[OrderNum].GetComponent<Image>().sprite = null;
                    Color newColor = inv.OrderIcon[OrderNum].GetComponent<Image>().color;
                    newColor.a = 0;
                    inv.OrderIcon[OrderNum].GetComponent<Image>().color = newColor;
                    inv.OrderIcon[OrderNum].GetComponent<Image>().sprite = null;
                    newColor = inv.OrderSlots[OrderSlotsNum].GetComponent<Image>().color;
                    newColor.a = 0;
                    inv.OrderSlots[OrderSlotsNum].GetComponent<Image>().color = newColor;
                    Debug.Log(inv.OrderItems.Count);
                    inv.playerPoints = (inv.OrderItems[OrderNum].points);
                    inv.OrderItems.RemoveAt(OrderNum);
                    Debug.Log(inv.OrderItems.Count);
                    inv.OrderSlots.RemoveAt(OrderSlotsNum);
                    ReCheck = false;
                    break;
                }
            }
        }
    }
    private void Update()
    {
  
    }
}
