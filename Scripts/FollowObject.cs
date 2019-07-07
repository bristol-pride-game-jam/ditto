using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject target;
    public float height;
    public float offset;

    void Update()
    {
        Vector3 pos = target.transform.position;
        pos.y += height;
        pos.z -= offset;
        transform.position = pos;
    }
}
