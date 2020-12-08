<%@ Page Title="Расходы" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Games.aspx.cs" Inherits="Lab11.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="mt-3 container row">
        <div class="col-xs-6 text-center">
            <asp:GridView runat="server" ID="expencesGrid" CssClass="table"
        ItemType="Lab11.Models.Table" DataKeyNames="Id" 
        SelectMethod="GetGames"
        AutoGenerateColumns="false">
        <Columns>
            <asp:DynamicField DataField="Name" />
            <asp:DynamicField DataField="Type" />
            <asp:DynamicField DataField="Price" />               
        </Columns>
    </asp:GridView>

    Потрачено на игры: <span ID="totalGamesCost" runat="server" ></span>.
        </div>
        <div class="col-xs-6 text-center">
            <asp:Button runat="server" CssClass="btn btn-primary pleasurebtn" Text="Рассчитать процент трат на RPG игры" OnClick="Calculate" />
        <% if (!Text.IsEmpty()) { %>
        <h3>Вы потратили <span ID="percentOfGames" runat="server"></span>% на RPG</h3>
    <%} %>
        </div>
    </div>

</asp:Content>
