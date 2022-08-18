using DG.Tweening;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public Hitbox hitbox;
    public Transform splatPrefab;
    public ExplosionEffect explosionEffect;

    private void Awake()
    {
        hitbox.OnGetDamage += OnGetDamage;
    }

    private void OnGetDamage(Vector3 damagePos, int damage)
    {
        var splat = Instantiate(splatPrefab, damagePos, Quaternion.Euler(0, 0, Random.Range(-90, 90)), transform);
        Camera.main.DOShakePosition(0.1f, strength: 0.3f, vibrato: 20);
        explosionEffect.Play(damagePos);
    }
}
