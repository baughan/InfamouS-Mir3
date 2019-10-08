using Library;
using Server.Envir;

namespace Server.Models.Monsters
{
    public class WingedOma : SkeletonAxeThrower
    {
        public override void ProcessTarget()
        {
            if (Target == null) return;

            MirDirection direction;
            int rotation;
            if (!InAttackRange())
            {
                if (CurrentLocation == Target.CurrentLocation)
                {
                    direction = (MirDirection) SEnvir.Random.Next(8);
                    rotation = SEnvir.Random.Next(2) == 0 ? 1 : -1;

                    for (int d = 0; d < 8; d++)
                    {
                        if (Walk(direction)) break;

                        direction = Functions.ShiftDirection(direction, rotation);
                    }
                }
                else
                    MoveTo(Target.CurrentLocation);

                return;
            }

            if (Functions.InRange(Target.CurrentLocation, CurrentLocation, AttackRange - 1))
            {
                if (SEnvir.Random.Next(2) == 0)
                    direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
                else
                    direction = Functions.DirectionFromPoint(Target.CurrentLocation, CurrentLocation);

                rotation = SEnvir.Random.Next(2) == 0 ? 1 : -1;

                for (int d = 0; d < 8; d++)
                {
                    if (Walk(direction)) break;

                    direction = Functions.ShiftDirection(direction, rotation);
                }
            }
            if (!CanAttack || SEnvir.Now < FearTime) return;

            Attack();
        }
    }
}
