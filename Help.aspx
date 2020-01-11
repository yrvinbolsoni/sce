<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="SCE.Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Solicitação de Melhoria</h2>
    <p>Toda e qualquer solicitação de melhoria ou informar sobre algum tipo de erro pode ser feita atravez do SISTEMA DE CHAMADOS, <a href="http://www.grieg.com.br/chamados/" target="_blank" >Clique aqui </a> para abrir seu chamado  </p>
    <h2>Como gerar relatórios? </h2>
    <p>
        Para gerar relatórios você deve escolher o Formulário e escolher os parâmetros necessários.
Note que quanto maior for a busca mais tempo deve demorar renderização do relatório. Também pode exportar relatório para Pdf, Excel e Word , como pode observar na imagem abaixo
    </p>
    <img src="Content/Img/ExemploExport.png" />

    <h3>O Sistema suporta impressão sem a necessidade de exportar para PDF.</h3>
    <p>
        Para habilitar essa função utilize o navegador Internet Explorer note que na barra de navegação aparecera um botão de impressão.
Como pode observar na imagem abaixo
    </p>
    <img src="Content/Img/ExemploPrinter.png" />

    <h3>Como Instalar o plug-in de impressão</h3>
    <p>
        Na maioria dos casos a primeira vez que for solicitado a impressão o sistema irar pedir para que instale um plug-in caso não tenha o plug-in instalado na sua máquina.
o processo é bem simples como pode observar no passo a passo  abaixo.
    </p>
    <h3>Passo 1</h3>
    <p>Clck em ok e depois em instalar.</p>

    <img src="Content/Img/ExemploPrinter03.PNG" />
    <h3>Passo 2</h3>
    <p>Depois aceite a solicitação</p>

    <img src="Content/Img/ExemploPrinter04.PNG" />
    <h3>Passo 3</h3>
    <p>Tudo pronto agora é só escolher a impressora que deseja imprimir</p>
    <img src="Content/Img/ExemploPrinter05.PNG" />


</asp:Content>
