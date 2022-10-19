using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] characters;

    [SerializeField] public static int avatarNum { get; set; }

    public static GameObject currentPlayer;

    private void OnEnable()
    {
        if (currentPlayer == null)
        {
            currentPlayer = characters[avatarNum];
            Instantiate(currentPlayer, spawnPoint.position, Quaternion.identity);
        }
    }



}
