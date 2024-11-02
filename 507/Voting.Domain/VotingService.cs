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

            var files = Directory.GetFiles(filePath);
            if (!files.Any(f => f.Contains(FileNameConst)))
            {
                _logger.Info($"No files to read.");
            }

            var records = new HashSet<VoterRecord>(new VoterIdEqualityComparer());

            foreach(var file in files)
            {
                using (var fs = File.OpenRead(file))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        var recordData = string.Empty;
                        while((recordData = reader.ReadLine()) != null)
                        {
                            if (recordData == null)
                            {
                                _logger.Error($"No data in file to process.");
                                return;
                            }
                            var voterId = recordData.Split('|')[0];
                            var candidateId = recordData.Split('|')[1];
                            if (!records.Add(new VoterRecord(voterId, candidateId)))
                            {
                                _logger.Error($"FRAUD: {voterId} was present twice in this file. Voting more than once is legally forbidden.");
                                //potentially add to a db for fraud team to investigate
                            }
                        }
                    }
                }
            } 

            _logger.Info($"{nameof(VotingService)}::{nameof(Run)} ending...");
        }


        private string GetFilePath()
        {
            return $"c:/src/projects/DailyCodingProblems/507/";
        }
    }
}
