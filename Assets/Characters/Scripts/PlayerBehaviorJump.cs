using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviorJump : IPlayerBehavior
{
    private float _jumpHeight = 4f;

    void IPlayerBehavior.Enter(Player player)
    {
        player.animator.SetBool("isJumping", true);
        Debug.Log("JUMPING");
    }

    void IPlayerBehavior.Exit(Player player)
    {
        player.animator.SetBool("isJumping", false);
    }

    void IPlayerBehavior.Update(Player player)
    {

    }

    void IPlayerBehavior.FixedUpdate(Player player)
    {
        if (player.controller.isGrounded)
        {
            player.playerDirection.y += _jumpHeight;
        }

        player.playerDirection += Physics.gravity * Time.deltaTime;
        player.controller.Move(player.playerDirection * Time.deltaTime);
    }
}
