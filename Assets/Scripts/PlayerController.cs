using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPun
{
    public string bulletPrefab;
    public Transform firePoint;
    public Button shootButton;

    // Start is called before the first frame update
    void Start()
    {
        shootButton = GetComponentInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShoot()
    {
        PhotonNetwork.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
