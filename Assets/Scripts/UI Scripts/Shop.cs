using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviourPunCallbacks
{
    [System.Serializable] class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool isPurchased = false;
    }

    [SerializeField] List<ShopItem> ShopItemsList;

    GameObject itemTemplate;
    GameObject g;
    [SerializeField] Transform shopScrollView;

    Button buyBtn;

    private void Start()
    {
        itemTemplate = shopScrollView.GetChild(0).gameObject;

        int len = ShopItemsList.Count;

        for (int i = 0; i < len; i++)
        {
            g = Instantiate(itemTemplate, shopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = ShopItemsList[i].Price.ToString();


            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            buyBtn.interactable = !ShopItemsList[i].isPurchased;
            buyBtn.AddEventListener(i, OnShopButtonClicked);
        }

        Destroy(itemTemplate);
    }

    void OnShopButtonClicked(int itemIndex)
    {
        ShopItemsList[itemIndex].isPurchased = true;

        buyBtn = shopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
        buyBtn.interactable = false;

        buyBtn.transform.GetChild(0).GetComponent<Text>().text = "Purchased";

        //Adding Item to ScrollView and static inventory list
        Inventory.Instance.AddToInventory(ShopItemsList[itemIndex].Image);
        Inventory.inventoryItems.Add(ShopItemsList[itemIndex].Image);
    }
}
