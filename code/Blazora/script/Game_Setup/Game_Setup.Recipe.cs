using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazora.Script
{
    public partial class Game_Setup
    { 

        public static async Task from_csv_load_recipe ()
        {
            string csv_filepath = "data/recipe.csv";
            List<dynamic> list_record = await from_csv_filepath_get_list_record (csv_filepath);

            foreach (IDictionary<String, Object> record in list_record)
            {
                string component_mix_text = (string)record["component_mix"];
                string component_result_text = (string)record["result_mix"];
                int time = int.Parse ((string)(record["time"]));
                string tool_kind = (string)record["tool_kind"];

                add_generator (component_mix_text, component_result_text, time, tool_kind);
                //break;
            }

            Console.WriteLine ("csv recipe finished");
        }

    }
}
