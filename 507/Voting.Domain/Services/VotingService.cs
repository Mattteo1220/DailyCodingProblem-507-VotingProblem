using GenericParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Common;

namespace Voting.Domain
{
    public class VotingService : IVotingService
    {
        private IDiagnosticLogger _logger;
        private const string FileNameConst = "VoterRecord";

        public VotingService(IDiagnosticLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Run()
        {
            _logger.Info($"{nameof(VotingService)}::{nameof(Run)} starting...");

            var filePath = GetFilePath();

            var files = GetFiles(filePath);

            var ballots = ProcessFiles(files);

            TabulateVotes(ballots);

            _logger.Info($"{nameof(VotingService)}::{nameof(Run)} ending...");
        }

        private void TabulateVotes(IEnumerable<BallotDto> ballots)
        {
            var candidates = ballots.GroupBy(x => x.CandidateId).Select(x => new CandidateDto(x.Key, x.Count()));

            var candidatesByVotes = candidates
                .OrderByDescending(x => x.VoteTally)
                .Take(3)
                .ToList();

            foreach(var candidate in candidatesByVotes)
            {
                _logger.Info($"{candidate.CandidateId} has {candidate.VoteTally} votes");
            }

            _logger.Info($"Candidate: {candidatesByVotes.First().CandidateId} is the winner with {candidatesByVotes.First().VoteTally} votes");
        }

        private HashSet<BallotDto> ProcessFiles(List<string> files)
        {
            var records = new HashSet<BallotDto>(new VoterIdEqualityComparer());
            foreach (var file in files)
            {
                var uniqueRecords = ProcessFile(file);
                foreach(var record in uniqueRecords)
                {
                    if (!records.Add(record))
                    {
                        _logger.Info($"FRAUD: Voter: {record.VoterId} is present across multiple files.");
                    };
                }
            }

            return records;
        }

        private HashSet<BallotDto> ProcessFile(string file)
        {
            var records = new HashSet<BallotDto>(new VoterIdEqualityComparer());
            using (var fs = File.OpenRead(file))
            {
                using (var reader = new StreamReader(fs))
                {
                    var recordData = string.Empty;
                    while ((recordData = reader.ReadLine()) != null)
                    {
                        if (recordData == null)
                        {
                            _logger.Error($"No data in file to process.");
                            return new HashSet<BallotDto>();
                        }
                        var voterId = recordData.Split('|')[0];
                        var candidateId = recordData.Split('|')[1];
                        if (!records.Add(new BallotDto(voterId, candidateId)))
                        {
                            _logger.Error($"FRAUD: {voterId} was present more than once in file {file}. Voting more than once is legally forbidden.");
                            //potentially add to a db for fraud team to investigate
                        }
                    }
                }
            }
            return records;
        }

        private List<string> GetFiles(string filePath)
        {
            var files = Directory.GetFiles(filePath);
            if (!files.Any(f => f.Contains(FileNameConst)))
            {
                _logger.Info($"No files to read.");
            }

            return files.ToList();
        }
        private string GetFilePath()
        {
            return $"c:/src/projects/DailyCodingProblems/507/";
        }
    }
}
