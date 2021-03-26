using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Employee
{
    public partial class Employee : System.Web.UI.Page
    {
        private string strConnectionString = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        private SqlCommand _sqlCommand;
        private SqlDataAdapter _sqlDataAdapter;
        DataSet _dtSet;
        protected GridView grvEmployee;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployeeData();

            }
            btnUpdate.Visible = false;
            submitBtn.Visible = true;
        }
        private static void ShowAlertMessage(string error)
        {
            System.Web.UI.Page page = System.Web.HttpContext.Current.Handler as System.Web.UI.Page;
            if (page != null)
            {
                error = error.Replace("'", "\'");
                System.Web.UI.ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
            }
        }
        public void CreateConnection()
        {
            SqlConnection _sqlConnection = new SqlConnection(strConnectionString);
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
        }
        public void OpenConnection()
        {
            _sqlCommand.Connection.Open();
        }
        public void CloseConnection()
        {
            _sqlCommand.Connection.Close();
        }
        public void DisposeConnection()
        {
            _sqlCommand.Connection.Dispose();
        }
        public void BindEmployeeData()
        {
            try
            {
                CreateConnection();
                OpenConnection();
                _sqlCommand.CommandText = "mastercrud";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddWithValue("@opType", "Select");
                _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);
                _dtSet = new DataSet();
                _sqlDataAdapter.Fill(_dtSet);
                grvEmployee.DataSource = _dtSet;
                grvEmployee.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("The Error is " + ex);
            }
            finally
            {
                CloseConnection();
                DisposeConnection();
            }
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                CreateConnection();
                OpenConnection();
                _sqlCommand.CommandText = "mastercrud";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddWithValue("@opType", "Insert");
                _sqlCommand.Parameters.AddWithValue("@name", Convert.ToString(txtName.Text.Trim()));

                string genderId = "";
                if (RadioButton1.Checked)
                {
                    genderId = RadioButton1.Text;
                }
                else genderId = RadioButton2.Text;

                _sqlCommand.Parameters.AddWithValue("@gender", Convert.ToString(genderId));
                _sqlCommand.Parameters.AddWithValue("@specification", Convert.ToString(dropSpecification.SelectedValue));
                _sqlCommand.Parameters.AddWithValue("@address", Convert.ToString(txtAddress.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@password", Convert.ToString(pwd.Text));
                _sqlCommand.Parameters.AddWithValue("@email", Convert.ToString(txtEmail.Text.Trim()));
                int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
                if (result > 0)
                {

                    ShowAlertMessage("Record Is Inserted Successfully");
                    BindEmployeeData();
                    ClearControls();
                }
                else
                {

                    ShowAlertMessage("Failed");
                }
            }
            catch (Exception ex)
            {

                ShowAlertMessage("Check your input data");

            }
            finally
            {
                CloseConnection();
                DisposeConnection();
            }
        }

        public void ClearControls()
        {
            txtName.Text = "";
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;
            txtAddress.Text = "";
            pwd.Text = "";
            txtEmail.Text = "";
            CheckBox1.Checked = false;
            dropSpecification.SelectedIndex = 0;
        } 

        protected void grvEmployee_RowEditing(object sender, GridViewEditEventArgs e)
        {
            submitBtn.Visible = false;
            btnUpdate.Visible = true;
            dropSpecification.SelectedIndex = 0;
            RadioButton1.Checked = false;
            RadioButton2.Checked = false;

            int RowIndex = e.NewEditIndex;
            Label empid = (Label)grvEmployee.Rows[RowIndex].FindControl("lblEmpId");
            Session["id"] = empid.Text;

            txtName.Text = ((Label)grvEmployee.Rows[RowIndex].FindControl("lblName")).Text.ToString();

            string str = "Female";
            string gender = ((Label)grvEmployee.Rows[RowIndex].FindControl("lblGender")).Text.ToString().Trim();
            RadioButton1.Checked = true;
            if (str.Equals(gender))
            {
                RadioButton2.Checked = true;
            }            
            dropSpecification.Text = ((Label)grvEmployee.Rows[RowIndex].FindControl("lblSpecification")).Text.ToString();
            txtAddress.Text = ((Label)grvEmployee.Rows[RowIndex].FindControl("lblAddress")).Text.ToString();
            txtEmail.Text = ((Label)grvEmployee.Rows[RowIndex].FindControl("lblEmail")).Text.ToString();

        }

        protected void grvEmployee_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                CreateConnection();
                OpenConnection();
                Label id = (Label)grvEmployee.Rows[e.RowIndex].FindControl("lblEmpId");
                _sqlCommand.CommandText = "mastercrud";
                _sqlCommand.Parameters.AddWithValue("@opType", "Delete");
                _sqlCommand.Parameters.AddWithValue("@id", Convert.ToInt32(id.Text));
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
                if (result > 0)
                {

                    ShowAlertMessage("Record Is Deleted Successfully");
                    grvEmployee.EditIndex = -1;
                    BindEmployeeData();
                }
                else
                {
                    lblMessage.Text = "Failed";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    BindEmployeeData();
                }
            }
            catch (Exception ex)
            {

                ShowAlertMessage("Check your input data");
            }
            finally
            {
                CloseConnection();
                DisposeConnection();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                CreateConnection();
                OpenConnection();

                _sqlCommand.CommandText = "mastercrud";
                _sqlCommand.CommandType = CommandType.StoredProcedure;
                _sqlCommand.Parameters.AddWithValue("@opType", "Update");
                _sqlCommand.Parameters.AddWithValue("@name", Convert.ToString(txtName.Text.Trim()));

                string genderId = "";
                if (RadioButton1.Checked)
                {
                    genderId = RadioButton1.Text;
                }
                else genderId = RadioButton2.Text;

                _sqlCommand.Parameters.AddWithValue("@gender", Convert.ToString(genderId));
                _sqlCommand.Parameters.AddWithValue("@specification", Convert.ToString(dropSpecification.SelectedValue));
                _sqlCommand.Parameters.AddWithValue("@address", Convert.ToString(txtAddress.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@password", Convert.ToString(pwd.Text));
                _sqlCommand.Parameters.AddWithValue("@email", Convert.ToString(txtEmail.Text.Trim()));
                _sqlCommand.Parameters.AddWithValue("@id", Convert.ToDecimal(Session["id"]));

                int result = Convert.ToInt32(_sqlCommand.ExecuteNonQuery());
                if (result > 0)
                {
                    ShowAlertMessage("Record Is Updated Successfully");
                    grvEmployee.EditIndex = -1;
                    BindEmployeeData();
                    ClearControls();
                }
                else
                {
                    ShowAlertMessage("Failed");
                }
            }

            catch (Exception ex)
            {
                ShowAlertMessage("Check your input data");
            }
            finally
            {
                CloseConnection();
                DisposeConnection();
            }
        }


        /* protected void grvEmployee_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvEmployee.EditIndex = -1;
            BindEmployeeData();
        }*/

        protected void grvEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvEmployee.PageIndex = e.NewPageIndex;
            BindEmployeeData();
        }
    }
}