using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NS_Manager_Resource
{
    public class Manager_Generator : Dictionary<string, Generator>
    {

        public Generator from_recipe_get_generator (Recipe recipe)
        {
            return this[recipe.get_text ()];
        }

        public Recipe from_recipe_result_resource_name_get_recipe (string recipe_result_resource_name)
        {
            return this.Values
                .FirstOrDefault (
                    generator => generator.recipe.get_first_result_resource_name () == recipe_result_resource_name)
                .recipe;
        }


        public Generator from_generator_id_get_generator (int generator_id)
        {
            return this.Values.FirstOrDefault (generator => generator.id == generator_id);
        }

        public void from_recipe_add_generator (Recipe recipe)
        {
            if (this.ContainsKey (recipe.get_text ()) == true)
            {
                return;
            }

            Generator generator = Generator.from_recipe_create_generator (recipe);

            this.Add (recipe.get_text (), generator);
        }


    }
}
