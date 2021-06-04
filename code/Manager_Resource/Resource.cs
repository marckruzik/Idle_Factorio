using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NS_Manager_Resource
{
    public class Resource
    {
        public string resource_name;

        public static List<string> list_resource_name = new List<string> ();

        public static List<string> list_mine_resource_name = new List<string> ();

        public static List<string> list_unique_resource_name = new List<string> ();

        public static List<string> list_can_craft_stock_resource_name = new List<string> ();

        public static Dictionary<string, int> dico_resource_name_plus_stack_resource_quantity_max = new Dictionary<string, int> ();

        public static int chest_size = 8;



        public static int from_resource_name_get_stack_resource_quantity_max (string resource_name)
        {
            return dico_resource_name_plus_stack_resource_quantity_max[resource_name];
        }

        public static string from_resource_name_get_resource_filename (string resource_name)
        {
            return $"{resource_name}.png";
        }

        public static void from_list_resource_name_and_resource_name_check_resource_name (
            List<string> list_resource_name, string resource_name)
        {
            if (list_resource_name.Contains (resource_name) == false)
            {
                throw new Exception ("unknown Resource Name : " + resource_name);
            }
        }


        public static void from_resource_name_check_resource_name (string resource_name)
        {
            from_list_resource_name_and_resource_name_check_resource_name (Resource.list_resource_name, resource_name);
        }


        public static Resource from_resource_name_get_resource (string resource_name)
        {
            from_resource_name_check_resource_name (resource_name);

            Resource resource = new Resource ();
            resource.resource_name = resource_name;
            return resource;
        }


        public static Resource from_resource_text_get_resource (string resource_text)
        {
            string resource_name = from_resource_text_get_resource_name (resource_text);
            Resource resource = from_resource_name_get_resource (resource_name);
            return resource;
        }

        public static string from_resource_text_get_resource_name (string resource_text)
        {
            string resource_name = resource_text.Trim ().ToLower ();
            return resource_name;
        }
    }
}
