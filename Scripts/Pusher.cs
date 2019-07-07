using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    public float ferocity = 1f;
    public bool canEngage;
    public float engageDistance = 5f;
    public float speedMax;
    public float velocityAcceleration;
    public float speedCurrent;
    public float rotationSpeed = 5f;
    public float activationFerocity = 0.2f;
    public float engageFullDistance = 3f;
    [SerializeField]
    private InfluenceAbsorption target;
    public float acceleration = 0.2f;
    public Vector3 minimumInfluenceRequired = new Vector3();
    public Vector3 maximumInfluenceAllowed = new Vector3(1f, 1f, 1f);
    //public float accelerationMaxDistance;
    //public float accelerationMax;
    public float moveSpeed;
    Rigidbody ownBody;

    float targetDistance {
        get { return Vector3.Distance(transform.position, target.transform.position);  }
    }
    
    float targetFacingAngleChange {
        get {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.z, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
            float rotationNeeded = (transform.eulerAngles.y % 360) - angle;
            return rotationNeeded < 0 ? 360 + rotationNeeded: rotationNeeded;
        }
    }

    void Start()
    {
        target = FindObjectOfType<InfluenceAbsorption>();
        ownBody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        SetFerocity();

        if (canEngage)
        {
            //Orientate
            FacePlayer();

            //Move
            if (ferocity > activationFerocity)
            {
                ChasePlayer();
            }
        }
    }

    private void SetFerocity()
    {
        var ferocitySum = 0f;
        if (target.Accumulation.x > maximumInfluenceAllowed.x || target.Accumulation.x < minimumInfluenceRequired.x)
        {
            ferocitySum += 0.2f;
        }

        if (target.Accumulation.y > maximumInfluenceAllowed.y || target.Accumulation.y < minimumInfluenceRequired.y)
        {
            ferocitySum += 0.2f;
        }

        if (target.Accumulation.z > maximumInfluenceAllowed.z || target.Accumulation.z < minimumInfluenceRequired.z)
        {
            ferocitySum += 0.2f;
        }

        ferocity = ferocitySum;
    }

    private void FacePlayer()
    {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.z, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion targetQuaternion = Quaternion.AngleAxis(angle - 90, Vector3.down);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, Time.deltaTime * rotationSpeed);
    }

    private void ChasePlayer()
    {
        float targetFacingSpeedMod = 0.1f;
        //float engageDistanceSpeedMod = Mathf.Lerp(0, 1, (targetDistance)/(engageDistance - engageFullDistance));

        if (targetFacingAngleChange < 90)
        {
            targetFacingSpeedMod = Mathf.Lerp(1, 0, targetFacingAngleChange / 90);
        }
        else if (targetFacingAngleChange > 270)
        {
            targetFacingSpeedMod = Mathf.Lerp(0, 1, (targetFacingAngleChange - 270) / 90);
        }
        speedCurrent = Mathf.Min((acceleration * Time.deltaTime) + speedCurrent, Mathf.Lerp(0, speedMax, ferocity * targetFacingSpeedMod));
        //Debug.Log(speedCurrent);
        ownBody.velocity = transform.forward * speedCurrent;
    }
}
