using System;
using System.Collections;
using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
     /// <summary>
    /// Base Class for most (if not all) Health Managers. 
    /// Extends from DamagableBase and implements IHealthManager.
    /// This class:
    /// - Receives ApplyDamage calls, triggers OnTakeDamage events, and handles health changes accordingly.
    /// - When health reaches 0, fires an OnDeath events and toggles an IsDead bool.
    /// - Hosts public methods for Kill, SetHealth, and Revive.
    /// 
    /// Michael Wolf
    /// April 8, 2017
    /// </summary>
    public abstract class HealthManagerBase : DamagableBase, IHealthManager
	{
         #region Properties

        [SerializeField] protected float maxHealth = 100f; 
        public float MaxHealth
        {
            get { return maxHealth; }
            protected set { maxHealth = value; }
        }
	    
        [SerializeField] protected float currentHealth = 100f;
        public float CurrentHealth 
        {
            get { return currentHealth; }
            protected set { currentHealth = value; }
        }

	    public float CurrentHealthPercent
	    {
	        get { return CurrentHealth / MaxHealth; }
	    }

        public bool IsDead { get; protected set; }

        protected Action onDeath = delegate { };
        public Action OnDeath
        {
            get { return onDeath; }
            set { onDeath = value; }
        }

        protected Action<float> onUpdateHealth = delegate { };
        public Action<float> OnUpdateHealth
        {
            get { return onUpdateHealth; }
            set { onUpdateHealth = value; }
        }
        #endregion

        //================================================================================

        #region Public Methods

        public void SetHealth(float health)
        {
            currentHealth = health;
            OnUpdateHealth(currentHealth);
        }

        public void AddHealth(float health)
        {
            currentHealth += health;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            OnUpdateHealth(currentHealth);
        }

        public void Revive()
        {
            if(!IsDead) return;
            IsDead = false;
            currentHealth = maxHealth;
            OnUpdateHealth(currentHealth);
        }
        public virtual void Kill()
        {
            if (IsDead) return;
            currentHealth = 0f;
            OnUpdateHealth(currentHealth);
            HandleDeath();
        }

	    public virtual void RegenerateHealthToFull(float regenSpeed)
	    {
	        RegenerateHealth((MaxHealth - CurrentHealth), regenSpeed);
	    }
	    public virtual void RegenerateHealth(float healAmmount, float regenSpeed)
	    {
	        StopHealthRegen();
	        RegenerateHealthCoroutine = StartCoroutine(CoRegenerateHealth(healAmmount, regenSpeed));
	    }

	    public void StopHealthRegen()
	    {
            if (RegenerateHealthCoroutine != null)
            {
                StopCoroutine(RegenerateHealthCoroutine);
                RegenerateHealthCoroutine = null;
            }
        }

        #endregion

        //================================================================================

	    #region Initialization

        protected virtual void Awake()
	    {
	        Initialize();
	    }

	    protected virtual void Initialize()
	    {
            IsDead = false;
            currentHealth = maxHealth;
        }
        #endregion

        //================================================================================

        #region Damage Handling
        
        public override void ApplyDamage(object sender, Damage.DamageEventArgs e)
        {
            if (IsDead) return;
            // Cannot receive damage from same faction.
            if (e.SourceFaction == Faction && e.SourceFaction != Damage.Faction.Generic)
            {
                //Debug.Log("SAME FACTION DAMAGE");
                return;
            } 

            // We still call TakeDamage if the damage would kill them, and leave it up to the delgates to check for death.
            HandleDamage(sender, e);
            //if(!IsDead)
            base.ApplyDamage(sender, e);    // Simply Calls OnTakeDamage(sender, e);
        }

        protected virtual void HandleDamage(object sender, Damage.DamageEventArgs e)
        {
            if (IsDead) return;
            float damage = CalculateDamage(e);
            currentHealth -= damage;
            OnUpdateHealth(currentHealth);
            if (currentHealth <= 0f)
            {
                currentHealth = 0f;
                HandleDeath();
            }
        }
        protected virtual void HandleDeath()
        {
            if (IsDead) return;
            IsDead = true;
            OnDeath();
        }

        /// <summary>
        /// Calculates the effective damage on the HealthManager.
        /// Override this for calculating the effects of armor and resistances.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
	    protected virtual float CalculateDamage(Damage.DamageEventArgs e)
	    {
            // For the simplest case, just pass the raw DamageValue.
            return e.DamageValue;
	    }
        
        #endregion

	    protected Coroutine RegenerateHealthCoroutine = null;
	    protected virtual IEnumerator CoRegenerateHealth(float healAmmount, float regenRate)
	    {
	        float duration = healAmmount / regenRate;
	        float timer = 0f;
	        while (timer < duration)
	        {
	            timer += Time.deltaTime;
                AddHealth((regenRate * Time.deltaTime));
	            yield return null;
	        }
            RegenerateHealthCoroutine = null;
        }
    }
}