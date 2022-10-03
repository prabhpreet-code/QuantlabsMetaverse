using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperBehaviorIdle : IShopkeeperBehavior
{
    private float _sadTimeRemaining = 20f;

    void IShopkeeperBehavior.Enter(Shopkeeper shopkeeper)
    {
        shopkeeper.animator.SetBool("isSad", false);

        if (shopkeeper.isSeating)
            shopkeeper.SetBehaviorSeat();
    }

    void IShopkeeperBehavior.Exit(Shopkeeper shopkeeper)
    {
        shopkeeper.animator.SetBool("isSad", false);
    }

    void IShopkeeperBehavior.Update(Shopkeeper shopkeeper)
    {
        if (_sadTimeRemaining > 0)
        {
            _sadTimeRemaining -= Time.deltaTime;
        } else
        {
            _sadTimeRemaining = 10f;
            shopkeeper.animator.SetBool("isSad", true);
        }
    }

    void IShopkeeperBehavior.FixedUpdate(Shopkeeper shopkeeper)
    {

    }
}
