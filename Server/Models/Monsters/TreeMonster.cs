using Library;

namespace Server.Models.Monsters
{
    public class TreeMonster : MonsterObject
    {
        public override bool CanMove => false;
        public override bool CanAttack => false;

        public TreeMonster()
        {
            Direction = MirDirection.Up;
            Passive = true;
        }
        
        public override void ProcessRoam()
        {
        }
        public override void ProcessSearch()
        {
        }
        public override bool CanAttackTarget(MapObject ob)
        {
            return false;
        }
        public override void ProcessTarget()
        {
        }

        public override long Attacked(MapObject ob, long power, Element element, bool canReflect = true, bool ignoreShield = false, bool canCrit = true, bool canStruck = true)
        {
            return base.Attacked(ob, 1, element, ignoreShield, canCrit);
        }

        public override bool ApplyPoison(Poison p)
        {
            return false;
        }
    }
}
