using NS_Manager_Resource;

namespace Blazora.Script
{
    public partial class Game_Setup
    {
        private static void setup_game_stat ()
        {
            Game_Stat.self = new Game_Stat ();
            Game_Stat.self.stat_create ("time_played");

            foreach (string resource_name in Resource.list_resource_name)
            {
                Game_Stat.self.stat_create (resource_name);
            }
        }

    }
}
