using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class MissionBaseLogic
{
    public BaseController Controller;

    public MissionBaseLogic(BaseController controller)
    {
        Controller = controller;
    }

    public virtual void Init()
    {
        
    }

    public void MissionTimeFinished()
    {
        Controller.FinishMission(FinishReason.TimeIsOver);
    }
}
