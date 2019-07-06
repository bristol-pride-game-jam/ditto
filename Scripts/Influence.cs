using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Influence : MonoBehaviour
{
    public float C;
    public float M;
    public float Y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetInfluenceProportions()
    {
        return new Vector3(C, M, Y).normalized;
    }
}
