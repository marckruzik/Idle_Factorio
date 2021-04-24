using NS_Manager_Resource;
using NUnit.Framework;
using System;

namespace Test_Idle_Factorio
{
    public class Recipe_Test
    {
        [SetUp]
        public void Setup ()
        {
        }

        [TestCase ("coal")]
        [TestCase ("wood")]
        public void when_resource_name_is_correct (string resource_name)
        {
            Assert.DoesNotThrow (() => { Resource.from_resource_name_check_resource_name (resource_name); });
        }

        [TestCase ("xyz")]
        public void when_resource_name_is_incorrect (string resource_name)
        {
            Assert.Throws<Exception> (() => { Resource.from_resource_name_check_resource_name (resource_name); });
        }


        [TestCase ("before => after", "before")]
        [TestCase ("coal*1 + iron_ore*1 => iron_plate*1", "coal*1 + iron_ore*1")]
        public void when_recipe__get_recipe_left (string recipe_text, string expected_recipe_text_left)
        {
            string recipe_text_left = Recipe.from_recipe_text_get_resource_mix_text_before (recipe_text);
            Assert.AreEqual (expected_recipe_text_left, recipe_text_left);
        }

        [TestCase ("before => after", "after")]
        [TestCase ("coal*1 + iron_ore*1 => iron_plate*1", "iron_plate*1")]
        public void when_recipe__get_recipe_right (string recipe_text, string expected_recipe_text_left)
        {
            string recipe_text_left = Recipe.from_recipe_text_get_resource_mix_text_after (recipe_text);
            Assert.AreEqual (expected_recipe_text_left, recipe_text_left);
        }


        [TestCase ("coal")]
        [TestCase ("wood")]
        public void when_resource_text__get_resource (string ressource_text)
        {
            Resource resource = Resource.from_resource_text_get_resource (ressource_text);
            string resource_name = Resource.from_resource_text_get_resource_name (ressource_text);

            Assert.AreEqual (resource_name, resource.name);
        }


        [TestCase ("coal", "coal")]
        [TestCase ("coal ", "coal")]
        [TestCase (" coal", "coal")]
        [TestCase (" coal ", "coal")]
        [TestCase ("  coal ", "coal")]
        [TestCase ("Coal", "coal")]
        [TestCase ("COAL", "coal")]
        public void when_resource_text__get_resource_name (string ressource_text, string expected_resource_name)
        {
            string resource_name = Resource.from_resource_text_get_resource_name (ressource_text);

            Assert.AreEqual (expected_resource_name, resource_name);
        }


        [TestCase ("coal*1", "coal")]
        [TestCase (" coal*1", "coal")]
        [TestCase ("coal*1 ", "coal")]
        [TestCase ("coal *1", "coal")]
        [TestCase ("coal* 1", "coal")]
        [TestCase ("coal * 1", "coal")]
        public void when_resource_stack_text_good__get_resource_stack_name (
            string resource_stack_text, string expected_resource_stack_name)
        {
            string resource_stack_name = Resource_Stack.from_resource_stack_text_get_resource_name (resource_stack_text);

            Assert.AreEqual (expected_resource_stack_name, resource_stack_name);
        }


        [TestCase ("coal*1", 1)]
        [TestCase (" coal*1", 1)]
        [TestCase ("coal*1 ", 1)]
        [TestCase ("coal *1", 1)]
        [TestCase ("coal* 1", 1)]
        [TestCase ("coal * 1", 1)]
        [TestCase ("coal * 99", 99)]
        public void when_resource_stack_text_good__get_resource_stack_quantity (
            string resource_stack_text, int expected_resource_stack_quantity)
        {
            int resource_stack_quantity = Resource_Stack.from_resource_stack_text_get_resource_quantity (resource_stack_text);

            Assert.AreEqual (expected_resource_stack_quantity, resource_stack_quantity);
        }


        [TestCase ("coal * 1 + wood * 2", "coal", 1, "wood", 2)]
        [TestCase ("coal*1+wood*2", "coal", 1, "wood", 2)]
        public void when_resource_mix_text2__get_resource_mix (
            string resource_mix_text, string name1, int quantity1, string name2, int quantity2)
        {
            Resource_Mix resource_mix = Resource_Mix.from_resource_mix_text_get_resource_mix (resource_mix_text);

            Assert.AreEqual (name1, resource_mix.list_resource_stack[0].resource.name);
            Assert.AreEqual (quantity1, resource_mix.list_resource_stack[0].quantity);
            Assert.AreEqual (name2, resource_mix.list_resource_stack[1].resource.name);
            Assert.AreEqual (quantity2, resource_mix.list_resource_stack[1].quantity);
        }

        [TestCase ("coal * 1", "coal", 1)]
        [TestCase ("coal*1", "coal", 1)]
        public void when_resource_mix_text1__get_resource_mix (
            string resource_mix_text, string name1, int quantity1)
        {
            Resource_Mix resource_mix = Resource_Mix.from_resource_mix_text_get_resource_mix (resource_mix_text);

            Assert.AreEqual (name1, resource_mix.list_resource_stack[0].resource.name);
            Assert.AreEqual (quantity1, resource_mix.list_resource_stack[0].quantity);
        }


        [TestCase ("coal*1 + iron_ore*1 => iron_plate*1", "coal", 1, "iron_ore", 1)]
        public void when_recipe_text__get_resource_mix_before (
            string recipe_text, string name1, int quantity1, string name2, int quantity2)
        {
            Resource_Mix resource_mix_before = Recipe.from_recipe_text_get_resource_mix_before (recipe_text);

            Assert.AreEqual (name1, resource_mix_before.list_resource_stack[0].resource.name);
            Assert.AreEqual (quantity1, resource_mix_before.list_resource_stack[0].quantity);
            Assert.AreEqual (name2, resource_mix_before.list_resource_stack[1].resource.name);
            Assert.AreEqual (quantity2, resource_mix_before.list_resource_stack[1].quantity);
        }


        [TestCase ("coal*1 + iron_ore*1 => iron_plate*1", "iron_plate", 1)]
        public void when_recipe_text__get_resource_mix_after (
            string recipe_text, string name1, int quantity1)
        {
            Resource_Mix resource_mix_before = Recipe.from_recipe_text_get_resource_mix_after (recipe_text);

            Assert.AreEqual (name1, resource_mix_before.list_resource_stack[0].resource.name);
            Assert.AreEqual (quantity1, resource_mix_before.list_resource_stack[0].quantity);
        }


        [TestCase ("iron_ore*1 + time*10 => iron_plate*1", 10)]
        public void when_recipe_text__get_time_quantity_correct (string recipe_text, int expected_time_quantity)
        {
            Recipe recipe = Recipe.from_recipe_text_get_recipe (recipe_text);
            int? time_quantity = Recipe.from_recipe_get_time_quantity (recipe);

            Assert.AreEqual (expected_time_quantity, time_quantity);
        }


        [TestCase ("iron_ore*1 => iron_plate*1")]
        public void when_recipe_text__get_time_quantity_incorrect (string recipe_text)
        {
            Recipe recipe = Recipe.from_recipe_text_get_recipe (recipe_text);
            int? time_quantity = Recipe.from_recipe_get_time_quantity (recipe);

            Assert.AreEqual (null, time_quantity);
        }
    }
}