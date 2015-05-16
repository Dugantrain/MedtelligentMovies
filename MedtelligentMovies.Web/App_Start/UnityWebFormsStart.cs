using System.Data.Entity;
using System.Web;
using MedtelligentMovies.Common.DAL.DbContexts;
using MedtelligentMovies.Common.DAL.Initializers;
using MedtelligentMovies.Common.Services;
using MedtelligentMovies.Web;
using MedtelligentMovies.Web.UserContext;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Unity.WebForms;

[assembly: WebActivator.PostApplicationStartMethod( typeof(UnityWebFormsStart), "PostStart" )]
namespace MedtelligentMovies.Web
{
	/// <summary>
	///		Startup class for the Unity.WebForms NuGet package.
	/// </summary>
	internal static class UnityWebFormsStart
	{
		/// <summary>
		///     Initializes the unity container when the application starts up.
		/// </summary>
		/// <remarks>
		///		Do not edit this method. Perform any modifications in the
		///		<see cref="RegisterDependencies" /> method.
		/// </remarks>
		internal static void PostStart()
		{
			IUnityContainer container = new UnityContainer();
			HttpContext.Current.Application.SetContainer( container );

			RegisterDependencies( container );
		}

		/// <summary>
		///		Registers dependencies in the supplied container.
		/// </summary>
		/// <param name="container">Instance of the container to populate.</param>
		private static void RegisterDependencies( IUnityContainer container )
		{
            container.RegisterTypes(
               AllClasses.FromLoadedAssemblies(),
               WithMappings.FromMatchingInterface,
               WithName.Default,
               WithLifetime.Hierarchical);
            UnityConfig.RegisterComponents(container);
		    container.RegisterType<IUserContextService, HttpUserContextService>();
            var unityServiceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);

		    var contextService = ServiceLocator.Current.GetInstance<IUserContextService>();
            var context = new MedtelligentMovieDbContext(contextService);
            Database.SetInitializer(ServiceLocator.Current.GetInstance<MedtelligentMovieDbInitializer>());
            context.Database.Initialize(false);
		}
	}
}