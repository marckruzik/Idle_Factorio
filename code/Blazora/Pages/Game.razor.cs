using NS_Game_Engine;
using System.Threading;
using System.Threading.Tasks;




namespace Blazora.Pages
{
    public partial class Game
    {
        private Timer timer_refresh;
        private Timer timer_logic;


        protected override async Task OnInitializedAsync ()
        {
            StartTimer ();
            await Task.CompletedTask;
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
                InvokeAsync (StateHasChanged);
            }), null, 100, 100);


        }


        public virtual void update_logical ()
        {
            Game_Engine.self.update_logical ();
        }


        public void Dispose ()
        {
            this.timer_refresh.Dispose ();
            this.timer_logic.Dispose ();
            this.timer_logic = null;
        }





    }
}