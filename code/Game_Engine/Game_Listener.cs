using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NS_Game_Engine
{
    public interface IGame_Listener
    {
        bool need_update ();
    }


    public class Game_Listener<T> : IGame_Listener
    {
        List<KeyValuePair<Func<T>, T>> list_kvp_func_val = new List<KeyValuePair<Func<T>, T>> ();

        public void add (Func<T> func)
        {
            if (func != null)
            {
                list_kvp_func_val.Add (new KeyValuePair<Func<T>, T> (func, default(T)));
            }
        }


        public bool need_update ()
        {
            for (int i = 0; i < this.list_kvp_func_val.Count; i++)
            {
                KeyValuePair<Func<T>, T> kvp_func_val = list_kvp_func_val[i];

                Func<T> func = kvp_func_val.Key;
                T func_val = func ();
                T val = kvp_func_val.Value;
                if (func_val.Equals (val) == false)
                {
                    list_kvp_func_val[i] = new KeyValuePair<Func<T>, T> (func, func_val);
                    return true;
                }
            }
            return false;
        }

    }
}
