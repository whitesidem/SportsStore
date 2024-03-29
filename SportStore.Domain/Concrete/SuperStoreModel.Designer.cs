﻿////------------------------------------------------------------------------------
//// <auto-generated>
////    This code was generated from a template.
////
////    Manual changes to this file may cause unexpected behavior in your application.
////    Manual changes to this file will be overwritten if the code is regenerated.
//// </auto-generated>
////------------------------------------------------------------------------------

//using System;
//using System.Data.Objects;
//using System.Data.Objects.DataClasses;
//using System.Data.EntityClient;
//using System.ComponentModel;
//using System.Xml.Serialization;
//using System.Runtime.Serialization;

//[assembly: EdmSchemaAttribute()]

//namespace SportStore.Domain.Concrete
//{
//    #region Contexts
    
//    /// <summary>
//    /// No Metadata Documentation available.
//    /// </summary>
//    public partial class SportsStoreEntities : ObjectContext
//    {
//        #region Constructors
    
//        /// <summary>
//        /// Initializes a new SportsStoreEntities object using the connection string found in the 'SportsStoreEntities' section of the application configuration file.
//        /// </summary>
//        public SportsStoreEntities() : base("name=SportsStoreEntities", "SportsStoreEntities")
//        {
//            this.ContextOptions.LazyLoadingEnabled = true;
//            OnContextCreated();
//        }
    
//        /// <summary>
//        /// Initialize a new SportsStoreEntities object.
//        /// </summary>
//        public SportsStoreEntities(string connectionString) : base(connectionString, "SportsStoreEntities")
//        {
//            this.ContextOptions.LazyLoadingEnabled = true;
//            OnContextCreated();
//        }
    
//        /// <summary>
//        /// Initialize a new SportsStoreEntities object.
//        /// </summary>
//        public SportsStoreEntities(EntityConnection connection) : base(connection, "SportsStoreEntities")
//        {
//            this.ContextOptions.LazyLoadingEnabled = true;
//            OnContextCreated();
//        }
    
//        #endregion
    
//        #region Partial Methods
    
//        partial void OnContextCreated();
    
//        #endregion
    
//        #region ObjectSet Properties
    
//        /// <summary>
//        /// No Metadata Documentation available.
//        /// </summary>
//        public ObjectSet<Product> Products
//        {
//            get
//            {
//                if ((_Products == null))
//                {
//                    _Products = base.CreateObjectSet<Product>("Products");
//                }
//                return _Products;
//            }
//        }
//        private ObjectSet<Product> _Products;

//        #endregion
//        #region AddTo Methods
    
//        /// <summary>
//        /// Deprecated Method for adding a new object to the Products EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
//        /// </summary>
//        public void AddToProducts(Product product)
//        {
//            base.AddObject("Products", product);
//        }

//        #endregion
//    }
    

//    #endregion
    
//    #region Entities
    
//    /// <summary>
//    /// No Metadata Documentation available.
//    /// </summary>
//    [EdmEntityTypeAttribute(NamespaceName="SportsStoreModel", Name="Product")]
//    [Serializable()]
//    [DataContractAttribute(IsReference=true)]
//    public partial class Product : EntityObject
//    {
//        #region Factory Method
    
//        /// <summary>
//        /// Create a new Product object.
//        /// </summary>
//        /// <param name="productID">Initial value of the ProductID property.</param>
//        /// <param name="name">Initial value of the Name property.</param>
//        /// <param name="description">Initial value of the Description property.</param>
//        /// <param name="category">Initial value of the Category property.</param>
//        /// <param name="price">Initial value of the Price property.</param>
//        public static Product CreateProduct(global::System.Int32 productID, global::System.String name, global::System.String description, global::System.String category, global::System.Decimal price)
//        {
//            Product product = new Product();
//            product.ProductID = productID;
//            product.Name = name;
//            product.Description = description;
//            product.Category = category;
//            product.Price = price;
//            return product;
//        }

//        #endregion
//        #region Primitive Properties
    
//        /// <summary>
//        /// No Metadata Documentation available.
//        /// </summary>
//        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
//        [DataMemberAttribute()]
//        public global::System.Int32 ProductID
//        {
//            get
//            {
//                return _ProductID;
//            }
//            set
//            {
//                if (_ProductID != value)
//                {
//                    OnProductIDChanging(value);
//                    ReportPropertyChanging("ProductID");
//                    _ProductID = StructuralObject.SetValidValue(value);
//                    ReportPropertyChanged("ProductID");
//                    OnProductIDChanged();
//                }
//            }
//        }
//        private global::System.Int32 _ProductID;
//        partial void OnProductIDChanging(global::System.Int32 value);
//        partial void OnProductIDChanged();
    
//        /// <summary>
//        /// No Metadata Documentation available.
//        /// </summary>
//        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
//        [DataMemberAttribute()]
//        public global::System.String Name
//        {
//            get
//            {
//                return _Name;
//            }
//            set
//            {
//                OnNameChanging(value);
//                ReportPropertyChanging("Name");
//                _Name = StructuralObject.SetValidValue(value, false);
//                ReportPropertyChanged("Name");
//                OnNameChanged();
//            }
//        }
//        private global::System.String _Name;
//        partial void OnNameChanging(global::System.String value);
//        partial void OnNameChanged();
    
//        /// <summary>
//        /// No Metadata Documentation available.
//        /// </summary>
//        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
//        [DataMemberAttribute()]
//        public global::System.String Description
//        {
//            get
//            {
//                return _Description;
//            }
//            set
//            {
//                OnDescriptionChanging(value);
//                ReportPropertyChanging("Description");
//                _Description = StructuralObject.SetValidValue(value, false);
//                ReportPropertyChanged("Description");
//                OnDescriptionChanged();
//            }
//        }
//        private global::System.String _Description;
//        partial void OnDescriptionChanging(global::System.String value);
//        partial void OnDescriptionChanged();
    
//        /// <summary>
//        /// No Metadata Documentation available.
//        /// </summary>
//        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
//        [DataMemberAttribute()]
//        public global::System.String Category
//        {
//            get
//            {
//                return _Category;
//            }
//            set
//            {
//                OnCategoryChanging(value);
//                ReportPropertyChanging("Category");
//                _Category = StructuralObject.SetValidValue(value, false);
//                ReportPropertyChanged("Category");
//                OnCategoryChanged();
//            }
//        }
//        private global::System.String _Category;
//        partial void OnCategoryChanging(global::System.String value);
//        partial void OnCategoryChanged();
    
//        /// <summary>
//        /// No Metadata Documentation available.
//        /// </summary>
//        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
//        [DataMemberAttribute()]
//        public global::System.Decimal Price
//        {
//            get
//            {
//                return _Price;
//            }
//            set
//            {
//                OnPriceChanging(value);
//                ReportPropertyChanging("Price");
//                _Price = StructuralObject.SetValidValue(value);
//                ReportPropertyChanged("Price");
//                OnPriceChanged();
//            }
//        }
//        private global::System.Decimal _Price;
//        partial void OnPriceChanging(global::System.Decimal value);
//        partial void OnPriceChanged();

//        #endregion
    
//    }

//    #endregion
    
//}
