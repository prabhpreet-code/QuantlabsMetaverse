using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopPortal : MonoBehaviourPunCallbacks
{
    private float portalTime = 3f;
    private string SHOP_TAG = "Shop";
    private string LOBBY_TAG = "Lobby";

    private void OnTriggerStay(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "Player")
            {
                if (portalTime > 0)
                {
                    portalTime -= Time.deltaTime;
                }
                else
                {
                    if (other.GetComponent<Player>().PLAYER_LOCATION_TAG == SHOP_TAG)
                    {
                        other.GetComponent<Player>().PLAYER_LOCATION_TAG = LOBBY_TAG;
                        PhotonNetwork.LoadLevel(LOBBY_TAG);
                    }
                    else
                    {

                        other.GetComponent<Player>().PLAYER_LOCATION_TAG = SHOP_TAG;
                        PhotonNetwork.LoadLevel(SHOP_TAG);
                    }
                }
            }
        }
        
    }
}
