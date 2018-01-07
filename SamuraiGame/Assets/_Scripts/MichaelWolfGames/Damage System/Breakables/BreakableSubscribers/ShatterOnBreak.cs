using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    public class ShatterOnBreak : BreakableSubscriberBase
    {
        [Header("Shatter Properties")]
        [SerializeField] protected GameObject UnshatteredGameObject;
        [SerializeField] protected Transform ShatterCenterTransform;
        [SerializeField] protected Rigidbody[] ShatterRigidbodies;
        [Space]
        [SerializeField] protected float ShatterForce = 3f;
        [SerializeField] protected float ShatterRadius = 3f;
        [SerializeField] protected float UpwardsModifier = 1f;
        [SerializeField] protected float Delay = 0f;
        protected bool _isShattered = false;

        protected override void Awake()
        {
            base.Awake();
            if (!ShatterCenterTransform) ShatterCenterTransform = this.transform;
        }

        protected override void DoOnBreak(object sender, Damage.DamageEventArgs e)
        {
            if (!_isShattered)
            {
                if (Delay <= 0f)
                {
                    Shatter();
                }
                else
                {
                    Invoke("Shatter", Delay);
                }
                _isShattered = true;
            }

        }

        protected virtual void Shatter()
        {
            if (UnshatteredGameObject)
            {
                UnshatteredGameObject.SetActive(false);
            }
            foreach (Rigidbody rb in ShatterRigidbodies)
            {
                rb.gameObject.SetActive(true);
                rb.isKinematic = false;
                rb.WakeUp();
                var c = rb.GetComponent<Collider>();
                if (c) c.enabled = true;
                rb.AddExplosionForce(ShatterForce, ShatterCenterTransform.position, ShatterRadius, UpwardsModifier, ForceMode.Impulse);
            }
        }
    }
}