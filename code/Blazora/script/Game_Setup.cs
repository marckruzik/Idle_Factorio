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
using Blazora.Components.BGenerator;
using Blazora.Script;

namespace Blazora.Script
{
    public class Game_Setup
    {
        public static bool initialized = false;

        public static async Task setup ()
        {
            //Console.WriteLine ("game setup");
            await from_csv_load_resource ();
            await from_csv_load_recipe ();
            //Console.WriteLine ("after csv load");
            Game_Engine.self.manager_resource = stock_manager_resource_setup ();

            List<Generator> list_generator_locally = Game_Engine.self.manager_generator.Values
                .Where (generator => generator.must_craft_locally () == true)
                .ToList ();
            foreach (Generator generator_locally in list_generator_locally)
            {
                string toggle_id_transport_belt_1 = generator_locally.get_toggle_id_know ("transport_belt_1");
                Game_Engine.self.manager_toggle.set (toggle_id_transport_belt_1, false);
            }

            Game_Stat.self = new Game_Stat ();
            Game_Stat.self.stat_create ("time_played");

            foreach (string resource_name in Resource.list_resource_name)
            {
                Game_Stat.self.stat_create (resource_name);
            }
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
            stock_manager_resource.id = "MR_Game_Engine";

            //Console.WriteLine ("stock_manager_resource_setup");

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



        public static void quickstart ()
        {
            // Quickstart
            quickstart ("iron_ore", 8);
            quickstart ("coal_ore", 8);
            quickstart ("stone_ore", 8);
            quickstart ("iron_plate", 6);
            quickstart ("furnace_stone", 8);
            quickstart ("burner_drill", 4);
            quickstart ("assembling_machine_1", 4);
            quickstart ("iron_gear", 8);
            quickstart ("transport_belt_1", 8);
            quickstart ("inserter_1", 8);
            quickstart ("electronic_circuit", 8);

            quickstart_set_transport_belt_1 ();
        }


        private static void quickstart_set_transport_belt_1 ()
        {
            string resource_name = "transport_belt_1";
            List<BGenerator> list_bgenerator = BGenerator.from_game_page_get_list_bgenerator ()
                .Where (bgenerator => bgenerator.generator.has_stack_tool_containing (resource_name) == true)
                .ToList ();
            foreach (BGenerator bgenerator in list_bgenerator)
            {
                List<string> list_resource_name_similar =
                    Resource.from_resource_name_main_and_list_resource_name_get_list_resource_name_similar (
                        resource_name,
                        bgenerator.generator.manager_resource_tool.get_resource_mix ().get_list_resource_name ());
                foreach (string resource_name_similar in list_resource_name_similar)
                {
                    if (bgenerator.generator.manager_resource_tool
                        .from_resource_name_contain_resource_stack (resource_name_similar) == false)
                    {
                        continue;
                    }
                    bgenerator.generator.manager_resource_tool
                        .from_resource_name_and_resource_quantity_add_resource (
                            resource_name_similar, 1);
                }

                bgenerator.state_change ();
            }
        }

        public static void quickstart (string resource_name, int quantity_min)
        {
            int quantity_current = Game_Engine.self.manager_resource.from_resource_name_get_resource_quantity (resource_name);
            if (quantity_current >= quantity_min)
            {
                return;
            }
            Game_Engine.self.manager_resource.from_resource_name_and_resource_quantity_set_resource_quantity 
                (resource_name, quantity_min);
        }



        public static void listener_flag_setup ()
        {
            List<(string, string)> list_tuple_resource_observed_plus_generator_resource_concerned = new List<(string, string)> ()
            {
                ("furnace_stone", "fire"),
                ("burner_drill", "pickaxe"),
                ("assembling_machine_1", "hand"),
                ("inserter_1", "inserter_1"),
                ("transport_belt_1", "transport_belt_1")
            };
            foreach ((string, string) resource_observed_plus_generator_resource_concerned in 
                list_tuple_resource_observed_plus_generator_resource_concerned)
            {
                listener_flag_setup (
                    resource_observed_plus_generator_resource_concerned.Item1,
                    resource_observed_plus_generator_resource_concerned.Item2);
            }
        }


        public static void listener_flag_setup (string resource_observed, string generator_resource_concerned)
        {
            string toggle_id = $"know_{resource_observed}";
            Game_Engine.self.manager_resource.from_resource_name_get_resource_stack (resource_observed)
                .observable_quantity.changed += (v) =>
                {
                    if (Game_Engine.self.manager_toggle.get_tog (toggle_id) != Toggle.Tog.is_true)
                    {
                        if (v > 0)
                        {
                            Game_Action.upgrade_tool (toggle_id, generator_resource_concerned);
                        }
                    }
                };
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
                if ((string)record["mine"] != "true" &&
                    (string)record["unique"] != "true" &&
                    (string)record["can_craft_stock"] != "true"
                    )
                {
                    Resource.list_resource_name.Add (Resource.from_resource_name_get_complex_resource_name ("transport_belt_1", resource_name));
                    Resource.list_resource_name.Add (Resource.from_resource_name_get_complex_resource_name ("inserter_1", resource_name));
                }
            }

            Resource.dico_resource_name_plus_stack_resource_quantity_max =
                Resource.list_resource_name.
                ToDictionary (x => x, x => new ObservableProperty<int> ("R_" + x, 1));

            //Console.WriteLine ("end resource csv load");
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
                //break;
            }

            //Console.WriteLine ("end recipe csv load");
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
