using System.Collections.Generic;
using System.Linq;
using Moq;
using SportsStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using SportStore.Domain.Abstract;
using System.Web.Mvc;
using SportStore.Domain.Concrete;

namespace SportsStore.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for NavControllerTest and is intended
    ///to contain all NavControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NavControllerTest
    {

        private TestContext testContextInstance;
        private Mock<IProductRepository> _mockProductRepository;
        private NavController _target;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductRepository.Setup(m => m.Products).Returns(new Product[]
                                                                     {
                                                                         new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                                                                         new Product {ProductID = 2, Name = "P2", Category = "Apples"},
                                                                         new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                                                                         new Product {ProductID = 4, Name = "P4", Category = "Oranges"},
                                                                     }.AsQueryable()
                );
            _target = new NavController(_mockProductRepository.Object);
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
        ///A test for Menu
        /// Ensure categories are distinct and in alphabetic order
        ///</summary>
        [TestMethod()]
        public void Can_Create_Categories()
        {
            //Action
            string[] results = ((IEnumerable<String>) _target.Menu().Model).ToArray();

            //Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], "Apples");
            Assert.AreEqual(results[1], "Oranges");
            Assert.AreEqual(results[2], "Plums");
        }

        /// <summary>
        ///A test for Menu
        /// Ensure selected category is passed in and out
        ///</summary>
        [TestMethod()]
        public void Indicates_Selected_Category()
        {
            //Arrange
            string selectedCategory = "Oranges";

            //Action
            string result = _target.Menu(selectedCategory).ViewBag.SelectedCategory;

            //Assert
            Assert.AreEqual(selectedCategory, result);
        }



    }
}
