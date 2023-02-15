using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Bot: MonoBehaviour
{
    [NonSerialized]
    public string BotCfg;
    [NonSerialized]
    public bool IsDead;
    private GameObject _configs;
    private Spawner _spawner;
    private GameObject _visual;

    public Action<Bot> BotKilledEvent; 

    public Bot(string botCfg)
    {
      
    }

    public void Init()
    {

    }

    public void Died()
    {
        Debug.Log($"You killed bot");
        IsDead = true;
        BotKilledEvent?.Invoke(this);
        Destroy(gameObject);
    }
}
