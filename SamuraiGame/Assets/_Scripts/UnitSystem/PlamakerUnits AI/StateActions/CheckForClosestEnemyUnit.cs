using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HutongGames.PlayMaker;

public class CheckForClosestEnemyUnit : FsmStateAction
{
    public FsmOwnerDefault raycastGameObject;
    public FsmFloat _checkDistance = 5f;

    public FsmGameObject _closestEnemyUnitVar;
    public FsmFloat _distanceVar;

    private RaycastUnitChecker _unitChecker;

    public override void Awake()
    {
        base.Awake();
        Fsm.HandleFixedUpdate = true;
    }
    public override void Reset()
    {
        raycastGameObject = null;
        _unitChecker = null;
    }

    public override void OnEnter()
    {
        var go = Fsm.GetOwnerDefaultTarget(raycastGameObject);

        if (go == null)
        {
            Debug.Log("Finishing State");
            Finish();
            return;
        }
        _unitChecker = go.GetComponent<RaycastUnitChecker>();
        if(_unitChecker)
        {
            //Debug.Log("Found Unit Checker");
        }
        //_raycastTransform = go.transform;
    }
    public override void OnFixedUpdate()
    {
        //Debug.Log("FixedUpdate State");
        //RaycastCheckForEnemyUnits();
        RaycastCheck();
    }
    private void RaycastCheck()
    {
        if(_unitChecker)
        {
            GameObject resultObj = null;
            float resultDist;
            if(_unitChecker.CheckUnits(out resultObj, out resultDist, _checkDistance.Value))
            {
                _closestEnemyUnitVar.Value = resultObj;
                _distanceVar.Value = resultDist;
                //Debug.Log("FOUND CLOSEST");
            }
        }
    }
    //private void RaycastCheckForEnemyUnits()
    //{
    //    //RaycastHit2D hit;
    //    //hit = Physics2D.Linecast(_raycastTransform.position, _raycastTransform.position + (new Vector3(_checkDirection.Value, 0f, 0f).normalized*_maxDist.Value), _enemyLayers.value);
    //    //if (hit)
    //    //{
    //    //    var go = (hit.collider.attachedRigidbody) ? hit.collider.attachedRigidbody.gameObject : hit.collider.gameObject;
    //    //    Unit unit = go.GetComponent<Unit>();
    //    //}

    //    RaycastHit2D[] hits;
    //    hits = Physics2D.LinecastAll(_raycastTransform.position, _raycastTransform.position + (new Vector3(_checkDirection.Value, 0f, 0f).normalized * _maxDist.Value), _enemyLayers.value);
    //    if (hits.Length > 0)
    //    {
    //        foreach (var h in hits)
    //        {
    //            var go = (h.collider.attachedRigidbody) ? h.collider.attachedRigidbody.gameObject : h.collider.gameObject;
    //            Unit unit = go.GetComponent<Unit>();
    //            if(unit)
    //            {
    //                if(unit.OwnerNumber != ownerNumber.Value)
    //                {
    //                    // This doesn't guaruntee its the closest.
    //                    _closestEnemyUnitVar.Value = go;
    //                    _distanceVar.Value = h.distance;
    //                    break;
    //                }
    //            }
    //        }
    //    }

    //}
}
