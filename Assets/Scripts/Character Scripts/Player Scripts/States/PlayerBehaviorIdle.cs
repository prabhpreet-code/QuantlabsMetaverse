using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorIdle : IPlayerBehavior
{
    void IPlayerBehavior.Enter(Player player, InterfaceManager interfaceManager)
    {

    }

    void IPlayerBehavior.Exit(Player player, InterfaceManager interfaceManager)
    {

    }

    void IPlayerBehavior.Update(Player player, InterfaceManager interfaceManager)
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && !player.isTrading && !player.isSeating)
            player.SetBehaviorWalk();

        if (Input.GetKeyDown("t"))
        {
            player.SetBehaviorTrade();
        }

        if (Input.GetKeyDown("f"))
        {
            player.SetBehaviourSeat();
        }
    }

    void IPlayerBehavior.FixedUpdate(Player player, InterfaceManager interfaceManager)
    {
        player.playerDirection.x = 0f;
        player.playerDirection.z = 0f;

        player.playerDirection += Physics.gravity * Time.deltaTime;
        player.controller.Move(player.playerDirection * Time.deltaTime);
    }
}
