using NS_Game_Engine;
using System;
using System.Threading;
using System.Threading.Tasks;

using System.Timers;


namespace Blazora.Pages
{
    public partial class Game_Page
    {

        public static event Action graphical_action;

        public bool graphical_running = true;
        public static bool logical_running_started = false;

        protected override async Task OnInitializedAsync ()
        {
            Console.WriteLine ("start1");
            logical_thread_task ();

            Console.WriteLine ("start2");
            graphical_thread_task ();

            Console.WriteLine ("start3");
            await Task.CompletedTask;
        }

        protected override void OnInitialized ()
        {
            base.OnInitialized ();

        }



        async Task graphical_thread_task ()
        {
            while (true)
            {
                if (this.graphical_running == false)
                {
                    break;
                }
                graphical_update ();
                await Task.Delay (16);
            }
        }

        async Task logical_thread_task ()
        {
            if (logical_running_started == true)
            {
                return;
            }
            logical_running_started = true;
            while (logical_running_started)
            {
                logical_update ();
                await Task.Delay (16);
            }
        }





        public virtual void logical_update ()
        {
            Game_Engine.self.logical_update ();
        }


        public virtual void graphical_update ()
        {
            Game_Engine.self.graphical_update ();
            //Console.WriteLine ("graphical update");
            Game_Page.graphical_action?.Invoke ();
        }



        public void Dispose ()
        {
            this.graphical_running = false;
        }





    }
}