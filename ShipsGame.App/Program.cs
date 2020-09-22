using Autofac;
using ShipGame.UI;
using ShipsGame.App.businessLogic;

namespace ShipsGame.App
{
    internal class Program
    {
        private static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<GameLogic>().As<IGameLogic>();
            builder.RegisterType<GameMachine>().As<IGameMachine>();
            builder.RegisterType<ConsoleUI>().As<IUserInterface>();

            return builder.Build();
        }

        private static void Main()
        {
            using (var scope = Configure().BeginLifetimeScope())
            {
                scope.Resolve<IGameMachine>().RunGameTrigger();
            }
        }
    }
}