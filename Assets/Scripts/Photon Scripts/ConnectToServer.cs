using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using UnityEngine.SceneManagement;


public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject selectionPanel;

    [SerializeField] private Button[] characterSelect;

    private Button selectedButton;

    private void Awake()
    {
        SetupCanvas();
    }

    private void SetupCanvas()
    {
        characterSelect[0].onClick.AddListener(LoadFirst);
        characterSelect[1].onClick.AddListener(LoadSecond);
        characterSelect[2].onClick.AddListener(LoadThird);
    }

    private void LoadFirst()
    {
        GameManager.avatarNum = 0;
        selectedButton = characterSelect[0]; 
        ConnectToMaster();
    }

    private void LoadSecond()
    {
        GameManager.avatarNum = 1;
        selectedButton= characterSelect[1];
        ConnectToMaster();
    }

    private void LoadThird()
    {
        GameManager.avatarNum = 2;
        selectedButton = characterSelect[2];
        ConnectToMaster();
    }

    public void ConnectToMaster()
    {
        Debug.Log("Connecting to Master");
        selectedButton.GetComponentInChildren<Text>().text = "Loading";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Joined Master");

        PhotonNetwork.AutomaticallySyncScene = false;

        selectionPanel.SetActive(true);

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        selectedButton.GetComponentInChildren<Text>().text = "Joined";
        PhotonNetwork.JoinRoom("LobbyRoom");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        PhotonNetwork.CreateRoom("LobbyRoom");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Loading Level");
        PhotonNetwork.LoadLevel("Lobby");
    }
    
}
