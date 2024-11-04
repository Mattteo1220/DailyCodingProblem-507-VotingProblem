using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.Domain
{
    public record BallotDto
    {
        public BallotDto(string voterId, string candidateId)
        {
            VoterId = voterId;
            CandidateId = candidateId;
        }
        public string VoterId { get; private set; } 
        public string CandidateId { get; private set; }
    }
}
