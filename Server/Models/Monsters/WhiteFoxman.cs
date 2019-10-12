using System;
using System.Collections.Generic;
using System.Linq;
using Library;
using Server.Envir;
using S = Library.Network.ServerPackets;

namespace Server.Models.Monsters
{
    public class WhiteFoxman : SkeletonAxeThrower
    {
        private DateTime NextCurseTime;
        private bool CanSummon, HasSummoned;
        private int SummonCount;
        
        public WhiteFoxman()
        {
            if (SEnvir.Random.Next(4) == 0)
                CanSummon = true;
        }

        protected override void Attack()
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);

            if (CanSummon && !HasSummoned && SEnvir.Random.Next(3) == 0)
            {
                HasSummoned = true;
                SpawnPet();
            }
            else if (SEnvir.Now > NextCurseTime && SEnvir.Random.Next(10) == 0)
            {
                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, CurrentLocation = CurrentLocation, Cast = true, Type = MagicType.None, Targets = new List<uint> { Target.ObjectID } });
                NextCurseTime = SEnvir.Now + TimeSpan.FromSeconds(10 + SEnvir.Random.Next(10));

                foreach (MapObject ob in GetTargets(Target.CurrentMap, Target.CurrentLocation, 1))
                {
                    if (SEnvir.Random.Next(2) == 0)
                        ob.ApplyPoison(new Poison
                        {
                            Owner = this,
                            Value = 20,
                            TickCount = 1,
                            TickFrequency = TimeSpan.FromSeconds(5),
                            Type = PoisonType.Slow,
                        });
                }
            }
            else
            {
                Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Targets = new List<uint> { Target.ObjectID } });

                ActionList.Add(new DelayedAction(
                               SEnvir.Now.AddMilliseconds(400 + Functions.Distance(CurrentLocation, Target.CurrentLocation) * Globals.ProjectileSpeed),
                               ActionType.DelayAttack,
                               Target,
                               GetDC(),
                               AttackElement));
            }

            UpdateAttackTime();

            if (SEnvir.Random.Next(FearRate) == 0)
                FearTime = SEnvir.Now.AddSeconds(FearDuration + SEnvir.Random.Next(4));            
        }

        public void SpawnPet()
        {
            MonsterObject mob = GetMonster(SEnvir.MonsterInfoList.Binding.First(x => x.Flag == MonsterFlag.Shinsu));
            if (mob == null) return;

            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

            mob.Spawn(CurrentMap.Info, CurrentMap.GetRandomLocation(CurrentLocation, 1));
            mob.Target = Target;

            if (++SummonCount > 5)
                CanSummon = false;
        }

        protected override void OnTargetChanged()
        {
            if (Target == null)
                HasSummoned = false;
        }
    }
}
