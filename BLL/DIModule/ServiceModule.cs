using Autofac;
using DataAccessLayer.Interfases;
using DataAccessLayer.Repository;
using Module = System.Reflection.Module;

namespace BusinessLogicLayer.DiModule
{
	/// <summary>
	/// Autofac service module
	/// </summary>
	public class ServiceModule : Autofac.Module
	{
		private readonly string _connection;

		public ServiceModule(string connection)
		{
			this._connection = connection;
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().WithParameter("connection", this._connection);
		}

	}
}
