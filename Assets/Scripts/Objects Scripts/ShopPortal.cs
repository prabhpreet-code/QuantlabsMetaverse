using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopPortal : MonoBehaviour
{
    private float portalTime = 3f;
    private string SHOP_TAG = "Shop";
    private string LOBBY_TAG = "Lobby";

    private void OnTriggerStay(Collider other)
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
                    PhotonNetwork.LoadLevel(LOBBY_TAG);
                    other.GetComponent<Player>().PLAYER_LOCATION_TAG = LOBBY_TAG;
                    DontDestroyOnLoad(other);
                }
                else
                {
                    PhotonNetwork.LoadLevel(SHOP_TAG);
                    other.GetComponent<Player>().PLAYER_LOCATION_TAG = SHOP_TAG;
                    DontDestroyOnLoad(other);
                }
            }
        }
    }
}
