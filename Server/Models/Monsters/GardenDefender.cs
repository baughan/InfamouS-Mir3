using System;
using System.Collections.Generic;
using Library;
using Server.Envir;
using S = Library.Network.ServerPackets;

namespace Server.Models.Monsters
{
    public class GardenDefender : MonsterObject
    {
        protected override void Attack()
        {

            if (CurrentHP <= MaximumHP / 2 && SEnvir.Random.Next(4) > 0)
                Defend();
            else
                base.Attack();
        }

        private void Defend()
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            UpdateAttackTime();

            Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

            List<MapObject> obs = GetAllObjects(CurrentLocation, 1);

            foreach (MapObject ob in obs)
            {
                if (!CanHelpTarget(ob)) continue;

                Stats buffStats = new Stats
                {
                    [Stat.MinAC] = ob.Stats[Stat.MaxAC] / 2,
                    [Stat.MaxAC] = ob.Stats[Stat.MaxAC] / 2,
                    [Stat.PhysicalResistance] = 1,
                };

                ob.BuffAdd(BuffType.Resilience, TimeSpan.FromSeconds(6), buffStats, true, false, TimeSpan.Zero);
            }
        }
    }
}
