namespace NS_Game_Engine
{
    public partial class Game_Engine
    {
        public void logical_update ()
        {
            this.manager_job.clock = this.clock;
            this.manager_job.logical_update ();

            this.clock += 1;
        }


        public void graphical_update ()
        {
            
        }



    }
}
