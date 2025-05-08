namespace ProjectFrameWar.Core.Players
{
    internal class EnergyComponent : PlayerComponent
    {
        public int energyCurrent;

        public float energyRegenRate;

        public override void Component_PreUpdate()
        {


            base.Component_PreUpdate();
        }
    }
}