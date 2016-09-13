using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz
{
    /// <summary>
    /// Manages products caried in inventory
    /// </summary>
    public class Product
    {
        #region Fields
        public const double InchesPerMeter = 39.37;

        public readonly decimal MinimumPrice;
        #endregion

        #region Constructors
        public Product()
        {
            Console.WriteLine("Product instance created");
            //this.ProductVendor = new Vendor();
            this.MinimumPrice = .96m;
            this.Category = "Tools";
        }
        
        public Product(int productID, string productName, string description) : this()
        {
            this.ProductID = productID;
            this.ProductName = productName;
            this.Description = description;
            if (ProductName.StartsWith("Bulk"))
            {
                this.MinimumPrice = 9.99m;
            }
            Console.WriteLine("Product instance has a name: " + ProductName);
        }
        #endregion

        #region Properties
        private DateTime? availabilityDate;

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }

        internal string Category { get; set; }
        public decimal Cost { get; set; }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string ProductCode => $"{this.Category}-{this.SequenceNumber:0000}";

        private int productID;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }

        private string productName;

        public string ProductName
        {
            get
            {
                var formattedValue = productName?.Trim();
                return formattedValue;
            }
            set
            {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least 3 characters";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name cannot be more than 20 characters";
                }
                else
                {
                    productName = value;
                }
            }
        }

        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get
            {
                if (productVendor == null)
                {
                    productVendor = new Vendor();
                }
                return productVendor;
            }
            set { productVendor = value; }
        }

        public int SequenceNumber { get; set; } = 1;
        public string ValidationMessage { get; private set; }

        #endregion

        #region Methods
        public decimal CalculateSuggestedPrice(decimal markupPercent) => this.Cost + (this.Cost * markupPercent / 100);

        public string SayHello()
        {
            //var vendor = new Vendor();
            //vendor.SendWelcomeEmail("Message from Product");

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("New Product",this.ProductName, "sales@abc.com");
            var result = LoggingService.LogAction("saying hello");
            return "Hello " + ProductName + "(" + ProductID + "): " + Description + " Available on: " + AvailabilityDate?.ToShortDateString();
        }

        public override string ToString()
        {
            return this.ProductName + " (" + this.productID + ") ";
        }
        #endregion

    }
}
