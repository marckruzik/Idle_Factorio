using System;
using System.Collections.Generic;
using System.Text;
using NS_Idle_Factorio_Basic;


namespace NS_Game_Engine
{

    public class Toggle
    {
        public enum Tog
        {
            blank,
            is_true,
            is_false,
            is_undefined,
            is_unknown
        }
        public Tog tog = Tog.is_undefined;

        public Toggle ()
        {
        }


        public Toggle (bool val)
        {
            this.tog = Toggle.from_bool_to_tog (val);
        }

        public static Tog from_bool_to_tog (bool val)
        {
            if (val == true)
            {
                return Tog.is_true;
            }
            else
            {
                return Tog.is_false;
            }
        }

        public bool is_true () => this.tog == Tog.is_true;
        public bool is_false () => this.tog == Tog.is_false;
        
    }



    public class Manager_Toggle : Dictionary<string, Toggle>
    {

        public bool is_true (string toggle_id)
            => get_tog (toggle_id) == Toggle.Tog.is_true;


        public bool is_false (string toggle_id)
            => get_tog (toggle_id) == Toggle.Tog.is_false;


        public void set (string toggle_id, bool val)
        {
            if (this.ContainsKey (toggle_id) == false)
            {
                Add (toggle_id, new Toggle ());
            }
            this[toggle_id].tog = Toggle.from_bool_to_tog (val);
        }

        public Toggle.Tog get_tog (string toggle_id)
        {
            if (this.ContainsKey (toggle_id) == false)
            {
                return Toggle.Tog.is_unknown;
            }
            return this[toggle_id].tog;
        }
    }
}
