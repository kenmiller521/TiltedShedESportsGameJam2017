using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    public class PlaySoundOnBreak : BreakableSubscriberBase
    {
        [SerializeField] protected AudioClip _audioClip;
        protected override void DoOnBreak(object sender, Damage.DamageEventArgs e)
        {
            if (_audioClip)
            {
                AudioSource.PlayClipAtPoint(_audioClip, this.transform.position);
            }
        }
    }
}