namespace MichaelWolfGames.DamageSystem
{
    public class SetInactiveOnBreak : BreakableSubscriberBase
    {
        protected override void DoOnBreak(object sender, Damage.DamageEventArgs e)
        {
            this.InvokeActionAtEndOfFrame(()=> {this.gameObject.SetActive(false);});

        }
    }
}