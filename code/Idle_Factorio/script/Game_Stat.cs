using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Idle_Factorio.Script
{
    public class Game_Stat : Dictionary<string, long>
    {
        public static Game_Stat self;

        public void stat_create (string id)
        {
            this.Add (id, 0);
        }

        public void stat_add (string id, long quantity)
        {
            this[id] += quantity;
        }
    }
}
