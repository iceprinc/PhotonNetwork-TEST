using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System;

public class PhotonController : MonoBehaviourPunCallbacks, IPunObservable
{
    public Text testText;
    private bool connected = false;
    private int test = 0;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(test);
        }
        if (stream.IsReading)
        {
            test = (int)stream.ReceiveNext();
        }

    }
    private void Start()
    {
        PhotonNetwork.NickName = $"Player {UnityEngine.Random.Range(1000, 9999)}";
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        Log("Connecting");
    }
    private void Update()
    {
        if (connected == true)
        {
            Log(Convert.ToString(test));
        }
    }
    public void ClickTestButton()
    {
        test += 5;
        Debug.Log($"Local test = {test}");
    }
    public void ClickCreateRoom()
    {
        Log("Create room");
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 4 });
    }
    public void ClickJoinRoom()
    {
        Log("Joinning room");
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnConnectedToMaster()
    {
        Log("ConnectedToMaster");
    }
    public override void OnJoinedRoom()
    {
        Log("Connected");
        connected = true;
    }
    private void Log(string message)
    {
        Debug.Log(message);
        testText.text = message;
    }

}
