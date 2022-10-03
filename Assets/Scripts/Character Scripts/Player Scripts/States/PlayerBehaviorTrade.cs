using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorTrade : IPlayerBehavior
{
    private float _shopkeeperRange = 2f;

    private LayerMask _shopkeeperLayer;

    private GameObject currentShopkeeper;

    void IPlayerBehavior.Enter(Player player)
    {
        _shopkeeperLayer = LayerMask.GetMask("Shopkeepers");

        Collider[] shopkeepers = Physics.OverlapSphere(player.transform.position, _shopkeeperRange, _shopkeeperLayer);

        foreach (Collider shopkeeper in shopkeepers)
        {
            currentShopkeeper = shopkeeper.gameObject;
            break;
        }

        if (currentShopkeeper != null)
        {
            currentShopkeeper.GetComponent<Shopkeeper>().SetBehaviorTrade();

            player.animator.SetBool("isTrading", true);
            player.isTrading = true;

            //Rotating player towards shopkeeper
            player.transform.LookAt(currentShopkeeper.transform.position);

            //Rotating shopkeeper towards player
            currentShopkeeper.transform.LookAt(player.transform.position);

        } else
        {
            player.SetBehaviorIdle();
        }
    }

    void IPlayerBehavior.Exit(Player player)
    {
        if (currentShopkeeper != null)
        {
            player.animator.SetBool("isTrading", false);
            player.isTrading = false;


            currentShopkeeper.GetComponent<Shopkeeper>().SetBehaviorIdle();
            currentShopkeeper = null;
        }
        
    }

    void IPlayerBehavior.Update(Player player)
    {
        if (Input.GetKeyDown("t") || Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            player.SetBehaviorIdle();
        }
    }

    void IPlayerBehavior.FixedUpdate(Player player)
    {
        
    }
}
