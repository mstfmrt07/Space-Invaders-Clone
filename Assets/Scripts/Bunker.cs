using System;
using DG.Tweening;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public Hitbox hitbox;
    public Transform splatPrefab;
    public ExplosionEffect explosionEffect;

    public Action<Bunker> OnBunkerDestroyed;

    private void Awake()
    {
        hitbox.OnGetDamage += OnGetDamage;
        hitbox.OnDestroy += OnBunkerDestroy;
    }

    private void OnGetDamage(Vector3 damagePos, int damage)
    {
        var splat = Instantiate(splatPrefab, damagePos, Quaternion.Euler(0, 0, UnityEngine.Random.Range(-90, 90)), transform);
        Camera.main.DOShakePosition(0.1f, strength: 0.3f, vibrato: 20);
        explosionEffect.Play(damagePos);
    }

    private void OnBunkerDestroy()
    {
        OnBunkerDestroyed?.Invoke(this);

        hitbox.OnGetDamage -= OnGetDamage;
        hitbox.OnDestroy -= OnBunkerDestroy;
    }
}
