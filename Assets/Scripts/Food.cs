using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food
{
    string foodName { get; set; }
    int foodNum { get; set; }

    public Food(string foodNameGiven, int foodNumGiven)
    {
        foodName = foodNameGiven;
        foodNum = foodNumGiven; 
    }

}
