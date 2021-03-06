﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastUnitChecker : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    [SerializeField] private int _ownerNumber;
    public LayerMask CheckMask;
	void Start ()
    {
        if (!_unit) _unit = this.GetComponentInParent<Unit>();
        if(_unit)
        {
            _ownerNumber = _unit.OwnerNumber;
        }
	}

    public bool CheckUnits(out GameObject resultGameObject, out float resultDistance, float checkDist)
    {
        RaycastHit2D[] hits;
        hits = Physics2D.LinecastAll(this.transform.position, this.transform.position + this.transform.forward * checkDist, CheckMask.value);
        if (hits.Length > 0)
        {
            //Debug.DrawLine(this.transform.position, this.transform.position + this.transform.right * checkDist, Color.red);

            foreach (var h in hits)
            {
                if (h.collider.isTrigger) continue;
                var go = (h.collider.attachedRigidbody) ? h.collider.attachedRigidbody.gameObject : h.collider.gameObject;
                Unit unit = go.GetComponent<Unit>();
                if (unit)
                {
                    if (unit.OwnerNumber != _ownerNumber)
                    {
                        // NOTE: This doesn't guaruntee its the closest.
                        Debug.DrawLine(this.transform.position, h.point, Color.blue);
                        resultGameObject = go;
                        resultDistance = h.distance;
                        return true;
                    }
                }
            }
        }
        else
        {
            //Debug.Log("LineCast Empty");
        }
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.right * checkDist, Color.red);
        resultGameObject = null;
        resultDistance = float.MaxValue;
        return false;
    }
	
}
