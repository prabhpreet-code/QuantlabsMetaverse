using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerBehaviorSeat : IPlayerBehavior
{
    private float _seatRange = 1f;
    private float _seatOffset = 90f;
    private float _camSpeed = 5f;

    private Vector3 _cameraInitialPos;
    private Vector3 _axisRotation;
    private Vector3 _cameraMovementInput;
    private Vector3 _cameraDirection;
    private Vector3 playerPosition;

    private float _turmSmoothTime = 0.1f;
    private float _turmSmoothvelocity;

    private LayerMask chairLayer;
    private LayerMask cinemaChairLayer;

    private GameObject currentChair;

    void IPlayerBehavior.Enter(Player player)
    {
        chairLayer = LayerMask.GetMask("Chairs");
        cinemaChairLayer = LayerMask.GetMask("CinemaChairs");
        playerPosition = player.transform.position;
        
        Collider[] chairs = Physics.OverlapSphere(playerPosition, _seatRange, chairLayer);
        Collider[] cinemaChairs = Physics.OverlapSphere(playerPosition, _seatRange, cinemaChairLayer);

        foreach (Collider chair in chairs)
        {
            if (!chair.GetComponent<Chair>().IsBusy)
            {
                currentChair = chair.gameObject;
                playerPosition = currentChair.transform.position;
                _seatOffset = 90f;
                break;
            }
        }

        foreach (Collider cinemaChair in cinemaChairs)
        {
            if (!cinemaChair.GetComponent<Chair>().IsBusy)
            {
                currentChair = cinemaChair.gameObject;
                playerPosition = currentChair.transform.position;
                playerPosition.y -= 0.6f;
                _seatOffset = 0;
                break;
            }
        }

        if (currentChair != null)
        {
            player.animator.applyRootMotion = false;

            player.animator.SetBool("isSeating", true);
            player.isSeating = true;

            currentChair.GetComponent<Chair>().IsBusy = true;

            _axisRotation.x = player.transform.eulerAngles.x;
            _axisRotation.y = currentChair.transform.eulerAngles.y + _seatOffset;
            _axisRotation.z = player.transform.eulerAngles.z;
             
            player.transform.eulerAngles = _axisRotation;
            player.transform.position = playerPosition;

            _cameraInitialPos = player.cameraTarget.position;

        } else
        {
            player.SetBehaviorIdle();
        }
        
    }

    void IPlayerBehavior.Exit(Player player)
    {
        if (currentChair != null)
        {

            player.animator.SetBool("isSeating", false);
            player.isSeating = false;

            player.cameraTarget.position = _cameraInitialPos;
            currentChair.GetComponent<Chair>().IsBusy = false;

            currentChair = null;
            player.animator.applyRootMotion = true;
        }
    }

    void IPlayerBehavior.Update(Player player)
    {
        if (currentChair != null)
        {
            _cameraMovementInput.x = Input.GetAxis("Horizontal");
            _cameraMovementInput.z = Input.GetAxis("Vertical");

            if (_cameraMovementInput.magnitude > 0)
            {
                float targetAngle = Mathf.Atan2(_cameraMovementInput.x, _cameraMovementInput.z) * Mathf.Rad2Deg + player.cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref _turmSmoothvelocity, _turmSmoothTime);

                player.cameraTarget.rotation = Quaternion.Euler(0f, angle, 0f);

                _cameraDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                if (player.cam.rotation.x < 0)
                {
                    _cameraDirection.y += 7 * Time.deltaTime;
                } else
                {
                    _cameraDirection.y -= 7 * Time.deltaTime;
                }

                player.cameraTarget.position += _cameraDirection * _camSpeed * Time.deltaTime;
            }
        }

        if (Input.GetKeyDown("f"))
        {
            player.SetBehaviorIdle();
        }

    }

    void IPlayerBehavior.FixedUpdate(Player player)
    {

    }
}
