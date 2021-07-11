using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    public Transform[] playerSpawnPositions;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region PHOTON CALL BACK METHODS
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            int randomSpawnPoint = Random.Range(0, playerSpawnPositions.Length - 1);
            Vector3 instaniatePostion = playerSpawnPositions[randomSpawnPoint].position;
            PhotonNetwork.Instantiate(playerPrefabs[0].name, instaniatePostion, Quaternion.identity);
        }
    }
    #endregion
}
