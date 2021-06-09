using System;

// get an event when a property changes
namespace NS_Blazora_Basic
{
    public interface IObservableProperty
    {
    }


    public class ObservableProperty<T> : IObservableProperty
    {
        static int id_count = 0;
        public string id;
        T value;

        public Action<T> changed;


        public ObservableProperty (T initialValue)
        {
            value = initialValue;
            this.id = id_count.ToString ();
            id_count++;
        }
        public ObservableProperty (string id, T initialValue) : this (initialValue)
        {
            this.id = id + ":" + this.id;
        }


        public void Set (T v)
        {
            if (!v.Equals (value))
            {
                value = v;
                if (changed != null)
                {
                    changed (value);
                }
            }
        }

        public void fastset (T v)
        {
            value = v;  
        }


        public T Get ()
        {
            return value;
        }

        public static implicit operator T (ObservableProperty<T> p)
        {
            return p.value;
        }

        public T Value { get { return Get (); } }

        public int get_listener_count ()
        {
            if (changed == null)
            {
                return 0;
            }
            return changed.GetInvocationList ().Length;
        }
    }
}