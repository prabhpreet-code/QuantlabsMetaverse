using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
    public static List<Sprite> inventoryItems = new List<Sprite>();
    public static int shoppingBalance = 20;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] Transform inventoryScrollView;
    [SerializeField] private Text balanceText;

    [SerializeField] private GameObject noMoneyText;

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
        LoadBalance();
    }

    public void AddToInventory(Sprite inputNFT, int inputbalance)
    {
        shoppingBalance -= inputbalance;
        inventoryItems.Add(inputNFT);

        balanceText.GetComponent<Text>().text = shoppingBalance.ToString() + " $";
        itemPrefab.transform.GetChild(0).GetComponent<Image>().sprite = inputNFT;

        Instantiate(itemPrefab, inventoryScrollView);
    }

    public void LoadItems()
    {
        noMoneyText.SetActive(false);

        if (inventoryItems.Count != 0)
        {
            foreach (Sprite item in inventoryItems)
            {
                itemPrefab.transform.GetChild(0).GetComponent<Image>().sprite = item;
                Instantiate(itemPrefab, inventoryScrollView);
            }
        }
    }

    public void LoadBalance()
    {
        balanceText.GetComponent<Text>().text = shoppingBalance.ToString() + " $";
    }

    public bool CheckBalance(int itemPrice)
    {
        bool acceptable;

        if (itemPrice > shoppingBalance)
        {
            acceptable = false;

            noMoneyText.SetActive(true);
        }
        else
        {
            acceptable = true;
        }

        return acceptable;
    }
}
