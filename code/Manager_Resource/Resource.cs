using System;
using System.Collections.Generic;
using System.Text;

namespace NS_Manager_Resource
{
    public class Resource
    {
        public string name;

        public static List<string> list_resource_name = new List<string>
        {
            "coal",
            "wood",
            "time",
            "iron_ore",
            "iron_plate"
        };

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
            resource.name = resource_name;
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
