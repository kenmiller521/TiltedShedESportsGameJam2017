namespace MichaelWolfGames.DamageSystem
{
    public class BreakableObject : HealthManager
    {
        public Damage.DamageEventHandler OnBreak = delegate (object sender, Damage.DamageEventArgs args) { };
        public bool IsBroken { get { return IsDead; } }
        protected override void HandleDeath()
        {
            base.HandleDeath();
            Break();
        }

        public virtual void Break()
        {
            if (IsBroken) return;
            Break(this, new Damage.DamageEventArgs(0f, this.transform.position));
        }

        public virtual void Break(object sender, Damage.DamageEventArgs e)
        {
            if (IsBroken) return;
            OnBreak(sender, e);
            if(!IsDead)
                IsDead = true;
        }
    }
    
}