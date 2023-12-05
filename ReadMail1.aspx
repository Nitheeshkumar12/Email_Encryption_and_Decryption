<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="ReadMail1.aspx.cs" Inherits="ReadMail1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <br />
    </p>
    <p>
    </p>
    <table class="w-100">
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td colspan="2">
            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="X-Large" 
                Text="Read Mail"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                <asp:Label ID="Label10" runat="server" Text="MailId" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label11" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                <asp:Label ID="Label1" runat="server" Text="Subject" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label6" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                <asp:Label ID="Label2" runat="server" Text="From" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label7" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                <asp:Label ID="Label3" runat="server" Text="Message" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                <asp:Label ID="Label4" runat="server" Text="File Details" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label9" runat="server"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                <asp:Label ID="Label12" runat="server" Text="Upload Image" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                <asp:Label ID="Label13" runat="server" Text="Unhidekey" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" class="btn btn-primary" 
                    onclick="Button1_Click" Text="Download" />
            &nbsp;<asp:Button ID="Button2" runat="server" class="btn btn-primary" 
                    onclick="Button2_Click" Text="SendKeyRequest" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 346px">
                &nbsp;</td>
            <td style="width: 158px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>

