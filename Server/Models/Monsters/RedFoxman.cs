using System.Collections.Generic;
using Library;
using Server.Envir;
using S = Library.Network.ServerPackets;

namespace Server.Models.Monsters
{
    public class RedFoxman : SkeletonAxeThrower
    {
        private bool CanTeleport = true;

        public override int Attacked(MapObject attacker, int power, Element element, bool canReflect = true, bool ignoreShield = false, bool canCrit = true, bool canStruck = true)
        {
            int result = base.Attacked(attacker, power, element, canReflect, ignoreShield, canCrit);

            if (result < 0 || Dead || !CanTeleport || CurrentHP > Stats[Stat.Health] / 2) return result;
            if (SEnvir.Random.Next(5) > 0) return result;

            CanTeleport = false;

            TeleportNearby(7, 12, Effect.FoxTeleportOut, Effect.FoxTeleportIn);

            return result;
        }

        protected override void Attack()
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            if (SEnvir.Random.Next(3) == 0)
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Targets = new List<uint> { Target.ObjectID } });
            else
                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, CurrentLocation = CurrentLocation, Cast = true, Type = MagicType.None, Targets = new List<uint> { Target.ObjectID } });

            UpdateAttackTime();

            if (SEnvir.Random.Next(FearRate) == 0)
                FearTime = SEnvir.Now.AddSeconds(FearDuration + SEnvir.Random.Next(4));

            ActionList.Add(new DelayedAction(
                               SEnvir.Now.AddMilliseconds(400 + Functions.Distance(CurrentLocation, Target.CurrentLocation) * Globals.ProjectileSpeed),
                               ActionType.DelayAttack,
                               Target,
                               GetDC(),
                               AttackElement));
        }
    }
}
