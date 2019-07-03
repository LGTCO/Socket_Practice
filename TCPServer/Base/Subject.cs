using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Base
{
    internal class Subject<T> where T:Observer
    {

        protected virtual void Attach(T t) => _observers.Add(t);
        protected virtual void Dettach(T t) => _observers.Remove(t);
        protected virtual void Notify() => _observers.ForEach(o => o.Update());

        protected List<T> _observers = new List<T>();
    }
}
