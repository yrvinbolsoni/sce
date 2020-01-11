<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormEmc.aspx.cs" Inherits="SCE.FormEmc" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator" ControlToValidate="ListYear" runat="server" ErrorMessage="* Year is a required field." ForeColor="Red"></asp:RequiredFieldValidator>
            <div style="font-family: Arial">
                <div class="stepwizard">
                    <div class="stepwizard-row row">
                        <div class="stepwizard-step col-lg-3">
                            <asp:Button ID="ButtonYear" class="btn-circle" runat="server" Text="1" OnClick="ButtonYear_Click" />
                            <p>Year/Direction/BrzPorts/Owner/Commodity</p>
                        </div>
                        <div class="stepwizard-step col-lg-3">
                            <asp:Button ID="ButtonRoutes" class="btn-circle" runat="server" Text="2" OnClick="ButtonRoutes_Click" />
                            <p>Routes/Areas/Slings</p>
                        </div>
                         <div class="stepwizard-step col-lg-3">
                            <asp:Button ID="ButtonReport" class="btn-circle" runat="server" Text="3" OnClick="ButtonReport_Click" />
                            <p>Choose the Report/</p>
                        </div>
                         <div class="stepwizard-step col-lg-3">
                            <asp:Button ID="ButtonViwerReport" class="btn-circle" runat="server" Text="3" OnClick="ButtonViwerReport_Click" Enabled="False" CssClass="btn-circle" />
                            <p>Report/</p>
                        </div>
                    </div>
                </div>  

                 <div id="Erros" runat="server"></div>
                <asp:MultiView ID="multiViewFormEmc" runat="server">
                    <asp:View   runat="server">
                        <h4>Year/Direction/Brz Ports/Types</h4>
                        <div class="Row">
                            <div class="col-lg-2">
                                <h4>Year/</h4>
                                <asp:ListBox ID="ListYear" ValidationGroup="ListYear" runat="server" Rows="6"></asp:ListBox>

                            </div>
                            <div class="col-lg-2">
                                <h4>Start month </h4>
                                <asp:ListBox ID="StartMonth" runat="server" Rows="6">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                </asp:ListBox>
                            </div>

                            <div class="col-lg-2">
                                <h4>Final month</h4>
                                <asp:ListBox ID="FinalMonth" runat="server" Rows="6">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                </asp:ListBox>
                            </div>

                            <div class="col-lg-2">
                                <h4>Direction</h4>
                                <asp:ListBox ID="ListDirecao" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>

                            <div class="col-lg-3">
                                <h4>Owners</h4>
                                <asp:ListBox ID="ListOwners" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <%--  Proxima linha --%>
                        </div>
                        <div class="Row">

                            <div class="col-lg-3">
                                <h4>Brazilian Ports</h4>
                                <asp:ListBox ID="ListPort" runat="server" SelectionMode="Multiple" Rows="13"></asp:ListBox>
                            </div>



                            <div class="col-lg-4">
                                <h4>Type of Commodity</h4>
                                <asp:ListBox ID="ListCommodity" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-lg-4">
                            </div>

                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </asp:View>
                    <asp:View   runat="server">
                      
                        <h4>Routes/Areas/Foring Ports</h4>
                        <div class="Row">
                            <div class="col-lg-4">
                                <h4>Service/Rotes</h4>

                                <asp:ListBox ID="ListRotas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListRotas_SelectedIndexChanged" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <div class="col-lg-4">
                                <h4>Sling</h4>
                                <asp:ListBox ID="ListSling" runat="server" AutoPostBack="True" SelectionMode="Multiple"></asp:ListBox>

                            </div>
                            <div class="col-lg-4">
                                <h4>Area</h4>
                                <asp:ListBox ID="ListArea" runat="server" AutoPostBack="True" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                      

                        <%--  Proxima linha --%>
                        <div class="Row">
                            <div class="col-lg-6">
                                <h4>Search CLIENTS</h4>
                                <asp:TextBox ID="Pcliente" Style="text-transform: uppercase;" runat="server"></asp:TextBox>
                                <asp:Button ID="SelectCliente" runat="server" Text="Search" OnClick="SelectCliente_Click" />
                                <div runat="server" id="ErroCliente" style="color: red;"></div>
                                <h4>Select below the clientes of your choice</h4>
                                <asp:ListBox ID="ListClientes" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                      </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
            </asp:View>
             <asp:View  runat="server">
                   <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="Content/Img/loading2.gif" width="80px" height="80px" class="loader" /> 
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                   <h4>Report</h4>
                          <div class="row" style="margin-bottom: 5px;">
                            <div class="col-lg-2">
                                <%-- Vago--%>
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ResumoServiceButton" runat="server"   Text="Resumo -Service" Width="200px" OnClick="ResumoServiceButton_Click" />
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="SummaryNavioButton" runat="server" Text="Summary -Navio" Width="200px" OnClick="SummaryNavioButton_Click" />
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ResumoMensalButton" runat="server" Text="Resumo -Mensal" Width="200px" OnClick="ResumoMensalButton_Click" />
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-lg-2">
                                <%-- Vago--%>
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ResumoNavioCommodityButton" runat="server" Text="Resumo -Navio -Commodity" Width="200px" OnClick="ResumoNavioCommodityButton_Click" />
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ResumoCommodityNavioButton" runat="server" Text="Resumo -Commodity -Navio" Width="200px" OnClick="ResumoCommodityNavioButton_Click" />
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ResumoClienteNavioButton" runat="server" Text="Resumo -Cliente -Navio" Width="200px" OnClick="ResumoClienteNavioButton_Click" />
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-lg-2">
                                <%-- Vago--%>
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ResumoNavioTosButton" runat="server" Text="Resumo -Navio -TOS" Width="200px" OnClick="ResumoNavioTosButton_Click" />
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ResumoTosButton" runat="server" Text="Resumo -TOS" Width="200px" OnClick="ResumoTosButton_Click" />
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ResumoContainerNavioButton" runat="server" Text="Resumo -Container -Navio" Width="200px" OnClick="ResumoContainerNavioButton_Click" />
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-lg-2">
                                <%-- Vago--%>
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="PerformanceClienteButton" runat="server" Text="Performance -Cliente" Width="200px" OnClick="PerformanceClienteButton_Click" />
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="PerformaceTotalClienteButton" runat="server" Text="Performance -Total -Cliente" Width="200px" OnClick="PerformaceTotalClienteButton_Click" />
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ResumoArmadorButton" runat="server" Text="Resumo -Armador" Width="200px" OnClick="ResumoArmadorButton_Click" />
                            </div>
                        </div>
                        <div class="row" style="margin-bottom: 5px;">
                            <div class="col-lg-2">
                                <%-- Vago--%>
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ResumoClienteButton" runat="server" Text="Resumo -Cliente" Width="200px" OnClick="ResumoClienteButton_Click" />
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ControleClientesButton" runat="server" Text="Controle -Clientes" Width="200px" OnClick="ControleClientesButton_Click" />
                            </div>
                            <div class="col-lg-3">
                                <asp:Button ID="ResumoCommodityButton" runat="server" Text="Resumo -Commodity" Width="200px" OnClick="ResumoCommodityButton_Click" />
                            </div>
                        </div>

                    </asp:View>

            <asp:View  runat="server">
          <rsweb:ReportViewer ID="ReportViewer1" BackColor="#DAEBFF" Height="800px" Width="100%"  runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" AsyncRendering="False">
                </rsweb:ReportViewer>
                </asp:View>
             

            </asp:MultiView>
             
             <div>
            <br />
            <br />
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
