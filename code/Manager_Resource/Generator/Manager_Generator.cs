using System;
using System.Collections.Generic;
using System.Text;

namespace NS_Manager_Resource
{
    public class Manager_Generator : Dictionary<string, Generator>
    {

        public Generator from_recipe_get_generator (Recipe recipe)
        {
            return this[recipe.get_text ()];
        }


        public void from_recipe_add_generator (Recipe recipe)
        {
            if (this.ContainsKey (recipe.get_text ()) == true)
            {
                return;
            }

            Generator generator = Generator.from_recipe_get_generator (recipe);

            this.Add (recipe.get_text (), generator);
        }


    }
}
