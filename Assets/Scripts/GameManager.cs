using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    [SerializeField] private Player player;

    protected override void Awake()
    {
        base.Awake();

        if (player == null)
        {
            player = gameObject.GetComponent<Player>();
        }

        if (player == null)
        {
            Debug.LogError("Player component is not assigned or found on GameManager.");
        }
    }

    public Player GetPlayer()
    {
        return player;
    }
}
