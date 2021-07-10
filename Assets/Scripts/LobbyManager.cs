using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Login UI"), SerializeField, Tooltip("Put the input field object in this variable.")]
    private InputField playerNameInputfield;
    public GameObject uI_loginGameobject;

    [Header("Lobby UI")]
    public GameObject uI_LobbyGameobject;

    [Header("Connection Status UI")]
    public GameObject uI_ConnectionStatusGameobject;
    public Text connectionStatusText;

    [Header("Unity Event(s):")]
    Event onConnected;
    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            uI_LobbyGameobject.SetActive(true);
            uI_ConnectionStatusGameobject.SetActive(false);

            uI_loginGameobject.SetActive(false);
        }
        else
        {
            uI_LobbyGameobject.SetActive(false);
            uI_ConnectionStatusGameobject.SetActive(false);

            uI_loginGameobject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        connectionStatusText.text = "Connection Status: " + PhotonNetwork.NetworkClientState;
    }
    #endregion

    #region UI Callback
    /// <summary>
    /// When the Start game button has been clicked, it will 
    /// connect the user to the photon network with the input 
    /// field set nickname.
    /// </summary>
    public void OnEnterGameButtonClicked()
    {
        // Grabs and stores the player nickname from the input field.
        string playerName = playerNameInputfield.text;
        // Checks to see if player name isn't null or empty.
        // If it's not empty...
        if (!string.IsNullOrEmpty(playerName))
        {
            uI_LobbyGameobject.SetActive(false);
            uI_ConnectionStatusGameobject.SetActive(true);

            uI_loginGameobject.SetActive(false);
            // If not connected to the photonNetwork...
            if (!PhotonNetwork.IsConnected)
            {
                // Set the player's Photon Nickname to be the inputted name.
                PhotonNetwork.LocalPlayer.NickName = playerName;
                // Connect to the Photon network with these setting(s).
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        // If the string is null or empty.
        else
        {
            Debug.Log("Player name is invalid or empty!");
        }
    }

    public void OnQuickMatchButtonClicked()
    {
        // SceneManager.LoadScene("Scene_Loading");
        SceneLoader.Instance.LoadScene("TutorialScene");
    }


    #endregion

    #region Photon Callback Methods

    public override void OnConnected()
    {
        Debug.Log("We connected to Internet");

    }
    public override void OnConnectedToMaster()
    {
        uI_LobbyGameobject.SetActive(true);
        uI_ConnectionStatusGameobject.SetActive(false);

        uI_loginGameobject.SetActive(false);
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Is connected to Photon Server");
    }

    #endregion
}


