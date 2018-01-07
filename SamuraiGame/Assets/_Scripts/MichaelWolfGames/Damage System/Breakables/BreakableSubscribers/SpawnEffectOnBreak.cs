using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    public class SpawnEffectOnBreak : BreakableSubscriberBase
    {
        [SerializeField] private GameObject _effectPrefab;
        protected override void DoOnBreak(object sender, Damage.DamageEventArgs e)
        {
            if (_effectPrefab)
            {
                GameObject.Instantiate(_effectPrefab, this.transform.position, Quaternion.identity);
            }
        }
    }
}