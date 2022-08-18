using System;
using UnityEngine;

public class InputController : MonoSingleton<InputController>
{
    public bool IsActive { get; set; }

    private float horizontalInput;
    public float HorizontalInput => horizontalInput;
    public Action OnShoot;

    private void Update()
    {
        if (IsActive)
            ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            horizontalInput = 1f;
        else
            horizontalInput = 0f;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            OnShoot?.Invoke();
    }
}
