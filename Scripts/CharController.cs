using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {
    [SerializeField] float moveSpeed = 4f;
    Vector3 forward, right;

    private Collider ownCol;
    private Rigidbody ownBody;
    void Awake() {
        ownBody = GetComponent<Rigidbody>();
        ownCol = GetComponent<Collider>();
    }

    void Start()
    {
    }

    void Update()
    {
        if(Input.anyKey)
            Move();
    }

    void Move()
    {
        float rightMovement = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float upMovement = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        ownBody.velocity = new Vector3 (rightMovement, 0f, upMovement);
    }

    public void OnTriggerEnter (Collider other) {
        Debug.Log(other.gameObject.name);
        Pusher pusher = other.GetComponent<Pusher>();
        if (pusher != null) {
            pusher.canEngage = true;
        }
    }

    public void OnTriggerExit (Collider other) {
        Pusher pusher = other.GetComponent<Pusher>();
        if (pusher != null) {
            pusher.canEngage = false;
        }
    }
}
