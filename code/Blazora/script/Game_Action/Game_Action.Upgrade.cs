using NS_Game_Engine;
using NS_Manager_Resource;
using NS_Blazora_Basic;
using Blazora.Components;
using Blazora.Components.BGenerator;
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

            int stock_resource_quantity_max = Resource.from_resource_name_get_stack_resource_quantity_max (
                resource_name) * Game_Engine.self.manager_resource.chest_size;
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

            if (resource_name != "transport_belt_1" && resource_name != "inserter_1")
            {
                Resource.dico_resource_name_plus_stack_resource_quantity_max[resource_name].Set (stack_resource_quantity_max_future);
            }
            else
            {
                List<string> list_resource_name_similar =
                    Resource.from_resource_name_main_and_list_resource_name_get_list_resource_name_similar (
                        resource_name,
                        Resource.list_resource_name);
                foreach (string resource_name_similar in list_resource_name_similar)
                {
                    Resource.dico_resource_name_plus_stack_resource_quantity_max[resource_name_similar]
                        .Set (stack_resource_quantity_max_future);
                }
            }
        }



        public static void upgrade_tool (string toggle_id, string generator_resource_concerned)
        {
            Game_Engine.self.manager_toggle.set (toggle_id, true);
            List<BGenerator> list_bgenerator = BGenerator.from_game_page_get_list_bgenerator ()
                .Where (bgenerator => bgenerator.generator.has_stack_tool_with_name_similar (generator_resource_concerned) == true)
                .ToList ();
            foreach (BGenerator bgenerator in list_bgenerator)
            {
                bgenerator.state_change ();
            }
        }


        public static void upgrade_tool (int generator_id, string tool_resource_name)
        {
            BGenerator bgenerator = BGenerator.from_generator_id_get_bgenerator (generator_id);
            bgenerator.recipe.list_tool_kind[0] = Resource.from_resource_name_get_resource (tool_resource_name);
            bgenerator_reload (generator_id);
            Manager_Resource.resource_transfer (
                Game_Engine.self.manager_resource,
                bgenerator.generator.manager_resource_tool,
                tool_resource_name,
                1);
        }


        public static void upgrade_accessory (int generator_id, string toggle_id)
        {
            Generator generator = Game_Engine.self.manager_generator.from_generator_id_get_generator (generator_id);
            Game_Engine.self.manager_toggle.set (toggle_id, true);
            bgenerator_reload (generator_id);
        }



        public static void bgenerator_reload (int generator_id)
        {
            BGenerator bgenerator = BGenerator.from_generator_id_get_bgenerator (generator_id);

            Resource_Mix component_resource_mix = bgenerator.generator.manager_resource.get_resource_mix ();
            Resource_Mix tool_resource_mix = bgenerator.generator.manager_resource_tool.get_resource_mix ();

            bgenerator.listener_remove ();
            bgenerator.generator.listener_clear ();
            foreach (Game_Component gc in bgenerator.ienumerable_get_child ())
            {
                gc.listener_remove ();
            }

            bgenerator.state_change ();

            bgenerator.generator.from_recipe_setup_manager ();

            bgenerator.generator.manager_resource.set_resource_mix (component_resource_mix);
            bgenerator.generator.manager_resource_tool.set_resource_mix (tool_resource_mix);

            bgenerator.listener_setup ();

            foreach (Game_Component gc in bgenerator.ienumerable_get_child ())
            {
                gc.listener_setup ();
                gc.set_need_refresh ();
            }
        }

    }
}