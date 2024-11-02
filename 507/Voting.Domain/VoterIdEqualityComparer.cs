using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.Domain
{
    public class VoterIdEqualityComparer : IEqualityComparer<VoterRecord>
    {
        public bool Equals(VoterRecord x, VoterRecord y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            if (x.VoterId == y.VoterId)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(VoterRecord obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return obj.VoterId.GetHashCode();
        }
    }
}
