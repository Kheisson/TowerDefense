namespace Turrets
{
    public class GroundTurret : BaseBuilding
    {
        public override void OnRemoval()
        {
            IsPlaced = false;
            base.OnRemoval();
        }
    }
}