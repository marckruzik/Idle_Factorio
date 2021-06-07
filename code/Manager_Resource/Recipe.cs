using System;
using System.Collections.Generic;
using System.Text;
using NS_Manager_Resource;
using System.Linq;

namespace NS_Manager_Resource
{
    public class Recipe
    {
        public Resource_Mix mix_component;
        public Resource_Mix mix_result;
        public int time;
        public int iteration;
        public List<Resource> list_tool_kind;


        public static Recipe get_recipe (string recipe_text, int recipe_time, string tool_kind_name)
        {
            Recipe recipe = new Recipe ();

            string component_text = Recipe.from_recipe_text_get_component_mix_text (recipe_text);
            string result_text = Recipe.from_recipe_text_get_result_mix_text (recipe_text);

            recipe.mix_component = Resource_Mix.from_resource_mix_text_get_resource_mix (component_text);
            recipe.time = (int)(recipe_time / 16.6666f);
            recipe.mix_result = Resource_Mix.from_resource_mix_text_get_resource_mix (result_text);

            recipe.list_tool_kind = new List<Resource> ()
            {
                Resource.from_resource_name_get_resource(tool_kind_name)
            };

            return recipe;
        }



        public int get_component_count ()
        {
            return mix_component.list_resource_stack.Count;
        }

        public string get_id ()
        {
            return get_text ();
        }

        public string get_text ()
        {
            return $"{mix_component.get_text ()} => {mix_result.get_text ()}";
        }

        public static Recipe from_recipe_text_get_recipe (string recipe_text)
        {
            Recipe recipe = new Recipe ();
            recipe.mix_component = Recipe.from_recipe_text_get_resource_mix_before (recipe_text);
            recipe.mix_result = Recipe.from_recipe_text_get_resource_mix_after (recipe_text);

            return recipe;
        }


        public static Resource_Mix from_recipe_text_get_resource_mix_before (string recipe_text)
        {
            string resource_mix_text_before = Recipe.from_recipe_text_get_component_mix_text (recipe_text);
            Resource_Mix resource_mix_before = Resource_Mix.from_resource_mix_text_get_resource_mix (resource_mix_text_before);

            return resource_mix_before;
        }


        public static Resource_Mix from_recipe_text_get_resource_mix_after (string recipe_text)
        {
            string resource_mix_text_after = Recipe.from_recipe_text_get_result_mix_text (recipe_text);
            Resource_Mix resource_mix_after = Resource_Mix.from_resource_mix_text_get_resource_mix (resource_mix_text_after);

            return resource_mix_after;
        }


        public static string from_recipe_text_get_component_mix_text (string recipe_text)
        {
            return from_text_and_separator_get_text_left (recipe_text, "=>");
        }

        public static string from_recipe_text_get_result_mix_text (string recipe_text)
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


    }
}
