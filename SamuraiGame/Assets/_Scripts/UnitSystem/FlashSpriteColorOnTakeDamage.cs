using System.Collections;
using MichaelWolfGames;
using MichaelWolfGames.DamageSystem;
using UnityEngine;

namespace UnitSystem
{
    public class FlashSpriteColorOnTakeDamage: SubscriberBase<HealthManager>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _color = Color.red;
        [SerializeField] private float _duration = 0.15f;

        private Color _originalColor = Color.white;
        private Coroutine _flashCoroutine = null;
        
        protected override void Start()
        {
            base.Start();
            if (!_spriteRenderer)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            if (_spriteRenderer)
            {
                _originalColor = _spriteRenderer.color;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (_spriteRenderer)
            {
                _spriteRenderer.color = _originalColor;
            }
        }

        protected override void SubscribeEvents()
        {
            SubscribableObject.OnTakeDamage += DoOnTakeDamage;
            SubscribableObject.OnDeath += DoOnDeath;
        }

        protected override void UnsubscribeEvents()
        {
            SubscribableObject.OnTakeDamage -= DoOnTakeDamage;
            SubscribableObject.OnDeath -= DoOnDeath;
        }

        private void DoOnTakeDamage(object sender, Damage.DamageEventArgs e)
        {
            if (_spriteRenderer)
            {
                if (_flashCoroutine != null)
                {
                    StopCoroutine(_flashCoroutine);

                    //Technically unnecessary, but good practice.
                    _flashCoroutine = null;
                    _spriteRenderer.color = _originalColor; 
                }
                _flashCoroutine = StartCoroutine(CoFlashColor(_color, _originalColor, _duration));
            }
        }

        private void DoOnDeath()
        {
            if (_spriteRenderer)
            {
                //_spriteRenderer.color = _originalColor;
            }
        }

        private IEnumerator CoFlashColor(Color flashColor, Color originalColor, float duration)
        {
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                _spriteRenderer.color = Color.Lerp(flashColor, originalColor, timer / duration);
                yield return null;
            }
            _spriteRenderer.color = originalColor;
            _flashCoroutine = null;
        }
    }
}