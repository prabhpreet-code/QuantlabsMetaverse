using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InterfaceManager : MonoBehaviourPunCallbacks
{
    [SerializeField] public GameObject shopPanel;
    [SerializeField] public GameObject inventoryPanel;
    [SerializeField] public GameObject controlsPanel;
 
    public void InteractWithInventory()
    {
        if (inventoryPanel.activeInHierarchy)
        {
            inventoryPanel.SetActive(false);
        }
        else inventoryPanel.SetActive(true);
    }

    public void InteractWithControls()
    {
        if (controlsPanel.activeInHierarchy)
        {
            controlsPanel.SetActive(false);
        }
        else controlsPanel.SetActive(true);
    }
}
