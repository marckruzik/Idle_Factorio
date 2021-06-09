using NS_Game_Engine;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Timers;
using System.Diagnostics;
using Blazora.Components;
using NS_Blazora_Basic;

namespace Blazora.Pages
{
    public partial class Game_Page
    {
        public bool graphical_running = true;
        public static bool logical_running_started = false;


        private System.Timers.Timer timer_logic;
        private Stopwatch stopwatch;
        private int logical_interval = 16;
        private int graphical_interval = 16;
        private long logical_time_elapsed;

        public static List<Game_Component> list_component = new List<Game_Component> ();

        public static Game_Page self;

        public ObservableProperty<int> stat_average = new ObservableProperty<int> ("stat_average", 0);

        protected override async Task OnInitializedAsync ()
        {
            Game_Page.self = this;
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


        long total = 0;
        long last_second = 0;
        int number = 0;
        async Task graphical_thread_task ()
        {
            while (true)
            {
                if (this.graphical_running == false)
                {
                    break;
                }
                long a = this.stopwatch.ElapsedMilliseconds;
                graphical_update ();
                long b = this.stopwatch.ElapsedMilliseconds;
                long tim = b - a;
                total += tim;
                number += 1;
                long current_second = this.stopwatch.ElapsedMilliseconds / 1000;
                if (last_second != current_second)
                {
                    this.stat_average.Set ((int)(((float)total) / number));
                    last_second = current_second;
                    total = 0;
                    number = 0;
                }
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

            foreach (Game_Component game_component in Game_Page.list_component)
            {
                game_component.graphical_update ();
            }
            
        }


        public virtual void Dispose ()
        {
            this.graphical_running = false;
            this.timer_logic.Dispose ();
            this.timer_logic = null;
            this.stopwatch = null;
        }


    }
}