using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    public Player player;

    private Vector3 _playerMoveInput;

    private bool _isTrading;

    private void Update()
    {
        _playerMoveInput.x = Input.GetAxis("Horizontal");
        _playerMoveInput.z = Input.GetAxis("Vertical");

        if (_playerMoveInput.magnitude > 0f)
        {
            this.player.SetBehaviorWalk();
            _isTrading = false;
        } else
        {
            if (!_isTrading)
                this.player.SetBehaviorIdle();
        }

        if (Input.GetButtonDown("Jump"))
        {
            this.player.SetBehaviorJump();
        }

        if (Input.GetKeyDown("t"))
        {
            _isTrading = true;

            if (_isTrading)
                this.player.SetBehaviorTrade();
        }
    }
}
