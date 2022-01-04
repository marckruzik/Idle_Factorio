using NS_Game_Engine;
using System;
using System.Collections.Generic;

namespace Idle_Factorio.Script
{
    public partial class Game_Setup
    {
        public static void listener_flag_setup ()
        {
            Console.WriteLine ("flag start");

            List<(string, string)> list_tuple_resource_observed_plus_generator_resource_concerned = new List<(string, string)> ()
            {
                ("furnace_stone", "fire"),
                ("burner_drill", "pickaxe"),
                ("assembling_machine_1", "hand"),
                ("inserter_1", "inserter_1"),
                ("transport_belt_1", "transport_belt_1")
            };
            foreach ((string, string) resource_observed_plus_generator_resource_concerned in
                list_tuple_resource_observed_plus_generator_resource_concerned)
            {
                listener_flag_setup (
                    resource_observed_plus_generator_resource_concerned.Item1,
                    resource_observed_plus_generator_resource_concerned.Item2);
            }
        }


        public static void listener_flag_setup (string resource_observed, string generator_resource_concerned)
        {
            string toggle_id = $"know_{resource_observed}";
            Game_Engine.self.manager_resource.from_resource_name_get_resource_stack (resource_observed)
                .observable_quantity.changed += (v) =>
                {
                    if (Game_Engine.self.manager_toggle.get_tog (toggle_id) != Toggle.Tog.is_true)
                    {
                        if (v > 0)
                        {
                            Game_Action.upgrade_tool (toggle_id, generator_resource_concerned);
                        }
                    }
                };
        }


    }
}
