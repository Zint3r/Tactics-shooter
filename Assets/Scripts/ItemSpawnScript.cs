using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Material[] itemMaterial;
    [SerializeField] private string[] itemName;
    [SerializeField] private int[] itemsCount = new int[4];
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float respawnTimer = 2f;
    void Start()
    {
        
    }    
    void Update()
    {
        if (respawnTimer <= 0)
        {
            respawnTimer = 2f;
            ItemSpawn(Random.Range(0, itemMaterial.Length));
        }
        else
        {
            respawnTimer -= Time.deltaTime;
        }
    }
    public void ItemSpawn(int num)
    {
        GameObject newItem = Instantiate(itemPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)]);
        newItem.GetComponentInChildren<MeshRenderer>().material = itemMaterial[num];
        ItemScript item = newItem.GetComponentInChildren<ItemScript>();
        item.ItemPanel.GetComponent<Text>().text = itemName[num];
        item.Count = itemsCount[num];
        item.ItemNumber = num;
    }
}