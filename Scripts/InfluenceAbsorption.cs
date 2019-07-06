using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InfluenceAbsorption : MonoBehaviour
{
    public Vector3 Accumulation { get; private set; } = new Vector3();

    public float absorptionRadius = 1.0f;
    public float maxAbsorptionRate = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AbsorbFromNearbyInfluences();
    }

    private void AbsorbFromNearbyInfluences()
    {
        var influences = FindObjectsOfType<Influence>();
        
        foreach (Influence influencer in influences)
        {
            if (Vector3.Distance(transform.position, influencer.transform.position) <= absorptionRadius)
            {
                influencer.Influencing = true;
                Vector3 influenceProportions = influencer.GetInfluenceProportions();
                Accumulation += influenceProportions * maxAbsorptionRate * Time.deltaTime;
            } 
            else
            {
                influencer.Influencing = false;
            }
        }
    }
}
