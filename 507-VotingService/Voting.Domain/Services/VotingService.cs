using GenericParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Common;
using Voting.Domain.Interfaces;

namespace Voting.Domain
{
    public class VotingService : IVotingService
    {
        private readonly IDiagnosticLogger _logger;
        private readonly IBallotProcessor _ballotProcessor;
        private readonly ITabulationService _tabulationService;

        public VotingService(IBallotProcessor ballotProcessor, ITabulationService tabulationService, IDiagnosticLogger logger)
        {
            _ballotProcessor = ballotProcessor ?? throw new ArgumentNullException(nameof(ballotProcessor));
            _tabulationService = tabulationService ?? throw new ArgumentNullException(nameof(tabulationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Run()
        {
            _logger.Info($"{nameof(VotingService)}::{nameof(Run)} starting...");

            var filePath = _ballotProcessor.GetFilePath();

            var ballotFiles= _ballotProcessor.GetFiles(filePath);

            var ballots = _ballotProcessor.ProcessBallotFiles(ballotFiles);

            _tabulationService.TabulateVotes(ballots);

            _logger.Info($"{nameof(VotingService)}::{nameof(Run)} ending...");
        }
    }
}
