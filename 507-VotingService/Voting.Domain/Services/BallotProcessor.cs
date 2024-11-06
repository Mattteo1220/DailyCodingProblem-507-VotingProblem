using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.Common;
using Voting.Domain.Interfaces;

namespace Voting.Domain.Services
{
    public class BallotProcessor : IBallotProcessor
    {
        private const string FileName = "VoterRecord";
        private IDiagnosticLogger _logger;
        public BallotProcessor(IDiagnosticLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public string GetFilePath()
        {
            return $"c:/src/projects/DailyCodingProblems/507-VotingService/";
        }

        public List<string> GetFiles(string filePath)
        {
            var files = Directory.GetFiles(filePath);
            if (!files.Any(f => f.Contains(FileName)))
            {
                _logger.Info($"No files to read.");
            }

            return files.ToList();
        }

        public HashSet<BallotDto> ProcessBallotFile(string file)
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

        public HashSet<BallotDto> ProcessBallotFiles(IEnumerable<string> files)
        {
            var records = new HashSet<BallotDto>(new VoterIdEqualityComparer());
            foreach (var file in files)
            {
                var uniqueRecords = ProcessBallotFile(file);
                foreach (var record in uniqueRecords)
                {
                    if (!records.Add(record))
                    {
                        _logger.Info($"FRAUD: Voter: {record.VoterId} is present across multiple files.");
                    };
                }
            }

            return records;
        }
    }
}
