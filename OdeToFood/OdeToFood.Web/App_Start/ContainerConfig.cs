using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using OdeToFood.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;

namespace OdeToFood.Web
{
    public class ContainerConfig
    {
        internal static void RegisterContainer(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            //sets RegisterControllers to scan our MvcApplication Assembly to find and register all our controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);
            //Tells our builder to know about the InMemeryRestaurtantData and use it when someone needs something that implemenets IRestaurantData
            builder.RegisterType<SqlRestaurantData>()
                .As<IRestaurantData>()
                .InstancePerRequest();
            builder.RegisterType<OdeToFoodDbContext>().InstancePerRequest();

            //AutoFac now knows about that type, and use that type whenever somebody needs something about IRestaurantData, this also means that down the line
            //we can easily switch the reference to our database so that it knows how to handle the data we will be passing.
            //To begin with, we want a singleinstance because when we start adding data in, we want it all to be inside of one Object

            var container = builder.Build();
            //Then we create out actual container using the Build method from Autofac on our ContainerBuilder

            //This uses MVC's Dependency resolver to set itself to our AutofacDependencyResolver and passes in our Container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //Passes in a our instance of container as Autofacs Web API Dependency Resolver for use in our API.

            //This will now work for all future API Controllers.
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}