using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorSeat : IPlayerBehavior
{
    private float _seatRange = 1f;
    private float _seatOffset = 90f;
    private Vector3 _axisRotation;

    private LayerMask chairLayer;

    private GameObject currentChair;

    void IPlayerBehavior.Enter(Player player)
    {
        chairLayer = LayerMask.GetMask("Chairs");

        Collider[] chairs = Physics.OverlapSphere(player.transform.position, _seatRange, chairLayer);

        foreach (Collider chair in chairs)
        {
            if (!chair.GetComponent<Chair>().IsBusy)
            {
                currentChair = chair.gameObject;
                break;
            }
        }

        if (currentChair != null)
        {
            player.animator.SetBool("isSeating", true);

            currentChair.GetComponent<Chair>().IsBusy = true;

            _axisRotation.x = player.transform.eulerAngles.x;
            _axisRotation.y = currentChair.transform.eulerAngles.y + _seatOffset;
            _axisRotation.z = player.transform.eulerAngles.z;

            player.transform.eulerAngles = _axisRotation;
            player.transform.position = currentChair.transform.position;
        }
        
    }

    void IPlayerBehavior.Exit(Player player)
    {
        if (currentChair != null)
        {
            player.animator.SetBool("isSeating", false);
            currentChair.GetComponent<Chair>().IsBusy = false;
            currentChair = null;
        }
    }

    void IPlayerBehavior.Update(Player player)
    {

    }

    void IPlayerBehavior.FixedUpdate(Player player)
    {

    }
}
