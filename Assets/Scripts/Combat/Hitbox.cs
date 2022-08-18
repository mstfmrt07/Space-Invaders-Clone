using System;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int initialHealth;
    public List<LayerMaskDamage> damageLayerPairs;

    public Action<Vector3, int> OnGetDamage;
    public Action OnDestroy;

    private int currentHealth;
    public int CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = initialHealth;
    }

    private void GetDamage(Vector3 damagePos, int damage)
    {
        currentHealth -= damage;

        OnGetDamage?.Invoke(damagePos, damage);

        if (currentHealth <= 0)
        {
            OnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var pair in damageLayerPairs)
        {
            //Check if other layer is included in the pair's layer mask.
            if (pair.layers == (pair.layers | (1 << other.gameObject.layer)))
            {
                GetDamage(other.transform.position, pair.damage);
                break;
            }
        }
    }
}


//Defines which layer will give how much damage to the hitbox.
[System.Serializable]
public struct LayerMaskDamage
{
    public LayerMask layers;
    public int damage;
}