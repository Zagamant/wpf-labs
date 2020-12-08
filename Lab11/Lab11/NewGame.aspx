<%@ Page Title="Я опять потратил деньги..............." Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewGame.aspx.cs" Inherits="Lab11.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="flexdiv">
        <asp:ValidationSummary runat="server" ShowModelStateErrors="true" />
        <p>Name</p>
    <asp:TextBox ID="ename" runat="server" ></asp:TextBox>
        <p>Type</p>
    <asp:TextBox ID="etype" runat="server" ></asp:TextBox>
        <p>Price</p>
    <asp:TextBox ID="eprice" runat="server" ></asp:TextBox>
    <asp:Button runat="server" Text="Insert" OnClick="insertButton_Click" />
    <asp:Button runat="server" Text="Cancel" CausesValidation="false" OnClick="cancelButton_Click" />
    </div>

</asp:Content>
