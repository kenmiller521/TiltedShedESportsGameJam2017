using System.Collections;
using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    /// <summary>
    /// Simply Implements the defaults from the abstract class HealthManagerBase, which currently has no abstract implementation.
    /// 
    /// Michael Wolf
    /// April 8, 2017
    /// </summary>
    public class HealthManager : HealthManagerBase
	{
        // (See Comments Above.)
	    [SerializeField] protected bool _debug;
	    [SerializeField] protected float _invulnTime = 0.25f;
	    [SerializeField] protected bool _isInvulnerable = false;
	    public override void ApplyDamage(object sender, Damage.DamageEventArgs e)
	    {
	        base.ApplyDamage(sender, e);
            //if(_debug) Debug.Log(this.gameObject.name + " RECEIVED DAMAGE: " + e.DamageValue);
	    }

	    protected override void HandleDamage(object sender, Damage.DamageEventArgs e)
	    {
            // Add an invulnerability delay for non-bullets.
            if (e.DamageType != Damage.DamageType.Bullet)
	        {
	            if(_isInvulnerable) return;
	            StartCoroutine(CoInvulnerabilityDelay(_invulnTime));
	        }
	        base.HandleDamage(sender, e);
	    }

	    protected virtual IEnumerator CoInvulnerabilityDelay(float delay)
	    {
	        _isInvulnerable = true;
            yield return new WaitForSeconds(_invulnTime);
	        _isInvulnerable = false;
	    }
	}
}