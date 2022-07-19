using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int tileNumarasi = 5;
    public Transform oyuncuTransform;
    private List<GameObject> activeTiles = new List<GameObject>();

    void Start()
    {
        for(int i = 0 ; i < tileNumarasi ; i++)
        {
            if(i == 0)
                SpawnTile(0);
            else    
                    SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    void Update()
    {
        if(oyuncuTransform.position.z - 35 > zSpawn- (tileNumarasi * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length)); 

            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        
        activeTiles.Add(go);
        zSpawn += tileLength;

    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
