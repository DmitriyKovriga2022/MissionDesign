using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Enums;
using System;

public class MissionExampleLogic : MissionBaseLogic
{

    public object Car1;

    public MissionExampleLogic(BaseController controller): base(controller)
    {

    }

    public override void Init()
    {
        var button1 = Controller.GetButton("Button1");
        button1.Pressed += Button1OnPressed;
        var trigger = Controller.GetTrigger("Trigger1");
        trigger.Enabled = true;
        trigger.Entered += OnTrigger1Enter;

        


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
        
    }

    void OnTrigger1Enter ()
    {
        var squad1 = CreateAndSpawnSquad(
                                 "Gunner",
                                 "Gunner",
                                 "Soldier",
                                 "Soldier",
                                 "Soldier",
                                 "Sniper",
                                    "cover1",
                                    "cover2",
                                    "cover3",
                                    "cover4",
                                    "cover5",
                                    "cover6"
                                 );
        Controller.GetTrigger("Trigger1").Enabled = false;
        squad1.AllSquadDead += OnSquad1Dead;
    }

    void OnSquad1Dead(Squad squad)
    {
        Debug.Log("Door open");
    }


    //Создал метод для удобного создания своих отрядов. Переопределил его ради гибкости использования. Присутствует повторение кода, но на мой взгляд это меньшее из зол.

    Squad CreateAndSpawnSquad(string enemyType1, string enemyType2, string enemyType3, string enemyType4,
        string cover1, string cover2, string cover3, string cover4)
    {
        Squad squad1 = Controller.SpawnSquad(enemyType1, 1, 0, cover1);
        try
        {
            squad1.Bots.AddRange(Controller.SpawnSquad(enemyType2, 1, 0, cover3).Bots);
            squad1.Bots.AddRange(Controller.SpawnSquad(enemyType3, 1, 0, cover3).Bots);
            squad1.Bots.AddRange(Controller.SpawnSquad(enemyType4, 1, 0, cover4).Bots);
        }
        catch (Exception e)
        {
            Debug.Log("Не критичная ошибка: " + e);
        }

        Debug.Log(squad1.Bots);
        return squad1;
    }

        Squad CreateAndSpawnSquad(string enemyType1, string enemyType2, string enemyType3, string enemyType4, string enemyType5,
        string cover1, string cover2, string cover3, string cover4, string cover5)
    {
        Squad squad1 = Controller.SpawnSquad(enemyType1, 1, 0, cover1);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType2, 1, 0, cover2).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType3, 1, 0, cover3).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType4, 1, 0, cover4).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType5, 1, 0, cover5).Bots);
        return squad1;
    }

    Squad CreateAndSpawnSquad(string enemyType1, string enemyType2, string enemyType3, string enemyType4, string enemyType5, string enemyType6,
        string cover1, string cover2, string cover3, string cover4, string cover5, string cover6)
    {
        Squad squad1 = Controller.SpawnSquad(enemyType1, 1, 0, cover1);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType2, 1, 0, cover2).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType3, 1, 0, cover3).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType4, 1, 0, cover4).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType5, 1, 0, cover5).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType6, 1, 0, cover6).Bots);
        Debug.Log(squad1.Bots);
        return squad1;
    }

    Squad CreateAndSpawnSquad(string enemyType1, string enemyType2, string enemyType3, string enemyType4, string enemyType5, string enemyType6, string enemyType7,
        string cover1, string cover2, string cover3, string cover4, string cover5, string cover6, string cover7)
    {
        Squad squad1 = Controller.SpawnSquad(enemyType1, 1, 0, cover1);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType2, 1, 0, cover2).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType3, 1, 0, cover3).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType4, 1, 0, cover4).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType5, 1, 0, cover5).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType6, 1, 0, cover6).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType7, 1, 0, cover7).Bots);
        return squad1;
    }

    Squad CreateAndSpawnSquad(string enemyType1, string enemyType2, string enemyType3, string enemyType4, string enemyType5, string enemyType6, string enemyType7, string enemyType8,
        string cover1, string cover2, string cover3, string cover4, string cover5, string cover6, string cover7, string cover8)
    {
        Squad squad1 = new Squad(enemyType1, 1, 0, cover1);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType2, 1, 0, cover2).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType3, 1, 0, cover3).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType4, 1, 0, cover4).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType5, 1, 0, cover5).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType6, 1, 0, cover6).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType7, 1, 0, cover7).Bots);
        squad1.Bots.AddRange(Controller.SpawnSquad(enemyType8, 1, 0, cover8).Bots);
        return squad1;
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