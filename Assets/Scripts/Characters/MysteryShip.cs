using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    [Header("References")]
    public Hitbox hitbox;
    public ExplosionEffect explosionEffect;

    [Header("Values")]
    public float invisibleDuration;
    public List<int> possibleRewards;
    public int surpriseReward;

    [Header("Movement")]
    public float movementSpeed;
    public Range<float> movementRange;

    private Vector3 direction;
    private float timer = 0f;
    private bool isActive = false;

    public void Initialize()
    {
        isActive = true;
        timer = invisibleDuration;
        direction = Vector3.right;

        hitbox.OnGetDamage += OnGetDamage;
        hitbox.OnDestroy += OnDestroyed;
    }

    public void Deactivate()
    {
        isActive = false;
        direction = Vector3.zero;

        hitbox.OnGetDamage -= OnGetDamage;
        hitbox.OnDestroy -= OnDestroyed;
    }

    private void Update()
    {
        if (!isActive)
            return;

        if (timer <= 0f)
        {
            Move();
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    private void Move()
    {
        transform.position += direction * movementSpeed * Time.deltaTime;
        
        var pos = transform.position;

        //If mystery ship hit either left or right walls.
        if ((direction == Vector3.right && pos.x >= movementRange.max) || (direction == Vector3.left && pos.x <= movementRange.min))
        {
            timer = invisibleDuration;
            direction.x *= -1f;
        }
    }

    private void OnGetDamage(Vector3 damagePos, int damage)
    {
        Camera.main.DOShakePosition(0.1f, strength: 0.3f, vibrato: 20);
        explosionEffect.Play(damagePos);
    }

    private void OnDestroyed()
    {
        var totalShots = GameManager.Instance.TotalShots;

        //Easter Egg
        //Player is eligible for max reward if mystery ship is destroyed at 23rd shot or every 15th shot after 23rd.
        bool eligibleForMaxReward = totalShots == 23 || (totalShots - 23) % 15 == 0;

        GameManager.Instance.GainScore(eligibleForMaxReward ? surpriseReward : possibleRewards[Random.Range(0, possibleRewards.Count)]);
        
        hitbox.OnGetDamage -= OnGetDamage;
        hitbox.OnDestroy -= OnDestroyed;
    }

}
