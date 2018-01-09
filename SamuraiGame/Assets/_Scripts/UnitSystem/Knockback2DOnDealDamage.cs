using MichaelWolfGames;
using MichaelWolfGames.DamageSystem;
using UnityEngine;

namespace UnitSystem
{
    public class Knockback2DOnDealDamage : SubscriberBase<DamageSenderBase>
    {
        [SerializeField] private float _knockbackForce = 3f;
        protected override void SubscribeEvents()
        {
            SubscribableObject.OnDealDamage += ApplyKnockback;
        }

        protected override void UnsubscribeEvents()
        {
            SubscribableObject.OnDealDamage -= ApplyKnockback;
        }

        private void ApplyKnockback(object receiver, Damage.DamageEventArgs e)
        {
            Debug.Log("Calling Applying Knockback!");
            if (receiver.GetType() == typeof(DamagableBase) || receiver.GetType().IsSubclassOf(typeof(DamagableBase)))
            {
                var damagable = (DamagableBase) receiver;
                var go = damagable.gameObject;
                var rb = go.GetComponent<Rigidbody2D>();
                if (rb)
                {
                    Vector3 forceDir = ((Vector3)rb.position - e.HitPoint).normalized;
                    forceDir = Vector3.Project(forceDir, Vector2.right).normalized;
                    forceDir += Vector3.up *0.25f;
                    forceDir = forceDir.normalized;
                    rb.AddForce(forceDir*_knockbackForce, ForceMode2D.Impulse);
                    //Debug.Log("Applying Knockback!");
                    //Debug.DrawLine(e.HitPoint, (Vector3)rb.position, Color.red, 0.5f);
                    //Debug.DrawLine(e.HitPoint, e.HitPoint + forceDir*5f, Color.red, 0.5f);
                }

            }
        }
    }
}