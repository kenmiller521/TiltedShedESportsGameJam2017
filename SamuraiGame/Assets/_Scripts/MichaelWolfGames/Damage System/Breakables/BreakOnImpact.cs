using MichaelWolfGames.SoundWaveSystem;
using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    public class BreakOnImpact : MonoBehaviour
    {
        [SerializeField] private BreakableObject _breakableObject;
        [SerializeField] private LayerMask _ignoreLayerMask = 0;
        [SerializeField] private float _minimumImpulse = 1f;

        private void Start()
        {
            if (!_breakableObject) _breakableObject = GetComponent<BreakableObject>();
        }

        private void OnCollisionEnter(Collision col)
        {
            if(!enabled) return;
            var go = col.collider.gameObject;
            if (_ignoreLayerMask.Contains(go.layer))
            {
                return;
            }
            var impMag = col.impulse.magnitude;
            //Debug.Log("Impulse = " + impMag);
            if (impMag > _minimumImpulse)
            {
                Debug.Log("Broke from Impact: " + impMag);
                _breakableObject.Break(this, new Damage.DamageEventArgs(impMag, col.contacts[0].point));
            }
        }
    }
}