using System;
using System.Collections.Generic;
using System.Text;
using NS_Idle_Factorio_Basic;
using NS_Manager_Resource;
using System.Linq;

namespace NS_Game_Engine.NS_Quest
{


    public class Manager_Quest : Dictionary<string, Quest>
    {

        public bool logical_update ()
        {
            bool changed = false;
            foreach (Quest quest in this.Values)
            {
                changed |= quest.logical_update ();
            }
            return changed;
        }


        public void quest_solve (Quest quest)
        {
            quest.state = Quest.State.finished;
            Console.WriteLine ($"quest finished: {quest.message}");
            Quest quest_next = from_quest_get_quest_next (quest);
            if (quest_next == null)
            {
                // end game
                return;
            }
        }


        public Quest from_quest_get_quest_next (Quest quest)
        {
            int id_number = int.Parse (quest.id);
            id_number += 1;
            string id_number_str = id_number.ToString ();
            if (this.ContainsKey (id_number_str) == false)
            {
                return null;
            }
            Quest quest_next = this[id_number_str];
            return quest_next;
        }


        public Quest from_quest_gain_recipe_result_resource_name_get_quest (string quest_gain_recipe_result_resource_name)
        {
            return this.Values.FirstOrDefault (
                quest => quest.gain_recipe.get_first_result_resource_name () ==
                    quest_gain_recipe_result_resource_name);
        }


        public bool from_generator_is_authorized (Generator generator)
        {
            string resource_name = generator.get_result_resource_name ();
            if (resource_name == "iron_ore")
            {
                return true;
            }
            Quest quest = from_quest_gain_recipe_result_resource_name_get_quest (resource_name);
            if (quest == null)
            {
                return false;
            }
            return quest.state == Quest.State.unlocked;
        }

    }
}
