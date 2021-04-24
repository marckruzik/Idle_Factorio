using System;
using System.Collections.Generic;
using System.Text;
using NS_Manager_Resource;

namespace NS_Manager_Resource
{
    public class Recipe
    {
        public Resource_Mix resource_mix_before;
        public Resource_Mix resource_mix_after;


        public static Recipe from_recipe_text_get_recipe (string recipe_text)
        {
            Recipe recipe = new Recipe ();
            recipe.resource_mix_before = Recipe.from_recipe_text_get_resource_mix_before (recipe_text);
            recipe.resource_mix_after = Recipe.from_recipe_text_get_resource_mix_after (recipe_text);

            return recipe;
        }


        public static Resource_Mix from_recipe_text_get_resource_mix_before (string recipe_text)
        {
            string resource_mix_text_before = Recipe.from_recipe_text_get_resource_mix_text_before (recipe_text);
            Resource_Mix resource_mix_before = Resource_Mix.from_resource_mix_text_get_resource_mix (resource_mix_text_before);

            return resource_mix_before;
        }


        public static Resource_Mix from_recipe_text_get_resource_mix_after (string recipe_text)
        {
            string resource_mix_text_after = Recipe.from_recipe_text_get_resource_mix_text_after (recipe_text);
            Resource_Mix resource_mix_after = Resource_Mix.from_resource_mix_text_get_resource_mix (resource_mix_text_after);

            return resource_mix_after;
        }


        public static string from_recipe_text_get_resource_mix_text_before (string recipe_text)
        {
            return from_text_and_separator_get_text_left (recipe_text, "=>");
        }

        public static string from_recipe_text_get_resource_mix_text_after (string recipe_text)
        {
            return from_text_and_separator_get_text_right (recipe_text, "=>");
        }

        public static string from_text_and_separator_get_text_left (string text, string separator)
        {
            string[] arr = text.Split (separator);
            string text_left = arr[0].Trim ();
            return text_left;
        }
        public static string from_text_and_separator_get_text_right (string text, string separator)
        {
            string[] arr = text.Split (separator);
            string text_right = arr[1].Trim ();
            return text_right;
        }

        public static int? from_recipe_get_time_quantity (Recipe recipe)
        {
            Resource_Stack resource_stack_time = Recipe.from_recipe_and_resource_name_get_before_resource_stack (recipe, "time");
            if (resource_stack_time == null)
            {
                return null;
            }
            return resource_stack_time.quantity;
        }

        public static Resource_Stack from_recipe_and_resource_name_get_before_resource_stack (Recipe recipe, string resource_name)
        {
            Resource_Mix resource_mix_before = recipe.resource_mix_before;
            Resource_Stack resource_stack = Resource_Mix.from_resource_mix_and_resource_name_get_resource_stack (
                resource_mix_before, resource_name);

            return resource_stack;
        }
    }
}
