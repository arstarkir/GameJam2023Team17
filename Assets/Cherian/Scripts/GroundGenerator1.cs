using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator1 : MonoBehaviour
{

    public GameObject[] tileprefabs;
    private Transform playertransform;
    public float spawnZ = -5f;
    public float tilelength = 30.4f;
    public int amntTilesOnScreen = 5;
    public float safezone = 30.0f;
    private List<GameObject> activeTiles = new List<GameObject>();
    public static float t;
    public int tileNumber;

    // Start is called before the first frame update
    void Start()
    {
        t = 0;

        playertransform = GameObject.FindGameObjectWithTag("Player").transform; //Selecting Player
        for (int i = 0; i < amntTilesOnScreen; i++)
        {
            SpawnTile(0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        t += 1 * Time.deltaTime;

        if (t < 3)
        {
            tileNumber = 0;
        }

        else if (t > 3 && t < 15)
        {
            tileNumber = 1;
        }

        else if (t > 15 && t < 35)
        {
            tileNumber = 3;
        }

        else if (t > 35 && t < 55)
        {
            tileNumber = Random.Range(0, 3);
        }

        else if (t > 55 && t < 65)
        {
            tileNumber = 4;
        }

        else if (t > 65 && t < 85)
        {
            tileNumber = 5;
        }

        else if (t > 85 && t < 100)
        {
            tileNumber = 6;
        }

        else if (t > 100 && t < 115)
        {
            tileNumber = 8;
        }

        else if (t > 115 && t < 135)
        {
            tileNumber = 9;
        }

        else if (t > 135 && t < 145)
        {
            tileNumber = Random.Range(6, 8);
        }

        else if (t > 145 && t < 150)
        {
            tileNumber = 6;
        }

        else if (t > 150 && t < 153)
        {
            tileNumber = 10;
        }

        else if (t > 153 && t < 170)
        {
            tileNumber = 11;
        }

        else if (t > 170 && t < 175)
        {
            tileNumber = 6;
        }

        else if (t > 175 && t < 185)
        {
            tileNumber = 12;
        }

        else if (t > 185 && t < 205)
        {
            tileNumber = 13;
        }

        else if (t > 205)
        {
            t = 0;
        }



        if (playertransform.position.z - safezone > (spawnZ - amntTilesOnScreen * tilelength))
        {
            SpawnTile(tileNumber);
            DeleteTile();
        }
    }
    private void SpawnTile(int prefabIndex)
    {
        GameObject go;
        go = Instantiate(tileprefabs[prefabIndex]) as GameObject; //Cloning Object For Specifying Postion
        go.transform.SetParent(transform);  //Local Oreintation
        go.transform.position = (Vector3.forward * spawnZ) + (Vector3.up * 0.0f);  // position property for Setting point to create Object in View
        spawnZ += tilelength;
        activeTiles.Add(go);//Creating Object TO Scene
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0); //Removes the element at 0 from the array.
    }
}

