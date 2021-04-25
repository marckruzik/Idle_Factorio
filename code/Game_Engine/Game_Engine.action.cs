using System;
using NS_Manager_Resource;

namespace NS_Game_Engine
{
    public partial class Game_Engine
    {
        
        public Job from_recipe_crafting_and_job_id_add_job (Recipe recipe, string job_id)
        {
            int? time_quantity = Recipe.from_recipe_get_time_quantity (recipe);
            if (time_quantity == null)
            {
                time_quantity = 0;
            }

            int time = time_quantity != null ? (int)time_quantity : 0;

            this.manager_resource.from_resource_mix_remove_resource (recipe.resource_mix_before);

            Action action = delegate ()
            {
                this.manager_resource.from_resource_mix_add_resource (recipe.resource_mix_after);
            }; 


            Job job = this.manager_job.from_action_and_time_total_add_job (action, time);
            job.id = job_id;

            return job;
        }



    }
}
