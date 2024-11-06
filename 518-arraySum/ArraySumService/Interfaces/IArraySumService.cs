using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArraySum.Domain.Interfaces
{
    public interface IArraySumService
    {
        bool ArrayContainsSumOfK(int k, int[] array);
    }
}
