<%@ Page Title="" Language="C#" MasterPageFile="~/TI/Ti.Master" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="SCE.TI.Consulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Consulta via banco de dados</h3>
    <style>
        input, select, textarea {
            max-width: 800px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-8">
                    <asp:TextBox ID="Parametro" runat="server" Height="118px" Width="706px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-4">
                    <asp:Button ID="Button1" runat="server" Text="Enviar" CssClass="btn btn-primary" OnClick="Button1_Click" />
                </div>
            </div>
            <br />
            <br />
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <br />
                    <img height="100" src="../Content/Img/Loading2.gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <br />
            <div runat="server" id="Error"></div>
            <asp:GridView ID="TableQuerry" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
