using Client.Models;
using System.Collections.Generic;
using System;

namespace Client.Scenes.Game.Map
{
    //TODO: MAP-1 Tidy up
    public sealed class WemadeMir2MapCell : MapCell
    {
        public short BackFile;
        public int BackImage;

        public short MiddleFile;
        public int MiddleImage;

        public short FrontFile;
        public int FrontImage;

        public byte DoorIndex;
        public byte DoorOffset;

        public int FrontAnimationFrame;
        public int FrontAnimationTick;

        public int MiddleAnimationFrame;
        public int MiddleAnimationTick;

        public short TileAnimationImage;
        public short TileAnimationOffset;
        public byte TileAnimationFrames;

        public int Light;
        public byte Unknown;

        public bool Flag;
        public bool FishingCell;

        public List<MapObject> Objects;

        public bool Blocking()
        {
            if (Objects != null)
            {
                foreach (MapObject ob in Objects)
                    if (ob.Blocking) return true;
            }

            return Flag;
        }

        public void AddObject(MapObject ob)
        {
            if (Objects == null)
                Objects = new List<MapObject>();

            if (ob.Race == Library.ObjectType.Spell)
                Objects.Insert(0, ob);
            else
                Objects.Add(ob);

            ob.CurrentCell = this;
        }

        public void RemoveObject(MapObject ob)
        {
            Objects.Remove(ob);

            if (Objects.Count == 0)
                Objects = null;

            ob.CurrentCell = null;
        }

        public List<MapObject> GetObjects()
        {
            return Objects;
        }

        public bool HasFlag()
        {
            return Flag;
        }
    }
}