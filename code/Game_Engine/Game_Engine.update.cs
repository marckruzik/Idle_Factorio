namespace NS_Game_Engine
{
    public partial class Game_Engine
    {
        public void update_logical ()
        {
            this.manager_job.clock = this.clock;
            this.manager_job.update_logical ();

            this.clock += 1;
        }


        public void update_graphical ()
        {
            
        }



    }
}
