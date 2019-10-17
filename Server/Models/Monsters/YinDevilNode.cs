using System;
using System.Collections.Generic;
using System.Linq;
using Library;
using Server.Envir;
using S = Library.Network.ServerPackets;

namespace Server.Models.Monsters
{
    public class YinDevilNode : MonsterObject
    {
        public override bool CanMove => false;
        public BuffType bufftype;
        public List<Stat> buffstats;

        public YinDevilNode()
        {
            Direction = MirDirection.Up;
            bufftype = BuffType.YinDevilNode;
            buffstats = new List<Stat>
            {
                Stat.ACPercent
            };
        }

        protected override bool InAttackRange()
        {
            if (Target.CurrentMap != CurrentMap) return false;
            if (Target.CurrentLocation == CurrentLocation) return false;

            return Functions.InRange(CurrentLocation, Target.CurrentLocation, 8);
        }

        public override bool ShouldAttackTarget(MapObject ob)
        {
            return ob != this && CanAttackTarget(ob);
        }
        public override bool CanAttackTarget(MapObject ob)
        {
            return CanHelpTarget(ob);
        }
        public override bool CanHelpTarget(MapObject ob)
        {
            return base.CanHelpTarget(ob) && ob.Buffs.All(x => x.Type != bufftype);
        }

        public override void ProcessAction(DelayedAction action)
        {
            switch (action.Type)
            {
                case ActionType.DelayAttack:
                    Buff();
                    return;
            }

            base.ProcessAction(action);
        }

        public override void ProcessSearch()
        {
            ProperSearch();
        }

        public void Buff()
        {
            List<MapObject> targets = GetTargets(CurrentMap, CurrentLocation, 8);

            Stats stats = new Stats();
            foreach (Stat stat in buffstats)
                stats[stat] = 15;

            foreach (MapObject ob in targets)
            {                
                ob.BuffAdd(bufftype, TimeSpan.FromSeconds(5), stats, false, false, TimeSpan.FromSeconds(1));
            }
        }

        protected override void Attack()
        {
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

            UpdateAttackTime();

            ActionList.Add(new DelayedAction(
                               SEnvir.Now.AddMilliseconds(400),
                               ActionType.DelayAttack));
        }
    }
}
