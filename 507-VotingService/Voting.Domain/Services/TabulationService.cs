using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Common;
using Voting.Domain.Interfaces;

namespace Voting.Domain.Services
{
    public class TabulationService : ITabulationService
    {
        private readonly IDiagnosticLogger _logger;
        public TabulationService(IDiagnosticLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void TabulateVotes(IEnumerable<BallotDto> ballots)
        {
            var candidates = ballots.GroupBy(x => x.CandidateId).Select(x => new CandidateDto(x.Key, x.Count()));

            var candidatesByVotes = candidates
                .OrderByDescending(x => x.VoteTally)
                .Take(3)
                .ToList();

            foreach (var candidate in candidatesByVotes)
            {
                _logger.Info($"{candidate.CandidateId} has {candidate.VoteTally} votes");
            }

            _logger.Info($"Candidate: {candidatesByVotes.First().CandidateId} is the winner with {candidatesByVotes.First().VoteTally} votes");
        }
    }
}
