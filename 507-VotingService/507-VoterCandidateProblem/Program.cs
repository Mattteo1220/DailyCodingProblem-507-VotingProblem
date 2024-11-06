// See https://aka.ms/new-console-template for more information


using Unity;
using Voting.Domain;
using Voting.Common;
using Voting.Domain.Services;
using Voting.Domain.Interfaces;
using Unity.Lifetime;

var unityContainer = new UnityContainer();
unityContainer.RegisterSingleton<IDiagnosticLogger, DiagnosticLogger>();
unityContainer.RegisterSingleton<IBallotProcessor, BallotProcessor>();
unityContainer.RegisterSingleton<ITabulationService, TabulationService>();
unityContainer.RegisterSingleton<IVotingService, Voting.Domain.VotingService>();
var votingService = unityContainer.Resolve<Voting.Domain.IVotingService>();

votingService.Run();
