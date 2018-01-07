using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;
using UnityStandardAssets._2D;

public class Move2DCharacter : FsmStateAction
{
    //[RequiredField]
   // [CheckForComponent(typeof(Animator))]
   // [Tooltip("The target. An Animator component is required")]
    public FsmOwnerDefault gameObject;
    public FsmFloat _moveX = 1f;
    private PlatformerCharacter2D _character;

    public override void Awake()
    {
        base.Awake();
        Fsm.HandleFixedUpdate = true;
    }
    public override void Reset()
    {
        gameObject = null;
        _character = null;
        _moveX.Value = 0f;
    }

    public override void OnEnter()
    {
        //Debug.Log("Entered State");
        // get the animator component
        var go = Fsm.GetOwnerDefaultTarget(gameObject);

        if (go == null)
        {
            Debug.Log("Cant find Character");
            Finish();
            return;
        }

        _character = go.GetComponent<PlatformerCharacter2D>();

        //MoveCharacter();
    }
    public override void OnFixedUpdate()
    {
        //Debug.Log("FixedUpdate State");
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        //To Do: Call Move
        if(_character)
        {
            //Debug.Log("Moving Character");
            _character.Move(_moveX.Value, false, false);
        }
    }

}
