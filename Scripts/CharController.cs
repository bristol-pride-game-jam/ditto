using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {
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
    }

    void Update()
    {
        if(Input.anyKey)
            Move();

        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, playerCamera.transform.position.z);
    }

    void Move()
    {
        float rightMovement = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float upMovement = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        ownBody.velocity = new Vector3(rightMovement, upMovement, 0f);
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
