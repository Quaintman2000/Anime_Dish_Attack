using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverFunctions : MonoBehaviour
{
    public void OnQuitGameClicked()
    {
        Application.Quit();
    }
    public void OnReturnToLobbyClicked()
    {
        SceneLoader.Instance.LoadScene("LobbyScene");
    }
}
