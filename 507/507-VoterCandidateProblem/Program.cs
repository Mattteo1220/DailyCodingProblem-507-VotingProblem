// See https://aka.ms/new-console-template for more information


using Unity;
using Voting.Domain;
using Voting.Common;

var unityContainer = new UnityContainer();
unityContainer.RegisterSingleton<IDiagnosticLogger, DiagnosticLogger>();
unityContainer.RegisterSingleton<IVotingService, Voting.Domain.VotingService>();
var votingService = unityContainer.Resolve<Voting.Domain.IVotingService>();

votingService.Run();
