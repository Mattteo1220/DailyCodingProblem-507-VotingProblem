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
        private HashSet<VoterRecord> _records;

        public VotingService(IDiagnosticLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Run()
        {
            _logger.Info($"{nameof(VotingService)}::{nameof(Run)} starting...");

            var filePath = GetFilePath();

            var files = GetFiles(filePath);

            _records = new HashSet<VoterRecord>(new VoterIdEqualityComparer());

            foreach(var file in files)
            {
               ProcessFile(file);
            } 

            TabulateVotes();

            _logger.Info($"{nameof(VotingService)}::{nameof(Run)} ending...");
        }

        private void TabulateVotes()
        {
            var candidatesByVotes = _records.GroupBy(x => x.CandidateId)
                .OrderByDescending(x => x.Select(x => x.VoterId).Count())
                .Take(3)
                .ToList();

            foreach(var candidate in candidatesByVotes)
            {
                var numOfCounts = candidate.Where(s => s.CandidateId == candidate.Key).Select(x => x.VoterId).Count();

                _logger.Info($"{candidate.Key} has {numOfCounts} votes");
            }

            _logger.Info($"Candidate: {candidatesByVotes.First().Key} is the winner with {candidatesByVotes.First().Count()} votes");
        }

        private void ProcessFile(string file)
        {
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
                            return;
                        }
                        var voterId = recordData.Split('|')[0];
                        var candidateId = recordData.Split('|')[1];
                        if (!_records.Add(new VoterRecord(voterId, candidateId)))
                        {
                            _logger.Error($"FRAUD: {voterId} was present twice in this file. Voting more than once is legally forbidden.");
                            //potentially add to a db for fraud team to investigate
                        }
                    }
                }
            }
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
