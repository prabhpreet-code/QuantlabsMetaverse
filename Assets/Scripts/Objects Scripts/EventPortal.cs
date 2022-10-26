using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.CreateRoom("EventHallRoom");
        PhotonNetwork.JoinRoom("EventHallRoom");
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(EVENT_HALL_TAG);
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.ConnectUsingSettings();
        GameManager.currentPlayer = null;
    }
}
