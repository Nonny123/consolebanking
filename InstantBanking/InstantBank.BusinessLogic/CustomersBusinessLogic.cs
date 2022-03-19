using InstantBank.BusinessLogic.BALContracts;
using InstantBank.DataAccess;
using InstantBank.DataAccess.DALContracts;
using InstantBank.Entities;
using InstantBank.Exceptions;
using System;
using System.Collections.Generic;


namespace InstantBank.BusinessLogic
{
    /// <summary>
    /// Represents customer business logic
    /// </summary>
    public class CustomersBusinessLogic : ICustomersBusinessLogic
    {
        #region Private Fields
        private ICustomersDataAccess _customersDataAccess;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that initializes CustomersDataAccessLayer
        /// </summary>
        public CustomersBusinessLogic()
        {
            _customersDataAccess = new CustomersDataAccess();
        }
        #endregion


        #region Properties
        /// <summary>
        /// Private property that represents reference of CustomersDataAccessLayer
        /// </summary>
        private ICustomersDataAccess CustomersDataAccess
        {
            set => _customersDataAccess = value;
            get => _customersDataAccess;
        }
        #endregion


        #region Methods
        // <summary>
        /// Returns all existing customers
        /// </summary>
        /// <returns>List of customers</returns>
        public List<Customer> GetCustomers()
        {
            try
            {
                //invoke DAL
                return CustomersDataAccess.GetCustomers();
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a set of customers that matches with specified criteria
        /// </summary>
        /// <param name="predicate">Lamdba expression that contains condition to check</param>
        /// <returns>The list of matching customers</returns>
        public List<Customer> GetCustomersByCondition(Predicate<Customer> predicate)
        {
            try
            {
                //invoke DAL
                return CustomersDataAccess.GetCustomersByCondition(predicate);
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds a new customer to the existing customers list
        /// </summary>
        /// <param name="customer">The customer object to add</param>
        /// <returns>Returns true, that indicates the customer is added successfully
        /// </returns>
        public Guid AddCustomer(Customer customer)
        {
            try
            {
                //get all customers
                List<Customer> allCustomers = CustomersDataAccess.GetCustomers();
                long maxCustCode = 0;
                foreach (var item in allCustomers)
                {
                    if (item.CustomerCode > maxCustCode)
                    {
                        maxCustCode = item.CustomerCode;
                    }
                }

                //generate new customer no
                if (allCustomers.Count >= 1)
                {
                    customer.CustomerCode = maxCustCode + 1;
                }
                else
                {
                    customer.CustomerCode = Configuration.Settings.BaseCustomerNo + 1;
                }

                //invoke DAL
                return CustomersDataAccess.AddCustomer(customer);
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an existing customer
        /// </summary>
        /// <param name="customer">Customer object that contains customer details to update</param>
        /// <returns>Returns true, that indicates the customer is updated successfully</returns>
        public bool UpdateCustomer(Customer customer)
        {
            try
            {
                //invoke DAL
                return CustomersDataAccess.UpdateCustomer(customer);
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes an existing customer
        /// </summary>
        /// <param name="customerID">CustomerID to delete</param>
        /// <returns>Returns true, that indicates the customer is deleted successfully</returns>
        public bool DeleteCustomer(Guid customerID)
        {
            try
            {
                //invoke DAL
                return CustomersDataAccess.DeleteCustomer(customerID);
            }
            catch (CustomerException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
