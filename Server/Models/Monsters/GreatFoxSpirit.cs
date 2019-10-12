using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Library;
using Library.SystemModels;
using Library.Network;
using Server.Envir;
using Server.DBModels;
using S = Library.Network.ServerPackets;

namespace Server.Models.Monsters
{
    public class GreatFoxSpirit : MonsterObject
    {
        public int AttackRange = 10;
        public override bool CanTurn => false;
        public override bool CanMove => false;
        public DateTime CastTime;

        public int Stage
        {
            get { return _Stage; }
            set
            {
                if (_Stage == value) return;

                _Stage = value;
                Broadcast(new S.FoxStageChange { ObjectID = ObjectID, Stage = (byte)Stage });
            }
        }
        private int _Stage;

        public GreatFoxSpirit()
        {
            Direction = MirDirection.Up;
            Stage = 0;
        }

        public override void ProcessAI()
        {
            base.ProcessAI();

            if (Dead) return;

            Stage = 4 - (CurrentHP / (Stats[Stat.Health] / 4));
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;
            if (Target.CurrentLocation == CurrentLocation) return false;

            return Functions.InRange(CurrentLocation, Target.CurrentLocation, AttackRange);
        }

        public override void ProcessTarget()
        {
            if (Target == null) return;

            if (InAttackRange() && CanAttack)
            {
                Attack();
                return;
            }
        }

        protected override void Attack()
        {
            UpdateAttackTime();

            if (SEnvir.Now > CastTime)
            {
                if (SEnvir.Random.Next(6) == 0 || !Functions.InRange(Target.CurrentLocation, CurrentLocation, 2))
                    RangeAttack();
                else
                {
                    Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation }); //Animation ?

                    foreach (MapObject ob in GetTargets(CurrentMap, CurrentLocation, 1))
                    {
                        ActionList.Add(new DelayedAction(
                            SEnvir.Now.AddMilliseconds(400),
                            ActionType.DelayAttack,
                            ob,
                            (int)(GetDC() * (0.25M * Stage)),
                            AttackElement));
                    }
                }
            }
        }

        private void RangeAttack()
        {
            switch (SEnvir.Random.Next(8))
            {
                case 0:
                    DragonRepulse();
                    CastTime = SEnvir.Now.AddSeconds(10);
                    break;
                case 1:
                    Purification();
                    break;
                default:
                    MassCyclone(MagicType.FoxCyclone, 45);
                    break;
            }
        }

        public override Packet GetInfoPacket(PlayerObject ob)
        {
            return new S.ObjectMonster
            {
                ObjectID = ObjectID,
                MonsterIndex = MonsterInfo.Index,

                Location = CurrentLocation,

                NameColour = NameColour,
                Direction = Direction,
                Dead = Dead,

                PetOwner = PetOwner?.Name,

                Skeleton = NeedHarvest && Skeleton && (Drops == null || !Drops.ContainsKey(ob.Character.Account)),

                Poison = Poison,

                EasterEvent = EasterEventMob,
                HalloweenEvent = HalloweenEventMob,
                ChristmasEvent = ChristmasEventMob,

                ExtraInt = Stage,

                Buffs = Buffs.Where(x => x.Visible).Select(x => x.Type).ToList()
            };
        }
    }
}
