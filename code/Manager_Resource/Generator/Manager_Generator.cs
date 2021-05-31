using System;
using System.Collections.Generic;
using System.Text;

namespace NS_Manager_Resource
{
    public class Manager_Generator : Dictionary<string, Generator>
    {

        public Generator from_recipe_get_generator (Recipe2 recipe)
        {
            return this[recipe.get_text ()];
        }

    }
}
