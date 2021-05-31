using NS_Game_Engine;
using System;
using System.Threading;
using System.Threading.Tasks;

using System.Timers;


namespace Blazora.Pages
{
    public partial class Game_Page
    {
        private System.Timers.Timer timer_refresh;
        private System.Timers.Timer timer_logic;

        public static event Action graphical_action;

        protected override async Task OnInitializedAsync ()
        {
            StartTimer ();
            await Task.CompletedTask;
        }

        protected override void OnInitialized ()
        {
            base.OnInitialized ();

            //Blazora.Pages.Game_Page.graphical_action += StateHasChanged;
        }


        public void StartTimer ()
        {
            if (this.timer_logic == null)
            {
                this.timer_logic = new System.Timers.Timer (16);
                this.timer_logic.Elapsed += NotifyTimerElapsed;
                this.timer_logic.AutoReset = true;
                this.timer_logic.Enabled = true;
            }


            this.timer_refresh = new System.Timers.Timer (16);
            this.timer_refresh.Elapsed += NotifyTimerElapsedGraphical;
            this.timer_refresh.AutoReset = true;
            this.timer_refresh.Enabled = true;

        }


        private void NotifyTimerElapsed (object source, ElapsedEventArgs e)
        {
            logical_update ();
        }

        private void NotifyTimerElapsedGraphical (object source, ElapsedEventArgs e)
        {
            graphical_update ();
        }


        public virtual void logical_update ()
        {
            Game_Engine.self.logical_update ();
        }


        public virtual void graphical_update ()
        {
            Game_Engine.self.graphical_update ();
            //InvokeAsync (StateHasChanged);

            Game_Page.graphical_action?.Invoke ();
        }



        public void Dispose ()
        {
            this.timer_refresh.Dispose ();
            this.timer_logic.Dispose ();
            this.timer_logic = null;
            //Blazora.Pages.Game_Page.graphical_action -= StateHasChanged;
        }





    }
}