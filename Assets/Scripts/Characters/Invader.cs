using DG.Tweening;
using System;
using UnityEngine;

public class Invader : MonoBehaviour
{
    [Header("References")]
    public Shooter shooter;
    public Hitbox hitbox;
    public ExplosionEffect explosionEffect;
    public LayerMask boundaryLayer;

    [Header("Values")]
    public int rewardScore;

    private bool isActive;
    public bool IsActive => isActive;
    public Action<Invader> OnDestroy;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        isActive = true;
        hitbox.OnGetDamage += OnGetDamage;
        hitbox.OnDestroy += OnInvaderDestroy;
    }

    private void OnGetDamage(Vector3 damagePos, int damage)
    {
        Camera.main.DOShakePosition(0.1f, strength: 0.3f, vibrato: 20);
        explosionEffect.Play(damagePos);
    }

    private void OnInvaderDestroy()
    {
        OnDestroy?.Invoke(this);
        hitbox.OnGetDamage -= OnGetDamage;
        hitbox.OnDestroy -= OnInvaderDestroy;
    }

    public void Shoot()
    {
        shooter.Shoot(Vector3.down);
    }
}
