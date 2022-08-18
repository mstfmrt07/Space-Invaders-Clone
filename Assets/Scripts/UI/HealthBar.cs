using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Hitbox hitbox;
    public Image heartPrefab;
    public Transform heartsParent;

    public Sprite heartFullIcon;
    public Sprite heartEmptyIcon;

    private List<Image> heartImages;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        heartImages = new List<Image>();

        for (int i = 0; i < hitbox.initialHealth; i++)
        {
            var image = Instantiate(heartPrefab, heartsParent);
            image.sprite = heartFullIcon;
            heartImages.Add(image);
        }

        hitbox.OnGetDamage += OnGetDamage;
        hitbox.OnDestroy += OnHitboxDestroy;
    }

    private void OnGetDamage(Vector3 pos, int damage)
    {
        for (int i = hitbox.initialHealth - 1; (i >= hitbox.CurrentHealth && i >= 0); i--)
        {
            if (heartImages[i].sprite != heartEmptyIcon)
            {
                //TODO: Visual effects to health bar icons
                heartImages[i].sprite = heartEmptyIcon;
            }
        }
    }
    
    private void OnHitboxDestroy()
    {
        hitbox.OnGetDamage -= OnGetDamage;
        hitbox.OnDestroy -= OnHitboxDestroy;
    }
}
