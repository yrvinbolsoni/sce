﻿<%@ Page Title="" Language="C#" MasterPageFile="~/TI/Ti.Master" AutoEventWireup="true" CodeBehind="Legends.aspx.cs" Inherits="SCE.TI.Legends" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <html lang="en">
<head>
	<meta charset="UTF-8">
	<title>Document</title>
	<link href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
	<style>
		* {margin:0; padding:0;
			font-family: arial;
		}
		body {background: #E81D62;}
		#ola {
			font-size: 50px;
			color: #fff;
			text-align: center;
			text-shadow: -1px -1px 0 #ccc; margin: 50px 0 30px}
		#transcription {
			width: 50%;
			border-radius: 5px;
			height: 100px;
			margin: 0 auto;
			display: block;
			font-size: 16px;
			padding: 11px;
			color: #666;
			background: #fff;
		}
		#gravar {
			border: none;
			background: transparent;
			font-size: 40px;
			color: #fff;
			width: 100%;
			outline-color: transparent;
			padding-top: 20px;
		}
		#gravar i { cursor: pointer;
		width: 80px;
		height: 80px;
		line-height: 80px;
		border-radius: 100%;
		box-shadow: inset 0 0 0 transparent;
		-webkit-transition: all 0.5s linear;
		-moz-transition: all 0.5s linear;
		-ms-transition: all 0.5s linear;
		-o-transition: all 0.5s linear;
		transition: all 0.5s linear;
	margin-bottom: 15px;}
		#gravar i:hover {
			box-shadow: inset 0 0 20px #fff;
		}
		#gravar i:active {box-shadow: inset 0 0 20px 100px #fff; color:#E81D62;  }
		#status {color: #fff; text-align: center; display: block}
		#status span {font-weight: bold;}
		#status span.gravando {color: rgb(70, 232, 29);}
		#status span.pausado {color: rgb(173, 115, 229);}
		.hidden {display: none;}
		#ws-unsupported {
font-size: 60px;
position: fixed;
width: 140%;
text-align: center;
height: 100px;
background: red;
color: #000;
-webkit-transform: rotateZ(-30deg);
-ms-transform: rotateZ(-30deg);
-o-transform: rotateZ(-30deg);
box-shadow: 0 0 7px rgba(0, 0, 0, 0.67);
transform: rotateZ(-30deg);
top: 190px;
		}
	#rect {
		display: block;
		margin: 30px auto;
		background: #fff;
		padding: 10px;
		border: none;
		font-size: 18px;
		border-radius: 5px;
		color: rgb(232, 29, 98);
		font-family: arial;
	}
	</style>
</head>
<body>
		<p id="ola">Olá tableless, você falou:</p>
		<div id="transcription"></div>
 
		<button id="rect">Gravar</button>
 
	    <span id="unsupported" class="hidden">API not supported</span>
 
    <script type="text/javascript">
      // Test browser support
      window.SpeechRecognition = window.SpeechRecognition       ||
                                 window.webkitSpeechRecognition ||
                                 null;
 
		//caso não suporte esta API DE VOZ                              
		if (window.SpeechRecognition === null) {
	    	document.getElementById('unsupported').classList.remove('hidden');
        }else {
            var recognizer = new window.SpeechRecognition();
            var transcription = document.getElementById("transcription");
        	//Para o reconhecedor de voz, não parar de ouvir, mesmo que tenha pausas no usuario
        	recognizer.continuous = true
        	recognizer.onresult = function(event){
        		transcription.textContent = "";
        		for (var i = event.resultIndex; i < event.results.length; i++) {
        			if(event.results[i].isFinal){
        				transcription.textContent = event.results[i][0].transcript+' (Taxa de acerto [0/1] : ' + event.results[i][0].confidence + ')';
        			}else{
		            	transcription.textContent += event.results[i][0].transcript;
        			}
        		}
        	}
        	document.querySelector("#rect").addEventListener("click",function(){
        		try {
		            recognizer.start();
		          } catch(ex) {
		          	alert("error: "+ex.message);
		          }
        	});
        }
    </script>
</body>
</html>
</asp:Content>
