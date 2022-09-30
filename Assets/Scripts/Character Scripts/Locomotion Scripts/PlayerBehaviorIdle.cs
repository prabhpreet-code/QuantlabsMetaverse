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

    }

    void IPlayerBehavior.FixedUpdate(Player player)
    {
        player.playerDirection.x = 0f;
        player.playerDirection.z = 0f;

        player.playerDirection += Physics.gravity * Time.deltaTime;
        player.controller.Move(player.playerDirection * Time.deltaTime);
    }
}
