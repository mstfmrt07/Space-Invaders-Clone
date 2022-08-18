using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public ParticleSystem effectPrefab;

    public void Play(Vector3 position)
    {
        var particle = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(particle.gameObject, 0.5f);
    }
}
