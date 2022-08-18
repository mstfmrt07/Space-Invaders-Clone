using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    public float timeBetweenFrames;

    private SpriteRenderer spriteRenderer;
    private int currentFrame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //if sprite count is no more than 1, return.
        if (sprites.Length <= 1)
            return;

        InvokeRepeating(nameof(Animate), timeBetweenFrames, timeBetweenFrames);
    }

    private void Animate()
    {
        currentFrame++;

        //if current frame exceeds sprite count, set it to 0.
        if (currentFrame >= sprites.Length)
            currentFrame = 0;

        spriteRenderer.sprite = sprites[currentFrame];
    }
}
