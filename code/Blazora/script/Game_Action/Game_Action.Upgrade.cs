using NS_Game_Engine;
using NS_Manager_Resource;

namespace Blazora.script
{
    public static partial class Game_Action
    {


        public static bool action_upgrade_is_ready (Recipe recipe)
        {
            string resource_name = Game_Engine.self
                .manager_generator
                .from_recipe_get_generator (recipe)
                .get_result_resource_name ();

            int stock_resource_quantity_max = Game_Engine.self.manager_resource.from_resource_name_get_stock_resource_quantity_max (
                resource_name);
            int stock_resource_quantity = Game_Engine.self.manager_resource.from_resource_name_get_resource_quantity (resource_name);

            return (stock_resource_quantity >= stock_resource_quantity_max);
        }


        public static void action_upgrade (Recipe recipe)
        {
            string resource_name = Game_Engine.self
               .manager_generator
               .from_recipe_get_generator (recipe)
               .get_result_resource_name ();

            int stack_resource_quantity_max = Resource.from_resource_name_get_stack_resource_quantity_max (resource_name);
            int stack_resource_quantity_max_future = stack_resource_quantity_max * 2;

            Resource.dico_resource_name_plus_stack_resource_quantity_max[resource_name].Set (stack_resource_quantity_max_future);
        }
    }
}