using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public void OnStartGameClicked(string sceneToLoad)
    {
        SceneLoader.Instance.LoadScene(sceneToLoad);
    }

    public void OnBackClicked(string sceneToLoad)
    {
        SceneLoader.Instance.LoadScene(sceneToLoad);
    }
}
