using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopPortal : MonoBehaviour
{

    private float _timeRemaining = 3f;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("HIII");

        if (other.gameObject.tag == "Player")
        {   Debug.Log("Player Checked");
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("Lobby");
            }
        }
    }
}
