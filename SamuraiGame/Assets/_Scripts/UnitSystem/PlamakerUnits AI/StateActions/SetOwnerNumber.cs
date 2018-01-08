using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class SetOwnerNumber : FsmStateAction
{
    public FsmOwnerDefault gameObject;
    public FsmInt OwnerNumberVariable;
    public override void Reset()
    {
        base.Reset();
        gameObject = null;
    }
    public override void OnEnter()
    {
        var go = Fsm.GetOwnerDefaultTarget(gameObject);

        if (go == null)
        {
            Finish();
            return;
        }
        var u = go.GetComponent<Unit>();
        if(u)
        {
            OwnerNumberVariable.Value = u.OwnerNumber;  
        }
        else
        {
            Debug.LogWarning("Could Not Find Unit.");
        }
        Finish();
    }
}
