using DG.Tweening;
using System;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Shooter shooter;
    public Hitbox hitbox;
    public ExplosionEffect explosionEffect;
    public int rewardScore;
    public LayerMask boundaryLayer;

    private bool isActive;
    public bool IsActive => isActive;
    public Action<Invader> OnDestroy;

    protected virtual void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        isActive = true;
        hitbox.OnGetDamage += OnGetDamage;
        hitbox.OnDestroy += OnInvaderDestroy;
    }

    protected virtual void OnGetDamage(Vector3 damagePos, int damage)
    {
        Camera.main.DOShakePosition(0.1f, strength: 0.3f, vibrato: 20);
        explosionEffect.Play(damagePos);
    }

    protected virtual void OnInvaderDestroy()
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
