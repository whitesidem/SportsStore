using SportStore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SportStore.Domain.Concrete;

namespace SportsStore.UnitTests
{
    
    
    /// <summary>
    ///This is a test class for CartTest and is intended
    ///to contain all CartTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CartTest
    {


        private TestContext testContextInstance;
        private Product _prod1 = new Product(){ ProductID = 1, Name="P1", Price = 10M};
        private Product _prod2 = new Product() { ProductID = 2, Name = "P2", Price = 20M };
        private Product _prod3 = new Product() { ProductID = 3, Name = "P3", Price = 30M };

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
        ///A test for AddItem
        ///</summary>
        [TestMethod()]
        public void Add_First_Instance_Of_Item_To_Basket()
        {
            //Arrange
            Cart target = new Cart(); 
            int quantity1 = 5;
            int quantity2 = 15; 

            //Action
            target.AddItem(_prod1, quantity1);
            target.AddItem(_prod2, quantity2);

            //Assert
            Assert.AreEqual(2,target.Lines.Count);
            Assert.AreEqual(_prod1, target.Lines[0].Product);
            Assert.AreEqual(quantity1, target.Lines[0].Quantity);
            Assert.AreEqual(_prod2, target.Lines[1].Product);
            Assert.AreEqual(quantity2, target.Lines[1].Quantity);
        }

        /// <summary>
        ///A test for AddItem
        ///</summary>
        [TestMethod()]
        public void Add_For_Existing_Lines()
        {
            //Arrange
            Cart target = new Cart();
            int quantity1 = 5;
            int quantity2 = 15;
            int quantity1b = 5;

            //Action
            target.AddItem(_prod1, quantity1);
            target.AddItem(_prod2, quantity2);
            target.AddItem(_prod1, quantity1b);

            //Assert
            Assert.AreEqual(2, target.Lines.Count);
            Assert.AreEqual(quantity1 + quantity1b, target.Lines[0].Quantity);
            Assert.AreEqual(quantity2, target.Lines[1].Quantity);
        }


        /// <summary>
        ///A test for AddItem
        ///</summary>
        [TestMethod()]
        public void Can_Remove_Line()
        {
            //Arrange
            Cart target = new Cart();
            int quantity1 = 5;
            int quantity2 = 15;
            int quantity3 = 40;
            target.AddItem(_prod1, quantity1);
            target.AddItem(_prod2, quantity2);
            target.AddItem(_prod3, quantity3);

            //Action
            var itemsInlinesBefore = target.Lines.Count;
            target.RemoveLine(_prod2);
            var itemsInlinesAfter = target.Lines.Count;

            //Assert
            Assert.AreEqual(3, itemsInlinesBefore);
            Assert.AreEqual(2, itemsInlinesAfter);
            Assert.AreEqual(_prod1, target.Lines[0].Product);
            Assert.AreEqual(_prod3, target.Lines[1].Product);
        }

        /// <summary>
        ///A test for AddItem
        ///</summary>
        [TestMethod()]
        public void Calc_Cart_Total()
        {
            //Arrange
            Cart target = new Cart();
            int quantity1 = 5;
            int quantity2 = 15;
            int quantity3 = 40;
            target.AddItem(_prod1, quantity1);
            target.AddItem(_prod2, quantity2);
            target.AddItem(_prod3, quantity3);

            //Action
            decimal totalValue = target.ComputeTotalValue();

            //Assert
            decimal expectedTotal = (quantity1*10M) + (quantity2*20M) + (quantity3*30M);
            Assert.AreEqual(expectedTotal, totalValue);
        }

        /// <summary>
        ///A test for AddItem
        ///</summary>
        [TestMethod()]
        public void Can_Clear_Contents()
        {
            //Arrange
            Cart target = new Cart();
            int quantity1 = 5;
            int quantity2 = 15;
            int quantity3 = 40;
            target.AddItem(_prod1, quantity1);
            target.AddItem(_prod2, quantity2);
            target.AddItem(_prod3, quantity3);

            //Action
            target.Clear();

            //Assert
            Assert.AreEqual(0, target.Lines.Count);
            Assert.AreEqual(0, target.ComputeTotalValue());
        }



    }
}
