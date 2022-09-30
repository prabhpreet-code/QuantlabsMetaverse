using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorTrade : IPlayerBehavior
{
    void IPlayerBehavior.Enter(Player player)
    {
        player.animator.SetBool("isTrading", true);
    }

    void IPlayerBehavior.Exit(Player player)
    {
        player.animator.SetBool("isTrading", false);
    }

    void IPlayerBehavior.Update(Player player)
    {

    }

    void IPlayerBehavior.FixedUpdate(Player player)
    {
        
    }
}
