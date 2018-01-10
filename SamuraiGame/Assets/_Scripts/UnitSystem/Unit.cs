using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int OwnerNumber = 0;
    public UnitType Type = UnitType.Generic;
    public enum UnitType
    {
        Generic,
        Infantry,
        Archer,
        Calvary,
        Building
    }
}
