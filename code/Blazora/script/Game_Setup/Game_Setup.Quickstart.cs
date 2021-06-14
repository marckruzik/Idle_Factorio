using NS_Game_Engine;
using NS_Manager_Resource;
using System.Collections.Generic;
using System.Linq;
using Blazora.Components.BGenerator;

namespace Blazora.Script
{
    public partial class Game_Setup
    {
        public static void quickstart ()
        {
            // Quickstart
            quickstart ("iron_ore", 8);
            quickstart ("coal_ore", 8);
            quickstart ("stone_ore", 8);
            quickstart ("iron_plate", 6);
            quickstart ("furnace_stone", 8);
            quickstart ("burner_drill", 4);
            quickstart ("assembling_machine_1", 4);
            quickstart ("iron_gear", 8);
            quickstart ("transport_belt_1", 8);
            quickstart ("inserter_1", 8);
            quickstart ("electronic_circuit", 8);

            quickstart_set_transport_belt_1 ();
        }


        private static void quickstart_set_transport_belt_1 ()
        {
            string resource_name = "transport_belt_1";
            List<BGenerator> list_bgenerator = BGenerator.from_game_page_get_list_bgenerator ()
                .Where (bgenerator => bgenerator.generator.has_stack_tool_containing (resource_name) == true)
                .ToList ();
            foreach (BGenerator bgenerator in list_bgenerator)
            {
                List<string> list_resource_name_similar =
                    Resource.from_resource_name_main_and_list_resource_name_get_list_resource_name_similar (
                        resource_name,
                        bgenerator.generator.manager_resource_tool.get_resource_mix ().get_list_resource_name ());
                foreach (string resource_name_similar in list_resource_name_similar)
                {
                    if (bgenerator.generator.manager_resource_tool
                        .from_resource_name_contain_resource_stack (resource_name_similar) == false)
                    {
                        continue;
                    }
                    bgenerator.generator.manager_resource_tool
                        .from_resource_name_and_resource_quantity_add_resource (
                            resource_name_similar, 1);
                }

                bgenerator.state_change ();
            }
        }

        public static void quickstart (string resource_name, int quantity_min)
        {
            int quantity_current = Game_Engine.self.manager_resource.from_resource_name_get_resource_quantity (resource_name);
            if (quantity_current >= quantity_min)
            {
                return;
            }
            Game_Engine.self.manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity
                (resource_name, quantity_min);
        }



    }
}
