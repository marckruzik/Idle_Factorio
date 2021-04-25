using System;
using System.Collections.Generic;
using System.Text;

namespace NS_Game_Engine
{
    public class Job
    {

        public Action action;
        public long date_start;
        
        public int time_total;
        public int time_remaining;
        public bool alive = true;
        public string id;

        public static Job from_action_and_date_start_and_time_total_get_job (
            Action action, 
            long date_start, 
            int time_total)
        {
            Job job = new Job ();
            job.action = action;
            job.date_start = date_start;
            job.time_total = time_total;
            job.time_remaining = job.time_total;
            return job;
        }

        public void update_logical ()
        {
            time_remaining_check ();
            time_remaining_update ();
            time_remaining_check ();
        }


        public void time_remaining_check ()
        {
            if (this.alive == false)
            {
                return;
            }
            if (this.time_remaining <= 0)
            {
                this.action ();
                dispose ();
            }
        }


        private void time_remaining_update ()
        {
            if (this.alive == false)
            {
                return;
            }
            this.time_remaining -= 1;
        }


        public float get_percentage_done ()
        {
            float percentage_done = (this.time_total - this.time_remaining) / (float)this.time_total;
            return percentage_done;
        }


        public void dispose ()
        {
            this.alive = false;
        }


    }
}
