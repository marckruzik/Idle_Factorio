using NS_Blazora_Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazora.Script
{
    public interface IListener_Data
    {
        Type typ { get; set; }
    }


    public class Listener_Data<T> : IListener_Data
    {
        public Type typ { get; set; }
        public ObservableProperty<T> observable;
        public Action<T> action;

        public Listener_Data (Type typ, ObservableProperty<T> observable, Action<T> action)
        {
            this.typ = typ;
            this.observable = observable;
            this.action = action;
        }
    }
}
