using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayPortal : MonoBehaviour
{
    private float _timeRemaining = 3f;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("HIII");
        Debug.Log(_timeRemaining);

        if (other.gameObject.tag == "Player")
        {
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("Shop");
            }
        }
    }
}
