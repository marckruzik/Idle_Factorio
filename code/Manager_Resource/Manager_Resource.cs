using System;
using System.Collections.Generic;
using System.Text;

namespace NS_Manager_Resource
{
    public class Manager_Resource
    {

        Dictionary<string, Resource_Stack> dico_resource_name_plus_resource_stack = new Dictionary<string, Resource_Stack> ();


        public bool can_craft (Recipe2 recipe)
        {
            foreach (Resource_Stack component in recipe.mix_component.list_resource_stack)
            {
                if (from_resource_name_contain_resource_stack (component.resource_name) == false)
                {
                    return false;
                }
                int resource_quantity = from_resource_name_get_resource_quantity (component.resource_name);
                if (component.quantity > resource_quantity)
                {
                    return false;
                }
            }
            return true;
        }

        public static void resource_transfer (
            Manager_Resource manager_resource_source, 
            Manager_Resource manager_resource_destination, 
            string resource_name, 
            int quantity)
        {
            quantity = Math.Min (manager_resource_source.from_resource_name_get_resource_quantity (resource_name), quantity);

            manager_resource_source.from_resource_name_and_resource_quantity_add_resource (resource_name, -quantity);

            manager_resource_destination.from_resource_name_and_resource_quantity_add_resource (resource_name, quantity);
        }


        public int from_resource_name_get_resource_quantity (string resource_name)
        {
            return from_resource_name_get_resource_stack (resource_name).quantity;
        }

        public void from_resource_name_add_resource (string resource_name)
        {
            bool contain_resource = from_resource_name_contain_resource_stack (resource_name);
            if (contain_resource == true)
            {
                return;
            }
            Resource_Stack resource_stack = Resource_Stack.from_resource_name_get_resource_stack (resource_name);
            resource_stack.quantity = 0;
            resource_stack.quantity_max = 999;
            dico_resource_name_plus_resource_stack.Add (resource_name, resource_stack);
        }


        public void from_resource_name_and_resource_quantity_add_resource (string resource_name, int resource_quantity)
        {
            if (resource_name == "time")
            {
                return;
            }

            bool contain_resource = from_resource_name_contain_resource_stack (resource_name);
            if (contain_resource == false)
            {
                from_resource_name_add_resource (resource_name);
            }

            Resource_Stack resource_stack = dico_resource_name_plus_resource_stack[resource_name];

            resource_stack.quantity += resource_quantity;
            if (resource_stack.quantity < 0)
            {
                resource_stack.quantity = 0;
            }
            else if (resource_stack.quantity > resource_stack.quantity_max)
            {
                resource_stack.quantity = resource_stack.quantity_max;
            }
        }



        public void from_resource_mix_add_resource (Resource_Mix resource_mix)
        {
            foreach (Resource_Stack resource_stack in resource_mix.list_resource_stack)
            {
                from_resource_name_and_resource_quantity_add_resource (resource_stack.resource_name, resource_stack.quantity);
            }
        }


        public void from_resource_mix_remove_resource (Resource_Mix resource_mix)
        {
            foreach (Resource_Stack resource_stack in resource_mix.list_resource_stack)
            {
                int quantity_negative = -resource_stack.quantity;
                from_resource_name_and_resource_quantity_add_resource (resource_stack.resource_name, quantity_negative);
            }
        }


        public Resource_Stack from_resource_name_get_resource_stack (string resource_name)
        {
            Resource_Stack resource_stack = dico_resource_name_plus_resource_stack[resource_name];
            return resource_stack;
        }


        public bool from_resource_name_contain_resource_stack (string resource_name)
        {
            return dico_resource_name_plus_resource_stack.ContainsKey (resource_name);
        }

    }
}
