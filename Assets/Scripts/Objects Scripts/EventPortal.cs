using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventPortal : MonoBehaviour
{
    private float portalTime = 3f;
    private string EVENT_HALL_TAG = "Cinema Hall";
    private string LOBBY_TAG = "Lobby";

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (portalTime > 0)
                {
                    portalTime -= Time.deltaTime;
                }
                else
                {
                    if (other.GetComponent<Player>().PLAYER_LOCATION_TAG == EVENT_HALL_TAG)
                    {
                        PhotonNetwork.LoadLevel(LOBBY_TAG);
                        other.GetComponent<Player>().PLAYER_LOCATION_TAG = LOBBY_TAG;
                        
                        DontDestroyOnLoad(other);
                        Destroy(this);
                    }
                    else
                    {
                        PhotonNetwork.LoadLevel(EVENT_HALL_TAG);
                        other.GetComponent<Player>().PLAYER_LOCATION_TAG = EVENT_HALL_TAG;
                        
                        DontDestroyOnLoad(other);
                        Destroy(this);
                    }
                }
            }
            
        }
    }
}
