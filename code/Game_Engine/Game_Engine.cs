using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NS_Manager_Resource;


namespace NS_Game_Engine
{
    public partial class Game_Engine
    {
        public static Game_Engine self;

        public Manager_Resource manager_resource;
        public Manager_Generator manager_generator;
        public Manager_Job manager_job;

        public static bool initialized = false;

        public long clock = 0;


        public static void setup ()
        {
            if (Game_Engine.self != null)
            {
                return;
            }

            Game_Engine.self = new Game_Engine ();

            Game_Engine.self.manager_resource = create_manager_resource ();
            Game_Engine.self.manager_generator = create_manager_generator ();
            Game_Engine.self.manager_job = create_manager_job ();


        }


        private static Manager_Resource create_manager_resource ()
        {
            Manager_Resource manager_resource = new Manager_Resource ();

            return manager_resource;
        }

        private static Manager_Generator create_manager_generator ()
        {
            Manager_Generator manager_generator = new Manager_Generator ();

            return manager_generator;
        }



        private static Manager_Job create_manager_job ()
        {
            Manager_Job manager_job = new Manager_Job ();

            return manager_job;
        }

    }
}
