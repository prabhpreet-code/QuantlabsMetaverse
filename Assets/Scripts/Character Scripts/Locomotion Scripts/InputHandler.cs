using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    public Player player;

    private Vector3 _playerMoveInput;

    private bool _isTrading;
    private bool _isSeating;

    private void Update()
    {
        _playerMoveInput.x = Input.GetAxis("Horizontal");
        _playerMoveInput.z = Input.GetAxis("Vertical");

        if (_playerMoveInput.magnitude > 0f)
        {
            this.player.SetBehaviorWalk();

            _isTrading = false;
            _isSeating = false;

        } else
        {
            if (!_isTrading && !_isSeating)
                this.player.SetBehaviorIdle();
        }

        if (Input.GetKeyDown("f") && !_isTrading)
        {
            if (_isSeating)
                _isSeating = false;
            else _isSeating = true;

            if (_isSeating)
                this.player.SetBehaviourSeat();
        }

        if (Input.GetKeyDown("t") && !_isSeating)
        {
            if (_isTrading)
                _isTrading = false;
            else _isTrading = true;

            if (_isTrading)
                this.player.SetBehaviorTrade();
        }
    }
}
