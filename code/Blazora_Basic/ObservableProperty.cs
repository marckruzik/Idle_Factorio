using System;

// get an event when a property changes
namespace NS_Blazora_Basic
{
    public class ObservableProperty<T>
    {
        T value;

        public Action<T> changed;

        public ObservableProperty (T initialValue)
        {
            value = initialValue;
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
    }
}