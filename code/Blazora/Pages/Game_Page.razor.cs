using NS_Game_Engine;
using System;
using System.Threading;
using System.Threading.Tasks;

using System.Timers;
using System.Diagnostics;


namespace Blazora.Pages
{
    public partial class Game_Page
    {

        public static event Action graphical_action;

        public bool graphical_running = true;
        public static bool logical_running_started = false;


        private System.Timers.Timer timer_logic;
        private Stopwatch stopwatch;
        private int logical_interval = 16;
        private int graphical_interval = 16;
        private long logical_time_elapsed;

        protected override async Task OnInitializedAsync ()
        {
            StartTimer ();

            graphical_thread_task ();

            await Task.CompletedTask;
        }


        public void StartTimer ()
        {
            if (this.timer_logic == null)
            {
                this.timer_logic = new System.Timers.Timer (this.logical_interval);
                this.timer_logic.Elapsed += NotifyTimerElapsed;
                this.timer_logic.AutoReset = true;
                this.timer_logic.Enabled = true;
            }
            this.stopwatch = new Stopwatch ();
            this.stopwatch.Start ();
            this.logical_time_elapsed = 0;
        }


        private void NotifyTimerElapsed (object source, ElapsedEventArgs e)
        {
            while (this.logical_time_elapsed < this.stopwatch.ElapsedMilliseconds)
            {
                logical_update ();
                this.logical_time_elapsed += this.logical_interval;
            }
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
                await Task.Delay (this.graphical_interval);
            }
        }


        public virtual void logical_update ()
        {
            //Console.WriteLine ("logical update");
            Game_Engine.self.logical_update ();
        }


        public virtual void graphical_update ()
        {
            //Console.WriteLine ("graphical update");
            Game_Engine.self.graphical_update ();
            Game_Page.graphical_action?.Invoke ();
        }


        public void Dispose ()
        {
            this.graphical_running = false;
            this.timer_logic.Dispose ();
            this.timer_logic = null;
            this.stopwatch = null;
        }


    }
}