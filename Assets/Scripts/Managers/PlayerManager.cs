using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ManagerSingleton<PlayerManager>
{
    public PlayerCondition Player { get; private set; }
    public void SetPlayer(PlayerCondition player)
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
