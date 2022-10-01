using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorIdle : IPlayerBehavior
{
    void IPlayerBehavior.Enter(Player player)
    {

    }

    void IPlayerBehavior.Exit(Player player)
    {

    }

    void IPlayerBehavior.Update(Player player)
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && !player.isTrading && !player.isSeating)
            player.SetBehaviorWalk();

        if (Input.GetKeyDown("t"))
        {
            player.SetBehaviorTrade();
            player.isTrading = true;
        }

        if (Input.GetKeyDown("f"))
        {
            player.SetBehaviourSeat();
            player.isSeating = true;
        }
    }

    void IPlayerBehavior.FixedUpdate(Player player)
    {
        player.playerDirection.x = 0f;
        player.playerDirection.z = 0f;

        player.playerDirection += Physics.gravity * Time.deltaTime;
        player.controller.Move(player.playerDirection * Time.deltaTime);
    }
}
