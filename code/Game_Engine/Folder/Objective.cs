using NS_Manager_Resource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NS_Game_Engine.NS_Quest
{
    public class Objective
    {
        public Resource_Mix resource_mix;

        public enum Role
        {
            blank,
            resource_possessed,
            level_reached,
            special
        }
        public Role role;
        static Dictionary<string, Role> dico_role_txt_plus_role = Enum.GetValues (typeof (Role)).Cast<Role> ()
            .ToDictionary (x => x.ToString (), x => x);


        public static Objective from_objective_txt_get_objective (string objective_txt)
        {
            string resource_mix_txt;
            string role_txt;
            if (objective_txt.Contains (':') == false)
            {
                resource_mix_txt = objective_txt;
                role_txt = "resource_possessed";
            }
            else
            {
                resource_mix_txt = objective_txt.Split (':')[1];
                role_txt = objective_txt.Split (':')[0];
            }

            Objective objective = Objective.from_resource_mix_txt_get_objective (resource_mix_txt);
            objective.role = Objective.from_role_txt_get_role (role_txt);

            return objective;
        }



        public static Role from_role_txt_get_role (string role_txt)
        {
            return dico_role_txt_plus_role[role_txt];
        }


        public static Objective from_resource_mix_txt_get_objective (string resource_mix_txt)
        {
            Resource_Mix resource_mix = Resource_Mix.from_resource_mix_text_get_resource_mix (resource_mix_txt);
            Objective objective = new Objective ();
            objective.resource_mix = resource_mix;
            return objective;
        }


        public bool is_accomplished ()
        {
            if (this.role == Role.resource_possessed)
            {
                return is_accomplished_with_resource_possessed ();
            }
            else if (this.role == Role.level_reached)
            {
                return is_accomplished_with_level_reached ();
            }
            else if (this.role == Role.special)
            {
                string special = this.resource_mix.get_list_resource_name ()[0];
                switch (special)
                {
                    case "furnace_stone": return is_accomplished_with_furnace_stone ();
                    case "burner_drill": return is_accomplished_with_burner_drill ();
                    case "transport_belt_1": return is_accomplished_with_transport_belt_1 ();
                    case "inserter_1": return is_accomplished_with_inserter_1 ();
                    default: throw new Exception ($"Objective: unknown special {special}");
                }
            }
            else
            {
                throw new Exception ($"Objective: unknown role {this.role}");
            }
        }


        public bool is_accomplished_with_furnace_stone ()
        {
            Generator generator = Game_Engine.self.manager_generator.from_recipe_result_resource_name_get_generator ("iron_plate");
            return (generator.has_stack_tool_with_name_similar ("furnace_stone") &&
                generator.manager_resource_tool.from_resource_name_get_resource_stack ("furnace_stone").quantity >= 2);
        }


        public bool is_accomplished_with_burner_drill ()
        {
            bool result = true;

            result &= generator_has_tool ("iron_ore", "burner_drill", 1);
            result &= generator_has_tool ("stone_ore", "burner_drill", 1);
            result &= generator_has_tool ("coal_ore", "burner_drill", 1);
            
            return result;
        }


        public bool is_accomplished_with_transport_belt_1 ()
        {
            bool result = true;

            result &= generator_has_tool ("iron_plate", "transport_belt_1-iron_ore", 2);
            result &= generator_has_tool ("iron_plate", "transport_belt_1-coal_ore", 2);

            return result;
        }


        public bool is_accomplished_with_inserter_1 ()
        {
            bool result = true;

            result &= generator_has_tool ("iron_plate", "inserter_1-iron_ore", 2);
            result &= generator_has_tool ("iron_plate", "inserter_1-coal_ore", 2);

            return result;
        }


        public static bool generator_has_tool (
            string generator_result_resource_name, 
            string tool_resource_name, 
            int tool_quantity)
        {
            Generator generator = Game_Engine.self.manager_generator
                   .from_recipe_result_resource_name_get_generator (generator_result_resource_name);

            return generator.manager_resource_tool.from_resource_name_contain_resource_stack (tool_resource_name) &&
                generator.manager_resource_tool
                    .from_resource_name_get_resource_stack (tool_resource_name).quantity >= tool_quantity;
        }




        public bool is_accomplished_with_resource_possessed ()
        {
            foreach (Resource_Stack resource_stack in this.resource_mix.list_resource_stack)
            {
                int stock_quantity = Game_Engine.self
                    .manager_resource
                    .from_resource_name_get_resource_quantity (resource_stack.resource_name);
                if (stock_quantity < resource_stack.quantity)
                {
                    return false;
                }
            }
            return true;
        }


        public bool is_accomplished_with_level_reached ()
        {
            foreach (Resource_Stack resource_stack in this.resource_mix.list_resource_stack)
            {
                int stack_max = Resource.from_resource_name_get_stack_resource_quantity_max (
                    resource_stack.resource_name);
                if (stack_max < resource_stack.quantity)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
