using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    [Header("UI")]
    public GameObject uI_InformPanelGameobject;
    public Text uI_InformText;
    public GameObject searchGameButton;

    [Header("Game Management")]
    public SpawnManager spawnManager;
    public List<Health> playerHealths = new List<Health>();
    public List<Health> enemyHealths = new List<Health>();
    public bool gameStart { get { return PhotonNetwork.CurrentRoom.PlayerCount == 2; } }
    public int maxEnemiesAtOneTime = 4;
    public float speedIncreaseIncrament = 1;
    float speedIncreaseModifier = 0;
    public float speedIncreaseDelay = 2;
    float nextTimeToIncreaseSpeed;
    // Start is called before the first frame update
    void Start()
    {
        uI_InformPanelGameobject.SetActive(true);
        uI_InformText.text = "Search for Games to BATTLE!";
    }

    // Update is called once per frame
    void Update()
    {
      
        if (playerHealths.Count < 1)
        {
            GameOver();
        }
        else if (gameStart && PhotonNetwork.IsMasterClient)
        {
            EnemySpawnManagement();
        }

    }

    public void OnQuitButtonClicked()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        else
        {
            SceneLoader.Instance.LoadScene("LobbyScene");
        }
    }
    void GameOver()
    {
        PhotonNetwork.LeaveRoom();
    }
    void EnemySpawnManagement()
    {
        // Calculate time to see if we can increase the speed.
        if (PhotonNetwork.Time > nextTimeToIncreaseSpeed)
        {
            speedIncreaseModifier += speedIncreaseIncrament;
            nextTimeToIncreaseSpeed = (float)PhotonNetwork.Time + speedIncreaseDelay;
        }
        // If the number of enemies in the scene is less than the number specified, spawn more.
        if (enemyHealths.Count < maxEnemiesAtOneTime)
        {
            spawnManager.SpawnEnemies(speedIncreaseModifier);
        }
    }
    #region UI Callback Methods
    public override void OnLeftRoom()
    {
        SceneLoader.Instance.LoadScene("GameOverScene");
    }
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
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
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
        PhotonNetwork.CreateRoom(randRoomName, roomOptions);
    }

    IEnumerator DeactivateAfterSeconds(GameObject gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
    #endregion

}
