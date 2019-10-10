using System;
using Library;
using Server.Envir;

namespace Server.Models.Monsters
{
    public class BlackFoxman : DuelHitMonster
    {
        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;
            if (Target.CurrentLocation == CurrentLocation) return false;

            int x = Math.Abs(Target.CurrentLocation.X - CurrentLocation.X);
            int y = Math.Abs(Target.CurrentLocation.Y - CurrentLocation.Y);

            if (x > 2 || y > 2) return false;


            return x == 0 || x == y || y == 0;
        }

        protected override void Attack()
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            UpdateAttackTime();

            if (Functions.InRange(CurrentLocation, Target.CurrentLocation, 1) && SEnvir.Random.Next(3) > 0)
                Attack1();
            else
                Attack2();
        }
    }
}
