using System.Collections;
using UnityEngine;
using System.Collections.Generic;
namespace MichaelWolfGames.DamageSystem
{
    public class BreakOnTooMuchWeight : MonoBehaviour
    {
        [SerializeField] private BreakableObject _breakableObject;
        [SerializeField] private LayerMask _ignoreLayerMask = 0;
        [SerializeField] private float _breakWeight = 10f;
        [SerializeField] private float _currentWeight = 0f;
        [SerializeField] private float _maxStressTime = 0.5f;

        //private MichaelWolfGames.Timer _timer;
        private float _timer;

        [SerializeField] private List<Rigidbody> _rigidBodiesThisFrame = new List<Rigidbody>();

        private void Awake()
        {
            if (!_breakableObject) _breakableObject = GetComponent<BreakableObject>();
            _timer = 0f;
        }

        private void OnEnable()
        {
            StartCoroutine(CoLateFixedUpdate());
        }

        private IEnumerator CoLateFixedUpdate()
        {
            while (this.enabled && !_breakableObject.IsBroken)
            {
                yield return new WaitForFixedUpdate();
                LateFixedUpdate();
            }
        }

        private void LateFixedUpdate()
        {
            if (_breakableObject.IsBroken) return;
            EvaluateWeight();
        }

        //private void OnCollisionEnter(Collision col)
        //{
        //    if (_breakableObject.IsBroken) return;
        //    var go = col.collider.gameObject;
        //    if (_ignoreLayerMask.Contains(go.layer))
        //    {
        //        return;
        //    }
        //    var rb = col.rigidbody;
        //    if (rb != null)
        //    {
        //        if (!_rigidBodiesThisFrame.Contains(rb))
        //        {
        //            _rigidBodiesThisFrame.Add(rb);
        //        }
        //    }
        //}

        private void OnCollisionStay(Collision col)
        {
            if (_breakableObject.IsBroken) return;
            var go = col.collider.gameObject;
            if (_ignoreLayerMask.Contains(go.layer))
            {
                return;
            }
            var rb = col.rigidbody;
            if (rb != null)
            {
                //Debug.Log("Normal = " + col.contacts[0].normal.y);
                bool isValidContact = (col.contacts[0].normal.y <= -0.9f);
                if (!_rigidBodiesThisFrame.Contains(rb))
                {
                    if(isValidContact)
                        _rigidBodiesThisFrame.Add(rb);
                }
                else
                {
                    if (!isValidContact)
                        _rigidBodiesThisFrame.Remove(rb);
                }
            }
        }
        private void OnCollisionExit(Collision col)
        {
            if (_breakableObject.IsBroken) return;
            var go = col.collider.gameObject;
            if (_ignoreLayerMask.Contains(go.layer))
            {
                return;
            }
            var rb = col.rigidbody;
            if (rb != null)
            {
                if (_rigidBodiesThisFrame.Contains(rb))
                {
                    _rigidBodiesThisFrame.Remove(rb);
                }
            }
        }

        private void EvaluateWeight()
        {
            float totalMass = 0f;
            var arr = _rigidBodiesThisFrame.ToArray();
            Rigidbody heaviestRigidbody = null;
            for (int i = 0; i < arr.Length; i++)
            {
                var rb = arr[i];
                //if(rb.)
                
                if (heaviestRigidbody == null) heaviestRigidbody = rb;
                else
                {
                    if (rb.mass > heaviestRigidbody.mass)
                        heaviestRigidbody = rb;
                }
                if (rb == null)
                {
                    _rigidBodiesThisFrame.Remove(rb);
                }
                else if(!rb.gameObject.activeInHierarchy)
                {
                    _rigidBodiesThisFrame.Remove(rb);
                }
                else
                {
                    totalMass += rb.mass;
                }
            }
            _currentWeight = totalMass;
            if (_currentWeight >= _breakWeight)
            {
                _timer += Time.fixedDeltaTime;
                if (_timer > _maxStressTime)
                {
                    var breakpoint = (heaviestRigidbody != null)
                        ? heaviestRigidbody.transform.position
                        : _breakableObject.transform.position;
                    _breakableObject.Break(this, new Damage.DamageEventArgs(_currentWeight, breakpoint));
                }
            }
            else
            {
                _timer = 0f;
            }
        }
    }
}