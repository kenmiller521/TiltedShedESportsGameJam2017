using UnityEngine;

namespace MichaelWolfGames.DamageSystem.DamageSenders.DamageSenderSubscribers
{
    public abstract class DamageSenderSubscriberBase : MonoBehaviour
    {
        [SerializeField] protected DamageSenderBase _damageSender;
        protected bool _isInitialized = false;
        protected bool _isSubscribed = false;

        protected virtual void Awake()
        {
            //VRTK_SDKManager.instance.AddBehaviourToToggleOnLoadedSetupChange(this);
            if (!_isInitialized)
            {
                Initialize();
            }
        }

        protected virtual void OnEnable()
        {
            if (!_isInitialized)
            {
                Initialize();
            }
            HandleEventSubscription(true);
        }

        protected virtual void OnDisable()
        {
            HandleEventSubscription(false);
        }

        protected virtual void OnDestroy()
        {
            //VRTK_SDKManager.instance.RemoveBehaviourToToggleOnLoadedSetupChange(this);
        }

        protected virtual void Initialize()
        {
            if (!_isInitialized)
            {
                if (!_damageSender) _damageSender = GetComponent<DamageSenderBase>();
                DoOnInitialize();

                OnInitialized();
                _isInitialized = true;
            }
            if (!_isSubscribed && this.isActiveAndEnabled)
                HandleEventSubscription(true);
        }
        protected virtual void HandleEventSubscription(bool state)
        {
            if (_damageSender && _isInitialized)
            {
                if (state)
                {
                    if (!_isSubscribed) _damageSender.OnDealDamage += OnDealDamageEvent;

                }
                else
                {
                    if (_isSubscribed) _damageSender.OnDealDamage -= OnDealDamageEvent;
                }

                _isSubscribed = state;
            }
        }
        protected virtual void DoOnInitialize() { }
        protected virtual void OnInitialized() { }

        protected abstract void OnDealDamageEvent(object sender, Damage.DamageEventArgs e);
    }
}