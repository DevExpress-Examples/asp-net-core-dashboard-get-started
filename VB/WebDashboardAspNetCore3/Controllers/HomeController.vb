﻿Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Logging
Imports WebDashboardAspNetCore3.Models

Namespace WebDashboardAspNetCore3.Controllers
	Public Class HomeController
		Inherits Controller

		Private ReadOnly _logger As ILogger(Of HomeController)

		Public Sub New(ByVal logger As ILogger(Of HomeController))
			_logger = logger
		End Sub

		Public Function Index() As IActionResult
			Return View()
		End Function

		Public Function Privacy() As IActionResult
			Return View()
		End Function

		<ResponseCache(Duration := 0, Location := ResponseCacheLocation.None, NoStore := True)>
		Public Function [Error]() As IActionResult
			Return View(New ErrorViewModel With {.RequestId = If(Activity.Current?.Id, HttpContext.TraceIdentifier)})
		End Function
	End Class
End Namespace
