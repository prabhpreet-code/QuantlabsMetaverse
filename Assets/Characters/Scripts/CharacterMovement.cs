using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //Character Components
    private CharacterController _controller;
    private Animator _animator;

    //Character properties
    private float _walkSpeed = 4f;
    private float _jumpHeight = 5f;

    //Character Physical Properties
    private bool _isGrounded;
    private bool _jumped;

    //Camera variables
    [SerializeField]
    private Transform _cam;
    private float _turmSmoothTime = 0.1f;
    private float _turmSmoothvelocity;

    private Vector3 _playerMoveInput;
    private Vector3 _playerDirection;

    //Animator Components
    private string WALKING_TAG = "isWalking";
    private bool WALKING_STATE;

    private string JUMPING_TAG = "isJumping";
    private bool JUMPING_STATE;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        GetUserInput();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        AnimateCharacter();

        Debug.Log(_controller.isGrounded);
    }

    private void GetUserInput()
    {
        _playerMoveInput.x = Input.GetAxis("Horizontal");
        _playerMoveInput.z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            _jumped = true;
        }

    }

    private void MoveCharacter()
    {
        if (_playerMoveInput.magnitude > 0)
        {
            WALKING_STATE = true;

            float targetAngle = Mathf.Atan2(_playerMoveInput.x, _playerMoveInput.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turmSmoothvelocity, _turmSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            _playerDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            _playerDirection *= _walkSpeed;

        } else
        {
            _playerDirection = Vector3.zero;
            WALKING_STATE = false;
        }

        _playerDirection += Physics.gravity;
        _controller.Move(_playerDirection * Time.deltaTime);
    }

    private void AnimateCharacter()
    {
        _animator.SetBool(JUMPING_TAG, JUMPING_STATE);
        _animator.SetBool(WALKING_TAG, WALKING_STATE);
    }


}
