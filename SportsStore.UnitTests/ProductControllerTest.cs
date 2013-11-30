using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;
using SportStore.Domain.Concrete;
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
    ///This is a test class for ProductControllerTest and is intended
    ///to contain all ProductControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProductControllerTest
    {

        private TestContext testContextInstance;
        private Mock<IProductRepository> _mockProductRepository;
        private ProductController _target;

        [TestInitialize()]
        public void MyTestInitialize()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockProductRepository.Setup(m => m.Products).Returns(new Product[]
                                                                     {
                                                                         new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                                                                         new Product {ProductID = 2, Name = "P2", Category = "Cat2"},
                                                                         new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                                                                         new Product {ProductID = 4, Name = "P4", Category = "Cat2"},
                                                                         new Product {ProductID = 5, Name = "P5", Category = "Cat3"}
                                                                     }.AsQueryable()
                );
            _target = new ProductController(_mockProductRepository.Object);
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
        ///A test for ProductController Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Sources\\WebApplications\\SportsStore\\SportsStore.WebUI", "/")]
        [UrlToTest("http://localhost:51838/")]
        public void ProductControllerConstructorTest()
        {
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>(); 
            ProductController target = new ProductController(mockProductRepository.Object);
            Assert.IsInstanceOfType(target, typeof(ProductController),"Not expected type constructed");
        }

        /// <summary>
        ///A test for List
        ///</summary>
        [TestMethod()]
        public void Can_Paginate()
        {
            _target.PageSize = 3;
            int currPage = 2; 

            //Action 
            var result = (ProductListViewModel)_target.List(null,currPage).Model; 

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length==2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        /// <summary>
        ///A test for List
        ///</summary>
        [TestMethod()]
        public void Can_Send_Paginate_Info()
        {
            //Arrange
            _target.PageSize = 3;
            int currPage = 2;

            //Action 
            var result = (ProductListViewModel)_target.List(null,currPage).Model;

            //Assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, currPage);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }

        /// <summary>
        ///A test for List
        ///</summary>
        [TestMethod()]
        public void Can_Filter_Products_By_Category()
        {
            _target.PageSize = 3;
            int currPage = 1;

            //Action 
            var result = (ProductListViewModel)_target.List("Cat2", currPage).Model;

            //Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P2");
            Assert.AreEqual(prodArray[0].Category, "Cat2");

            Assert.AreEqual(prodArray[1].Name, "P4");
            Assert.AreEqual(prodArray[1].Category, "Cat2");
        }

        /// <summary>
        ///A test for List
        ///</summary>
        [TestMethod()]
        public void CorrectGenerate_Category_Specific_Total_Product_Count_For_Paging()
        {
            //Arrange
            _target.PageSize = 3;
            int currPage = 2;

            //Action 
            var nullCatRes = ((ProductListViewModel)_target.List(null).Model).PagingInfo;
            var cat1Res = ((ProductListViewModel)_target.List("Cat1" ).Model).PagingInfo;
            var cat2Res = ((ProductListViewModel)_target.List("Cat2", currPage).Model).PagingInfo;
            var cat3Res = ((ProductListViewModel)_target.List("Cat3", currPage).Model).PagingInfo;

            //Assert
            Assert.AreEqual(5, nullCatRes.TotalItems);
            Assert.AreEqual(2, nullCatRes.TotalPages);

            Assert.AreEqual(2, cat1Res.TotalItems);
            Assert.AreEqual(1, cat1Res.TotalPages);

            Assert.AreEqual(2, cat2Res.TotalItems);

            Assert.AreEqual(1, cat3Res.TotalItems);
        }


    }
}
