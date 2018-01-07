﻿using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;
using MichaelWolfGames.DetectorSystem;
using MichaelWolfGames.Utility;

namespace MichaelWolfGames.AI.Playmaker
{
	public class CheckDetector : StateActionBehaviour
	{
        public DetectorBase detector;
        [ActionSection("Detect Event")]
        public FsmEvent DetectEvent;
        public float DetectorRange = 20f;
	    public bool CheckEveryFrame = true;

	    public bool UseRealizeTimer = false;
	    public FsmFloat RealizeTime = 0.5f;

	    private Timer _realizeTimer;

	    public override void OnEnter()
	    {
	        base.OnEnter();
            if (!UseRealizeTimer || RealizeTime.Value <= 0f)
            {
                if (DoCheckDetector())
                {
                    this.Fsm.Event(DetectEvent);
                }
                if (!CheckEveryFrame)
                {
                    Finish();
                }
            }
            else
            {
                _realizeTimer = new Timer(RealizeTime.Value);
            }
        }

	    public override void OnUpdate()
	    {
	        //base.OnFixedUpdate();
            //Debug.Log("Checking Detector...");

            if (!UseRealizeTimer)
            {
                if (DoCheckDetector())
                {
                    this.Fsm.Event(DetectEvent);
                }
            }
            else
            {
                if (DoCheckDetector())
                {
                    if (_realizeTimer.Tick(Time.fixedDeltaTime))
                    {
                        this.Fsm.Event(DetectEvent);
                    }
                }
                else
                {
                    _realizeTimer.Restart();
                }
            }
        }

	    //public override void OnUpdate()
     //   {
     //       if (!UseRealizeTimer)
     //       {
     //           if (DoCheckDetector())
     //           {
     //               this.Fsm.Event(DetectEvent);
     //           }
     //       }
     //       else
     //       {
     //           if (DoCheckDetector())
     //           {
     //               if (_realizeTimer.Tick())
     //               {
     //                   this.Fsm.Event(DetectEvent);
     //               }
     //               else
     //               {
     //                   _realizeTimer.Restart();
     //               }
     //           }
     //       }
     //   }

	    public bool DoCheckDetector()
	    {
	        if (this.BehaviourController.LookTargetTransform == null)
	        {
                //Debug.LogWarning("[CheckDetector] Detector does not have a target!");
                return false;
	        }
            return detector.CheckForTarget(this.BehaviourController.LookTargetTransform, DetectorRange);
        }
    }
}
