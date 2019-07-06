using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InfluenceAbsorption : MonoBehaviour
{
    public Vector3 Accumulation
    {
        get
        {
            return new Vector3(r, g, b);
        }
    }

    private float r = 0f;
    private float g = 0f;
    private float b = 0f;

    public float absorptionRadius = 10.0f;
    public float absorptionPerSecond = 0.1f;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        AbsorbFromNearbyInfluences();
        rend.material.SetVector("_Influence", Accumulation);
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
                r = Mathf.Min(r + (influenceProportions.x * absorptionPerSecond * Time.deltaTime), 1f);
                g = Mathf.Min(g + (influenceProportions.y * absorptionPerSecond * Time.deltaTime), 1f);
                b = Mathf.Min(b + (influenceProportions.z * absorptionPerSecond * Time.deltaTime), 1f);

                if (r + g + b >= 2.2f)
                {
                    r = 0;
                    g = 0;
                    b = 0;
                }
            } 
            else
            {
                influencer.Influencing = false;
            }
        }
    }
}
