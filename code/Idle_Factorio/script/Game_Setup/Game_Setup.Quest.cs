using NS_Manager_Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NS_Idle_Factorio_Basic;
using NS_Game_Engine;
using NS_Game_Engine.NS_Quest;

namespace Idle_Factorio.Script
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

                string objective_txt = (string)record["mix_objective"];
                Objective objective = Objective.from_objective_txt_get_objective (objective_txt);
                
                Resource_Mix resource_mix_related = objective.resource_mix;

                string gain = (string)record["gain"];
                Recipe recipe = null;
                if (gain != "endgame")
                {
                    recipe = Game_Engine.self.manager_generator.from_recipe_result_resource_name_get_recipe (gain);
                }
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

            Console.WriteLine ("load quest finished");
        }



    }
}
