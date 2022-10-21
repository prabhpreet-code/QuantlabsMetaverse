using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using System.Linq;


public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button login_button;

    
    private string ROOM_TAG = "CurrentRoom";

    private void Awake()
    {
        SetupCanvas();
    }

    private void SetupCanvas()
    {
        login_button.onClick.AddListener(ConnectToMaster);
    }

    public void ConnectToMaster()
    {
        Debug.Log("Connecting to Master");

        login_button.GetComponentInChildren<Text>().text = "Loading";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Joined Master");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");

        login_button.GetComponentInChildren<Text>().text = "Joined";
        CreateRoom();
    }

    public void CreateRoom()
    {
        if (PhotonNetwork.CountOfRooms < 1)
        {
            PhotonNetwork.CreateRoom(ROOM_TAG);
        } else if (PhotonNetwork.CountOfRooms > 0)
        {
            PhotonNetwork.JoinRoom(ROOM_TAG);
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }
}
