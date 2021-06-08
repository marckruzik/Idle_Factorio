using NS_Game_Engine;
using NS_Manager_Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Globalization;
using CsvHelper.Configuration;
using System.Net.Http;
using NS_Blazora_Basic;
using System.Net.Http.Headers;

namespace Blazora.Scripts
{
    public class Game_Setup
    {
        public static bool initialized = false;

        public static async Task setup ()
        {
            Console.WriteLine ("game setup");
            await from_csv_load_resource ();
            await from_csv_load_recipe ();
            Console.WriteLine ("after csv load");
            Game_Engine.self.manager_resource = stock_manager_resource_setup ();
            quickstart ();
        }


        public static void add_generator (string component_mix_text, string result_mix_text, int time, string tool_kind_name)
        {
            string recipe_text = $"{component_mix_text} => {result_mix_text}";
            Recipe recipe = Recipe.get_recipe (recipe_text, time, tool_kind_name);

            Game_Engine.self.manager_generator.from_recipe_add_generator (recipe);
        }


        public static Manager_Resource stock_manager_resource_setup ()
        {
            Manager_Resource stock_manager_resource = new Manager_Resource ();

            Console.WriteLine ("stock_manager_resource_setup");

            // Configuration
            stock_manager_resource.chest_size = Resource.chest_size;

            foreach (string resource_name in Resource.list_resource_name)
            {
                stock_manager_resource.from_resource_name_add_resource (resource_name);
                
            }

            // Mine
            foreach (string mine_resource_name in Resource.list_mine_resource_name)
            {
                Resource.dico_resource_name_plus_stack_resource_quantity_max[mine_resource_name].Set (999);
                stock_manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity (mine_resource_name, 999);
            }


            return stock_manager_resource;
        }



        private static void quickstart ()
        {
            // Quickstart
            Game_Engine.self.manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity ("iron_ore", 7);
            Game_Engine.self.manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity ("coal_ore", 8);
            Game_Engine.self.manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity ("stone_ore", 8);
            Game_Engine.self.manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity ("iron_plate", 6);
            Game_Engine.self.manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity ("furnace_stone", 8);
            Game_Engine.self.manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity ("burner_drill", 0);
            Game_Engine.self.manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity ("iron_gear", 0);
        }


        public static async Task from_csv_load_resource ()
        {
            string csv_filepath = "data/resource.csv";
            List<dynamic> list_record = await from_csv_filepath_get_list_record (csv_filepath);

            foreach (IDictionary<String, Object> record in list_record)
            {
                string resource_name = (string)record["resource_name"];
                Resource.list_resource_name.Add (resource_name);
                if ((string)record["mine"] == "true")
                {
                    Resource.list_mine_resource_name.Add (resource_name);
                }
                if ((string)record["unique"] == "true")
                {
                    Resource.list_unique_resource_name.Add (resource_name);
                }
                if ((string)record["can_craft_stock"] == "true")
                {
                    Resource.list_can_craft_stock_resource_name.Add (resource_name);
                }
            }

            Resource.dico_resource_name_plus_stack_resource_quantity_max =
                Resource.list_resource_name.
                ToDictionary (x => x, x => new ObservableProperty<int> (1));

            Console.WriteLine ("end resource csv load");
        }



        public static async Task from_csv_load_recipe ()
        {
            string csv_filepath = "data/recipe.csv";
            List<dynamic> list_record = await from_csv_filepath_get_list_record (csv_filepath);
            
            foreach (IDictionary<String, Object> record in list_record)
            {
                string component_mix_text = (string)record["component_mix"];
                string component_result_text = (string)record["result_mix"];
                int time = int.Parse((string)(record["time"]));
                string tool_kind = (string)record["tool_kind"];

                add_generator (component_mix_text, component_result_text, time, tool_kind);
            }

            Console.WriteLine ("end recipe csv load");
        }






        private async static Task<List<dynamic>> from_csv_filepath_get_list_record (string csv_filepath)
        {
            CsvConfiguration config = new CsvConfiguration (CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ","
            };

            HttpClient http = new HttpClient ();
            http.BaseAddress = new Uri (Program.base_adress_str);
            http.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
            Stream st = await http.GetStreamAsync (csv_filepath);
            

            IEnumerable<dynamic> records;
            using (var reader = new StreamReader (st))
            using (var csv = new CsvReader (reader, config))
            {
                records = csv.GetRecords<dynamic> ();
                return records.ToList ();
            }

        }

    }
}
