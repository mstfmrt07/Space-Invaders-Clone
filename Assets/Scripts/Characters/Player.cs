using UnityEngine;
using DG.Tweening;

public class Player : MonoSingleton<Player>
{
    [Header("References")]
    public Shooter shooter;
    public Hitbox hitbox;
    public ExplosionEffect explosionEffect;

    [Header("Values")]
    public Range<float> movementRange;
    public float movementSpeed;

    private bool canMove;

    public void Initialize()
    {
        canMove = true;
        InputController.Instance.OnShoot += Shoot;
        hitbox.OnGetDamage += OnGetDamage;
    }

    public void ResetPlayer()
    {
        canMove = false;
        InputController.Instance.OnShoot -= Shoot;
        hitbox.OnGetDamage -= OnGetDamage;
    }

    private void Update()
    {
        if (!canMove)
            return;

            Move();
    }

    private void Move()
    {
        if (InputController.Instance.HorizontalInput == 0f)
            return;

        var direction = Vector3.right * InputController.Instance.HorizontalInput;

        var pos = transform.position;
        pos += direction * movementSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, movementRange.min, movementRange.max);

        transform.position = pos;
    }

    private void Shoot()
    {
        shooter.Shoot(Vector3.up);
    }

    private void OnGetDamage(Vector3 damagePos, int damage)
    {
        Camera.main.DOShakePosition(0.2f, strength: 0.5f, vibrato: 20);
        explosionEffect.Play(damagePos);
    }
}
