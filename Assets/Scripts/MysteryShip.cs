using System.Collections.Generic;
using UnityEngine;

public class MysteryShip : Invader
{
    public List<int> possibleRewards;

    private Vector3 direction;

    protected override void OnInvaderDestroy()
    {
        base.OnInvaderDestroy();

        var totalShots = GameManager.Instance.TotalShots;

        //Easter Egg
        //Player is eligible for max reward if mystery ship is destroyed at 23rd shot or every 15th shot after 23rd.
        bool eligibleForMaxReward = totalShots == 23 || (totalShots - 23) % 15 == 0;

        GameManager.Instance.GainScore(eligibleForMaxReward ? rewardScore : possibleRewards[Random.Range(0, possibleRewards.Count)]);
    }

    private void Update()
    {
        if (IsActive)
        {
            Move();
        }
    }

    private void Move()
    {

    }
}
