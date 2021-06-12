using System;
using System.Collections.Generic;
using System.Text;
using NS_Blazora_Basic;
using System.Linq;

namespace NS_Manager_Resource
{
    public class Manager_Resource
    {
        Dictionary<string, Resource_Stack> dico_resource_name_plus_resource_stack = new Dictionary<string, Resource_Stack> ();
        public int chest_size = 1;

        Dictionary<string, ObservableProperty<int>> dico_resource_name_plus_stock_quantity_max =
            new Dictionary<string, ObservableProperty<int>> ();

        public string id = "MR_unknown";

        public bool can_craft (Resource_Mix mix_component)
        {
            foreach (Resource_Stack component in mix_component.list_resource_stack)
            {
                if (from_resource_name_contain_resource_stack (component.resource_name) == false)
                {
                    return false;
                }
                int resource_quantity = from_resource_name_get_resource_quantity (component.resource_name);
                if (component.quantity > resource_quantity)
                {
                    return false;
                }
            }
            return true;
        }


        public Resource_Mix get_resource_mix ()
        {
            Resource_Mix resource_mix = new Resource_Mix ();
            resource_mix.list_resource_stack = dico_resource_name_plus_resource_stack.Values.ToList ();
            return resource_mix;
        }


        public void set_resource_mix (Resource_Mix resource_mix)
        {
            foreach (Resource_Stack resource_stack in resource_mix.list_resource_stack)
            {
                from_resource_name_and_resource_quantity_set_resource_quantity (
                    resource_stack.resource_name, resource_stack.quantity);
            }
        }


        public static void resource_transfer (
            Manager_Resource manager_resource_source,
            Manager_Resource manager_resource_destination,
            string resource_name,
            int quantity)
        {
            resource_transfer (
                manager_resource_source,
                manager_resource_destination,
                resource_name,
                resource_name,
                quantity);
        }


        public static void resource_transfer (
            Manager_Resource manager_resource_source,
            Manager_Resource manager_resource_destination,
            string resource_name_source,
            string resource_name_destination,
            int quantity)
        {
            quantity = Math.Min (
                manager_resource_source.from_resource_name_get_resource_quantity (resource_name_source),
                quantity);

            int destination_before = manager_resource_destination.from_resource_name_get_resource_quantity (
                resource_name_destination);
            manager_resource_destination.from_resource_name_and_resource_quantity_add_resource (
                resource_name_destination, quantity);
            int destination_after = manager_resource_destination.from_resource_name_get_resource_quantity (
                resource_name_destination);

            int change = destination_after - destination_before;

            manager_resource_source.from_resource_name_and_resource_quantity_add_resource (resource_name_source, -change);
        }


        public void from_list_resource_name_setup (List<string> list_resource_name)
        {
            clear ();

            foreach (string resource_name in list_resource_name)
            {
                from_resource_name_add_resource (resource_name);
                if (Resource.list_unique_resource_name.Contains (resource_name) == true)
                {
                    from_resource_name_and_resource_quantity_set_resource_quantity (resource_name, 1);
                }
            }
        }


        private void clear ()
        {
            this.dico_resource_name_plus_resource_stack.Clear ();
            this.dico_resource_name_plus_stock_quantity_max.Clear ();
        }


        public int from_resource_name_get_resource_quantity (string resource_name)
        {
            return from_resource_name_get_resource_stack (resource_name).quantity;
        }


        public void from_resource_name_add_resource (string resource_name)
        {
            Resource.from_resource_name_check_resource_name (resource_name);

            bool contain_resource = from_resource_name_contain_resource_stack (resource_name);
            if (contain_resource == true)
            {
                return;
            }
            Resource_Stack resource_stack = Resource_Stack.from_resource_name_create_resource_stack (resource_name);
            resource_stack.quantity = 0;
            this.dico_resource_name_plus_resource_stack.Add (resource_name, resource_stack);


            int max = Resource.dico_resource_name_plus_stack_resource_quantity_max[resource_name] * this.chest_size;
            this.dico_resource_name_plus_stock_quantity_max
                .Add (resource_name, new ObservableProperty<int> (this.id + ":" + resource_name, max));
        }


        Dictionary<string, Action<int>> dico_resource_name_plus_action = new Dictionary<string, Action<int>> ();

