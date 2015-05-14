using System.Web;
using MedtelligentMovies.Web;
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
            var unityServiceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
		}
	}
}