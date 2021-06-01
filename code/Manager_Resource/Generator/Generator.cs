using System;
using System.Collections.Generic;
using System.Text;

namespace NS_Manager_Resource
{
    public class Generator
    {
        public Recipe2 recipe;
        public Manager_Resource manager_resource;


        public Resource_Stack get_stack_tool ()
        {
            Resource resource_tool = this.recipe.list_tool_kind[0];
            Resource_Stack stack_tool = this.manager_resource.from_resource_name_get_resource_stack (resource_tool.resource_name);
            return stack_tool;
        }



    }
}
