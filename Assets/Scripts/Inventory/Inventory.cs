using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Camera mCamera;
    IdSystem idSystem;

    [SerializeField] int numOfSlot;

    public List<Item> inv = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();
    Item nullItem = new Item();

    bool justStarted = false; // for a bug wich I don't know how to fix(
    void Start()
    {
        idSystem = this.gameObject.GetComponent<IdSystem>();

        //Image tempIMG = slot.GetComponent<Image>();
        //Color newColor = tempIMG.color;
        //newColor.a = 0.39f;
        //tempIMG.color = newColor;
        inv.Clear();
        for (int i = 0; i < numOfSlot; i++)
        {
            Item item = idSystem.ItemById(i);
            item.amount = 1;
            inv.Add(item);
        }
        justStarted = true;
        VisualizeInv();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            for (int i = 0; i < numOfSlot; i++)
                if (IsPointerOverGameObject(slots[i]))
                    inv[i] = nullItem;
        VisualizeInv();
        //if (justStarted)//if you want to add some items at the start
        //{
        //    justStarted = false;
        //    inv[0] = idSystem.ItemById(1);
        //    inv[0].amount = 9;
        //    inv[16] = idSystem.ItemById(1);
        //    VisualizeInv();
        //}

    }
    public static bool IsPointerOverGameObject(GameObject gameObject)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults.Any(x => x.gameObject == gameObject);
    }
    void VisualizeInv() //Visualizing inventory item in slot (amount/sprite)
    {
        for (int i = 0; i < numOfSlot; i++)
        {
            //Debug.Log(inv[i].sprite);
            if (inv[i].id != -1)
            {
                if (inv[i].amount == 0)
                {
                    inv[i] = nullItem;
                    slots[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = " ";
                    slots[i].GetComponent<Image>().sprite = inv[i].sprite;
                }
                else
                {
                    slots[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = inv[i].amount.ToString();
                    slots[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + inv[i].title);
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