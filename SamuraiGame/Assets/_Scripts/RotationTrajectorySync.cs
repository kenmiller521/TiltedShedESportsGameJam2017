using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTrajectorySync : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

	// Use this for initialization
	void Start ()
    {
        if (!rb) rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(rb)
        {
            Vector3 diff = rb.velocity.normalized;
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
	}
}
