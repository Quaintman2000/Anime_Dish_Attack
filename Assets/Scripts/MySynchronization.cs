using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class MySynchronization : MonoBehaviour, IPunObservable
{
    public Rigidbody rb;
    public PhotonView photonView;

    Vector3 networkPosition;
    Quaternion networkRotation;

    

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();

        networkPosition = new Vector3();
        networkRotation = new Quaternion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!this.photonView.IsMine)
        {
            rb.position = Vector3.MoveTowards(rb.position, networkPosition, Time.fixedDeltaTime);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, networkRotation, Time.fixedDeltaTime * 100);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Then, photon is mine and i am the one who controls this player.
            // Should send poisiton, velocity etc. data to the other players.

            stream.SendNext(rb.position);
            stream.SendNext(rb.rotation);
        }
        else if (stream.IsReading)
        {
            // On my player gameobject that exists in the remote player's game.
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
