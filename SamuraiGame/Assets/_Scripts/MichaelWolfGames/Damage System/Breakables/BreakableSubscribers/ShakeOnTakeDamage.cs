using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    public class ShakeOnTakeDamage : DamagableSubscriberBase
    {
        [Header("Shake Properties")]
        [SerializeField] protected Transform ShakeTransform;
        [SerializeField] protected float MaxShakeMagnitude = 0.2f;
        [SerializeField] protected float ShakeDuration = 0.2f;
        [SerializeField] protected uint NumberOfShakes = 2;

        protected Vector3 ShakeStartLocalPos;
        protected Coroutine ShakeCoroutine;

        protected override void Awake()
        {
            base.Awake();
            if (!ShakeTransform) ShakeTransform = this.transform;
            ShakeStartLocalPos = ShakeTransform.localPosition;
        }

        protected override void DoOnTakeDamage(object sender, Damage.DamageEventArgs damageEventArgs)
        {
            Shake();    
        }

        protected virtual void Shake()
        {
            if (ShakeCoroutine != null)
            {
                StopCoroutine(ShakeCoroutine);
            }
            ShakeCoroutine = StartCoroutine(CoMultiShake((int)NumberOfShakes, ShakeDuration, MaxShakeMagnitude));
        }

        protected virtual IEnumerator CoMultiShake(int numberOfShakes, float totalDuration, float maxMagnitude)
        {
            float dividedDuration = totalDuration/numberOfShakes;
            for (int i = 0; i < numberOfShakes; i++)
            {
                yield return StartCoroutine(CoShake(dividedDuration, maxMagnitude));
            }
        }

        protected virtual IEnumerator CoShake(float duration, float maxMagnitude)
        {
            float halfDuration = duration/2f;
            Vector3 shakeEndPos = ShakeStartLocalPos + Random.insideUnitSphere*Random.Range(maxMagnitude/2f, maxMagnitude);
            float timer = 0f;
            while (timer < halfDuration)
            {
                timer += Time.deltaTime;
                ShakeTransform.localPosition = Vector3.Lerp(ShakeStartLocalPos, shakeEndPos, timer/ halfDuration);
                yield return null;
            }
            timer = 0f;
            while (timer < halfDuration)
            {
                timer += Time.deltaTime;
                ShakeTransform.localPosition = Vector3.Lerp(shakeEndPos, ShakeStartLocalPos, timer / halfDuration);
                yield return null;
            }
            ShakeTransform.localPosition = ShakeStartLocalPos;
            ShakeCoroutine = null;
        }
    }
}