using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    public float ferocity = 1f;
    public bool engaging;
    public float engageDistance = 5f;
    public float speedMax;
    public float velocityAcceleration;
    public float speedCurrent;
    public float rotationSpeed = 5f;
    public float activationFerocity = 0.2f;
    public float engageFullDistance = 3f;
    [SerializeField]
    private GameObject target;
    public float acceleration = 0.2f;
    //public float accelerationMaxDistance;
    //public float accelerationMax;
    public float moveSpeed;
    Rigidbody2D ownBody;

    float targetDistance {
        get { return Vector3.Distance(transform.position, target.transform.position);  }
    }
    
    float targetFacingAngleChange {
        get {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
            float rotationNeeded = (transform.eulerAngles.z % 360) - angle;
            return rotationNeeded < 0 ? 360 + rotationNeeded: rotationNeeded;
        }
    }

    private void Awake() {

    }

    void Start()
    {

        ownBody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {


        if (engaging) {
            
            //Orientate
            {
                Vector3 vectorToTarget = target.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion targetQuaternion = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.deltaTime * rotationSpeed);
            }
            
            //Move
            {
                if (ferocity > activationFerocity) {
                    float targetFacingSpeedMod = 0.1f;
                    //float engageDistanceSpeedMod = Mathf.Lerp(0, 1, (targetDistance)/(engageDistance - engageFullDistance));

                    if (targetFacingAngleChange < 90) {
                        targetFacingSpeedMod = Mathf.Lerp(1, 0, targetFacingAngleChange / 90);
                    } else if (targetFacingAngleChange > 270) {
                        targetFacingSpeedMod = Mathf.Lerp(0, 1, (targetFacingAngleChange - 270) / 90);
                    }
                    speedCurrent = Mathf.Min((acceleration * Time.deltaTime) + speedCurrent, Mathf.Lerp(0, speedMax, ferocity * targetFacingSpeedMod));
                    //Debug.Log(speedCurrent);
                    ownBody.velocity = transform.up * speedCurrent;
                }
                
            }
        } else {

        }
    }
}
