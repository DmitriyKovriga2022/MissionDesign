using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Enums;
using System;

public class MissionExampleLogic : MissionBaseLogic
{

    public object Car1;
    public GameObject door1;
    public GameObject door2;
    public GameObject door2_1;
    public GameObject door2_2;

    public bool wave1isKilled = false;
    public bool wave2isKilled = false;
    public bool wave3isKilled = false;

    public Button button1;

    private List<SetupForEnemyRespawn> wave1;
    private List<SetupForEnemyRespawn> wave2;
    private List<SetupForEnemyRespawn> wave3;

    public MissionExampleLogic(BaseController controller): base(controller)
    {

    }

    public override void Init()
    {
        GenerateWaves();
        door1 = GameObject.Find("Door1");
        door2 = GameObject.Find("Door2");
        door2_1 = GameObject.Find("Door2-1");
        door2_2 = GameObject.Find("Door2-2");
        button1 = Controller.GetButton("Button1");
        button1.Pressed += Button1OnPressed;
        var trigger = Controller.GetTrigger("Trigger1");
        trigger.Enabled = true;
        trigger.Entered += OnTrigger1Enter;
        button1.enabled = true;
    }

    void GenerateWaves()
    {
        wave1 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover1"),
            new SetupForEnemyRespawn("Gunner", "cover2"),
            new SetupForEnemyRespawn("Soldier", "cover3"),
            new SetupForEnemyRespawn("Soldier", "cover4"),
            new SetupForEnemyRespawn("Soldier", "cover5"),
            new SetupForEnemyRespawn("Sniper", "cover6"),
        };

        wave2 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover2-1"),
            new SetupForEnemyRespawn("Gunner", "cover2-2"),
            new SetupForEnemyRespawn("Soldier", "cover2-3"),
            new SetupForEnemyRespawn("Soldier", "cover2-4"),
            new SetupForEnemyRespawn("Soldier", "cover2-5"),
            new SetupForEnemyRespawn("Sniper", "cover2-6"),
        };
        wave3 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover3-1"),
            new SetupForEnemyRespawn("Gunner", "cover3-2"),
            new SetupForEnemyRespawn("Soldier", "cover3-3"),
            new SetupForEnemyRespawn("Sniper", "cover3-6"),
        };
    }

    
    //----------- evacuation logic -------------
    void OnBossTimeEnded()
    {
        Controller.FinishMission(FinishReason.Defeat);
    }
    //----------- evacuation logic -------------

    void CarDestroy(UnityEngine.Object car)
    {
        Controller.DestroyObject(car);
    }
    
    void Button1OnPressed()
    {
        if (wave1isKilled && wave2isKilled && wave3isKilled)
        {
            Debug.Log("Первая цель выполнена, двигаемся дальше");
        } else
        {
            Debug.Log("Не выполнены условия нажатия кнопки");
        }
    }

    void OnTrigger1Enter ()
    {
        var currentWave1 = SpawnSquadWave(wave1);
        Controller.GetTrigger("Trigger1").Enabled = false;
        currentWave1.WaveHaveTwoAliveEnemy += Wave1HaveTwoAliveEnemy;
        currentWave1.WaveKilled += Wave1OnKilled;
    }

    void Wave1HaveTwoAliveEnemy(Wave wave)
    {
        Debug.Log("doors open");
        door1.SetActive(false);
        door2.SetActive(false);

        var currentWave2 = SpawnSquadWave(wave2);
        currentWave2.WaveHaveTwoAliveEnemy += Wave2HaveTwoAliveEnemy;
        currentWave2.WaveKilled += Wave2OnKilled;
    }

    void Wave2HaveTwoAliveEnemy(Wave wave)
    {
        door2_1.SetActive(false);
        door2_2.SetActive(false);

        var currentWave3 = SpawnSquadWave(wave3);
        currentWave3.WaveHaveTwoAliveEnemy += Wave3HaveTwoAliveEnemy;
        currentWave3.WaveKilled += Wave3OnKilled;
    }
    void Wave3HaveTwoAliveEnemy(Wave wave)
    {
        Debug.Log("Кноку можно будет нажать после полной зачистки местности");
    }

        void Wave3OnKilled(Wave wave)
    {
        wave3isKilled = true;
        Debug.Log("wave3 killed");
    }

    void Wave2OnKilled(Wave wave)
    {
        wave2isKilled = true;
        Debug.Log("wave2 killed");
    }

    void Wave1OnKilled(Wave wave)
    {
        wave1isKilled = true;
        Debug.Log("wave1 killed");
    }

    Wave SpawnSquadWave(List<SetupForEnemyRespawn> setupForEnemyRespawns)
    {
        List<Squad> squadList = new List<Squad>() { };

        foreach (SetupForEnemyRespawn setup in setupForEnemyRespawns)
        {
            var squad = Controller.SpawnSquad(setup.type, 1, 1, setup.cover);
            squadList.Add(squad);
        }
        return new Wave(squadList);
    }


    //Создал метод для удобного создания своих отрядов. Переопределил его ради гибкости использования. Присутствует повторение кода, но на мой взгляд это меньшее из зол.

    
}

public class SetupForEnemyRespawn
{
    public string type;
    public string cover;

    public SetupForEnemyRespawn (string type, string cover)
    {
        this.type = type;
        this.cover = cover;
    }
}

public class Wave
{
    List<Squad> squadList = new List<Squad>();
    public Action<Wave> WaveKilled;
    public Action<Wave> WaveHaveTwoAliveEnemy;

    public Wave (List<Squad> squads)
    {
        squadList.AddRange(squads);

        foreach (Squad squad in squadList)
        {
            squad.AllSquadDead += DeleteSquad;
        }
    }

    public void DeleteSquad(Squad squad)
    {
        if (squadList.Contains(squad))
        {
            squadList.Remove(squad);
        }

        if (squadList.Count == 2)
        {
            WaveHaveTwoAliveEnemy.Invoke(this);
        }

        if (squadList.Count == 0)
        {
            WaveKilled.Invoke(this);
        }
    }
}

//example
/*Controller.SpawnSquad("Soldier", 1, 2, "Covers/cover2");
Controller.SpawnSquad("Soldier", 1, 2, "Covers/cover1", "Covers/cover3");*/

/*Car1 = Controller.SpawnObject("Car", new Vector3(-14.72f, 0.7f, -22.376f));
Car1 = Controller.SpawnObject("Car", "CarPoint");*/


//----------- evacuation logic -------------
/*var missionTimer = Controller.CreateTimer();
    missionTimer.TimerFinish += MissionTimeFinished; // функция лежит в MissionBaseLogic
    missionTimer.Start(50f);*/

/*var timer = Controller.CreateTimer();
timer.TimerFinish += OnBossTimeEnded;
timer.Start(8f);*/
//----------- evacuation logic -------------