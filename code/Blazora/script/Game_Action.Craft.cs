﻿using NS_Game_Engine;
using NS_Manager_Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazora.script
{
    public static partial class Game_Action
    {



        public static bool from_recipe_is_craft_ready (Recipe recipe)
        {
            if (from_recipe_get_job (recipe) != null)
            {
                return false;
            }

            Generator generator = Game_Engine.self.manager_generator.from_recipe_get_generator (recipe);

            return generator.can_craft (Game_Engine.self.manager_resource);
        }



        public static void from_recipe_craft (Recipe recipe)
        {
            if (from_recipe_is_craft_ready (recipe) == false)
            {
                return;
            }

            Generator generator = Game_Engine.self.manager_generator.from_recipe_get_generator (recipe);

            Resource_Stack resource_stack = Game_Engine.self.manager_resource
                .from_resource_name_get_resource_stack (generator.get_result_resource_name ());

            Resource_Stack stack_tool = generator.get_stack_tool ();

            generator.manager_resource.from_resource_mix_remove_resource (recipe.mix_component * stack_tool.quantity);

            Resource_Mix mix_result = recipe.mix_result * stack_tool.quantity;

            Action action = delegate ()
            {
                Game_Engine.self.manager_resource.from_resource_mix_add_resource (mix_result);
            };

            Job job = Game_Engine.self.manager_job.from_action_and_time_total_add_job (action, recipe.time);
            job.id = from_recipe_get_job_id (recipe);
        }

    }
}
