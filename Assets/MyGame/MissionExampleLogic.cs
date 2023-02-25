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
    public GameObject door3_1;
    public GameObject door4_1;
    public GameObject door5_1;

    public bool wave1isKilled = false;
    public bool wave2isKilled = false;
    public bool wave3isKilled = false;
    public bool wave4isKilled = false;
    public bool wave5isKilled = false;
    public bool wave6isKilled = false;
    public bool wave7isKilled = false;
    public bool wave8isKilled = false;
    public bool wave9isKilled = false;
    public bool wave10isKilled = false;
    public bool wave11isKilled = false;
    public bool wave12isKilled = false;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Trigger trigger2;

    private List<SetupForEnemyRespawn> wave1;
    private List<SetupForEnemyRespawn> wave2;
    private List<SetupForEnemyRespawn> wave3;
    private List<SetupForEnemyRespawn> wave4;
    private List<SetupForEnemyRespawn> wave5;
    private List<SetupForEnemyRespawn> wave6;
    private List<SetupForEnemyRespawn> wave7;
    private List<SetupForEnemyRespawn> wave8;
    private List<SetupForEnemyRespawn> wave9;
    private List<SetupForEnemyRespawn> wave10;
    private List<SetupForEnemyRespawn> wave11;
    private List<SetupForEnemyRespawn> wave12;

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
        door3_1 = GameObject.Find("Door3-1");
        door4_1 = GameObject.Find("Door4-1");
        door5_1 = GameObject.Find("Door5-1");

        button1 = Controller.GetButton("Button1");
        button1.Pressed += Button1OnPressed;
        button2 = Controller.GetButton("Button2");
        button2.Pressed += Button2OnPressed;
        button3 = Controller.GetButton("Button3");
        button3.Pressed += Button3OnPressed;
        button4 = Controller.GetButton("Button4");
        button4.Pressed += Button4OnPressed;

        var trigger = Controller.GetTrigger("Trigger1");
        trigger.Enabled = true;
        trigger.Entered += OnTrigger1Enter;
        trigger2 = Controller.GetTrigger("Trigger2");
        trigger2.Enabled = false;
        trigger2.Entered += OnTrigger2Enter;
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
            new SetupForEnemyRespawn("Sniper", "cover3-4"),
        };
        wave4 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover4-1"),
            new SetupForEnemyRespawn("Gunner", "cover4-2"),
            new SetupForEnemyRespawn("Soldier", "cover4-3"),
            new SetupForEnemyRespawn("Sniper", "cover4-4"),
            new SetupForEnemyRespawn("Sniper", "cover4-5"),
            new SetupForEnemyRespawn("Soldier", "cover4-6"),
        };
        wave5 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover5-1"),
            new SetupForEnemyRespawn("Gunner", "cover5-2"),
            new SetupForEnemyRespawn("Soldier", "cover5-3"),
            new SetupForEnemyRespawn("Soldier", "cover5-4"),
            new SetupForEnemyRespawn("Sniper", "cover5-5"),
            new SetupForEnemyRespawn("Sniper", "cover5-6"),
        };
        wave6 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover6-1"),
            new SetupForEnemyRespawn("Gunner", "cover6-2"),
            new SetupForEnemyRespawn("Soldier", "cover6-3"),
            new SetupForEnemyRespawn("Sniper", "cover6-4"),
        };
        wave7 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover7-1"),
            new SetupForEnemyRespawn("Gunner", "cover7-2"),
            new SetupForEnemyRespawn("Soldier", "cover7-3"),
            new SetupForEnemyRespawn("Soldier", "cover7-4"),
            new SetupForEnemyRespawn("Sniper", "cover7-5"),
            new SetupForEnemyRespawn("Sniper", "cover7-6"),
        };
        wave8 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover8-1"),
            new SetupForEnemyRespawn("Gunner", "cover8-2"),
            new SetupForEnemyRespawn("Soldier", "cover8-3"),
            new SetupForEnemyRespawn("Soldier", "cover8-4"),
            new SetupForEnemyRespawn("Soldier", "cover8-5"),
            new SetupForEnemyRespawn("Soldier", "cover8-6"),
        };
        wave9 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover9-1"),
            new SetupForEnemyRespawn("Gunner", "cover9-2"),
            new SetupForEnemyRespawn("Soldier", "cover9-3"),
            new SetupForEnemyRespawn("Soldier", "cover9-4"),
            new SetupForEnemyRespawn("Soldier", "cover9-5"),
            new SetupForEnemyRespawn("Sniper", "cover9-6"),
        };

        wave10 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover10-1"),
            new SetupForEnemyRespawn("Gunner", "cover10-2"),
            new SetupForEnemyRespawn("Soldier", "cover10-3"),
            new SetupForEnemyRespawn("Sniper", "cover10-4"),
        };

        wave11 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover11-1"),
            new SetupForEnemyRespawn("Gunner", "cover11-2"),
            new SetupForEnemyRespawn("Soldier", "cover11-3"),
            new SetupForEnemyRespawn("Soldier", "cover11-4"),
            new SetupForEnemyRespawn("Soldier", "cover11-5"),
            new SetupForEnemyRespawn("Sniper", "cover11-6"),
        };
        wave12 = new List<SetupForEnemyRespawn>()
        {
            new SetupForEnemyRespawn("Gunner", "cover12-1"),
            new SetupForEnemyRespawn("Gunner", "cover12-2"),
            new SetupForEnemyRespawn("Soldier", "cover12-3"),
            new SetupForEnemyRespawn("Soldier", "cover12-4"),
            new SetupForEnemyRespawn("Sniper", "cover12-5"),
            new SetupForEnemyRespawn("Sniper", "cover12-6"),
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
            var currentWave4 = SpawnSquadWave(wave4);
            currentWave4.WaveHaveTwoAliveEnemy += Wave4HaveTwoAliveEnemy;
            currentWave4.WaveKilled += Wave4OnKilled;
            button1.Pressed -= Button1OnPressed;
        } else
        {
            Debug.Log("Не выполнены условия нажатия кнопки");
        }
    }

    void Button2OnPressed()
    {
        if (wave4isKilled && wave5isKilled && wave6isKilled)
        {
            Debug.Log("Дверь открывается");
            var currentWave4 = SpawnSquadWave(wave7);
            currentWave4.WaveHaveTwoAliveEnemy += Wave7HaveTwoAliveEnemy;
            currentWave4.WaveKilled += Wave7OnKilled;
            door3_1.SetActive(false);
            button2.Pressed -= Button2OnPressed;
        }
        else
        {
            Debug.Log("Не выполнены условия нажатия кнопки");
        }
    }

    void Button3OnPressed()
    {
        if (wave7isKilled)
        {
            Debug.Log("Дверь открывается");
            var currentWave8 = SpawnSquadWave(wave8);
            currentWave8.WaveHaveTwoAliveEnemy += Wave8HaveTwoAliveEnemy;
            currentWave8.WaveKilled += Wave8OnKilled;
            door4_1.SetActive(false);
            button3.Pressed -= Button3OnPressed;
        }
        else
        {
            Debug.Log("Не выполнены условия нажатия кнопки");
        }
    }

    void Button4OnPressed()
    {
        if (wave8isKilled && wave9isKilled && wave10isKilled)
        {
            Debug.Log("Дверь открывается");
            door5_1.SetActive(false);
            var currentWave11 = SpawnSquadWave(wave11);
            currentWave11.WaveHaveTwoAliveEnemy += Wave11HaveTwoAliveEnemy;
            currentWave11.WaveKilled += Wave11OnKilled;
            button4.Pressed -= Button4OnPressed;

        }
        else
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

    void OnTrigger2Enter ()
    {
        Debug.Log("Начало процесса эвакуации");
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

    void Wave4HaveTwoAliveEnemy(Wave wave)
    {
        var currentWave5 = SpawnSquadWave(wave5);
        currentWave5.WaveHaveTwoAliveEnemy += Wave5HaveTwoAliveEnemy;
        currentWave5.WaveKilled += Wave5OnKilled;
    }

    void Wave5HaveTwoAliveEnemy(Wave wave)
    {
        var currentWave6 = SpawnSquadWave(wave6);
        currentWave6.WaveHaveTwoAliveEnemy += Wave6HaveTwoAliveEnemy;
        currentWave6.WaveKilled += Wave6OnKilled;
    }

    void Wave6HaveTwoAliveEnemy(Wave wave)
    {
        Debug.Log("Кноку можно будет нажать после полной зачистки местности");
    }

    void Wave7HaveTwoAliveEnemy(Wave wave)
    {
        Debug.Log("Осторожно! Снайперы, ликведируйте их!");
    }
    void Wave8HaveTwoAliveEnemy(Wave wave)
    {
        var currentWave9 = SpawnSquadWave(wave9);
        currentWave9.WaveHaveTwoAliveEnemy += Wave9HaveTwoAliveEnemy;
        currentWave9.WaveKilled += Wave9OnKilled;
    }

    void Wave9HaveTwoAliveEnemy(Wave wave)
    {
        var currentWave10 = SpawnSquadWave(wave10);
        currentWave10.WaveHaveTwoAliveEnemy += Wave10HaveTwoAliveEnemy;
        currentWave10.WaveKilled += Wave10OnKilled;
    }

    void Wave10HaveTwoAliveEnemy(Wave wave)
    {
        Debug.Log("Необходимо зачистить местность перед выходом из здания");
    }
    void Wave11HaveTwoAliveEnemy(Wave wave)
    {
        var currentWave12 = SpawnSquadWave(wave12);
        currentWave12.WaveHaveTwoAliveEnemy += Wave12HaveTwoAliveEnemy;
        currentWave12.WaveKilled += Wave12OnKilled;
    }

    void Wave12HaveTwoAliveEnemy(Wave wave)
    {
        trigger2.Enabled = true;
        Debug.Log("Двигайтесь к зоне эвакуации!");
    }

    void Wave12OnKilled(Wave wave)
    {
        wave12isKilled = true;
        Debug.Log("wave12 killed");
    }

    void Wave11OnKilled(Wave wave)
    {
        wave11isKilled = true;
        Debug.Log("wave11 killed");
    }

    void Wave10OnKilled(Wave wave)
    {
        wave10isKilled = true;
        Debug.Log("wave10 killed");
    }

    void Wave9OnKilled(Wave wave)
    {
        wave9isKilled = true;
        Debug.Log("wave9 killed");
    }

    void Wave8OnKilled(Wave wave)
    {
        wave8isKilled = true;
        Debug.Log("wave8 killed");
    }

    void Wave7OnKilled(Wave wave)
    {
        wave7isKilled = true;
        Debug.Log("wave7 killed");
    }

    void Wave6OnKilled(Wave wave)
    {
        wave6isKilled = true;
        Debug.Log("wave6 killed");
    }

    void Wave5OnKilled(Wave wave)
    {
        wave5isKilled = true;
        Debug.Log("wave5 killed");
    }

    void Wave4OnKilled(Wave wave)
    {
        wave4isKilled = true;
        Debug.Log("wave4 killed");
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