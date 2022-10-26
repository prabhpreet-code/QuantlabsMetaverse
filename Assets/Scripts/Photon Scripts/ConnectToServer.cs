using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using UnityEngine.SceneManagement;


public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button login_button;

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
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");

        login_button.GetComponentInChildren<Text>().text = "Joined";

        Debug.Log("Joined Room");

        //Creating Lobby Room
        if (PhotonNetwork.CountOfRooms == 0) PhotonNetwork.CreateRoom("LobbyRoom");
        else PhotonNetwork.JoinRoom("LobbyRoom");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Loading Level");
        PhotonNetwork.LoadLevel("Lobby");
    }
    
}
