using System.Collections;
using System.Collections.Generic;
using UnityEngine;public class CharController : MonoBehaviour {
    [SerializeField] float moveSpeed = 4f;
    Vector3 forward, right;

    private Collider2D ownCol;
    private Rigidbody2D ownBody;
    public Camera playerCamera;
    void Awake() {
        ownBody = GetComponent<Rigidbody2D>();
        ownCol = GetComponent<Collider2D>();
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

        playerCamera.transform.position = new Vector3 (transform.position.x, transform.position.y, playerCamera.transform.position.z);
    }

    void Move()
    {
        //Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        //Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        float rightMovement = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float upMovement = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        //Debug.Log(new Vector3(rightMovement, upMovement, 0f));
        ownBody.velocity = new Vector3 (rightMovement, upMovement, 0f);
        //Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        //transform.forward = heading;
        //transform.position += rightMovement;
        //transform.position += upMovement;
    }

    public void OnTriggerEnter2D (Collider2D other) {
        Debug.Log(other.gameObject.name);
        Pusher pusher = other.GetComponent<Pusher>();
        if (pusher != null) {
            pusher.engaging = true;
        }
    }

    public void OnTriggerExit2D (Collider2D other) {
        Pusher pusher = other.GetComponent<Pusher>();
        if (pusher != null) {
            pusher.engaging = false;
        }
    }
}
