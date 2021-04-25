using NS_Game_Engine;
using System;
using System.Threading;
using System.Threading.Tasks;




namespace Blazora.Pages
{
    public partial class Game_Page
    {
        private Timer timer_refresh;
        private Timer timer_logic;

        public static event Action OnChange;

        protected override async Task OnInitializedAsync ()
        {
            StartTimer ();
            await Task.CompletedTask;
        }

        protected override void OnInitialized ()
        {
            base.OnInitialized ();

            Blazora.Pages.Game_Page.OnChange += StateHasChanged;
        }


        public void StartTimer ()
        {
            if (this.timer_logic == null)
            {
                this.timer_logic = new Timer (new TimerCallback (_ =>
                {
                    update_logical ();
                }), null, 1000, 1000);
            }


            this.timer_refresh = new Timer (new TimerCallback (_ =>
            {
                update_graphical ();
            }), null, 100, 100);


        }


        public virtual void update_logical ()
        {
            Game_Engine.self.update_logical ();
        }


        public virtual void update_graphical ()
        {
            Game_Engine.self.update_graphical ();
            //InvokeAsync (StateHasChanged);

            Game_Page.OnChange?.Invoke ();
        }



        public void Dispose ()
        {
            this.timer_refresh.Dispose ();
            this.timer_logic.Dispose ();
            this.timer_logic = null;
            Blazora.Pages.Game_Page.OnChange -= StateHasChanged;
        }





    }
}