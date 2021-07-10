using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("UI")]
    public GameObject uI_InformPanelGameobject;
    public TextMeshPro uI_InformText;
    public GameObject searchGameButton;
    // Start is called before the first frame update
    void Start()
    {
        uI_InformPanelGameobject.SetActive(true);
        uI_InformText.text = "Search for Games to BATTLE!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI Callback Methods
    public void JoinRandomRoom()
    {
        uI_InformText.text = "Searching for available rooms...";
        PhotonNetwork.JoinRandomRoom();

        searchGameButton.SetActive(false);
    }

    #endregion

    #region PhotonCallBack methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        uI_InformText.text = message;
        CreateAndJoinRoom();
    }

    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            uI_InformText.text = "Joined to " + PhotonNetwork.CurrentRoom.Name + ". Waiting for other players...";
        }
        else
        {
            uI_InformText.text = "Joined to " + PhotonNetwork.CurrentRoom.Name;
            StartCoroutine(DeactivateAfterSeconds(uI_InformPanelGameobject, 2));
        }
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        uI_InformText.text = newPlayer.NickName + " Joined to " + PhotonNetwork.CurrentRoom + ". Player count: " + PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.LogError(newPlayer.NickName + " Joined to " + PhotonNetwork.CurrentRoom.PlayerCount);

        StartCoroutine(DeactivateAfterSeconds(uI_InformPanelGameobject, 2));
    }
    #endregion

    #region PRIVATE METHODS
    void CreateAndJoinRoom()
    {
        string randRoomName = "Room" + Random.Range(0, 1000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        // Creates the room.
        PhotonNetwork.CreateRoom(randRoomName,roomOptions);
    }

    IEnumerator DeactivateAfterSeconds(GameObject gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
    #endregion

}
