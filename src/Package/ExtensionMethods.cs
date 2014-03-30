using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChrisTaylor.TfsQuickTasks
{
   public static class ExtensionMethods
    {
       public static void AddRange<T>(this Collection<T> collection, IEnumerable<T> items)
       {
           foreach (T item in items)
               collection.Add(item);

       }
    }
}
