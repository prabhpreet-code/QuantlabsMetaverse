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
    private float _jumpHeight = 4f;

    //Character Physical Properties
    private bool _isGrounded;
    private bool _jumped;
    private bool _talked;

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

    private string TALKING_TAG = "isTalking";
    private bool TALKING_STATE;

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
        HandleMovement();
        AnimateCharacter();

        Debug.Log(_controller.isGrounded);
    }

    private void GetUserInput()
    {
        _playerMoveInput.x = Input.GetAxis("Horizontal");
        _playerMoveInput.z = Input.GetAxis("Vertical");

        _jumped = Input.GetKey("space");
        if (Input.GetKeyDown("f"))
            _talked = true;
            
    }

    private void HandleMovement()
    {
        if (_playerMoveInput.magnitude > 0)
        {
            MovePlayer();
            JumpPlayer();

        } else
        {
            IdlePlayer();
        }

        TalkPlayer();
        MoveCharacterController();
    }

    private void MovePlayer()
    {

        float targetAngle = Mathf.Atan2(_playerMoveInput.x, _playerMoveInput.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turmSmoothvelocity, _turmSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (_controller.isGrounded)
        {
            _playerDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _playerDirection *= _walkSpeed;

            JUMPING_STATE = false;
            
        }

        _talked = false;
        WALKING_STATE = true;
    }

    private void IdlePlayer()
    {
        if (!_talked)
        {
            _playerDirection.x = 0f;
            _playerDirection.z = 0f;

            JUMPING_STATE = false;
            WALKING_STATE = false;
        }
        
    }

    private void TalkPlayer()
    {
        if (_talked)
        {
            TALKING_STATE = true;

            JUMPING_STATE = false;
            WALKING_STATE = false;

            _playerDirection.x = 0f;
            _playerDirection.z = 0f;
        } else
        {
            TALKING_STATE = false;
        }
    }

    private void JumpPlayer()
    {
        if (_jumped & _controller.isGrounded)
        {
            JUMPING_STATE = true;

            _playerDirection.y += _jumpHeight;
            _jumped = false;
        }  
    }

    private void MoveCharacterController()
    {
        _playerDirection += Physics.gravity * Time.deltaTime;
        _controller.Move(_playerDirection * Time.deltaTime);
    }


    private void AnimateCharacter()
    {
        _animator.SetBool(JUMPING_TAG, JUMPING_STATE);
        _animator.SetBool(WALKING_TAG, WALKING_STATE);
        _animator.SetBool(TALKING_TAG, TALKING_STATE);
    }
}
