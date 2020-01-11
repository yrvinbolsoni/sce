<%@ Page Title="" EnableEventValidation="false"  Language="C#" MasterPageFile="~/TI/Ti.Master" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="SCE.TI.Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="wrapper">
        <form>
            <h4>Filtro de log SCE </h4>
            <input type="date" id="DataForm" runat="server">
            <%--<asp:Button ID="butFiltraXML" runat="server" Text="Filtrar XML" onclick="butFiltraXML_Click" />--%>
            <asp:Button ID="ButtonSend" runat="server" Text="Filtrar" CssClass="btn btn-options " OnClick="butFiltraXML_Click" />
            <br />
            <br />
            <asp:Button ID="butGetXML" runat="server" Text="Todos Registros" CssClass="btn btn-options " OnClick="butGetXML_Click" />
            <br />
            <br />
            <div style="color: red">
                <asp:Literal ID="litXMLDados" runat="server"></asp:Literal>
            </div>
        </form> 
        <%--minha implementação --%>
        <div id="Cabecalho" runat="server">
            <table>
                <table class='table table-striped table-hover '>
                    <thead>
                        <tr class='bg-primary'>
                            <th>usuario</th>
                            <th>NomeRelatorio</th>
                            <th>Data</th>
                            <th>Hora</th>
                            <th>IP</th>
                            <th>Erro</th> 
                    </thead>
        </div>
        <tbody>
            <div id="CorpoTabela" runat="server"></div>
        </tbody>
        </table>
    </div>
    <br />
    <br />
    <br />
</div>
</asp:Content>
