using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_yield
{
    class Class1 : IEnumerable
    {
        private int[] array = new int[10];
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < 8)
                {
                    yield return array[i];
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}
