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
                        other.GetComponent<Player>().PLAYER_LOCATION_TAG = LOBBY_TAG;
                        SceneManager.LoadScene(LOBBY_TAG);
                        DontDestroyOnLoad(other);
                    }
                    else
                    {
                        other.GetComponent<Player>().PLAYER_LOCATION_TAG = EVENT_HALL_TAG;
                        SceneManager.LoadScene(EVENT_HALL_TAG);
                        DontDestroyOnLoad(other);
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        portalTime = 3f;
    }
}
