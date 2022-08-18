using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;

    private Vector3 direction = Vector3.zero;
    private bool isShot = false;

    private void Update()
    {
        if (isShot)
            Move();
    }

    private void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public void Shoot(Vector3 direction)
    {
        isShot = true;
        this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}