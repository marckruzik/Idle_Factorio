﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NS_Manager_Resource;


namespace NS_Game_Engine
{
    public class Game_Engine
    {
        public static Game_Engine self;

        public Manager_Resource manager_resource;

        public static void start ()
        {
            if (Game_Engine.self != null)
            {
                return;
            }

            Game_Engine.self = new Game_Engine ();

            Game_Engine.self.manager_resource = create_manager_resource ();


        }


        private static Manager_Resource create_manager_resource ()
        {
            Manager_Resource manager_resource = new Manager_Resource ();

            manager_resource.from_resource_name_add_resource ("wood");
            manager_resource.from_resource_name_add_resource ("coal");

            return manager_resource;
        }
    }
}
