using System;

namespace NS_Manager_Resource
{
    public class Resource
    {
        public string name;
        public int amount = 0;
        public int max = 16;

        public static Resource from_resource_name_get_resource (string resource_name)
        {
            Resource resource = new Resource ();
            resource.name = resource_name;
            return resource;
        }

        public void increment ()
        {
            if (amount_max_reached () == true)
            {
                return;
            }

            this.amount += 1;
        }


        public bool amount_max_reached ()
        {
            return this.amount >= this.max;
        }

    }
}
