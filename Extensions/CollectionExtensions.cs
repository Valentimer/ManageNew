using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerFamily.Extensions
{
    public static class CollectionExtensions
    {
        public static void Replace<T>(this IList<T> data, T itemToRemove, T itemToAdd)
        {
            int index = data.IndexOf(itemToRemove);
            if(index == -1)
            {
                throw new InvalidOperationException("item to remove not found");
            }
            data[index] = itemToAdd;
        }
    }
}
