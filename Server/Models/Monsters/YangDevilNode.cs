using System;
using System.Collections.Generic;
using System.Linq;
using Library;

namespace Server.Models.Monsters
{
    public class YangDevilNode : YinDevilNode
    {
        public YangDevilNode()
        {
            Direction = MirDirection.Up;
            bufftype = BuffType.YangDevilNode;
            buffstats = new List<Stat>
            {
                Stat.MRPercent
            };
        }
    }
}
