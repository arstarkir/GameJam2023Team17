using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using UnityEngine.Rendering.VirtualTexturing;
using static UnityEditor.Progress;

[ExecuteInEditMode] //needed for future conveniences
public class IdSystem : MonoBehaviour
{
    public List<Item> idSystem = new List<Item>();
    private List<string> itemData = new List<string>();
    void OnValidate() //needed for future conveniences
    {
        readTextFile("Assets/Items.txt");
        ConstructItemDatabase();
    }
    void Start()
    {
        idSystem.Clear();
        itemData.Clear();
        readTextFile("Assets/Items.txt");
        ConstructItemDatabase();
    }
    void readTextFile(string file_path)
    {
        StreamReader inp_stm = new StreamReader(file_path);

        while (!inp_stm.EndOfStream)
        {
            itemData.Add(inp_stm.ReadLine());
        }

        inp_stm.Close();
    }

    public Item ItemById(int id)
    {
        for (int i = 0; i < idSystem.Count; i++)
        {
           
            if (idSystem[i].id == id)
            {
                return idSystem[i];
            }
        }

        return null;
    }
    public Item ItemByName(string title)
    {
        for (int i = 0; i < idSystem.Count; i++)
        {

            if (idSystem[i].title == title)
            {
                return idSystem[i];
            }
        }

        return null;
    }
    public Item ARcipe()
    {
        List<Item> rcipes = new List<Item>();
        for (int i = 0; i < idSystem.Count; i++)
            if(idSystem[i].isOrder)
                rcipes.Add(idSystem[i]);
        if (rcipes.Count == 0)
            return null;
        return rcipes[UnityEngine.Random.Range(0, rcipes.Count-1)];
    }
    public int NumOfItems()
    {
        return idSystem.Count;
    }
    public Item CheckTheRcipe(int id1, int id2, int id3)
    {
        for (int i = 0; i < idSystem.Count; i++)
        {

            if (idSystem[i].isOrder == true)
            {
                if ((idSystem[i].component1ID == id1 && idSystem[i].component2ID == id2 && idSystem[i].component3ID == id3) ||
                    (idSystem[i].component1ID == id1 && idSystem[i].component2ID == id3 && idSystem[i].component3ID == id2) ||
                    (idSystem[i].component1ID == id2 && idSystem[i].component2ID == id1 && idSystem[i].component3ID == id3) ||
                    (idSystem[i].component1ID == id2 && idSystem[i].component2ID == id3 && idSystem[i].component3ID == id1) ||
                    (idSystem[i].component1ID == id3 && idSystem[i].component2ID == id1 && idSystem[i].component3ID == id2) ||
                    (idSystem[i].component1ID == id3 && idSystem[i].component2ID == id2 && idSystem[i].component3ID == id1))
                    return idSystem[i];
            }
        }
        Item temp = new Item();
        temp.title = "Nothing";
        return temp;
    }
    void ConstructItemDatabase()
    {
        for (int j = 0; j < itemData.Count; j++)
        {
            var matches = Regex.Matches(itemData[j], @"\w+[^\s]*\w+|\w");

            Item newItem = new Item();
            int i = 0;
            foreach (Match match in matches)
            {
                var part = match.Value;
                switch (i)
                {
                    case 0:
                        newItem.id = Int32.Parse(part);
                        break;
                    case 1:
                        newItem.title = part.ToString();
                        break;
                    case 2:
                        if (part == "1")
                        {
                            newItem.sprite = Resources.Load<Sprite>("Sprites/" + newItem.title); // file of the sprite should be in folder "Assets/Resources/Sprites/Items/"
                        }
                        break;
                    case 3:
                        if (part == "1")
                            newItem.isOrder = true;
                        else newItem.isOrder = false;
                        break;
                    case 4:
                        if (newItem.isOrder == true)
                            newItem.points = float.Parse(part);
                        break;
                    case 5:
                        if (newItem.isOrder == true)
                            newItem.component1ID = int.Parse(part);
                        break;
                    case 6:
                        if (newItem.isOrder == true)
                            newItem.component2ID = int.Parse(part);
                        break;
                    case 7:
                        if (newItem.isOrder == true)
                        {
                            newItem.component3ID = int.Parse(part);
                        }
                        break;
                }   
                i++;
            }

            idSystem.Add(newItem);
        }
    }
}

public class Item
{
    public int id { get; set; }
    public string title { get; set; } // title = the name of a sprite and gameobject
    public int amount { get; set; }
    public Sprite sprite { get; set; }
    public bool isOrder { get; set; }
    public int component1ID { get; set; }
    public int component2ID { get; set; }
    public int component3ID { get; set; }
    public float points { get; set; }
    IdSystem idSystem;
    public Item(int givenItemID = 0, int givenAmount = 0, string givenTitel = null, Sprite givenSprite = null, float givenCost = 0, int givenComponent1ID = -1, int givenComponent2ID = -1, int givenComponent3ID = -1)
    {
        //Debug.Log("component3ID " + givenComponent3ID);
        points = givenCost;
        amount = givenAmount;
        id = givenItemID;
        title = givenTitel;
        sprite = givenSprite;
        component1ID = givenComponent1ID;
        component2ID = givenComponent2ID;
        component3ID = givenComponent3ID;
    }
    public Item(Item item)
    {
        id = item.id;
        title = item.title;
        sprite = item.sprite;
        points = item.points;
        amount = item.amount;
        isOrder = item.isOrder;
        component1ID = item.component1ID;
        component2ID = item.component2ID;
        component3ID = item.component3ID;
    }
}