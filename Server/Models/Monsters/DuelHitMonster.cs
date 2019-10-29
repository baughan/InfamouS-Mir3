using Library;
using Server.Envir;
using S = Library.Network.ServerPackets;

namespace Server.Models.Monsters
{
    public class DuelHitMonster : MonsterObject
    {
        protected override void Attack()
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            UpdateAttackTime();

            if (SEnvir.Random.Next(3) == 0)
                Attack2();
            else
                Attack1();
        }

        protected virtual void Attack1()
        {
            Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation });

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

        protected virtual void Attack2()
        {
            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = Direction, CurrentLocation = CurrentLocation, Cast = true, Type = MagicType.None, AttackElement = Element.None });

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
}
