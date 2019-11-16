using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Scenes.Game.Map
{
    public interface MapCell
    {
        List<MapObject> GetObjects();

        bool Blocking();

        void AddObject(MapObject ob);
        void RemoveObject(MapObject ob);

        bool HasFlag();

    }
}