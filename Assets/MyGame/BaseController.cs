using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Random = System.Random;
using Enums;
using Vector3 = UnityEngine.Vector3;

namespace Enums
{
    public enum FinishReason
    {
        TimeIsOver,
        Defeat,
        Victory,
        Default
    }
}


public class BaseController : MonoBehaviour
{
    private MissionBaseLogic _missionLogic;

    [NonSerialized]
    public FinishReason State;
    public List<Squad> Squads = new List<Squad>();
    public List<Timer> Timers = new List<Timer>();
    public readonly Random Rand = new Random(DateTime.Now.Millisecond);

    [NonSerialized]
    public float Dt;
    private Spawner _spawner;
    
    void Start()
    {
        Dt = 1f / 30;
        _spawner = Spawner.Instance;
        _missionLogic = new MissionExampleLogic(this);
        State = FinishReason.Default;

        _missionLogic.Init();

    }

    public Timer CreateTimer()
    {
        var timer = new Timer();
        Timers.Add(timer);
        return timer;
    }

    public object SpawnObject(string name, Vector3 position)
    {
        var prefab = LoadResource(name);
        return prefab != null ? _spawner.Spawn(prefab, position) : null;
    }

    public object SpawnObject(string name, string position)
    {
        var pos = GameObject.Find(position);
        var prefab = LoadResource(name);
        return prefab != null ? _spawner.Spawn(prefab, pos.transform) : null;
    }

    public UnityEngine.Object LoadResource(string name)
    {
        var prefab = Resources.Load(name);
        if (prefab == null)
        {
            Debug.LogError($"Can't find prefab with name {name} in /Resources");
        }
        return prefab;
    }

    public void DestroyObject(object obj)
    {
        Destroy((GameObject)obj);
    }

    public Squad SpawnSquad(string botCfg, int botCount, float spawnInterval, params string[] spawnPoints)
    {
        var squad = new Squad(botCfg, botCount, spawnInterval, GetRandomElement(spawnPoints));
        Squads.Add(squad);
        squad.Enabled = true;
        return squad;
    }

    public Trigger GetTrigger(string name)
    {
        var trigger = GameObject.Find(name);
        if (trigger == null)
        {
            Debug.LogError($"Can't find trigger with name \"{name}\"");
            return null;
        }
        return trigger.GetComponentInChildren<Trigger>(); 
    }

    public Button GetButton(string name)
    {
        var button = GameObject.Find(name);
        if (button == null)
        {
            Debug.LogError($"Can't find button with name \"{name}\"");
            return null;
        }
        return button.GetComponentInChildren<Button>(); ;
    }

    public void FinishMission(FinishReason reason)
    {
        if (State != FinishReason.Default)
            return;

        if (reason == FinishReason.TimeIsOver)
        {
            State = FinishReason.TimeIsOver;
            Debug.Log("Time is Over! You Lose!");
            return;
        }

        if (reason == FinishReason.Defeat)
        {
            State = FinishReason.Defeat;
            Debug.Log("You Lose!");
            return;
        }

        if (reason == FinishReason.Victory)
        {
            State = FinishReason.Victory;
            Debug.Log("You Won!");
        }
    }

    string GetRandomElement(string[] elements)
    {
        var i = Rand.Next(elements.Length);
        return elements[i];
    }
    
    void Update()
    {
        if (State != FinishReason.Default)
            return;
		var tempSquads = new List<Squad>(Squads);
        foreach (var squad in tempSquads)
        {
            squad.Update(Dt);
        }

        var tempTimers = new List<Timer>(Timers);
        foreach (var timer in tempTimers)
        {
            timer.Update(Dt);
        }
        Squads.RemoveAll(x => x.Finished);
    
    }
}
