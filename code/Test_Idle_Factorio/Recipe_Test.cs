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

        [TestCase ("coal_ore")]
        [TestCase ("wood_ore")]
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
        [TestCase ("coal_ore*1 + iron_ore*1 => iron_plate*1", "coal_ore*1 + iron_ore*1")]
        public void when_recipe__get_recipe_left (string recipe_text, string expected_recipe_text_left)
        {
            string recipe_text_left = Recipe.from_recipe_text_get_resource_mix_text_before (recipe_text);
            Assert.AreEqual (expected_recipe_text_left, recipe_text_left);
        }

        [TestCase ("before => after", "after")]
        [TestCase ("coal_ore*1 + iron_ore*1 => iron_plate*1", "iron_plate*1")]
        public void when_recipe__get_recipe_right (string recipe_text, string expected_recipe_text_left)
        {
            string recipe_text_left = Recipe.from_recipe_text_get_resource_mix_text_after (recipe_text);
            Assert.AreEqual (expected_recipe_text_left, recipe_text_left);
        }


        [TestCase ("coal_ore")]
        [TestCase ("wood_ore")]
        public void when_resource_text__get_resource (string ressource_text)
        {
            Resource resource = Resource.from_resource_text_get_resource (ressource_text);
            string resource_name = Resource.from_resource_text_get_resource_name (ressource_text);

            Assert.AreEqual (resource_name, resource.resource_name);
        }


        [TestCase ("coal_ore", "coal_ore")]
        [TestCase ("coal_ore ", "coal_ore")]
        [TestCase (" coal_ore", "coal_ore")]
        [TestCase (" coal_ore ", "coal_ore")]
        [TestCase ("  coal_ore ", "coal_ore")]
        [TestCase ("coal_ore", "coal_ore")]
        [TestCase ("coal_ore", "coal_ore")]
        public void when_resource_text__get_resource_name (string ressource_text, string expected_resource_name)
        {
            string resource_name = Resource.from_resource_text_get_resource_name (ressource_text);

            Assert.AreEqual (expected_resource_name, resource_name);
        }


        [TestCase ("coal_ore*1", "coal_ore")]
        [TestCase (" coal_ore*1", "coal_ore")]
        [TestCase ("coal_ore*1 ", "coal_ore")]
        [TestCase ("coal_ore *1", "coal_ore")]
        [TestCase ("coal_ore* 1", "coal_ore")]
        [TestCase ("coal_ore * 1", "coal_ore")]
        public void when_resource_stack_text_good__get_resource_stack_name (
            string resource_stack_text, string expected_resource_stack_name)
        {
            string resource_stack_name = Resource_Stack.from_resource_stack_text_get_resource_name (resource_stack_text);

            Assert.AreEqual (expected_resource_stack_name, resource_stack_name);
        }


        [TestCase ("coal_ore*1", 1)]
        [TestCase (" coal_ore*1", 1)]
        [TestCase ("coal_ore*1 ", 1)]
        [TestCase ("coal_ore *1", 1)]
        [TestCase ("coal_ore* 1", 1)]
        [TestCase ("coal_ore * 1", 1)]
        [TestCase ("coal_ore * 99", 99)]
        public void when_resource_stack_text_good__get_resource_stack_quantity (
            string resource_stack_text, int expected_resource_stack_quantity)
        {
            int resource_stack_quantity = Resource_Stack.from_resource_stack_text_get_resource_quantity (resource_stack_text);

            Assert.AreEqual (expected_resource_stack_quantity, resource_stack_quantity);
        }


        [TestCase ("coal_ore * 1 + wood_ore * 2", "coal_ore", 1, "wood_ore", 2)]
        [TestCase ("coal_ore*1+wood_ore*2", "coal_ore", 1, "wood_ore", 2)]
        public void when_resource_mix_text2__get_resource_mix (
            string resource_mix_text, string name1, int quantity1, string name2, int quantity2)
        {
            Resource_Mix resource_mix = Resource_Mix.from_resource_mix_text_get_resource_mix (resource_mix_text);

            Assert.AreEqual (name1, resource_mix.list_resource_stack[0].resource.resource_name);
            Assert.AreEqual (quantity1, resource_mix.list_resource_stack[0].quantity);
            Assert.AreEqual (name2, resource_mix.list_resource_stack[1].resource.resource_name);
            Assert.AreEqual (quantity2, resource_mix.list_resource_stack[1].quantity);
        }

        [TestCase ("coal_ore * 1", "coal_ore", 1)]
        [TestCase ("coal_ore*1", "coal_ore", 1)]
        public void when_resource_mix_text1__get_resource_mix (
            string resource_mix_text, string name1, int quantity1)
        {
            Resource_Mix resource_mix = Resource_Mix.from_resource_mix_text_get_resource_mix (resource_mix_text);

            Assert.AreEqual (name1, resource_mix.list_resource_stack[0].resource.resource_name);
            Assert.AreEqual (quantity1, resource_mix.list_resource_stack[0].quantity);
        }


        [TestCase ("coal_ore*1 + iron_ore*1 => iron_plate*1", "coal_ore", 1, "iron_ore", 1)]
        public void when_recipe_text__get_resource_mix_before (
            string recipe_text, string name1, int quantity1, string name2, int quantity2)
        {
            Resource_Mix resource_mix_before = Recipe.from_recipe_text_get_resource_mix_before (recipe_text);

            Assert.AreEqual (name1, resource_mix_before.list_resource_stack[0].resource.resource_name);
            Assert.AreEqual (quantity1, resource_mix_before.list_resource_stack[0].quantity);
            Assert.AreEqual (name2, resource_mix_before.list_resource_stack[1].resource.resource_name);
            Assert.AreEqual (quantity2, resource_mix_before.list_resource_stack[1].quantity);
        }


        [TestCase ("coal_ore*1 + iron_ore*1 => iron_plate*1", "iron_plate", 1)]
        public void when_recipe_text__get_resource_mix_after (
            string recipe_text, string name1, int quantity1)
        {
            Resource_Mix resource_mix_before = Recipe.from_recipe_text_get_resource_mix_after (recipe_text);

            Assert.AreEqual (name1, resource_mix_before.list_resource_stack[0].resource.resource_name);
            Assert.AreEqual (quantity1, resource_mix_before.list_resource_stack[0].quantity);
        }


        [TestCase ("coal_ore*1 + iron_ore*1 => iron_plate*1", "coal_ore * 1 + iron_ore * 1 => iron_plate * 1")]
        public void when_recipe_text__get_recipe_text (
            string recipe_text, string recipe_text_2)
        {
            Recipe2 recipe = Recipe2.from_recipe_text_get_recipe (recipe_text);
            string recipe_text_redo = recipe.get_text ();

            Assert.AreEqual (recipe_text_2, recipe_text_redo);
        }



    }
}