using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Camera mCamera;
    IdSystem idSystem;

    [SerializeField] GameObject slot;
    [SerializeField] int numOfSlot;
    [SerializeField] int numOfSlotInBP = 0;

    GameObject BP;
    public List<Item> inv = new List<Item>();
    List<GameObject> slots = new List<GameObject>();
    List<GameObject> slotsBP = new List<GameObject>();
    Item nullItem = new Item();

    bool justStarted = false; // for a bug wich I don't know how to fix(
    bool isBPActive = false;
    void Start()
    {
        idSystem = GameObject.FindGameObjectsWithTag("Player").First(a => a.gameObject).GetComponent<IdSystem>();

        Image tempIMG = slot.GetComponent<Image>();
        Color newColor = tempIMG.color;
        newColor.a = 0.39f;
        tempIMG.color = newColor;

        inv.Clear();
        slot.GetComponent<RectTransform>().position = new Vector2(0, 0);
        for (int i = 0; i < numOfSlot; i++)
        {
            inv.Add(nullItem);
            Vector2 pos = new Vector2(0, 0);
            slots.Add(Instantiate<GameObject>(slot, pos, Quaternion.identity, canvas.transform)); //creating slots and puting tham in "slots" List
        }
        BP = new GameObject();
        BP.name = "BP";
        BP.transform.position = new Vector3(-725,-125,0);
        BP.transform.parent = canvas.transform;
        for (int i = 0; i < numOfSlotInBP/4; i++)
        {
            for (int j = 0; j < numOfSlotInBP / 4;j++)
            {
                inv.Add(nullItem);
                Vector2 pos = new Vector2(475 + 60 * j,225+60*i );
                slotsBP.Add(Instantiate<GameObject>(slot, pos, Quaternion.identity, BP.transform));
            }
        }
        BP.SetActive(isBPActive);
        justStarted = true;
    }

    private void Update()
    {

        //if (justStarted)//if you want to add some items at the start
        //{
        //    justStarted = false;
        //    inv[0] = idSystem.ItemById(1);
        //    inv[0].amount = 9;
        //    inv[16] = idSystem.ItemById(1);
        //    VisualizeInv();
        //}
;
        if(Input.GetKeyDown(KeyCode.Tab) && numOfSlotInBP != 0)
        {
            bp_change_later();
        }

    }
    void bp_change_later()
    {
        isBPActive = !isBPActive;
        BP.SetActive(isBPActive);
        VisualizeInvBP();
    }
    
    void VisualizeInvBP() //Visualizing BP item in slot (amount/sprite)
    {
        for (int i = 0; i < inv.Count- numOfSlot; i++)
        {
            if (inv[i+ numOfSlot].id != 0)
            {
                if (inv[i+ numOfSlot].amount == 0)
                {
                    inv[i + numOfSlot] = nullItem;
                    slotsBP[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = " ";
                    slotsBP[i].GetComponent<Image>().sprite = null;
                }
                else
                {
                    slotsBP[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = inv[i + numOfSlot].amount.ToString();
                    slotsBP[i].GetComponent<Image>().sprite = inv[i+ numOfSlot].sprite;
                }
            }
        }
    }

    void VisualizeInv() //Visualizing inventory item in slot (amount/sprite)
    {
        for (int i = 0; i < numOfSlot; i++)
        {
            if (inv[i].id != 0)
            {
                if (inv[i].amount == 0)
                {
                    inv[i] = nullItem;
                    slots[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = " ";
                    slots[i].GetComponent<Image>().sprite = null;
                }
                else
                {
                    slots[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = inv[i].amount.ToString();
                    slots[i].GetComponent<Image>().sprite = inv[i].sprite;
                }
            }
        }
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < numOfSlot; i++)
        {
            if (inv.ElementAt(i).id == item.id)
            {
                inv[i].amount += item.amount;
                VisualizeInv();
                return true;
            }
        }
        for (int i = 0; i < numOfSlot; i++)
        {
            if (inv.ElementAt(i) == nullItem)
            {
                inv[i] = item;
                VisualizeInv();
                return true;
            }
        }
        return false;
    }

    //public void RemoveItem(Item item) { for (int i = 0; i < numOfSlot; i++) { inv[i] = (inv.ElementAt(i) == item) ? (inv[i] = (inv[i].amount <= 1) ? nullItem : new Item(inv[i].id, inv[i].amount - 1)) : inv[i]; } VisualizeInv(); }
}