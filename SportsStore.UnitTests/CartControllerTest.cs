using System.Linq;
using Moq;
using SportStore.Domain.Concrete;
using SportStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using SportStore.Domain.Abstract;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CartControllerTest and is intended
    ///to contain all CartControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CartControllerTest
    {


        private TestContext testContextInstance;


        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IOrderProcessor> _mockOrderProcessor;
        private CartController _target;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductRepository.Setup(m => m.Products).Returns(new Product[]
                                                                     {
                                                                         new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                                                                     }.AsQueryable()
                );

            //Required by constructor - used in checkout processing
            _mockOrderProcessor = new Mock<IOrderProcessor>();
            _target = new CartController(_mockProductRepository.Object, _mockOrderProcessor.Object);
        }




        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddToCart controller.
        /// Note - Cart business model itself has all tests for all its methods, 
        ///         we are testing the controller is interfacting with cart correctly
        ///</summary>
        [TestMethod()]
        public void Can_Add_To_Cart()
        {
            //Arrange 
            Cart cart = new Cart();

            //Act
            _target.AddToCart(cart,1,null);

            //Assert
            Assert.AreEqual(1, cart.Lines.Count);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }


        /// <summary>
        ///A test for AddToCart controller.
        /// Note - Cart business model itself has all tests for all its methods, 
        ///         we are testing the controller is interfacting with cart correctly
        ///</summary>
        [TestMethod()]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            //Arrange 
            Cart cart = new Cart();

            //Act
            var result = _target.AddToCart(cart, 2, "myUrl");

            //Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("myUrl", result.RouteValues["returnUrl"]);
        }


        /// <summary>
        ///A test for Index
        /// Ensures the view model is generated and passed through to the view and back
        ///</summary>
        [TestMethod()]
        public void Can_View_Cart_Contents()
        {
            //Arrange
            Cart cart = new Cart();

            //Act
            CartIndexViewModel result = (CartIndexViewModel) _target.Index(cart, "myUrl").ViewData.Model;

            //Assert
            Assert.AreSame(cart,result.Cart);
            Assert.AreEqual("myUrl",result.ReturnUrl);
        }

        /// <summary>
        ///A test for Checkout - when cart is empty
        ///</summary>
        [TestMethod()]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange
            Cart cart = new Cart(); //Empty Cart
            ShippingDetails shippingDetails = new ShippingDetails(); 

            //Act
            ViewResult actual = _target.Checkout(cart,shippingDetails) as ViewResult;

            //Assert
            Assert.IsNotNull(actual, "Has not returned a view result");

            //Should be redirected back to default view
            Assert.AreEqual(String.Empty,actual.ViewName);

            //Should be invalid state
            Assert.AreEqual(false,actual.ViewData.ModelState.IsValid);
            
            //Ensure the proessing is never triggered
            _mockOrderProcessor.Verify(m => m.ProcessOrder(It.IsAny<Cart>(),It.IsAny<ShippingDetails>()),Times.Never());

        }

        /// <summary>
        ///A test for Checkout - when cart is valid - but checkout details are not
        ///</summary>
        [TestMethod()]
        public void Cannot_Checkout_Invalid_Shipping_Details()
        {
            //Arrange
            Cart cart = new Cart(); //Empty Cart
            cart.AddItem(new Product(), 1);

            ShippingDetails shippingDetails = new ShippingDetails();

            //Arrange invalid model state (i.e. simulate error caused by checkout model being invalid)
            _target.ModelState.AddModelError("Error","MockValidationFailureMessage");

            //Act
            ViewResult actual = _target.Checkout(cart, shippingDetails) as ViewResult;

            //Assert
            Assert.IsNotNull(actual, "Has not returned a view result");

            //Should be redirected back to default view
            Assert.AreEqual(String.Empty, actual.ViewName);

            //Should be invalid state
            Assert.AreEqual(false, actual.ViewData.ModelState.IsValid);

            //Ensure the proessing is never triggered
            _mockOrderProcessor.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());

        }

        /// <summary>
        ///A test for Checkout - when cart and checkout details are is valid
        /// Note: ModelState is not invalidated here as that is performed via model binding with view on post prior to this call 
        ///</summary>
        [TestMethod()]
        public void Can_Checkout_Vvalid_Shipping_Details()
        {
            //Arrange
            Cart cart = new Cart(); //Empty Cart
            cart.AddItem(new Product(), 1);

            ShippingDetails shippingDetails = new ShippingDetails();
            shippingDetails.Name = "TestName";

            //Act
            RedirectToRouteResult actual = _target.Checkout(cart, shippingDetails) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(actual, "Has not returned a redirect result");

            //Should be redirected back to default view
            Assert.AreEqual("ShowAsCompleted", actual.RouteValues["action"]);
            Assert.AreEqual("TestName", actual.RouteValues["shippedToName"]);

            //Ensure the proessing is never triggered
            _mockOrderProcessor.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());

        }


    }
}
