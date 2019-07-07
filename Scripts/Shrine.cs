using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    public static List<Shrine> shrines = new List<Shrine>();
    public gameObject player;

    private InfluenceAbsorption playerAura;
    private bool activated = false;

    [Header("Activation")]
    public Vector3 aura = new Vector3(1f, 1f, 1f);
    public float tolerance = 0.1f;

    bool Activated {
        get { return activated; }
        set {
            activated = value;
            bool won = true;

            //Check Victory
            foreach (Shrine shrine in shrines) {
                if (!shrine.Activated) {
                    won = false;
                }
            }
            if (won) Victory();
        }
    }

    void Start() {
        shrines.Add(this);
        playerAura = player.GetComponent<InfluenceAbsorption>();
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            if (CanActivate(playerAura)) {
                Activated = true;
            }
        }
    }

    bool CanActivate (InfluenceAbsorption character) {
        return(
            Mathf.Magnitude(aura.x - character.Accumulation.x) < tolerance &&
            Mathf.Magnitude(aura.y - character.Accumulation.y) < tolerance &&
            Mathf.Magnitude(aura.z - character.Accumulation.z) < tolerance
        );
    }

    void Victory () {

    }
}
