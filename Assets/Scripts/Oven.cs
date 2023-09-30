using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven
{
    public GameObject OvenSlots { get; set; }
    public Item OvenInerSlot1 { get; set; }
    public Item OvenInerSlot2 { get; set; }
    public Item OvenInerSlot3 { get; set; }
    public GameObject Outcome { get; set; }
    public int OvenSlotsState { get; set; }
    public Oven(GameObject ovenSlots, Item ovenInerSlot1, Item ovenInerSlot2, Item ovenInerSlot3, GameObject outcome, int ovenSlotsState)
    {
        OvenSlots = ovenSlots;
        OvenInerSlot1 = ovenInerSlot1;
        OvenInerSlot2 = ovenInerSlot2;
        OvenInerSlot3 = ovenInerSlot3;
        Outcome = outcome;
        OvenSlotsState = ovenSlotsState;
    }
}
