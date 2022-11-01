using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    public static List<Sprite> inventoryItems = new List<Sprite>();

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] Transform inventoryScrollView;

    public static Inventory Instance { get; set;}

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        LoadItems();
    }

    public void AddToInventory(Sprite inputNFT)
    {
        itemPrefab.transform.GetChild(0).GetComponent<Image>().sprite = inputNFT;
        Instantiate(itemPrefab, inventoryScrollView);
    }

    public void LoadItems()
    {
        if (inventoryItems.Count != 0)
        {
            foreach (Sprite item in inventoryItems)
            {
                itemPrefab.transform.GetChild(0).GetComponent<Image>().sprite = item;
                Instantiate(itemPrefab, inventoryScrollView);
            }
        }
    }
}
