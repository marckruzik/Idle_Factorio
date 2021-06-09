using System;
using System.Collections.Generic;
using System.Text;
using NS_Blazora_Basic;

namespace NS_Manager_Resource
{
    public class Generator
    {
        public Recipe recipe;
        public Manager_Resource manager_resource;


        public Resource_Stack get_stack_tool ()
        {
            Resource resource_tool = this.recipe.list_tool_kind[0];
            Resource_Stack stack_tool = this.manager_resource.from_resource_name_get_resource_stack (resource_tool.resource_name);
            return stack_tool;
        }

        public int get_result_val ()
        {
            string result_resource_name = get_result_resource_name ();
            int result_quantity = this.recipe.mix_result.from_resource_name_get_resource_stack (result_resource_name).quantity;
            return result_quantity * get_tool_val ();
        }

        public int get_tool_val ()
        {
            return get_stack_tool ().quantity;
        }

        public int get_loc_val (string resource_name)
        {
            return get_loc_quantity (resource_name).Value;
        }

        public ObservableProperty<int> get_loc_quantity (string resource_name)
        {
            return this.manager_resource.from_resource_name_get_resource_stack (resource_name).observable_quantity;
        }

        public int get_loc_min (string resource_name)
        {
            int component_quantity = this.recipe.mix_component.from_resource_name_get_resource_stack (resource_name).quantity;
            return component_quantity * get_tool_val ();
        }



        public static Generator from_recipe_create_generator (Recipe recipe)
        {
            Generator generator = new Generator ();
            generator.recipe = recipe;
            generator.manager_resource = new Manager_Resource ();

            generator.manager_resource.from_recipe_setup (recipe);

            return generator;
        }


        public string get_result_resource_name ()
        {
            return this.recipe.mix_result.list_resource_stack[0].resource_name;
        }


        public bool can_craft (Manager_Resource stock_manager_resource)
        {
            Resource_Stack stack_tool = get_stack_tool ();

            if (stack_tool.quantity == 0)
            {
                return false;
            }

            string result_resource_name = get_result_resource_name ();
            int result_quantity_max = stock_manager_resource.from_resource_name_get_stock_resource_quantity_max (result_resource_name);
            int result_quantity = stock_manager_resource.from_resource_name_get_resource_quantity (result_resource_name);
            
            if (result_quantity >= result_quantity_max)
            {
                return false;
            }

            bool ready;
            if (must_craft_locally () == true)
            {
                ready = this.manager_resource.can_craft (this.recipe.mix_component * stack_tool.quantity);
            }
            else
            {
                ready = stock_manager_resource.can_craft (this.recipe.mix_component * stack_tool.quantity);
            }

            //Console.WriteLine ($"ready : {ready}");
            return ready;
        }

        public bool must_craft_locally ()
        {
            return (Resource.list_can_craft_stock_resource_name.Contains (get_stack_tool ().resource_name) == false);
        }


    }
}
