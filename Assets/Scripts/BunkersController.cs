using System.Collections.Generic;
using UnityEngine;

public class BunkersController : MonoBehaviour
{
    public Bunker bunkerPrefab;
    public Transform container;
    public int bunkerCount;
    public Vector3 startPosition;
    public float spacing;

    private List<Bunker> bunkers = new List<Bunker>();

    public void SpawnBunkers()
    {
        DestroyAllBunkers();
        for (int i = 0; i < bunkerCount; i++)
        {
            Vector3 position = startPosition + Vector3.right * i * spacing;
            var bunker = Instantiate(bunkerPrefab, position, Quaternion.identity, container);
            bunkers.Add(bunker);
            bunker.OnBunkerDestroyed += OnBunkerDestroyed;
        }
    }

    public void DestroyAllBunkers()
    {
        if (bunkers.Count <= 0)
            return;

        foreach (var bunker in bunkers)
        {
            Destroy(bunker.gameObject);
        }

        bunkers.Clear();
    }

    private void OnBunkerDestroyed(Bunker bunker)
    {
        bunkers.Remove(bunker);
        bunker.OnBunkerDestroyed -= OnBunkerDestroyed;
    }

}
