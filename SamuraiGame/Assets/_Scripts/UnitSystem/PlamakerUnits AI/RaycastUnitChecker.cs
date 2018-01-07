using System.Collections;
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
        hits = Physics2D.LinecastAll(this.transform.position, this.transform.position + this.transform.right * checkDist, CheckMask.value);
        if (hits.Length > 0)
        {
            foreach (var h in hits)
            {
                var go = (h.collider.attachedRigidbody) ? h.collider.attachedRigidbody.gameObject : h.collider.gameObject;
                Unit unit = go.GetComponent<Unit>();
                if (unit)
                {
                    if (unit.OwnerNumber != _ownerNumber)
                    {
                        // NOTE: This doesn't guaruntee its the closest.
                        resultGameObject = go;
                        resultDistance = h.distance;
                        return true;
                    }
                }
            }
        }
        else
        {
            Debug.Log("LineCast Empty");
        }
        resultGameObject = null;
        resultDistance = float.MaxValue;
        return false;
    }
	
}
