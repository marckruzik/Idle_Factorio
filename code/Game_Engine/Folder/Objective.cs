using NS_Manager_Resource;

namespace NS_Game_Engine.NS_Quest
{
    public class Objective
    {
        public Resource_Mix resource_mix;

        public static Objective from_resource_mix_get_objective (Resource_Mix resource_mix)
        {
            Objective objective = new Objective ();
            objective.resource_mix = resource_mix;
            return objective;
        }

        public bool is_accomplished ()
        {
            foreach (Resource_Stack resource_stack in this.resource_mix.list_resource_stack)
            {
                int stock_quantity = Game_Engine.self
                    .manager_resource
                    .from_resource_name_get_resource_quantity (resource_stack.resource_name);
                if (stock_quantity < resource_stack.quantity)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
