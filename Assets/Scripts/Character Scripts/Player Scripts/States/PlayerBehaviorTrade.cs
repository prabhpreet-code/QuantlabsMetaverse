using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PlayerBehaviorTrade : IPlayerBehavior
{
    private float _shopkeeperRange = 3f;

    private LayerMask _shopkeeperLayer;

    private GameObject currentShopkeeper;

    void IPlayerBehavior.Enter(Player player, InterfaceManager interfaceManager)
    {
        _shopkeeperLayer = LayerMask.GetMask("Shopkeepers");

        Collider[] shopkeepers = Physics.OverlapSphere(player.transform.position, _shopkeeperRange, _shopkeeperLayer);

        foreach (Collider shopkeeper in shopkeepers)
        {
            if (!shopkeeper.GetComponent<Shopkeeper>().isSeating)
            {
                currentShopkeeper = shopkeeper.gameObject;
                break;
            }
        }

        if (currentShopkeeper != null)
        {
            //Unlocking cursor
            Cursor.lockState = CursorLockMode.None;

            currentShopkeeper.GetComponent<Shopkeeper>().SetBehaviorTrade();

            player.animator.SetBool("isTrading", true);
            player.isTrading = true;

            //Rotating player towards shopkeeper
            player.transform.LookAt(currentShopkeeper.transform.position);

            //Rotating shopkeeper towards player
            currentShopkeeper.transform.LookAt(player.transform.position);

            //Managing UI
            interfaceManager.inventoryPanel.SetActive(true);
            interfaceManager.shopPanel.SetActive(true);

        } else
        {
            player.SetBehaviorIdle();
        }
    }

    void IPlayerBehavior.Exit(Player player, InterfaceManager interfaceManager)
    {
        if (currentShopkeeper != null)
        {
            //Locking cursor
            Cursor.lockState = CursorLockMode.Locked;

            player.animator.SetBool("isTrading", false);
            player.isTrading = false;

            currentShopkeeper.GetComponent<Shopkeeper>().SetBehaviorIdle();
            currentShopkeeper = null;

            //Managing UI
            interfaceManager.inventoryPanel.SetActive(false);
            interfaceManager.shopPanel.SetActive(false);
        }
        
    }

    void IPlayerBehavior.Update(Player player, InterfaceManager interfaceManager)
    {
        if (Input.GetKeyDown("t") || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            player.SetBehaviorIdle();
        }
    }

    void IPlayerBehavior.FixedUpdate(Player player, InterfaceManager interfaceManager)
    {
        
    }
}
