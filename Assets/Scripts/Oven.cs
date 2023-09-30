using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven
{
    public GameObject OvenSlots { get; set; }
    public Item OvenInerSlot1 { get; set; }
    public GameObject Iner1 { get; set; }
    public Item OvenInerSlot2 { get; set; }
    public GameObject Iner2 { get; set; }
    public Item OvenInerSlot3 { get; set; }
    public GameObject Iner3 { get; set; }
    public GameObject Outcome { get; set; }
    public int OvenSlotsState { get; set; }
    public Oven(GameObject ovenSlots, GameObject iner1, GameObject iner2, GameObject iner3, GameObject outcome, int ovenSlotsState)
    {
        OvenSlots = ovenSlots;
        OvenInerSlot1 = new Item();
        OvenInerSlot2 = new Item();
        OvenInerSlot3 = new Item();
        Iner1 = iner1;
        Iner2 = iner2;
        Iner3 = iner3;
        Outcome = outcome;
        OvenSlotsState = ovenSlotsState;
    }
}
