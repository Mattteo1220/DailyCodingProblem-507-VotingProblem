using ArraySum.Domain.Interfaces;

namespace ArraySumService
{
    public class ArraySumService : IArraySumService
    {
        public bool ArrayContainsSumOfK(int k, int[] array)
        {
            var result = false;

            if (array.Contains(k))
            {
                result = true;
            }

            if (!result)
            {
                for (var i = 0; i < array.Length; i++)
                {
                    if (result)
                    {
                        break;
                    }
                    if (!result)
                    {
                        result = FindSumByTwo(k, i, array);
                    }

                    if (!result)
                    {
                        result = FindSumByThree(k, i, array);
                    }


                }
            }


            return result; 
        }

        private bool FindSumByThree(int k, int i, int[] array)
        {
            var result = false;

            foreach(var item in array)
            {
                if (result)
                {
                    break;
                }

                if(array[i] == item)
                {
                    continue;
                }
                var first = array[i];
                var second = item;
                var third = array[Array.IndexOf(array, item) + 1];
                if (first + second + third == k)
                {
                    result = true;
                }
            }

            return result;
        }

        private bool FindSumByTwo(int k, int i, int[] array)
        {
            var result = false;
            foreach (var item in array)
            {
                if (result)
                {
                    break;
                }
                if (array[i] == item)
                {
                    continue;
                }

                if (array[i] + item == k)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}