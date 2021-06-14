using System;
using System.Collections.Generic;
using System.Text;
using NS_Blazora_Basic;

namespace NS_Game_Engine.NS_Quest
{


    public class Manager_Quest : Dictionary<string, Quest>
    {

        public void logical_update ()
        {
            foreach (Quest quest in this.Values)
            {
                quest.logical_update ();
            }
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
            quest_next.state = Quest.State.started;
        }

        public Quest from_quest_get_quest_next (Quest quest)
        {
            int id_number = int.Parse (quest.id);
            id_number += 1;
            string id_number_str = id_number.ToString ();
            if (this.ContainsKey(id_number_str) == false)
            {
                return null;
            }
            Quest quest_next = this[id_number_str];
            return quest_next;
        }

    }
}
