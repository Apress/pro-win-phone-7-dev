using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using ContosoWcfService.Models;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Collections.ObjectModel;

namespace ContosoWcfService
{
  public class RestData : IRestData
  {
    //Get the Database Connection string 
    private string _connectionString = WebConfigurationManager.ConnectionStrings["ContosoBottlingConnectionString"].ConnectionString;

    public ObservableCollection<Customer> GetCustomers()
    {
      SqlConnection _cn = new SqlConnection(_connectionString);
      SqlCommand _cmd = new SqlCommand();
      _cmd.CommandText = "SELECT CustomerId, DistributionCenterId, RouteId, Name, StreetAddress, City, StateProvince, PostalCode FROM Customer";

      try
      {
        _cn.Open();
        _cmd.Connection = _cn;

        ObservableCollection<Customer> _customerList = new ObservableCollection<Customer>();

        SqlDataReader _dr = _cmd.ExecuteReader();
        while (_dr.Read())
        {
          Customer _customer = new Customer();
          _customer.CustomerId = Convert.ToInt32(_dr["CustomerId"]);
          _customer.DistributionCenterId = Convert.ToInt32(_dr["DistributionCenterId"]);
          _customer.RouteId = Convert.ToInt32(_dr["RouteId"]);
          _customer.Name = Convert.ToString(_dr["Name"]);
          _customer.StreetAddress = Convert.ToString(_dr["StreetAddress"]);
          _customer.City = Convert.ToString(_dr["City"]);
          _customer.StateProvince = Convert.ToString(_dr["StateProvince"]);
          _customer.PostalCode = Convert.ToString(_dr["PostalCode"]);

          //Add to List 
          _customerList.Add(_customer);
        }
        return _customerList;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
      finally
      {
        _cmd.Dispose();
        _cn.Close();
      }
    }
  }
}