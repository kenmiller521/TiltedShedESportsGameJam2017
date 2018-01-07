using MichaelWolfGames.SoundWaveSystem;
using UnityEngine;

namespace MichaelWolfGames.DamageSystem
{
    public class EmitSoundWaveOnBreak : BreakableSubscriberBase
    {
        [SerializeField] private float breakSoundIntensity = 50;
        protected override void DoOnBreak(object sender, Damage.DamageEventArgs e)
        {
            SoundWave.EmitSoundWave(this, new SoundWaveEventArguments(this.transform.position, breakSoundIntensity));
        }

    }
}