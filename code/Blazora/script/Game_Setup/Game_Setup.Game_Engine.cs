using NS_Game_Engine;
using NS_Manager_Resource;

namespace Blazora.Script
{
    public partial class Game_Setup
    {
        public static void add_generator (string component_mix_text, string result_mix_text, int time, string tool_kind_name)
        {
            string recipe_text = $"{component_mix_text} => {result_mix_text}";
            Recipe recipe = Recipe.get_recipe (recipe_text, time, tool_kind_name);

            Game_Engine.self.manager_generator.from_recipe_add_generator (recipe);
        }


        public static void stock_manager_resource_setup ()
        {
            Manager_Resource stock_manager_resource = new Manager_Resource ();
            stock_manager_resource.id = "MR_Game_Engine";

            //Console.WriteLine ("stock_manager_resource_setup");

            // Configuration
            stock_manager_resource.chest_size = Resource.chest_size;

            foreach (string resource_name in Resource.list_resource_name)
            {
                stock_manager_resource.from_resource_name_add_resource (resource_name);
            }

            // Mine
            foreach (string mine_resource_name in Resource.list_mine_resource_name)
            {
                Resource.dico_resource_name_plus_stack_resource_quantity_max[mine_resource_name].Set (999);
                stock_manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity (mine_resource_name, 999);
            }

            Game_Engine.self.manager_resource = stock_manager_resource;
        }



    }
}
