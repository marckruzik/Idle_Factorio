using System;

namespace NS_Manager_Resource
{
    public class Resource_Stack
    {
        public int quantity = 0;
        public int quantity_max = 16;
        public Resource resource;
        public string resource_name { get { return this.resource.name; } }


        public string get_text ()
        {
            return $"{this.resource_name} * {this.quantity}";
        }


        public static Resource_Stack from_resource_name_get_resource_stack (string resource_name)
        {
            Resource resource = Resource.from_resource_name_get_resource (resource_name);
            Resource_Stack resource_stack = new Resource_Stack ();
            resource_stack.resource = resource;
            return resource_stack;
        }

        public static Resource_Stack from_resource_name_and_resource_quantity_get_resource_stack (
            string resource_name, int resource_quantity)
        {
            Resource_Stack resource_stack = Resource_Stack.from_resource_name_get_resource_stack (resource_name);
            resource_stack.quantity = resource_quantity;
            return resource_stack;
        }


        public static Resource_Stack from_resource_stack_text_get_resource_stack (string resource_stack_text)
        {
            string resource_name = Resource_Stack.from_resource_stack_text_get_resource_name (resource_stack_text);
            int resource_quantity = Resource_Stack.from_resource_stack_text_get_resource_quantity (resource_stack_text);

            Resource_Stack resource_stack = Resource_Stack.from_resource_name_and_resource_quantity_get_resource_stack (
                resource_name, resource_quantity);
            
            return resource_stack;
        }


        public static string from_resource_stack_text_get_resource_name (string resource_stack_text)
        {
            return Recipe.from_text_and_separator_get_text_left (resource_stack_text, "*");
        }


        public static int from_resource_stack_text_get_resource_quantity (string resource_stack_text)
        {
            string resource_quantity_text = Recipe.from_text_and_separator_get_text_right (resource_stack_text, "*");
            int resource_quantity = int.Parse (resource_quantity_text);
            return resource_quantity;
        }


        public void increment ()
        {
            if (amount_max_reached () == true)
            {
                return;
            }

            this.quantity += 1;
        }


        public bool amount_max_reached ()
        {
            return this.quantity >= this.quantity_max;
        }

    }
}
