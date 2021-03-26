<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="Employee.Employee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Detail</title>
    <script language="javascript" type="text/javascript">
        function ValidateCheckBox() {
            alert("called");
            if ($("#CheckBox1").is(':checked')) {
                return true;
            }
            else {
                alert("Please accept term and condition");
                return false;
            }
        }
    </script>
</head>
<body>
    <div>
        <h3>Employee Register Page</h3>
    </div>
    <form id="employeeForm" runat="server">
        <div>
            <asp:label runat="server" text="Label">Name:</asp:label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="formData" runat="server" ErrorMessage="Name required"  
        ControlToValidate="txtName"></asp:RequiredFieldValidator>

        </div>
        <div>
            <asp:label runat="server" text="Label">Gender:</asp:label>
            <asp:RadioButton ID="RadioButton1" runat="server" value="Male" GroupName="gender" Text="Male" Checked="True"/>
            <asp:RadioButton ID="RadioButton2" runat="server" value="Female" GroupName="gender" Text="Female"/>
        </div>
        <div>
            <asp:label runat="server" text="Label">Specification:</asp:label>
            <asp:DropDownList ID="dropSpecification" runat="server">
                <asp:ListItem>--select--</asp:ListItem>
                <asp:ListItem>Engineering</asp:ListItem>
                <asp:ListItem>MCA</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldSpecification" runat="server" ControlToValidate ="dropSpecification" ValidationGroup="formData"
               ErrorMessage="Please choose a specification" InitialValue="--select--">
            </asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:label runat="server" text="Label">Address:</asp:label>
            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldAddress" ValidationGroup="formData" runat="server" ErrorMessage="Address required"  
        ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:label runat="server" text="Label">Password:</asp:label>
            <asp:TextBox ID="pwd" runat="server" type="password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldPassword" ValidationGroup="formData" runat="server" ErrorMessage="Password required"  
        ControlToValidate="pwd"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:label runat="server" text="Label">Confrim Password:</asp:label>
            <asp:TextBox ID="confrimPwd" runat="server" type="password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidatorPassword" ValidationGroup="formData" runat="server" ControlToCompare="pwd" ErrorMessage="Password not matched" ControlToValidate="confrimPwd"></asp:CompareValidator>
        </div>
        <div>
            <asp:label runat="server" text="Label">Email:</asp:label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldEmail" ValidationGroup="formData" runat="server" ErrorMessage="Email required"  
        ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="remail" runat="server" ValidationGroup="formData"
               ControlToValidate="txtEmail" ErrorMessage="Valid Email required" 
               ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
            </asp:RegularExpressionValidator>
        </div>
        <div>
            <asp:CheckBox ID="CheckBox1" runat="server" />Are you ready for registration?
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please check this checkbox if you want to proceed" ClientValidationFunction="ValidateCheckBox"></asp:CustomValidator>
        </div>
        <div>
            <asp:Button ID="submitBtn" OnClick="btnAddEmployee_Click" runat="server" Text="Submit" ValidationGroup="formData"/>
            <asp:Button runat="server" ID="btnUpdate" Text="Update" OnClick="btnUpdate_Click"/>
            <asp:Button ID="resetBtn" OnClick="btnReset_Click" runat="server" Text="Reset" />
        </div>
        
        
        <asp:Label runat="server" ID="lblMessage"></asp:Label>
        <asp:GridView ID="grvEmployee" runat="server" AllowPaging="true" OnPageIndexChanging="grvEmployee_PageIndexChanging" AutoGenerateColumns="false"  OnRowDeleting="grvEmployee_RowDeleting" OnRowEditing="grvEmployee_RowEditing">
            <Columns>
                <asp:TemplateField HeaderText="EmpId">  
                    <ItemTemplate>  
                        <asp:Label runat="server" ID="lblEmpId" Text='<%#Eval("id") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EmpName">  
                    <ItemTemplate>  
                        <asp:Label runat="server" ID="lblName" Text='<%#Eval("name") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EmpGender">  
                    <ItemTemplate>  
                        <asp:Label runat="server" ID="lblGender" Text='<%#Eval("gender") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EmpSpecification">  
                    <ItemTemplate>  
                        <asp:Label runat="server" ID="lblSpecification" Text='<%#Eval("specification") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EmpAddress">  
                    <ItemTemplate>  
                        <asp:Label runat="server" ID="lblAddress" Text='<%#Eval("address") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EmpEmail">  
                    <ItemTemplate>  
                        <asp:Label runat="server" ID="lblEmail" Text='<%#Eval("email") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Update">  
                    <ItemTemplate>  
                        <asp:LinkButton runat="server" ID="btnEdit" Text="Edit" CommandName="Edit" ToolTip="Click here to Edit the record" />                                                                                         
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Delete">  
                    <ItemTemplate>                                                                          
                        <asp:LinkButton runat="server" ID="btnDelete" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are You Sure You want to Delete the Record?');" ToolTip="Click here to Delete the record" />  
                    </ItemTemplate>                                         
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
    
</body>
</html>
