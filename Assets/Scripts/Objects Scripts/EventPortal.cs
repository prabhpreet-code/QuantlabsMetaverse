using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class EventPortal : MonoBehaviourPunCallbacks
{
    private float portalTime = 3f;
    private string EVENT_HALL_TAG = "Cinema Hall";
    private string LOBBY_TAG = "Lobby";
    private int currentRoom = 0;

    private string currentLocation;


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PhotonView>().IsMine)
        {
            if (other.gameObject.tag == "Player")
            {
                if (portalTime > 0)
                {
                    portalTime -= Time.deltaTime;
                }
                else
                {
                    portalTime = 3f;

                    if (SceneManager.GetActiveScene().name == EVENT_HALL_TAG)
                    {
                        currentRoom = 1;
                        
                        PhotonNetwork.LeaveRoom();
                        //other.GetComponent<Player>().PLAYER_LOCATION_TAG = LOBBY_TAG;
                        //PhotonNetwork.LoadLevel(LOBBY_TAG);
                        //DontDestroyOnLoad(other);

                    }
                    else
                    {
                        currentRoom = 0;

                        PhotonNetwork.LeaveRoom();
                        //other.GetComponent<Player>().PLAYER_LOCATION_TAG = EVENT_HALL_TAG;
                        //PhotonNetwork.LoadLevel(EVENT_HALL_TAG);
                        //DontDestroyOnLoad(other);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        portalTime = 3f;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        if (currentRoom == 0) PhotonNetwork.JoinRoom("EventHallRoom");
        else PhotonNetwork.JoinRoom("LobbyRoom");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        if (currentRoom == 0) PhotonNetwork.CreateRoom("EventHallRoom");
        else PhotonNetwork.CreateRoom("LobbyRoom");
    }

    public override void OnJoinedRoom()
    {
        if (currentRoom == 0) PhotonNetwork.LoadLevel(EVENT_HALL_TAG);
        else PhotonNetwork.LoadLevel("Lobby");

    }

    public override void OnLeftRoom()
    {
        GameManager.currentPlayer = null;
    }
}
