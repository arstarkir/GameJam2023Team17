using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using UnityEngine.XR;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class Inventory : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Camera mCamera;
    [SerializeField] GameObject newOrder;
    public List<Item> OrderItems = new List<Item>();
    public List<GameObject> OrderSlots = new List<GameObject>();
    public List<Oven> oven = new List<Oven>();

    public List<GameObject> OvenSlots = new List<GameObject>();
    public List<GameObject> Outcome = new List<GameObject>();
    public List<int> OvenSlotsState = new List<int>();
    public List<GameObject> iner1 = new List<GameObject>();
    public List<GameObject> iner2 = new List<GameObject>();
    public List<GameObject> iner3 = new List<GameObject>();

    public List<Sprite> OvenSprites = new List<Sprite>();
    public List<Sprite> OvenOutcomeSprites = new List<Sprite>();

    public Sprite questionMark;
    IdSystem idSystem;

    [SerializeField] int numOfSlot;
    public List<Item> inv = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();
    Item nullItem = new Item();
    Item inHand = new Item();
    Coroutine showHide, showHideSmall;
    List<Coroutine> ovenTimers = new List<Coroutine>();
    Coroutine nullCor = null;
    bool justStarted = false; // for a bug wich I don't know how to fix(
    void Start()
    {
        ovenTimers.Add(nullCor);
        ovenTimers.Add(nullCor);
        ovenTimers.Add(nullCor);
        for (int i = 0; i < 3; i++)
        {
            Oven ovenTemp = new Oven(OvenSlots[i], iner1[i], iner2[i], iner3[i], Outcome[i], OvenSlotsState[i]);
            oven.Add(ovenTemp);
        }
        newOrder.SetActive(false);
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
        NewOrder();
    }

    void NewOrder()
    {
        Item newOrderItem = idSystem.ARcipe();
        foreach (Transform childTemp in newOrder.transform)
        {
            GameObject child = childTemp.gameObject;
            if (child.name == "FoodIcon")
                child.GetComponent<Image>().sprite = newOrderItem.sprite;
            if (child.name == "FoodName")
                child.GetComponent<TMPro.TextMeshProUGUI>().text = idSystem.ItemById(newOrderItem.component1ID).title + " " + newOrderItem.title;
            if (child.name == "PictureOfAComponent1")
                child.GetComponent<Image>().sprite = idSystem.ItemById(newOrderItem.component1ID).sprite;
            if (child.name == "PictureOfAComponent2")
                child.GetComponent<Image>().sprite = idSystem.ItemById(newOrderItem.component2ID).sprite;
            if (child.name == "PictureOfAComponent3")
                child.GetComponent<Image>().sprite = idSystem.ItemById(newOrderItem.component3ID).sprite;
            if (child.name == "NameOfAComponent1")
                child.GetComponent<TMPro.TextMeshProUGUI>().text = idSystem.ItemById(newOrderItem.component1ID).title;
            if (child.name == "NameOfAComponent2")
                child.GetComponent<TMPro.TextMeshProUGUI>().text = idSystem.ItemById(newOrderItem.component2ID).title;
            if (child.name == "NameOfAComponent3")
                child.GetComponent<TMPro.TextMeshProUGUI>().text = idSystem.ItemById(newOrderItem.component3ID).title;
        }
        newOrder.SetActive(true);
        showHide = StartCoroutine(ShowHide(newOrderItem));
    }
    void VisualizeOrder() //Visualizing inventory item in slot (amount/sprite)
    {
        for (int i = 0; i < OrderItems.Count; i++)
        {
            OrderSlots[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + OrderItems[i].title);
            Color newColor = OrderSlots[i].GetComponent<Image>().color;
            newColor.a = 1;
            OrderSlots[i].GetComponent<Image>().color = newColor;
        }
    }
    IEnumerator ShowHide(Item newOrderItem)
    {
        float c = newOrder.GetComponent<CanvasGroup>().alpha;
        for (float alpha = 0f; alpha < 1; alpha += 0.1f)
        {
            c = alpha;
            newOrder.GetComponent<CanvasGroup>().alpha = c;
            yield return new WaitForSeconds(.1f);
        }
        OrderItems.Add(newOrderItem);
        VisualizeOrder();
        yield return new WaitForSeconds(3f);
        for (float alpha = 1f; alpha > 0; alpha -= 0.2f)
        {
            c = alpha;
            newOrder.GetComponent<CanvasGroup>().alpha = c;
            yield return new WaitForSeconds(.1f);
        }
        showHide = null;
    }
    void OldOrder(int i)
    {
        Item newOrderItem = OrderItems[i];
        foreach (Transform childTemp in newOrder.transform)
        {
            GameObject child = childTemp.gameObject;
            Debug.Log(child);
            if (child.name == "FoodIcon")
                child.GetComponent<Image>().sprite = newOrderItem.sprite;
            if (child.name == "FoodName")
                child.GetComponent<TMPro.TextMeshProUGUI>().text = idSystem.ItemById(newOrderItem.component1ID).title + " " + newOrderItem.title;
            if (child.name == "PictureOfAComponent1")
                child.GetComponent<Image>().sprite = idSystem.ItemById(newOrderItem.component1ID).sprite;
            if (child.name == "PictureOfAComponent2")
                child.GetComponent<Image>().sprite = idSystem.ItemById(newOrderItem.component2ID).sprite;
            if (child.name == "PictureOfAComponent3")
                child.GetComponent<Image>().sprite = idSystem.ItemById(newOrderItem.component3ID).sprite;
            if (child.name == "NameOfAComponent1")
                child.GetComponent<TMPro.TextMeshProUGUI>().text = idSystem.ItemById(newOrderItem.component1ID).title;
            if (child.name == "NameOfAComponent2")
                child.GetComponent<TMPro.TextMeshProUGUI>().text = idSystem.ItemById(newOrderItem.component2ID).title;
            if (child.name == "NameOfAComponent3")
                child.GetComponent<TMPro.TextMeshProUGUI>().text = idSystem.ItemById(newOrderItem.component3ID).title;
        }
        newOrder.SetActive(true);
        showHideSmall = StartCoroutine(ShowHideSmall(newOrderItem));
    }
    IEnumerator ShowHideSmall(Item newOrderItem)
    {
        float c = newOrder.GetComponent<CanvasGroup>().alpha;
        for (float alpha = 0f; alpha < 1; alpha += 0.2f)
        {
            c = alpha;
            newOrder.GetComponent<CanvasGroup>().alpha = c;
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(1f);
        for (float alpha = 1f; alpha > 0; alpha -= 0.2f)
        {
            c = alpha;
            newOrder.GetComponent<CanvasGroup>().alpha = c;
            yield return new WaitForSeconds(.1f);
        }
        showHide = null;
    }
    void OvenWorks(int i)
    {
        if (oven[i].Iner1.GetComponent<Image>().sprite == null)
        {
            oven[i].Iner1.GetComponent<Image>().sprite = inHand.sprite;
            Color newColor = oven[i].Iner1.GetComponent<Image>().color;
            newColor.a = 1;
            oven[i].Iner1.GetComponent<Image>().color = newColor;
            oven[i].OvenInerSlot1 = inHand;
            inHand = null;
        }
        else
        if (oven[i].Iner2.GetComponent<Image>().sprite == null)
        {
            oven[i].Iner2.GetComponent<Image>().sprite = inHand.sprite;
            Color newColor = oven[i].Iner2.GetComponent<Image>().color;
            newColor.a = 1;
            oven[i].Iner2.GetComponent<Image>().color = newColor;
            oven[i].OvenInerSlot2 = inHand;
            inHand = null;
        }
        else
        if (oven[i].Iner3.GetComponent<Image>().sprite == null)
        {
            oven[i].Iner3.GetComponent<Image>().sprite = inHand.sprite;
            Color newColor = oven[i].Iner3.GetComponent<Image>().color;
            newColor.a = 1;
            oven[i].Iner3.GetComponent<Image>().color = newColor;
            oven[i].OvenInerSlot3 = inHand;
            inHand = null;
        }
    }
    void TakeFromOven(int i)
    {
        StopCoroutine(ovenTimers[i]);
        ovenTimers[i] = null;
        Color newColor = oven[i].Outcome.GetComponent<Image>().color;
        newColor.a = 0;
        oven[i].Outcome.GetComponent<Image>().color = newColor;
        oven[i].Iner3.GetComponent<Image>().sprite = null;
        oven[i].Iner3.GetComponent<Image>().color = newColor;
        oven[i].Iner2.GetComponent<Image>().sprite = null;
        oven[i].Iner2.GetComponent<Image>().color = newColor;
        oven[i].Iner1.GetComponent<Image>().sprite = null;
        oven[i].Iner1.GetComponent<Image>().color = newColor;
        oven[i].OvenSlots.GetComponent<Image>().sprite = OvenSprites[0];
    }
    void QTE(int i)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverGameObject(oven[i].OvenSlots))
                TakeFromOven(i);
            if (IsPointerOverGameObject(oven[i].Outcome))
                TakeFromOven(i);
            if (IsPointerOverGameObject(oven[i].Iner1))
                TakeFromOven(i);
            if (IsPointerOverGameObject(oven[i].Iner2))
                TakeFromOven(i);
            if (IsPointerOverGameObject(oven[i].Iner3))
                TakeFromOven(i);
        }
    }

    IEnumerator OvenTimer(int i)
    {
        Color newColor = oven[i].Outcome.GetComponent<Image>().color;
        newColor.a = 1;
        oven[i].Outcome.GetComponent<Image>().color = newColor;
        oven[i].Outcome.GetComponent<Image>().sprite = OvenOutcomeSprites[0];
        oven[i].OvenSlotsState = oven[i].OvenSlotsState + 1;
        oven[i].OvenSlots.GetComponent<Image>().sprite = OvenSprites[1];
        for (int j = 0; j < 10; j++)
        {
            QTE(i);
            yield return new WaitForSeconds(0.2f);
        }
        if (idSystem.CheckTheRcipe(oven[i].OvenInerSlot1.id, oven[i].OvenInerSlot2.id, oven[i].OvenInerSlot3.id).sprite == null)
            oven[i].Outcome.GetComponent<Image>().sprite = questionMark;
        else
            oven[i].Outcome.GetComponent<Image>().sprite = idSystem.CheckTheRcipe(oven[i].OvenInerSlot1.id, oven[i].OvenInerSlot2.id, oven[i].OvenInerSlot3.id).sprite; 
        oven[i].OvenSlotsState = oven[i].OvenSlotsState + 1;
        oven[i].OvenSlots.GetComponent<Image>().sprite = OvenSprites[2];
        for (int j = 0; j < 10; j++)
        {
            QTE(i);
            yield return new WaitForSeconds(0.1f);
        }
        oven[i].OvenSlotsState = oven[i].OvenSlotsState + 1;
        oven[i].OvenSlots.GetComponent<Image>().sprite = OvenSprites[3];
        oven[i].Outcome.GetComponent<Image>().sprite = OvenOutcomeSprites[1];
        bool TakeOutBurn = false;
        while (TakeOutBurn == false)
        {
            QTE(i);
            yield return null;
        }

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            for (int i = 0; i < numOfSlot; i++)
                if (IsPointerOverGameObject(slots[i]))
                    inHand = inv[i];
        if (Input.GetMouseButtonDown(0) && showHide == null)
            for (int i = 0; i < OrderSlots.Count; i++)
                if (IsPointerOverGameObject(OrderSlots[i]) && i < OrderItems.Count)
                    OldOrder(i);
        if (Input.GetMouseButtonDown(0) && inHand != null)
            for (int i = 0; i < oven.Count; i++)
                if (IsPointerOverGameObject(oven[i].OvenSlots) && oven[i].OvenSlotsState == 0)
                    OvenWorks(i);
        for (int i = 0; i < oven.Count; i++)
            OvenChecker(i);

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
    void OvenChecker(int i)
    {
        if (ovenTimers[i] == null && oven[i].Iner1.GetComponent<Image>().sprite != null
            && oven[i].Iner2.GetComponent<Image>().sprite != null 
            && oven[i].Iner3.GetComponent<Image>().sprite != null)
        {
            ovenTimers[i] = StartCoroutine(OvenTimer(i));
            Debug.Log("Something");
        }
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
        for (int i = 0; i < inv.Count; i++)
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