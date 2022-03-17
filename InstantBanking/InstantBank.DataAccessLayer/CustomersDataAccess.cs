using InstantBank.DataAccess.DALContracts;
using InstantBank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBank.DataAccess
{
    /// <summary>
    /// Represents DAL for bank customers
    /// </summary>
    public class CustomersDataAccess : ICustomersDataAccessLayer
    {
        #region Fields
        private List<Customer> _customers;
        #endregion


        #region Constructors
        public CustomersDataAccess()
        {
            _customers = new List<Customer>();
        }
        #endregion


        #region Properties
        /// <summary>
        /// Represents source customers collection
        /// </summary>
        private List<Customer> Customers
        {
            set => _customers = value;
            get => _customers;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Returns all existing customers
        /// </summary>
        /// <returns>Customers list</returns>
        public List<Customer> GetCustomers()
        {
            //create a new customers list
            List<Customer> customersList = new List<Customer>();

            //copy all customers from the soruce collection into the newCustomers list
            Customers.ForEach(item => customersList.Add(item.Clone() as Customer));
            return customersList;
        }

        /// <summary>
        /// Returns list of customers that are matching with specified criteria
        /// </summary>
        /// <param name="predicate">Lambda expression with condition</param>
        /// <returns>List of matching customers</returns>
        public List<Customer> GetCustomersByCondition(Predicate<Customer> predicate)
        {
            //create a new customers list
            List<Customer> customersList = new List<Customer>();

            //filter the collection
            List<Customer> filteredCustomers = customersList.FindAll(predicate);

            //copy all customers from the soruce collection into the newCustomers list
            Customers.ForEach(item => filteredCustomers.Add(item.Clone() as Customer));
            return customersList;
        }


        /// <summary>
        /// Adds a new customer to the existing list
        /// </summary>
        /// <param name="customer">Customer object to add</param>
        /// <returns>Returns Guid of newly created customer</returns>
        public Guid AddCustomer(Customer customer)
        {
            //generate new Guid
            customer.CustomerID = Guid.NewGuid();

            //add customer
            Customers.Add(customer);

            return customer.CustomerID;
        }


        /// <summary>
        /// Updates an existing customer's details
        /// </summary>
        /// <param name="customer">Customer object with updated details</param>
        /// <returns>Determines whether the customer is updated or not</returns>
        public bool UpdateCustomer(Customer customer)
        {
            //find existing customer by CustomerID
            Customer existingCustomer = Customers.Find(item => item.CustomerID == customer.CustomerID);

            //update all details of customer
            if (existingCustomer != null)
            {
                existingCustomer.CustomerCode = customer.CustomerCode;
                existingCustomer.CustomerName = customer.CustomerName;
                existingCustomer.Address = customer.Address;
                existingCustomer.Landmark = customer.Landmark;
                existingCustomer.City = customer.City;
                existingCustomer.Country = customer.Country;
                existingCustomer.Mobile = customer.Mobile;

                return true; //indicates the customer is updated
            }
            else
            {
                return false; //indicates no object is updated
            }
        }

        /// <summary>
        /// Deletes an existing customer based on CustomerID
        /// </summary>
        /// <param name="customerID">CustomerID to delete</param>
        /// <returns>Indicates whether the customer is deleted or not</returns>
        public bool DeleteCustomer(Guid customerID)
        {
            //delete customer by CustomerID
            if (Customers.RemoveAll(item => item.CustomerID == customerID) > 0)
            {
                return true;  //indicates one or more customers are deleted
            }
            else
            {
                return false; //indicates no customer is deleted
            }
        }
        #endregion
    }
}
