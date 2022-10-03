using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperBehaviorTrade : IShopkeeperBehavior
{
    private Quaternion _initialPos;

    void IShopkeeperBehavior.Enter(Shopkeeper shopkeeper)
    {
        shopkeeper.animator.SetBool("isTrading", true);
        shopkeeper.isTrading = true;

        _initialPos = shopkeeper.transform.rotation;
    }

    void IShopkeeperBehavior.Exit(Shopkeeper shopkeeper)
    {
        shopkeeper.animator.SetBool("isTrading", false);
        shopkeeper.isTrading = false;

        shopkeeper.transform.rotation = _initialPos;
    }

    void IShopkeeperBehavior.Update(Shopkeeper shopkeeper)
    {

    }

    void IShopkeeperBehavior.FixedUpdate(Shopkeeper shopkeeper)
    {

    }
}
