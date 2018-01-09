using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSetLayer : MonoBehaviour
{
    [SerializeField] private SingleUnityLayer _layer = 2;
	void Start ()
	{
	    this.gameObject.layer = _layer.LayerIndex;
	}
}
