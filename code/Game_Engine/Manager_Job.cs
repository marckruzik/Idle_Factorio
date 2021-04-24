using System;
using System.Collections.Generic;
using System.Text;

namespace NS_Game_Engine
{
    public class Manager_Job
    {
        public List<Job> list_job = new List<Job> ();

        public long clock;


        public void from_action_and_time_total_add_job (Action action, int time_total)
        {
            long date_start = this.clock;
            Job job = Job.from_action_and_date_start_and_time_total_get_job (action, date_start, time_total);

            this.list_job.Add (job);
        }




        public void update_logical ()
        {
            foreach (Job job in this.list_job)
            {
                job.update_logical ();
            }

            from_list_job_remove_job_dead ();
        }


        private void from_list_job_remove_job_dead ()
        {
            int i = 0;
            while (i < this.list_job.Count)
            {
                Job job = this.list_job[i];
                if (job.alive == false)
                {
                    this.list_job.RemoveAt (i);
                    continue;
                }
                i++;
            }
        }

    }
}
