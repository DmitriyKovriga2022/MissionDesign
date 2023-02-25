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


    public MissionExampleLogic(BaseController controller): base(controller)
    {

    }

    public override void Init()
    {
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
        var wave1 = SpawnSquadWave("Sniper", "Sniper", "Sniper", "Sniper", "Sniper", "Sniper",
            "cover1", "cover2", "cover3", "cover4", "cover5", "cover6");
        Controller.GetTrigger("Trigger1").Enabled = false;
        wave1.WaveHaveTwoAliveEnemy += Wave1HaveTwoAliveEnemy;
        wave1.WaveKilled += Wave1OnKilled;
    }

    void Wave1HaveTwoAliveEnemy(Wave wave)
    {
        Debug.Log("doors open");
        door1.SetActive(false);
        door2.SetActive(false);

        var wave2 = SpawnSquadWave("Sniper", "Sniper", "Sniper", "Sniper", "Sniper", "Sniper",
            "cover2-1", "cover2-2", "cover2-3", "cover2-4", "cover2-5", "cover2-6");
        wave2.WaveHaveTwoAliveEnemy += Wave2HaveTwoAliveEnemy;
        wave2.WaveKilled += Wave2OnKilled;
    }

    void Wave2HaveTwoAliveEnemy(Wave wave)
    {
        door2_1.SetActive(false);
        door2_2.SetActive(false);

        var wave3 = SpawnSquadWave("Sniper", "Sniper", "Sniper", "Sniper", "Sniper", "Sniper",
            "cover3-1", "cover3-2", "cover3-3", "cover3-4", "cover3-5", "cover3-6");
        wave3.WaveHaveTwoAliveEnemy += Wave3HaveTwoAliveEnemy;
        wave3.WaveKilled += Wave3OnKilled;
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

    Wave SpawnSquadWave(string type1, string type2, string type3, string type4, string type5, string type6,
        string cover1, string cover2, string cover3, string cover4, string cover5, string cover6)
    {
        var squad1 = Controller.SpawnSquad(type1, 1, 1, cover1);
        var squad2 = Controller.SpawnSquad(type2, 1, 1, cover2);
        var squad3 = Controller.SpawnSquad(type3, 1, 1, cover3);
        var squad4 = Controller.SpawnSquad(type4, 1, 1, cover4);
        var squad5 = Controller.SpawnSquad(type5, 1, 1, cover5);
        var squad6 = Controller.SpawnSquad(type6, 1, 1, cover6);
        List<Squad> squadList = new List<Squad>() { squad1, squad2, squad3, squad4, squad5, squad6 };
        return new Wave(squadList);
    }


    //Создал метод для удобного создания своих отрядов. Переопределил его ради гибкости использования. Присутствует повторение кода, но на мой взгляд это меньшее из зол.

    
}

public class SetupForEnemyRespawn
{
    public string type;
    public string cover;
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