using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class MySynchronization : MonoBehaviour, IPunObservable
{
    public Rigidbody rb;
    public PhotonView photonView;
    public Animator anim;

    Vector3 networkPosition;
    Quaternion networkRotation;

    public bool synchronizedVelocity = true;
    public bool syncrhonizedAngularVelocity = true;
    public bool isTeleportEnabled = true;
    public float teleportIfDistanceGreaterThan = 1.0f;

    float distance;
    float angle;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();

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
            rb.position = Vector3.MoveTowards(rb.position, networkPosition, distance * (1.0f/PhotonNetwork.SerializationRate));
            rb.rotation = Quaternion.RotateTowards(rb.rotation, networkRotation, angle * (1.0f/PhotonNetwork.SerializationRate));
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

            if (anim != null)
            {
                stream.SendNext(anim.GetFloat("Speed"));
            }
            if (synchronizedVelocity)
            {
                stream.SendNext(rb.velocity);

            }
            if (syncrhonizedAngularVelocity)
            {
                stream.SendNext(rb.angularVelocity);
            }
        }
        else if (stream.IsReading)
        {
            // On my player gameobject that exists in the remote player's game.
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();

            if (anim != null)
            {
                anim.SetFloat("Speed", (float)stream.ReceiveNext());
            }
            if (isTeleportEnabled)
            {
                if (Vector3.Distance(rb.position, networkPosition)>teleportIfDistanceGreaterThan)
                {
                    rb.position = networkPosition;
                }
            }

            if (synchronizedVelocity || syncrhonizedAngularVelocity)
            {
                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));

                if(synchronizedVelocity)
                {
                    rb.velocity = (Vector3)stream.ReceiveNext();

                    networkPosition += rb.velocity * lag;

                    distance = Vector3.Distance(rb.position, networkPosition);
                }

                if(syncrhonizedAngularVelocity)
                {
                    rb.angularVelocity = (Vector3)stream.ReceiveNext();

                    networkRotation = Quaternion.Euler(rb.angularVelocity * lag) * networkRotation;

                    angle = Quaternion.Angle(rb.rotation, networkRotation);
                }

            }
        }
    }
}
