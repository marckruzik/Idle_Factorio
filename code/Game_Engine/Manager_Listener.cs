using System;

namespace NS_Game_Engine
{
    public class Manager_Listener
    {
        /*
        Dictionary<Type, IGame_Listener> dico_typ_game_listener = new Dictionary<Type, IGame_Listener> ();

        public void add<T> (Func<T> func)
        {
            if (dico_typ_game_listener.ContainsKey (typeof(T)) == false)
            {
                dico_typ_game_listener.Add (typeof (T), new Game_Listener<T> ());
            }
            var x = dico_typ_game_listener[typeof (T)];
        }
        */

        Game_Listener<int> listener_int = new Game_Listener<int> ();
        Game_Listener<string> listener_string = new Game_Listener<string> ();
        Game_Listener<bool> listener_bool = new Game_Listener<bool> ();

        public void add<T> (Func<T> func)
        {
            Game_Listener<T> game_listener = get_listener<T> ();
            game_listener.add (func);
        }

        private Game_Listener<T> get_listener<T> ()
        {
            if (typeof (T) == typeof (int))
            {
                return this.listener_int as Game_Listener<T>;
            }
            else if (typeof (T) == typeof (string))
            {
                return this.listener_string as Game_Listener<T>;
            }
            else if (typeof (T) == typeof (bool))
            {
                return this.listener_bool as Game_Listener<T>;
            }
            throw new Exception ("unknown listener type : " + typeof(T).ToString ());
        }

        public bool need_update ()
        {
            bool need_update = false;
            need_update |= this.listener_int.need_update ();
            need_update |= this.listener_string.need_update ();
            need_update |= this.listener_bool.need_update ();
            return need_update;
        }

    }
}
