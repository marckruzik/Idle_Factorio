using NS_Game_Engine;
using NS_Manager_Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazora.Shared
{
    public class Game_Setup
    {

        public static void setup ()
        {
            if (Game_Engine.self.initialized == false)
            {
                stock_manager_resource_setup (Game_Engine.self.manager_resource);

                Game_Engine.self.initialized = true;
            }

        }


        public static void add_generator (string recipe_text, int time, string tool_kind_name)
        {
            Recipe recipe = Recipe.get_recipe (recipe_text, time, tool_kind_name);

            Game_Engine.self.manager_generator.from_recipe_add_generator (recipe);
        }


        public static void stock_manager_resource_setup (Manager_Resource stock_manager_resource)
        {

            // Configuration
            stock_manager_resource.chest_size = Resource.chest_size;

            // Mine
            foreach (string mine_resource_name in Resource.list_mine_resource_name)
            {
                Resource.dico_resource_name_plus_stack_resource_quantity_max[mine_resource_name] = 999;
                stock_manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity (mine_resource_name, 999);
            }

            // Recipe
            add_generator ("iron_ore_mine * 1 => iron_ore * 1", 2, "pickaxe");
            add_generator ("coal_ore_mine * 1 => coal_ore * 1", 2, "pickaxe");
            add_generator ("stone_ore_mine * 1 => stone_ore * 1", 2, "pickaxe");
            add_generator ("iron_ore * 1 + coal_ore * 1 => iron_plate * 1", 2, "furnace_stone");
            add_generator ("stone_ore * 4 => furnace_stone * 1", 2, "hand");

            // Quickstart
            stock_manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity ("iron_ore", 8);
            stock_manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity ("coal_ore", 8);
            stock_manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity ("stone_ore", 8);
            stock_manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity ("furnace_stone", 8);

        }



    }
}
