using NS_Manager_Resource;

namespace NS_Game_Engine.NS_Quest
{
    public class Quest
    {
        public string id;
        public string message;
        public Resource_Mix resource_mix_related;
        public Objective objective;
        public Recipe gain_recipe;

        public enum State
        {
            blank,
            unknown,
            started,
            finished,
            unlocked
        }
        public State state;

        public bool logical_update ()
        {
            if (this.state == State.started)
            {
                if (this.objective.is_accomplished () == true)
                {
                    Game_Engine.self.manager_quest.quest_solve (this);
                    return true;
                }
            }
            return false;
        }
    }
}
