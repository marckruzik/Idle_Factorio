using NS_Game_Engine;
using NS_Manager_Resource;
using NS_Blazora_Basic;
using Blazora.Components;
using System.Linq;
using System.Collections.Generic;

namespace Blazora.Script
{
    public static partial class Game_Action
    {


        public static bool action_upgrade_is_ready (Recipe recipe)
        {
            string resource_name = Game_Engine.self
                .manager_generator
                .from_recipe_get_generator (recipe)
                .get_result_resource_name ();

            int stock_resource_quantity_max = Game_Engine.self.manager_resource.from_resource_name_get_stock_resource_quantity_max (
                resource_name);
            int stock_resource_quantity = Game_Engine.self.manager_resource.from_resource_name_get_resource_quantity (resource_name);

            return (stock_resource_quantity >= stock_resource_quantity_max);
        }


        public static void action_upgrade (Recipe recipe)
        {
            string resource_name = Game_Engine.self
               .manager_generator
               .from_recipe_get_generator (recipe)
               .get_result_resource_name ();

            ObservableProperty<int> stack_resource_quantity_max = Resource.from_resource_name_get_stack_resource_quantity_max (resource_name);
            int stack_resource_quantity_max_future = stack_resource_quantity_max * 2;

            int count = Resource.dico_resource_name_plus_stack_resource_quantity_max[resource_name].get_listener_count ();

            Resource.dico_resource_name_plus_stack_resource_quantity_max[resource_name].Set (stack_resource_quantity_max_future);
        }


        public static void flag_furnace_stone ()
        {
            Game_Engine.self.manager_toggle.set ("know_furnace", true);
            List<BGenerator> list_bgenerator = BGenerator.from_game_page_get_list_bgenerator ()
                .Where (bgenerator => bgenerator.get_stack_tool ().resource_name == "fire")
                .ToList ();
            foreach (BGenerator bgenerator in list_bgenerator)
            {
                bgenerator.state_change ();
            }
        }

        public static void flag_burner_drill ()
        {
            Game_Engine.self.manager_toggle.set ("know_burner_drill", true);
            List<BGenerator> list_bgenerator = BGenerator.from_game_page_get_list_bgenerator ()
                .Where (bgenerator => bgenerator.get_stack_tool ().resource_name == "pickaxe")
                .ToList ();
            foreach (BGenerator bgenerator in list_bgenerator)
            {
                bgenerator.state_change ();
            }
        }

        public static void flag_assembling_machine_1 ()
        {
            Game_Engine.self.manager_toggle.set ("know_assembling_machine_1", true);
            List<BGenerator> list_bgenerator = BGenerator.from_game_page_get_list_bgenerator ()
                .Where (bgenerator => bgenerator.get_stack_tool ().resource_name == "hand")
                .ToList ();
            foreach (BGenerator bgenerator in list_bgenerator)
            {
                bgenerator.state_change ();
            }
        }


        public static void upgrade_tool (int generator_id, string tool_resource_name)
        {
            BGenerator bgenerator = BGenerator.from_generator_id_get_bgenerator (generator_id);

            bgenerator.listener_remove ();
            bgenerator.generator.manager_resource.listener_clear ();
            foreach (Game_Component gc in bgenerator.ienumerable_get_child ())
            {
                gc.listener_remove ();
            }

            bgenerator.recipe.list_tool_kind[0] = Resource.from_resource_name_get_resource (tool_resource_name);

            bgenerator.state_change ();

            bgenerator.generator.manager_resource.from_recipe_setup (bgenerator.recipe);
            bgenerator.generator.manager_resource.listener_setup (bgenerator.recipe);
            bgenerator.listener_setup ();

            foreach (Game_Component gc in bgenerator.ienumerable_get_child ())
            {
                gc.listener_setup ();
                gc.need_refresh = true;
            }
        }

    }
}