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
        /*forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;*/
    }

    void Update()
    {
        if(Input.anyKey)
            Move();

        //playerCamera.transform.position = new Vector3 (transform.position.x, transform.position.y, playerCamera.transform.position.z);
    }

    void Move()
    {
        //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        //Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        float rightMovement = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float upMovement = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        //Debug.Log(new Vector3(rightMovement, upMovement, 0f));
        ownBody.velocity = new Vector3 (rightMovement, 0f, upMovement);
        //Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        //transform.forward = heading;
        //transform.position += rightMovement;
        //transform.position += upMovement;
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
