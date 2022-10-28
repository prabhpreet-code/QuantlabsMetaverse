using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public static string nextRoom;
    public static string nextLevel;


    public override void OnConnectedToMaster()
    {
        Debug.Log("Reloading master connect");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Reloading lobby connect");
        //PhotonNetwork.JoinRoom(nextRoom);
        RoomOptions roomOptions = new RoomOptions() { };
        PhotonNetwork.JoinOrCreateRoom(nextRoom, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room has been reconnected");
        Debug.Log("Room:" + nextRoom);
        Debug.Log("Level:" + nextLevel);
        PhotonNetwork.LoadLevel(nextLevel);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Join room failed");
        PhotonNetwork.CreateRoom(nextRoom);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Player leaving room");
        GameManager.currentPlayer = null;
    }
}

