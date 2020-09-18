using Autofac;
using ShipGame.UI;
using ShipsGame.App.buisnesLogic;

namespace ShipsGame.App
{
    internal class Program
    {
        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Game>().As<IGame>();
            builder.RegisterType<GameLogic>().As<IGameLogic>();
            builder.RegisterType<GameMachine>().As<IGameMachine>();
            builder.RegisterType<ConsoleUI>().As<IUserInterface>();

            return builder.Build();
        }

        private static void Main()
        {
            using (var scope = Configure().BeginLifetimeScope())
            {
                var game = scope.Resolve<IGame>();
                game.Run();
            }
        }
    }
}