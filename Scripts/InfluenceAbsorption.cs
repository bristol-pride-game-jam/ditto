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
        var nearbyInfluences = influences.Where(i => Vector3.Distance(transform.position, i.transform.position) <= absorptionRadius);
        foreach (Influence nearbyInfluence in nearbyInfluences)
        {
            Vector3 influenceProportions = nearbyInfluence.GetInfluenceProportions();
            Accumulation += influenceProportions * maxAbsorptionRate * Time.deltaTime;
        }
    }
}
