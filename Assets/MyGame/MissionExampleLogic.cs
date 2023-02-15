using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Enums;

public class MissionExampleLogic : MissionBaseLogic
{

    public object Car1;

    public MissionExampleLogic(BaseController controller): base(controller)
    {

    }

    public override void Init()
    {
        var missionTimer = Controller.CreateTimer();
        missionTimer.TimerFinish += MissionTimeFinished; // функция лежит в MissionBaseLogic
        missionTimer.Start(50f);
        
        Controller.SpawnSquad("Soldier", 1, 2, "Covers/cover2");
		Controller.SpawnSquad("Soldier", 1, 2, "Covers/cover1", "Covers/cover3");
		
        var button1 = Controller.GetButton("Button1");
        button1.Pressed += Button1OnPressed;
    }

    void OnBossTimeEnded()
    {
        Controller.FinishMission(FinishReason.Defeat);
    }

    void CarDestroy()
    {
        Controller.DestroyObject(Car1);
    }
    
    void Button1OnPressed()
    {
        var squad1 = Controller.SpawnSquad("Sniper", 1, 0, "Covers/cover4");
        squad1.AllSquadDead += OnSquad1Dead;
    }

    void OnSquad1Dead(Squad squad)
    {
        var squad1 = Controller.SpawnSquad("Gunner", 1, 0, "Covers/cover5");
        squad1.BotKilled += OnBossKilled;

        Car1 = Controller.SpawnObject("Car", new Vector3(-14.72f, 0.7f, -22.376f));
		//Car1 = Controller.SpawnObject("Car", "CarPoint");

        var timer = Controller.CreateTimer();
        timer.TimerFinish += OnBossTimeEnded;
        timer.Start(8f);
    }

    void OnBossKilled(Bot bot)
    {
        CarDestroy();
        Controller.FinishMission(FinishReason.Victory);
    }
}
