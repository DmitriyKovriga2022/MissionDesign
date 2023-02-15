using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad
{
    public string BotCfg;
    public int BotsCount;
    public float SpawnInterval;
    public string SpawnPoint;
    public float SpawnTimer;
    public int CurrentSpawnWave;

    public bool Enabled;
    public bool Finished;
    public bool IsDead;

    public List<Bot> Bots = new List<Bot>();


    public Action<Bot> BotKilled;
    public Action<Squad> AllSquadDead;
    public Action<Squad> SpawnFinished;

    public Squad(string botCfg, int botsCount, float spawnInterval, string spawnPoint)
    {
        BotCfg = botCfg;
        BotsCount = botsCount;
        SpawnInterval = spawnInterval;
        SpawnPoint = spawnPoint;
        
    }

    public void Update(float dt)
    {
        if (!Finished)
        {
            if (IsAllBotsDead())
            {
                IsDead = true;
                Finished = true;
                AllSquadDead?.Invoke(this);
            }
        }

        if (!Enabled) return;
        
        SpawnTimer += dt;
        if (SpawnTimer >= SpawnInterval)
            SpawnSquad();
        
        if (CurrentSpawnWave >= BotsCount)
        {
            Enabled = false;
            SpawnFinished?.Invoke(this);
        }
    }

    bool IsAllBotsDead()
    {
        if (Bots.Count == 0) return false;

        var aliveBot = Bots.Find(x => !x.IsDead);
        return aliveBot == null;

        return false;
    }

    void SpawnSquad()
    {
        var spawner = Spawner.Instance;

        GameObject prefab = null;
        switch (BotCfg)
        {
            case "Soldier":
            {
                prefab = spawner.Soldier;
                break;
            }
            case "Gunner":
            {
                prefab = spawner.Gunner;
                    break;
            }
            case "Sniper":
            {
                prefab = spawner.Sniper;
                    break;
            }
            default:
            {
                Debug.LogError($"Can't find bot-type \"{BotCfg}\"");
                return;
            }
        }


        var position = GameObject.Find(SpawnPoint);
        if (position == null)
        {
            Debug.LogError($"Can't find point with name \"{SpawnPoint}\"");
            return;
        }
        var transform = position.transform;
        var instance = GameObject.Instantiate(prefab, transform.position, transform.rotation);

        var bot = instance.GetComponentInChildren<Bot>();
        Bots.Add(bot);
        Debug.Log($"{BotCfg} spawned");

        bot.BotKilledEvent += OnBotKilledEvent;
        SpawnTimer -= SpawnInterval;
        CurrentSpawnWave++;
    }

    void OnBotKilledEvent(Bot bot)
    {
        BotKilled?.Invoke(bot);
    }

}
