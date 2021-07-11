using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPun
{
    public TextMeshProUGUI playerNameText;


    // Start is called before the first frame update
    void Start()
    {
        if(this.photonView.IsMine)
        {
            Debug.Log(this.gameObject + "PhotonView is mine");
            // The player is a local player
            this.transform.GetComponent<PlayerMovement>().enabled = true;
            this.transform.GetComponent<PlayerMovement>().joystick.gameObject.SetActive(true);
            this.transform.GetComponent<Animator>().enabled = true;
            this.transform.GetComponent<PlayerController>().enabled = true;
            this.transform.GetComponent<PlayerController>().shootButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log(this.gameObject + "PhotonView is not mine");
            // The player is a remote player
            this.transform.GetComponent<PlayerMovement>().enabled = false;
            this.transform.GetComponent<PlayerMovement>().joystick.gameObject.SetActive(false);
            this.transform.GetComponent<Animator>().enabled = true;
            this.transform.GetComponent<PlayerController>().enabled = false;
            this.transform.GetComponent<PlayerController>().shootButton.gameObject.SetActive(false);
        }
        SetPlayerName();
       
    }

   void SetPlayerName()
    {
        if(playerNameText != null)
        {
            if(this.photonView.IsMine)
            {
                playerNameText.text = "YOU";
                playerNameText.color = Color.green;
            }
            else
            {
                playerNameText.text = photonView.Owner.NickName;
            }
           
        }
    }
}
