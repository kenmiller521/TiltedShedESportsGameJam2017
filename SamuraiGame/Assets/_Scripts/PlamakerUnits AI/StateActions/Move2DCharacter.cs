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
    private PlatformerCharacter2D _character;

    public override void Reset()
    {
        gameObject = null;
        _character = null;
    }

    public override void OnEnter()
    {
        // get the animator component
        var go = Fsm.GetOwnerDefaultTarget(gameObject);

        if (go == null)
        {
            Finish();
            return;
        }

        _character = go.GetComponent<PlatformerCharacter2D>();

        //MoveCharacter();
    }

    public override void OnFixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        //To Do: Call Move
    }

}
