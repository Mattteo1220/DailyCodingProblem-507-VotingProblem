using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.Domain
{
    public class VoterIdEqualityComparer : IEqualityComparer<BallotDto>
    {
        public bool Equals(BallotDto x, BallotDto y)
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

        public int GetHashCode(BallotDto obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return obj.VoterId.GetHashCode();
        }
    }
}