        public void listener_set (string resource_name)
        {
            Action<int> action = delegate (int v) 
            {
                this.dico_resource_name_plus_stock_quantity_max[resource_name]
                    .Set (v * this.chest_size);
                //Console.WriteLine ($"{resource_name} new : {v}");
            };
            dico_resource_name_plus_action.Add (resource_name, action);

            ObservableProperty<int> obs = Resource.dico_resource_name_plus_stack_resource_quantity_max[resource_name];
            obs.changed += action;

            //Console.WriteLine ($"Listener for Manager_Resource {this.id} with [{obs.id}]{resource_name}");
        }


        public void listener_unset (string resource_name)
        {
            Action<int> action = dico_resource_name_plus_action[resource_name];
            Resource.dico_resource_name_plus_stack_resource_quantity_max[resource_name].changed -= action;
            dico_resource_name_plus_action.Remove (resource_name);
        }



        public void listener_display ()
        {
            foreach (ObservableProperty<int> obs in dico_resource_name_plus_stock_quantity_max.Values)
            {
                Console.WriteLine (obs.id);
            }
        }




        public void listener_setup (List<string> list_resource_name)
        {
            foreach (string resource_name in list_resource_name)
            {
                listener_set (resource_name);
            }
        }


        public void listener_clear ()
        {
            List<ObservableProperty<int>> list_observable =
                this.dico_resource_name_plus_resource_stack
                .Values
                .Select (rs => rs.observable_quantity)
                .ToList ();
            foreach (ObservableProperty<int> observable in list_observable)
            {
                observable.changed = null;
            }

            foreach (ObservableProperty<int> observable in this.dico_resource_name_plus_stock_quantity_max.Values)
            {
                observable.changed = null;
            }

            List<string> list_resource_name = dico_resource_name_plus_action.Keys.ToList ();
            foreach (string resource_name in list_resource_name)
            {
                listener_unset (resource_name);
            }

        }




        public void from_resource_name_and_resource_quantity_add_resource (string resource_name, int resource_quantity)
        {
            bool contain_resource = from_resource_name_contain_resource_stack (resource_name);
            if (contain_resource == false)
            {
                from_resource_name_add_resource (resource_name);
            }

            int quantity_current = from_resource_name_get_resource_quantity (resource_name);
            int quantity_future = quantity_current + resource_quantity;
            from_resource_name_and_resource_quantity_set_resource_quantity (resource_name, quantity_future);
        }


        public void from_resource_name_and_resource_quantity_set_resource_quantity (string resource_name, int resource_quantity)
        {
            from_resource_name_add_resource (resource_name);

            Resource_Stack resource_stack = this.dico_resource_name_plus_resource_stack[resource_name];

            int stock_max = from_resource_name_get_stock_resource_quantity_max (resource_name);

            if (resource_quantity < 0)
            {
                resource_stack.quantity = 0;
            }
            else if (resource_quantity > stock_max)
            {
                resource_stack.quantity = stock_max;
            }
            else
            {
                resource_stack.quantity = resource_quantity;
            }
            resource_stack.observable_quantity.Set (resource_stack.quantity);
        }


        public ObservableProperty<int> from_resource_name_get_stock_resource_quantity_max (string resource_name)
        {
            return dico_resource_name_plus_stock_quantity_max[resource_name];
        }




        public void from_resource_mix_add_resource (Resource_Mix resource_mix)
        {
            foreach (Resource_Stack resource_stack in resource_mix.list_resource_stack)
            {
                from_resource_name_and_resource_quantity_add_resource (resource_stack.resource_name, resource_stack.quantity);
            }
        }


        public void from_resource_mix_remove_resource (Resource_Mix resource_mix)
        {
            foreach (Resource_Stack resource_stack in resource_mix.list_resource_stack)
            {
                int quantity_negative = -resource_stack.quantity;
                from_resource_name_and_resource_quantity_add_resource (resource_stack.resource_name, quantity_negative);
            }
        }


        public Resource_Stack from_resource_name_get_resource_stack (string resource_name)
        {
            Resource_Stack resource_stack = this.dico_resource_name_plus_resource_stack[resource_name];
            return resource_stack;
        }


        public bool from_resource_name_contain_resource_stack (string resource_name)
        {
            return this.dico_resource_name_plus_resource_stack.ContainsKey (resource_name);
        }


    }
}
