using System;
using System.Collections.Generic;
using Library;
using Library.SystemModels;
using Server.Envir;
using S = Library.Network.ServerPackets;

namespace Server.Models.Monsters
{
    public class HellBringer : MonsterObject
    {
        public int AttackRange = 8;
        private bool BatsSpawned;
        public MonsterInfo BatInfo;

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;
            if (Target.CurrentLocation == CurrentLocation) return false;

            return Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
        }

        public override void ProcessTarget()
        {
            if (Target == null) return;

            if (InAttackRange() && CanAttack && SEnvir.Random.Next(3) == 0)
            {
                Attack();
                return;
            }

            if (CurrentLocation == Target.CurrentLocation)
            {
                MirDirection direction = (MirDirection)SEnvir.Random.Next(8);
                int rotation = SEnvir.Random.Next(2) == 0 ? 1 : -1;

                for (int d = 0; d < 8; d++)
                {
                    if (Walk(direction)) break;

                    direction = Functions.ShiftDirection(direction, rotation);
                }
            }
            else if (!Functions.InRange(CurrentLocation, Target.CurrentLocation, 1))
                MoveTo(Target.CurrentLocation);

            if (InAttackRange() && CanAttack)
                Attack();
        }

        protected override void Attack()
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            UpdateAttackTime();

            if (!BatsSpawned && CurrentHP <= MaximumHP / 4)
            {
                Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, CurrentLocation = CurrentLocation, Cast = true, Type = MagicType.HellBringerBats });
                BatsSpawned = true;
                if (BatInfo == null) return;

                for (int i = 0; i < 14; i++)
                {
                    MonsterObject mob = GetMonster(BatInfo);
                    SpawnMinion(mob);
                }
                return;
            }

            if (SEnvir.Random.Next(3) == 0 || !Functions.InRange(Target.CurrentLocation, CurrentLocation, 2))
                RangeAttack();
            else
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation }); //Animation ?

                foreach (MapObject ob in GetTargets(CurrentMap, Functions.Move(CurrentLocation, Direction), 1))
                {
                    ActionList.Add(new DelayedAction(
                        SEnvir.Now.AddMilliseconds(400),
                        ActionType.DelayAttack,
                        ob,
                        GetDC(),
                        AttackElement));
                }
            }
        }

        public override bool SpawnMinion(MonsterObject mob)
        {
            return mob.Spawn(CurrentMap.Info, CurrentMap.GetRandomLocation(CurrentLocation, AttackRange));
        }

        private void RangeAttack()
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            switch (SEnvir.Random.Next(3))
            {
                case 0:
                    Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, CurrentLocation = CurrentLocation, Cast = true, Type = MagicType.None });
                    List<MapObject> targets = GetTargets(CurrentMap, CurrentLocation, ViewRange);

                    if (targets.Count > 0)
                    {
                        foreach (MapObject ob in targets)
                        {
                            if (SEnvir.Random.Next(2) > 0) continue;

                            ob.ApplyPoison(new Poison
                            {
                                Owner = this,
                                Type = PoisonType.Silenced,
                                TickFrequency = TimeSpan.FromSeconds(10),
                                TickCount = 1,
                            });
                        }
                    }
                    break;
                default:
                    Broadcast(new S.ObjectRangeAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation, Targets = new List<uint> { Target.ObjectID } });
                    foreach (MapObject ob in GetTargets(CurrentMap, Target.CurrentLocation, 2))
                        ActionList.Add(new DelayedAction(
                                       SEnvir.Now.AddMilliseconds(400),
                                       ActionType.DelayAttack,
                                       ob,
                                       GetMC(),
                                       AttackElement));
                    break;
            }
        }
    }
}
