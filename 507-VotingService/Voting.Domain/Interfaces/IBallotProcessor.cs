using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.Domain.Interfaces
{
    public interface IBallotProcessor
    {
        public List<string> GetFiles(string filePath);
        public string GetFilePath();
        public HashSet<BallotDto> ProcessBallotFile(string file);
        public HashSet<BallotDto> ProcessBallotFiles(IEnumerable<string> files);
    }
}
