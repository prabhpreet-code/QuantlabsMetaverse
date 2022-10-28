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
                            ConnectionManager.nextRoom = "LobbyRoom";
                            ConnectionManager.nextLevel = LOBBY_TAG;
                        }
                        else
                        {
                            if (teleportToShop)
                            {
                                ConnectionManager.nextRoom = "ShopRoom";
                                ConnectionManager.nextLevel = SHOP_TAG;
                            } else
                            {
                                ConnectionManager.nextRoom = "EventRoom";
                                ConnectionManager.nextLevel = EVENT_HALL_TAG;
                            }
                        }

                        PhotonNetwork.LeaveRoom();
                    }
                }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        portalTime = 3f;
    }
}
