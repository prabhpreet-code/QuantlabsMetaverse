using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public List<GameObject> inventoryItems;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] Transform inventoryScrollView;

    public static Inventory Instance { get; set;}

    private void Awake()
    {
        Debug.Log("HI");

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Instance.LoadInventory();
    }

    public void AddToInventory(Sprite inputNFT)
    {
        
        itemPrefab.transform.GetChild(0).GetComponent<Image>().sprite = inputNFT;
        inventoryItems.Add(itemPrefab);
        Instantiate(itemPrefab, inventoryScrollView);
    }

    public void LoadInventory()
    {
        Debug.Log("Method has started");
        foreach (GameObject item in inventoryItems)
        {
            Debug.Log("Method has once again");
            Instantiate(item, inventoryScrollView);
        }
    }
}
