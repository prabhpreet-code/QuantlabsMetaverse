using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopkeeperBehavior
{
    void Enter(Shopkeeper shopkeeper);
    void Exit(Shopkeeper shopkeeper);
    void Update(Shopkeeper shopkeeper);
    void FixedUpdate(Shopkeeper shopkeeper);
}
