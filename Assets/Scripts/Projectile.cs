using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviourPun
{
    [SerializeField] Rigidbody rgbd;
    [SerializeField] float projectileForce = 500;
    // Variables needed for projectiles.
    [SerializeField] private protected float damage, lifeSpan;
    public GameObject shooter;

    protected virtual void Start()
    {
        rgbd = GetComponent<Rigidbody>();

        rgbd.AddForce(transform.forward * projectileForce);
    }
    private void Update()
    {
        // If the lifespan timer is less than 0,
        if (lifeSpan <= 0)
        {
            // Die
            PhotonNetwork.Destroy(this.photonView);
        }
        else
        {
            // Subtract the lifespan by time.
            lifeSpan -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        PhotonNetwork.Destroy(this.photonView);
    }
}
