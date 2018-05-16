using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSelfDestroy : MonoBehaviour
{
    void Awake()
    {
        Destroy(this.gameObject, this.GetComponent<ParticleSystem>().main.duration);
    }
}
