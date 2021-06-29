using NS_Manager_Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NS_Blazora_Basic;

namespace Blazora.Script
{
    public partial class Game_Setup
    {

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

            Console.WriteLine ("csv resource finished");
        }

    }
}
