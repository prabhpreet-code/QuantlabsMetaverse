using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviourPunCallbacks
{
    [SerializeField] private bool teleportToShop;

    private float portalTime = 3f;
    private string EVENT_HALL_TAG = "Cinema Hall";
    private string LOBBY_TAG = "Lobby";
    private string SHOP_TAG = "Shop";

    private int currentRoom = 0;
    private string nextRoom;

    private PhotonView portalView;

    private void Awake()
    {
        portalView = GetComponent<PhotonView>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (portalView.IsMine)
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

                        if (SceneManager.GetActiveScene().name == EVENT_HALL_TAG || SceneManager.GetActiveScene().name == SHOP_TAG)
                        {
                            currentRoom = 1;
                            PhotonNetwork.LeaveRoom();

                        }
                        else
                        {
                            currentRoom = 0;
                            PhotonNetwork.LeaveRoom();
                        }
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
        if (currentRoom == 0)
        {
            if (!teleportToShop)
            {
                PhotonNetwork.JoinRoom("EventHallRoom");
            }
            else if (teleportToShop)
            {
                PhotonNetwork.JoinRoom("ShopRoom");
            }
        }
        else PhotonNetwork.JoinRoom("LobbyRoom");

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        if (currentRoom == 0)
        {
            if (!teleportToShop)
            {
                PhotonNetwork.CreateRoom("EventHallRoom");
            }
            else if (teleportToShop)
            {
                PhotonNetwork.CreateRoom("ShopRoom");
            }
        }
        else PhotonNetwork.CreateRoom("LobbyRoom");

    }

    public override void OnJoinedRoom()
    {
        if (currentRoom == 0)
        {
            if (!teleportToShop)
            {
                PhotonNetwork.LoadLevel(EVENT_HALL_TAG);

            }
            else if (teleportToShop)
            {
                PhotonNetwork.LoadLevel(SHOP_TAG);
            }
        }
        else PhotonNetwork.LoadLevel(LOBBY_TAG);
    }

    public override void OnLeftRoom()
    {
        GameManager.currentPlayer = null;
    }
}
