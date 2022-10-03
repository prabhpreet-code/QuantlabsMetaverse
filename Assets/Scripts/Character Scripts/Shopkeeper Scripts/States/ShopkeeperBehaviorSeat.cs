using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperBehaviorSeat : IShopkeeperBehavior
{
    private float _seatOffset = 90f;
    private LayerMask _chairLayer;

    private GameObject _currentChair;

    private Vector3 _axisRotation;

    void IShopkeeperBehavior.Enter(Shopkeeper shopkeeper)
    {
        shopkeeper.animator.applyRootMotion = false;
        shopkeeper.animator.SetBool("isSeating", true);

        _chairLayer = LayerMask.GetMask("Chairs");

        Collider[] chairs = Physics.OverlapSphere(shopkeeper.transform.position, 1f, _chairLayer);

        foreach (Collider chair in chairs)
        {
            if (!chair.GetComponent<Chair>().IsBusy)
            {
                _currentChair = chair.gameObject;
                break;
            }
        }

        
        _currentChair.GetComponent<Chair>().IsBusy = true;

        _axisRotation.x = shopkeeper.transform.eulerAngles.x;
        _axisRotation.y = _currentChair.transform.eulerAngles.y + _seatOffset;
        _axisRotation.z = shopkeeper.transform.eulerAngles.z;

        shopkeeper.transform.eulerAngles = _axisRotation;
        shopkeeper.transform.position = _currentChair.transform.position;

    }

    void IShopkeeperBehavior.Exit(Shopkeeper shopkeeper)
    {
        shopkeeper.animator.SetBool("isSeating", false);
        shopkeeper.animator.applyRootMotion = true;
    }

    void IShopkeeperBehavior.Update(Shopkeeper shopkeeper)
    {

    }

    void IShopkeeperBehavior.FixedUpdate(Shopkeeper shopkeeper)
    {

    }
}
