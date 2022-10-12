using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public Button inventoryButton;
    public GameObject inventoryPanel;
    public GameObject shopPanel;

    private void Awake()
    {
        LoadUI();
    }

    private void LoadUI()
    {
        inventoryButton.onClick.AddListener(LoadInventory);
    }

    public void LoadInventory()
    {
        if (inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(false);
        }
        else inventoryPanel.SetActive(true);
    }
}
