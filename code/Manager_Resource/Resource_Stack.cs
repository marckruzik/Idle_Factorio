using System;

namespace NS_Manager_Resource
{
    public class Resource_Stack
    {
        public int quantity = 0;
        public Resource resource;
        public string resource_name { get { return this.resource.resource_name; } }


        public string get_text ()
        {
            return $"{this.resource_name} * {this.quantity}";
        }


        public static Resource_Stack from_resource_name_create_resource_stack (string resource_name)
        {
            Resource resource = Resource.from_resource_name_get_resource (resource_name);
            Resource_Stack resource_stack = new Resource_Stack ();
            resource_stack.resource = resource;
            return resource_stack;
        }

        public static Resource_Stack from_resource_name_and_resource_quantity_create_resource_stack (
            string resource_name, int resource_quantity)
        {
            Resource_Stack resource_stack = Resource_Stack.from_resource_name_create_resource_stack (resource_name);
            resource_stack.quantity = resource_quantity;
            return resource_stack;
        }


        public static Resource_Stack from_resource_stack_text_create_resource_stack (string resource_stack_text)
        {
            string resource_name = Resource_Stack.from_resource_stack_text_get_resource_name (resource_stack_text);
            int resource_quantity = Resource_Stack.from_resource_stack_text_get_resource_quantity (resource_stack_text);

            Resource_Stack resource_stack = Resource_Stack.from_resource_name_and_resource_quantity_create_resource_stack (
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


        public static Resource_Stack operator * (Resource_Stack resource_stack, int multiplier)
        {
            Resource_Stack result = new Resource_Stack ();
            result.resource = resource_stack.resource;
            result.quantity = resource_stack.quantity * multiplier;
            return result;
        }

    }
}
