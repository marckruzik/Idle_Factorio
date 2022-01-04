using NS_Game_Engine;
using NS_Manager_Resource;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Idle_Factorio.Script;

namespace Idle_Factorio.Script
{
    public partial class Game_Setup
    {
        public static bool initialized = false;

        public static async Task setup ()
        {
            await from_csv_load_resource ();
            stock_manager_resource_setup ();

            await from_csv_load_recipe ();
            await from_csv_load_quest ();

            setup_game_stat ();
        }

    }
}
