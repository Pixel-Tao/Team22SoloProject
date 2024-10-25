using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            Init();
            return instance;
        }
    }

    private static void Init()
    {
        if (instance == null)
        {
            PlayerManager manager = FindObjectOfType<PlayerManager>();
            if (manager == null)
            {
                manager = new GameObject { name = "PlayerManager" }.AddComponent<PlayerManager>();
                instance = manager;
                DontDestroyOnLoad(manager.gameObject);
            }
        }
    }

    public Player Player { get; private set; }
    public void SetPlayer(Player player)
    {
        Player = player;
    }

    public event Action<int> CoinChangedEvent;

    public int Coin { get; private set; }
    public void AddCoin(int coin)
    {
        Coin += coin;
        CoinChangedEvent?.Invoke(Coin);
    }
    public void SubtractCoint(int coin)
    {
        Coin -= coin;
        CoinChangedEvent?.Invoke(Coin);
    }
}
