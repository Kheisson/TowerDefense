namespace Turrets
{
    public class AirTurret : BaseBuilding
    {
        public override void OnRemoval()
        {
            IsPlaced = false;
            base.OnRemoval();
        }
    }
}