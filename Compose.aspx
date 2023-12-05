<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Compose.aspx.cs" Inherits="Compose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <br />
    </p>
    <p>
    </p>
    <table class="w-100" width="100%">
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td colspan="2" align=center>
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="X-Large" 
                    Text="Compose Mail"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                <asp:Label ID="Label1" runat="server" style="color: #000000" Text="Send Option"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>Location</asp:ListItem>
                    <asp:ListItem>Group</asp:ListItem>
                    <asp:ListItem Value="Individual">Individual</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                <asp:Label ID="Label6" runat="server" style="color: #000000" 
                    Text="EmailId or Option"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server" Visible="False" 
                    onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                    <asp:ListItem>--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                <asp:Label ID="Label2" runat="server" style="color: #000000" Text="Subject"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                <asp:Label ID="Label3" runat="server" style="color: #000000" Text="Message"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                <asp:Label ID="Label4" runat="server" style="color: #000000" Text="Upload File"></asp:Label>
            </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                &nbsp;</td>
            <td>
                <asp:Label ID="Label7" runat="server" Font-Bold="True" ForeColor="#CC0066"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                &nbsp;</td>
            <td>
                <i class="fa fa-envelope-o">
                <asp:Button ID="Button1" runat="server" class="btn btn-primary" 
                    onclick="Button1_Click" Text="Send" type="submit" value="Sent" />
&nbsp;
                <asp:Button ID="Button2" runat="server" class="btn btn-primary" 
                    onclick="Button1_Click" Text="Clear" type="submit" value="Sent" />
                </i></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 247px">
                &nbsp;</td>
            <td style="width: 298px">
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

