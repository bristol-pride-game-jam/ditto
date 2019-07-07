using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Influence : MonoBehaviour
{
    public float R;
    public float G;
    public float B;

    [HideInInspector]
    public bool Influencing;

    private ParticleSystem system;
    private InfluenceAbsorption player;

    // Start is called before the first frame update
    void Start()
    {
        system = GetComponent<ParticleSystem>();
        player = FindObjectOfType<InfluenceAbsorption>();

        var colourProportions = GetInfluenceProportions();
        var colour = new Color(colourProportions.x, colourProportions.y, colourProportions.z);

        var renderer = GetComponent<Renderer>();
        renderer.material.SetVector("_Influence", colour);

        var particleColours = system.colorOverLifetime;
        particleColours.enabled = true;
        var gradient = new Gradient();
        gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(colour, 0f), new GradientColorKey(colour, 1f) }, 
            new GradientAlphaKey[] { new GradientAlphaKey(1f, 0.66f), new GradientAlphaKey(0f, 1f) });
        particleColours.color = gradient;
    }

    // Update is called once per frame
    void Update()
    {
        //AlignInfluenceParticles();
        ShowParticles();
    }

    private void AlignInfluenceParticles()
    {
        var particleRotation = system.shape;
        var directionToPlayer = player.transform.position - transform.position;
        var angleBetweenSelfAndPlayer = Vector3.Angle(Vector3.forward, directionToPlayer);
        var angleFromYOrigin = player.transform.position.x >= transform.position.x ? angleBetweenSelfAndPlayer : 360 - angleBetweenSelfAndPlayer;

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
        return new Vector3(R, G, B).normalized;
    }
}
