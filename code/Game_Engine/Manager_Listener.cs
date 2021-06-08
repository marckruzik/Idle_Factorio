using System;
using NS_Blazora_Basic;
using System.Collections.Generic;

namespace NS_Game_Engine
{
    public class Manager_Listener
    {
        Dictionary<Type, IGame_Listener> dico_typ_plus_game_listener = new Dictionary<Type, IGame_Listener> ();

        public void add<T> (Func<T> func)
        {
            Game_Listener<T> game_listener = get_listener<T> ();
            game_listener.add (func);
        }

        private Game_Listener<T> get_listener<T> ()
        {
            if (dico_typ_plus_game_listener.ContainsKey (typeof(T)) == false)
            {
                dico_typ_plus_game_listener.Add (typeof(T), new Game_Listener<T> ());
            }
            return dico_typ_plus_game_listener[typeof(T)] as Game_Listener<T>;
        }

        public bool need_update ()
        {
            bool need_update = false;
            foreach (IGame_Listener game_listener in dico_typ_plus_game_listener.Values)
            {
                need_update |= game_listener.need_update ();
            }
            return need_update;
        }

    }
}
