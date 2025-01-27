using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VHCBCommon.DataAccessLayer;

namespace vhcbcloud
{
    public partial class NewLookup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtViewName.Text != "")
                {
                    LookupMaintenanceData.AddNewLookup(txtViewName.Text, cbOrdered.Checked, cbTiered.Checked);
                    lblErrorMsg.Text = "New Lookup added successfully";

                    txtViewName.Text = "";
                    cbTiered.Checked = false;
                    cbOrdered.Checked = false;
                }
                else
                    lblErrorMsg.Text = "Please enter Viewname";
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }
    }
}