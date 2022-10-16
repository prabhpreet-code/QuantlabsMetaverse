using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private Button[] characterSelectButton;
    [SerializeField] private string sceneName = "Lobby";


    private void Awake()
    {
        characterSelectButton[0].onClick.AddListener(LoadFirst);
        characterSelectButton[1].onClick.AddListener(LoadSecond);
        characterSelectButton[2].onClick.AddListener(LoadThird);
    }

    private void LoadFirst()
    {
        GameManager.avatarNum = 0;
        SceneManager.LoadScene(sceneName);
    }

    private void LoadSecond()
    {
        GameManager.avatarNum = 1;
        SceneManager.LoadScene(sceneName);
    }

    private void LoadThird()
    {
        GameManager.avatarNum = 2;
        SceneManager.LoadScene(sceneName);
    }
}
