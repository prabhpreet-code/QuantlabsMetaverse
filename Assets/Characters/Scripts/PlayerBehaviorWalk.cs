using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBehaviorWalk : IPlayerBehavior
{
    private float _turmSmoothTime = 0.1f;
    private float _turmSmoothvelocity;

    private float _walkSpeed = 4f;

    private Vector3 _playerMoveInput;
    private Vector3 _playerDirection;

    void IPlayerBehavior.Enter(Player player)
    {
        player.animator.SetBool("isWalking", true);
    }

    void IPlayerBehavior.Exit(Player player)
    {
        player.animator.SetBool("isWalking", false);
    }

    void IPlayerBehavior.Update(Player player)
    {
        _playerMoveInput.x = Input.GetAxis("Horizontal");
        _playerMoveInput.z = Input.GetAxis("Vertical");
    }

    void IPlayerBehavior.FixedUpdate(Player player)
    {
        float targetAngle = Mathf.Atan2(_playerMoveInput.x, _playerMoveInput.z) * Mathf.Rad2Deg + player.cam.eulerAngles.y;

        float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref _turmSmoothvelocity, _turmSmoothTime);

        player.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (player.controller.isGrounded)
        {
            player.playerDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            player.playerDirection *= _walkSpeed;
        }

        player.playerDirection += Physics.gravity * Time.deltaTime;

        player.controller.Move(player.playerDirection * Time.deltaTime);
    }
}
