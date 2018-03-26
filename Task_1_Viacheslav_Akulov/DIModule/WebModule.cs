using Autofac;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;

namespace Task1ViacheslavAkulov.DiModule
{
	public class WebModule : Module
	{
		protected override void Load(ContainerBuilder containerBuilder)
		{
			containerBuilder.RegisterType<GameService>().As<IGameService>();

			containerBuilder.RegisterType<CommentService>().As<ICommentService>();

		}
	}
}
