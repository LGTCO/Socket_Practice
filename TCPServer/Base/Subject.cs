using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer.Base
{
    internal class Subject<T> where T : Observer
    {

        public virtual void Attach(T t) => _observers.Add(t);
        public virtual void Dettach(T t) => _observers.Remove(t);
        public virtual void Notify(string endPoint) => _observers.ForEach(o => o.Update(endPoint));

        protected List<T> _observers = new List<T>();
    }
}
