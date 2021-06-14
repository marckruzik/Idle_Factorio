using NS_Manager_Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NS_Blazora_Basic;
using NS_Game_Engine;
using NS_Game_Engine.NS_Quest;

namespace Blazora.Script
{
    public partial class Game_Setup
    {

        public static async Task from_csv_load_quest ()
        {
            string csv_filepath = "data/quest.csv";
            List<dynamic> list_record = await from_csv_filepath_get_list_record (csv_filepath);
            //id,message,mix_resource_related,objective,gain
            foreach (IDictionary<String, Object> record in list_record)
            {
                string id = (string)record["id"];
                string message = (string)record["message"];

                string mix_resource_related_txt = (string)record["mix_resource_related"];
                Resource_Mix resource_mix_related = 
                    Resource_Mix.from_resource_mix_text_get_resource_mix (mix_resource_related_txt);

                string mix_objective_txt = (string)record["mix_objective"];
                Resource_Mix resource_mix_objective =
                    Resource_Mix.from_resource_mix_text_get_resource_mix (mix_objective_txt);
                Objective objective = Objective.from_resource_mix_get_objective (resource_mix_objective);

                string gain = (string)record["gain"];
                Recipe recipe = Game_Engine.self.manager_generator.from_recipe_result_resource_name_get_recipe (gain);

                Quest quest = new Quest ();
                quest.id = id;
                quest.message = message;
                quest.resource_mix_related = resource_mix_related;
                quest.objective = objective;
                quest.gain_recipe = recipe;
                quest.state = Quest.State.unknown;

                Game_Engine.self.manager_quest.Add (quest.id, quest);
            }

            Game_Engine.self.manager_quest["1"].state = Quest.State.started;
        }



    }
}
