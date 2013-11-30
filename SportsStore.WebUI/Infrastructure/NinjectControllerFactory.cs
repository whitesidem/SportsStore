using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using SportStore.Domain.Abstract;
using SportStore.Domain.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType==null? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            //Mock<IProductRepository> mockProdRepository = new Mock<IProductRepository>();
            //mockProdRepository.Setup(m => m.Products).Returns(new List<Product>
            //                                                      {
            //                                                          new Product(){ Name="Football", Price = 25},
            //                                                          new Product(){ Name="Surf Board", Price = 179},
            //                                                          new Product(){ Name="Running Shoes", Price = 95},
            //                                                     }.AsQueryable());
            //ninjectKernel.Bind<IProductRepository>().ToConstant(mockProdRepository.Object);
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();

            //Instantiate settings as standard
            EmailSettings emailSettings = new EmailSettings();
            //Then override defaults with any config reqs
            //Get config value for this property - true means write to file instead of sending email!
            emailSettings.WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"]);

            ninjectKernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("emailSettingsParam",
                                                                                                    emailSettings);


        }
    }
}