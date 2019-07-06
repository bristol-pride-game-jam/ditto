using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Influence : MonoBehaviour
{
    public float C;
    public float M;
    public float Y;
    public bool Influencing;

    private ParticleSystem system;
    private InfluenceAbsorption player;

    // Start is called before the first frame update
    void Start()
    {
        system = GetComponent<ParticleSystem>();
        player = FindObjectOfType<InfluenceAbsorption>();
    }

    // Update is called once per frame
    void Update()
    {
        AlignInfluenceParticles();
        ShowParticles();
    }

    private void AlignInfluenceParticles()
    {
        var particleRotation = system.shape;
        var directionToPlayer = player.transform.position - transform.position;
        var angleFromYOrigin = Vector3.Angle(Vector3.forward, directionToPlayer);
        particleRotation.rotation = new Vector3(90, angleFromYOrigin, 0.5f);
    }

    private void ShowParticles()
    {
        if (Influencing)
        {
            if (!system.isPlaying)
            {
                system.Play();
            }
        }
        else
        {
            system.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    public Vector3 GetInfluenceProportions()
    {
        return new Vector3(C, M, Y).normalized;
    }
}
