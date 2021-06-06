using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NS_Manager_Resource
{
    public class Resource_Mix
    {
        public List<Resource_Stack> list_resource_stack = new List<Resource_Stack> ();


        public string get_text ()
        {
            return string.Join (" + ", list_resource_stack
                .Select (resource_stack => resource_stack.get_text ()));
        }

        public Resource_Stack from_resource_name_get_resource_stack (string resource_name)
        {
            return this.list_resource_stack
                .FirstOrDefault (resource_stack => resource_stack.resource_name == resource_name);
        }


        public static Resource_Mix from_resource_mix_text_get_resource_mix (string resource_mix_text)
        {
            List<string> list_resource_stack_text = from_resource_mix_text_get_list_resource_stack_text (resource_mix_text);
            List<Resource_Stack> list_resource_stack = list_resource_stack_text
                .Select (resource_stack_text => Resource_Stack.from_resource_stack_text_create_resource_stack (resource_stack_text))
                .ToList ();

            Resource_Mix resource_mix = new Resource_Mix ();
            resource_mix.list_resource_stack = list_resource_stack;

            return resource_mix;
        }

        private static List<string> from_resource_mix_text_get_list_resource_stack_text (string resource_mix_text)
        {
            List<string> list_resource_stack_text = new List<string> (resource_mix_text.Split ('+'));
            return list_resource_stack_text;
        }



        public static Resource_Mix operator * (Resource_Mix resource_mix, int multiplier)
        {
            Resource_Mix result = new Resource_Mix ();
            result.list_resource_stack = 
                resource_mix
                .list_resource_stack
                .Select (resource_stack => resource_stack * multiplier)
                .ToList ();
            return result;
        }
    }
}
