using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.Domain
{
    public record CandidateDto
    {
        public CandidateDto(string candidateId)
        {
            CandidateId = candidateId;
        }

        public CandidateDto(string candidateId, int votes)
        {
            CandidateId = candidateId;
            VoteTally = votes;
        }

        public string CandidateId { get; private set; }
        public int VoteTally { get; private set; }

        public void AddVote() => VoteTally++;


    }
}
