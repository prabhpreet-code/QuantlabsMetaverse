using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] characters;
    [SerializeField] private CinemachineFreeLook cinemachineCamera;

    [SerializeField] public static int avatarNum { get; set; }

    public static GameObject currentPlayer;

    private void OnEnable()
    {
        if (currentPlayer == null)
        {
            //Assigning character number
            currentPlayer = characters[avatarNum];

            var playerObj = PhotonNetwork.Instantiate(currentPlayer.name, spawnPoint.position, Quaternion.identity);

            //Setting camera properties
            cinemachineCamera.GetComponent<CinemachineFreeLook>().Follow = playerObj.transform.GetChild(0).transform;
            cinemachineCamera.GetComponent<CinemachineFreeLook>().LookAt = playerObj.transform.GetChild(0).transform;

            //Instantiating cinemachine camera and setting as a child object
            var cinemachine = PhotonNetwork.Instantiate(cinemachineCamera.name, spawnPoint.position, Quaternion.identity);
            cinemachine.transform.parent = playerObj.transform;
        }
    }
}
