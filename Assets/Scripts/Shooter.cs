using System;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Projectile projectilePrefab;
    public Transform shootPoint;

    public Action OnShoot;
    private Projectile currentProjectile;

    public void Shoot(Vector3 direction)
    {
        if (currentProjectile == null)
        {
            currentProjectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            currentProjectile.Shoot(direction);
            OnShoot?.Invoke();
        }
    }
}
