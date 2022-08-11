# Virtual Organization Expert System [VOES]
	VOES is a management system for dealing with Voluntary Organization Management System.

## Tools Used 

	To develop this project, the following tools were used:
	->Microsoft Visual Studio 2013 Community Edition
	->Microsoft SQL Server 2014 Express 
	->Bunifu Framework 1.11.5.1
	->Microsoft Report Viewer

## Installation

	Use sql server query, "voes_database.txt" to create database. 
	After creating database, replace the connection string from appconfig.

	Select "Report1.rdlc" from solution explorer of the project in visual studio and go to properties. 
	Then copy the full path of "Report1.rdlc" and replace inside the " " path from "ReportViewer" class.
	->	reportViewer1.LocalReport.ReportPath = @"paste your full path here"; 

## Initial Login Credential

	From the login form click "Continue as System Administrator" first, Then use below credentials.
	*	Private Property: admin
	*	NID No : 12345
	
	After a successful login, 
	insert a President into the system and Then just logout and use the new username and password to use the full feature of this software.

## Support
	sayedsyfuzzaman@gmail.com
	
Enjoy this software.
THANK YOU! 
