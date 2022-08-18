using System;
using System.Collections.Generic;
using UnityEngine;

public class InvadersController : MonoBehaviour
{
    [Header("Spawn Invaders")]
    public Invader[] invaderPrefabs;
    public Vector2Int gridSize;
    public Transform container;
    public float spacing;

    [Header("Movement")]
    public Range<float> movementRange;
    public float baseSpeed;
    public float speedMultiplier;
    public float rowHeight;

    [Header("Attack")]
    public float attackCooldown;

    private Vector3 initialPosition;
    private List<Invader> invaders = new List<Invader>();
    private Vector3 direction;
    private bool canMove;
    private float currentSpeed;

    public int InvadersEliminated => (gridSize.x * gridSize.y) - invaders.Count;
    public Action OnGridClear;
    public Action OnBaseInvaded;

    private void Awake()
    {
        initialPosition = container.position;
    }

    public void Initialize()
    {
        SpawnInvaders();
        InvokeRepeating(nameof(AttemptShoot), attackCooldown, attackCooldown);

        canMove = true;
        direction = Vector3.right;
        currentSpeed = baseSpeed;
    }
    private void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    public void StopInvaders()
    {
        canMove = false;
        direction = Vector3.zero;
        currentSpeed = 0f;

        CancelInvoke(nameof(AttemptShoot));
    }

    public void ResetGrid()
    {
        foreach (Invader invader in invaders)
        {
            invaders.Remove(invader);
            Destroy(invader.gameObject);
        }
    }

    private void SpawnInvaders()
    {
        invaders.Clear();

        Vector2 size = new Vector2(gridSize.x - 1, gridSize.y - 1) * spacing;
        Vector2 middle = size / 2f;

        container.position = initialPosition;
        container.position -= GameManager.Instance.WavesClear * spacing * Vector3.up;

        for (int row = 0; row < gridSize.y; row++)
        {
            for (int column = 0; column < gridSize.x; column++)
            {
                var invader = Instantiate(invaderPrefabs[row], container);
                invader.transform.localPosition = -middle + (spacing * new Vector2(column, row));

                invaders.Add(invader);
                invader.OnDestroy += OnInvaderDestroyed;
            }
        }
    }

    private void OnInvaderDestroyed(Invader invader)
    {
        invaders.Remove(invader);
        invader.OnDestroy -= OnInvaderDestroyed;

        GameManager.Instance.GainScore(invader.rewardScore);

        if (invaders.Count == 0)
        {
            OnGridClear?.Invoke();
        }
    }

    private void Move()
    {
        var pos = container.position;
        var speed = baseSpeed * Mathf.Pow(1 + speedMultiplier, InvadersEliminated);
        pos += direction * speed * Time.deltaTime;
        container.position = pos;

        foreach (Invader invader in invaders)
        {
            if (!invader.IsActive)
                continue;

            var invaderPos = invader.transform.position;
            //If invader hit either left or right walls.
            if ((direction == Vector3.right && invaderPos.x >= movementRange.max) || (direction == Vector3.left && invaderPos.x <= movementRange.min))
            {
                AdvanceRow();
            }

            //If invader has reached the player row.
            if (invaderPos.y <= Player.Instance.transform.position.y)
            {
                OnBaseInvaded?.Invoke();
            }
        }
    }

    private void OnInvadeBase()
    {
        OnBaseInvaded?.Invoke();
    }

    private void AttemptShoot()
    {
        foreach (Invader invader in invaders)
        {
            if (!invader.IsActive)
                continue;

            var probabilty = 1.0f / invaders.Count;
            if (UnityEngine.Random.value < probabilty)
            {
                invader.Shoot();
                break;
            }
        }
    }

    private void AdvanceRow()
    {
        direction.x *= -1f;
        container.position -= Vector3.up * rowHeight;
    }
}
