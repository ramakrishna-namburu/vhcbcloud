﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.NetworkInformation;

namespace VHCBCommon.DataAccessLayer
{
    public class CommonHelper
    {
        private static string _sortDirection;

        public static void GridViewSetFocus(GridViewRow row, string ControlName)
        {
            bool found = false;
            string control_name_lower = ControlName.ToLower();
            foreach (TableCell cell in row.Cells)
            {
                foreach (Control control in cell.Controls)
                {
                    if (control.ID != null && control.ID.ToLower() == control_name_lower)
                    {
                        found = true;
                        control.Focus();
                        break;
                    }
                }
                if (found)
                    break;
            }
        }
        public static string myDollarFormat(object amount)
        {

            if (amount is DBNull || amount.ToString() == "")
            {
                amount = 0.0;
            }
            decimal val = Convert.ToDecimal(amount);

            return string.Format("{0:c}", val);

        }

        public static void GridViewSetFocus(GridViewRow row)
        {
            bool found = false;
            foreach (TableCell cell in row.Cells)
            {
                foreach (Control control in cell.Controls)
                {
                    if (control.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                    {
                        found = true;
                        control.Focus();
                        break;
                    }
                }
                if (found)
                    break;
            }
        }

        protected static void SetSortDirection(string sortDirection)
        {
            if (sortDirection == "")
            {
                _sortDirection = "ASC";
            }
            else
            {
                _sortDirection = sortDirection;
            }
        }

        public static string GridSorting(GridView gv, DataTable dt, string SortExpression, string SortDireaction)
        {
            SetSortDirection(SortDireaction);
            if (dt != null)
            {
                //Sort the data.
                if (SortExpression != "")
                    dt.DefaultView.Sort = SortExpression + " " + _sortDirection;
                gv.DataSource = dt;
                gv.DataBind();
                return _sortDirection;
            }
            return null;
        }

        public static void PopulateDropDown(DropDownList ddl, string DBSelectedvalue)
        {
            foreach (ListItem item in ddl.Items)
            {
                if (DBSelectedvalue == item.Value.ToString())
                {
                    ddl.ClearSelection();
                    item.Selected = true;
                }
            }
        }
        public static void DisableButton(Button btn)
        {
            btn.Enabled = false;
            btn.CssClass = "btn btn-info";
        }

        public static void EnableButton(Button btn)
        {
            btn.Enabled = true;
            btn.CssClass = "btn btn-info";
        }

        public static bool IsVPNConnected()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface networkInterface in interfaces)
                {
                    if ((networkInterface.OperationalStatus == OperationalStatus.Up &&
                        networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet) &&
                        (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback) &&
                        (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Wireless80211))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
