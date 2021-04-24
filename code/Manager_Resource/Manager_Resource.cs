using System;
using System.Collections.Generic;
using System.Text;

namespace NS_Manager_Resource
{
    public class Manager_Resource
    {

        Dictionary<string, Resource> dico_resource_name_and_resource = new Dictionary<string, Resource> ();

        public Manager_Resource ()
        {
        }


        public void from_resource_name_add_resource (string resource_name)
        {
            bool contain_resource = from_resource_name_contain_resource (resource_name);
            if (contain_resource == true)
            {
                return;
            }
            Resource resource = Resource.from_resource_name_get_resource (resource_name);
            dico_resource_name_and_resource.Add (resource_name, resource);
        }

        public Resource from_resource_name_get_resource (string resource_name)
        {
            Resource resource = dico_resource_name_and_resource[resource_name];
            return resource;
        }

        public bool from_resource_name_contain_resource (string resource_name)
        {
            return dico_resource_name_and_resource.ContainsKey (resource_name);
        }

    }
}
