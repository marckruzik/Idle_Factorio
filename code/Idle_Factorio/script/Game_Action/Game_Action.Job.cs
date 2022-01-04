using NS_Game_Engine;
using NS_Manager_Resource;

namespace Idle_Factorio.Script
{
    public static partial class Game_Action
    {



        public static string from_recipe_get_job_id (Recipe recipe)
        {
            return recipe.get_text ();
        }


        public static Job from_recipe_get_job (Recipe recipe)
        {
            string job_id = from_recipe_get_job_id (recipe);
            Job job = Game_Engine.self.manager_job.from_job_id_find_job (job_id);

            return job;
        }


        public static int from_recipe_get_job_time_percentage (Recipe recipe)
        {
            Job job = from_recipe_get_job (recipe);
            if (job == null)
            {
                return 0;
            }

            float percentage_done = job.get_percentage_done ();
            int perc = (int)(percentage_done * 100);

            return perc;
        }



    }
}
