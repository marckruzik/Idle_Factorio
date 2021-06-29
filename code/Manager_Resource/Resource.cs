using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NS_Blazora_Basic;
using System.Text.RegularExpressions;
using System.Globalization;

namespace NS_Manager_Resource
{
    public class Resource
    {
        public string resource_name;

        public static List<string> list_resource_name = new List<string> ();

        public static List<string> list_mine_resource_name = new List<string> ();

        public static List<string> list_unique_resource_name = new List<string> ();

        public static List<string> list_can_craft_stock_resource_name = new List<string> ();

        public static Dictionary<string, ObservableProperty<int>> dico_resource_name_plus_stack_resource_quantity_max = 
            new Dictionary<string, ObservableProperty<int>> ();

        public static int chest_size = 8;



        public static ObservableProperty<int> from_resource_name_get_stack_resource_quantity_max (string resource_name)
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

        public static List<string> from_resource_name_main_and_list_resource_name_get_list_resource_name_similar (
            string resource_name_main, List<string> list_resource_name)
        {
            List<string> list_resource_name_similar = new List<string> ();
            foreach (string resource_name in list_resource_name)
            {
                if (resource_name == resource_name_main)
                {
                    list_resource_name_similar.Add (resource_name);
                }
                if (Resource.from_resource_name_is_complex_resource_name (resource_name) == false)
                {
                    continue;
                }
                if (Resource.from_complex_resource_name_get_resource_name_main (resource_name) == resource_name_main ||
                    Resource.from_complex_resource_name_get_resource_name_secondary (resource_name) == resource_name_main)
                {
                    list_resource_name_similar.Add (resource_name);
                }
            }
            return list_resource_name_similar;
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


        public static bool from_resource_name_is_complex_resource_name (string resource_name)
        {
            return resource_name.Contains ("-") == true;
        }

        public static string from_complex_resource_name_get_resource_name_main (string complex_resource_name)
        {
            return complex_resource_name.Split ("-")[0];
        }

        public static string from_complex_resource_name_get_resource_name_secondary (string complex_resource_name)
        {
            return complex_resource_name.Split ("-")[1];
        }


        public static string from_resource_name_get_complex_resource_name (
            string resource_name_a, string resource_name_b)
        {
            return $"{resource_name_a}-{resource_name_b}";
        }


        public static List<string> get_list_resource_name_craftable ()
        {
            return Resource.list_resource_name
                .Where (resource_name => Resource.from_resource_name_is_complex_resource_name (resource_name) == false)
                .Where (resource_name => Resource.list_mine_resource_name.Contains (resource_name) == false)
                .Where (resource_name => Resource.list_unique_resource_name.Contains (resource_name) == false)
                .ToList ();
        }


        public static string from_resource_name_to_resource_name_readable (string resource_name)
        {
            string resource_name_readable = resource_name.Replace ("_", " ");
            resource_name_readable = new Regex (@"\d+$").Replace (resource_name_readable, "");
            resource_name_readable = resource_name_readable.Trim ();
            resource_name_readable = CultureInfo.CurrentCulture.TextInfo.ToTitleCase (resource_name_readable);
            return resource_name_readable;
        }
    }
}
