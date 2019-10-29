using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Library;
using Library.SystemModels;
using Server.Envir;
using Server.DBModels;
using S = Library.Network.ServerPackets;

namespace Server.Models.Monsters
{
    public class FireBird : MonsterObject
    {
        public int AttackRange = 10;

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
            else if (!Functions.InRange(CurrentLocation, Target.CurrentLocation, 2))
                MoveTo(Target.CurrentLocation);
        }

        public override void ProcessAction(DelayedAction action)
        {
            switch (action.Type)
            {
                case ActionType.RangeAttack:
                    ScorchedEarth((MirDirection)action.Data[0]);
                    return;
            }

            base.ProcessAction(action);
        }

        protected override void Attack()
        {
            Direction = Functions.DirectionFromPoint(CurrentLocation, Target.CurrentLocation);
            UpdateAttackTime();

            if (SEnvir.Random.Next(6) == 0 || !Functions.InRange(Target.CurrentLocation, CurrentLocation, 2))
                RangeAttack();
            else
            {
                Broadcast(new S.ObjectAttack { ObjectID = ObjectID, Direction = Direction, Location = CurrentLocation }); //Animation ?

                foreach (MapObject ob in GetTargets(CurrentMap, Functions.Move(CurrentLocation, Direction, 2), 1))
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

        private void RangeAttack()
        {
            switch (SEnvir.Random.Next(2))
            {
                case 0:
                    MassCyclone(MagicType.IgyuCyclone, 45);
                    break;
                default:
                    foreach (MirDirection dir in Enum.GetValues(typeof(MirDirection)))
                    {
                        ActionList.Add(new DelayedAction(
                               SEnvir.Now.AddMilliseconds(500 + 500 * (byte)dir),
                               ActionType.RangeAttack,
                               dir));
                    }

                    break;
            }
        }

        private void ScorchedEarth(MirDirection direction)
        {
            if (Dead) return;
            UpdateAttackTime();

            LineAoE(10, 0, 0, MagicType.IgyuScorchedEarth, Element.Fire, direction);
        }

        public void LineAoE(int distance, int min, int max, MagicType magic, Element element, MirDirection dir)
        {
            List<uint> targetIDs = new List<uint>();
            List<Point> locations = new List<Point>();

            Broadcast(new S.ObjectMagic { ObjectID = ObjectID, Direction = dir, CurrentLocation = CurrentLocation, Cast = true, Type = magic, Targets = targetIDs, Locations = locations, AttackElement = Element.None });

            UpdateAttackTime();


            for (int d = min; d <= max; d++)
            {
                MirDirection direction = Functions.ShiftDirection(dir, d);

                for (int i = 1; i <= distance; i++)
                {
                    Point location = Functions.Move(CurrentLocation, direction, i);
                    Cell cell = CurrentMap.GetCell(location);

                    if (cell == null) continue;

                    locations.Add(cell.Location);

                    if (cell.Objects != null)
                    {
                        foreach (MapObject ob in cell.Objects)
                        {
                            if (!CanAttackTarget(ob)) continue;

                            ActionList.Add(new DelayedAction(
                                SEnvir.Now.AddMilliseconds(500 + i * 75),
                                ActionType.DelayAttack,
                                ob,
                                GetMC(),
                                element));
                        }
                    }

                    switch (direction)
                    {
                        case MirDirection.Up:
                        case MirDirection.Right:
                        case MirDirection.Down:
                        case MirDirection.Left:
                            cell = CurrentMap.GetCell(Functions.Move(location, Functions.ShiftDirection(direction, -2)));

                            if (cell == null) continue;

                            locations.Add(cell.Location);

                            if (cell?.Objects != null)
                            {
                                foreach (MapObject ob in cell.Objects)
                                {
                                    if (!CanAttackTarget(ob)) continue;

                                    ActionList.Add(new DelayedAction(
                                        SEnvir.Now.AddMilliseconds(500 + i * 75),
                                        ActionType.DelayAttack,
                                        ob,
                                        GetMC(),
                                        element));
                                }
                            }
                            cell = CurrentMap.GetCell(Functions.Move(location, Functions.ShiftDirection(direction, 2)));

                            if (cell == null) continue;

                            locations.Add(cell.Location);

                            if (cell?.Objects != null)
                            {
                                foreach (MapObject ob in cell.Objects)
                                {
                                    if (!CanAttackTarget(ob)) continue;

                                    ActionList.Add(new DelayedAction(
                                        SEnvir.Now.AddMilliseconds(500 + i * 75),
                                        ActionType.DelayAttack,
                                        ob,
                                        GetMC(),
                                        element));
                                }
                            }
                            break;
                        case MirDirection.UpRight:
                        case MirDirection.DownRight:
                        case MirDirection.DownLeft:
                        case MirDirection.UpLeft:
                            cell = CurrentMap.GetCell(Functions.Move(location, Functions.ShiftDirection(direction, -1)));

                            if (cell == null) continue;

                            locations.Add(cell.Location);

                            if (cell?.Objects != null)
                            {
                                foreach (MapObject ob in cell.Objects)
                                {
                                    if (!CanAttackTarget(ob)) continue;

                                    ActionList.Add(new DelayedAction(
                                        SEnvir.Now.AddMilliseconds(500 + i * 75),
                                        ActionType.DelayAttack,
                                        ob,
                                        GetMC(),
                                        element));
                                }
                            }
                            cell = CurrentMap.GetCell(Functions.Move(location, Functions.ShiftDirection(direction, 1)));

                            if (cell == null) continue;

                            locations.Add(cell.Location);

                            if (cell?.Objects != null)
                            {
                                foreach (MapObject ob in cell.Objects)
                                {
                                    if (!CanAttackTarget(ob)) continue;

                                    ActionList.Add(new DelayedAction(
                                        SEnvir.Now.AddMilliseconds(500 + i * 75),
                                        ActionType.DelayAttack,
                                        ob,
                                        GetMC(),
                                        element));
                                }
                            }
                            break;
                    }
                }
            }
        }

        /*public override void OnYieldReward(PlayerObject player)
        {
            if (player == null) return;
            if (SEnvir.Random.Next(20) > 0) return;

            UserItem armour = player.Equipment[(int)EquipmentSlot.Armour];
            if (armour == null || armour.Info.Effect != ItemEffect.Level75ArmourBase) return;

            ItemInfo newarmour = SEnvir.ItemInfoList.Binding.First(x => x.Effect == ItemEffect.Level75ArmourUpgrade && x.Image == armour.Info.Image);
            armour.Info = newarmour;

            player.Enqueue(new S.ItemInfoRefreshed { Slot = (int)EquipmentSlot.Armour, GridType = GridType.Equipment, ItemIndex = newarmour.Index });

            player.RefreshWeight();
            player.SendShapeUpdate();
        }*/
    }
}
