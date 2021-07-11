using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovementController : MonoBehaviourPun
{
    public Rigidbody rb;
    public float speed = 5;
    public float turnSpeed = 15;
    public List<PlayerController> players = new List<PlayerController>();
    public Transform currentTarget;
    public float playerOneDistance;
    public float playerTwoDistance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        foreach(PlayerController controller in FindObjectsOfType<PlayerController>())
        {
            players.Add(controller);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTarget = players[Random.Range(0, players.Count)].transform;
        //find target
        Vector3 vectorToTarget = currentTarget.position - this.transform.position;
        //find the rotation needed
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);
        transform.rotation = targetRotation;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime) ;


    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (collision.collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                collision.gameObject.GetComponent<PhotonView>().RPC("DoDamage", RpcTarget.AllBuffered, 10);
                
            }
            PhotonNetwork.Destroy(this.gameObject);
        }
    }

    [PunRPC]
    public void DoDamage(float _damage)
    {
        SendMessage("TakeDamage", _damage);
    }
    
}
