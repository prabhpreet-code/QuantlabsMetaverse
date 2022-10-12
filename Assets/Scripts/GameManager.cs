using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject almazPrefab;

    public static GameObject currentPlayer;

    private void OnEnable()
    {
        if (currentPlayer == null)
        {
            currentPlayer = almazPrefab;
            Instantiate(currentPlayer, spawnPoint.position, Quaternion.identity);
        }
    }
}
