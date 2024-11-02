using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.Domain
{
    public class Candidate
    {
        public Candidate(string candidateId)
        {
            CandidateId = candidateId;
        }
        public string CandidateId { get; private set; }
        public int VoteTally { get; private set; }

        public void AddVote() => VoteTally++;


    }
}
