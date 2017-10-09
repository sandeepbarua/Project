using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MiddleLayer;
using System.Data;
using System.Web.UI.WebControls;
using System.Reflection;

namespace DataAccessLayer
{
    public class ListtoDataTableConverter
    {
        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if(Props!=null)
            {
                if(Props.Length>0)
                {
                    foreach (PropertyInfo prop in Props)
                    {
                        //Setting column names as Property names
                        dataTable.Columns.Add(prop.Name);
                    }
                }
            }
           
            if(items!=null)
            {
                if(items.Count>0)
                {
                    foreach (T item in items)
                    {
                        var values = new object[Props.Length];
                        for (int i = 0; i < Props.Length; i++)
                        {
                            //inserting property values to datatable rows
                            values[i] = Props[i].GetValue(item, null);
                        }
                        dataTable.Rows.Add(values);
                    }
                }
            }
            
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }

    public class BlLocation
    {
        public static void ddlCustomerName(DropDownList ddlCustomerName)
        {
            ddlCustomerName.Items.Clear();
            DataTable dtddlCustomerName;
            ListItem li = new ListItem();
            li.Text = "<--Select Company Name-->";
            li.Value = "0";
            ddlCustomerName.Items.Add(li);
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(new BLConsumeApi().bindDropDowngetCustomerDetailsAll());
            dtddlCustomerName = dt;

            for (int i = 0; i < dtddlCustomerName.Rows.Count; i++)
            {
                li = new ListItem();
                li.Value = dtddlCustomerName.Rows[i]["CompanyID"].ToString();
                li.Text = dtddlCustomerName.Rows[i]["CompanyName"].ToString();
                ddlCustomerName.Items.Add(li);
            }
        }
        //public static bool ddlCustomerAddName(DropDownList ddlCustomerAddName, int FADV_CustomerID)
        //{
        //    ddlCustomerAddName.Items.Clear();
        //    DataTable dtddlCustomerAddName;
        //    ListItem li = new ListItem();
        //    ddlCustomerAddName.Items.Add(li);
        //    dtddlCustomerAddName = ExecuteSP(@" ");
        //    for (int i = 0; i < dtddlCustomerAddName.Rows.Count; i++)
        //    {
        //        li = new ListItem();
        //        li.Value = dtddlCustomerAddName.Rows[i]["CompanyID"].ToString();
        //        li.Text = dtddlCustomerAddName.Rows[i]["CompanyName"].ToString();
        //        ddlCustomerAddName.Items.Add(li);
        //        return true;
        //    }
        //    return true;
        //}
        public bool addLocationDetails(MlLocation Locationname)
        {
            BLConsumeApi consume = new BLConsumeApi();
            consume.setLocationDetails(Locationname.Fadv_LocationID,Locationname.CustomerId,Locationname.LocationName);
            return true;



        }
        public List<MlLocation> getLocationDetails()
        {
            List<MlLocation> lstUserDetails = new List<MlLocation>();
            List<MlLocation> reader = new BLConsumeApi().getCustomerLocationDetailsAll();
            foreach (var r in reader)
            {
                MlLocation setData = new MlLocation();
                setData.LocationID = r.LocationID;
                setData.Fadv_LocationID = r.Fadv_LocationID;
                setData.LocationName = r.LocationName;
                setData.CompanyName = r.CompanyName;
                setData.FADV_CustomerID = r.FADV_CustomerID;
                setData.CompanyID = r.CompanyID;
                lstUserDetails.Add(setData);
            }
            return lstUserDetails;
        }
        public List<MlLocationForExcel> getLocationDetailsByCustomer(int CustomerID)
        {
            List<MlLocationForExcel> lstUserDetails = new List<MlLocationForExcel>();
            List<MlLocation> reader = new BLConsumeApi().getCustomerLocationDetailsByCustomerID(CustomerID);
            if (reader == null)
            {

            }
            else { 
            foreach (var r in reader)
            {
                    MlLocationForExcel setData = new MlLocationForExcel();
                setData.LocationID = r.LocationID;
                setData.Fadv_LocationID = r.Fadv_LocationID;
                setData.LocationName = r.LocationName;
                setData.CompanyName = r.CompanyName;             
              
                lstUserDetails.Add(setData);
            }
            }
            return lstUserDetails;
        }
        public bool updateLocation(MlLocation setData)
        {
            BLConsumeApi consume = new BLConsumeApi();
            consume.updateLocationDetails(setData.Fadv_LocationID, setData.CustomerId, setData.LocationName, setData.LocationID);
            return true;
        }
        public bool DeleteLocation(MlLocation deldata)
        {
            BLConsumeApi api = new BLConsumeApi();
            api.deleteLocationDetails(deldata.LocationID);
            return true;
        }
    }
}
