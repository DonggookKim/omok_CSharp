@echo off

if "%NETCF_PATH%" == "" (
  set NETCF_PATH=c:\Program Files\Microsoft.NET\Framework\v4.0.30319)
if DEFINED REF ( set REF= )

set REF=%REF% "/r:%NETCF_PATH%\MsCorlib.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.Data.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.Drawing.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.Messaging.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.Net.IrDA.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.Web.Services.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.Windows.Forms.DataGrid.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.Windows.Forms.dll"
set REF=%REF% "/r:%NETCF_PATH%\Microsoft.WindowsCE.Forms.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.Xml.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.ServiceModel.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.Xml.Xlinq.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.Data.Entity.dll"
set REF=%REF% "/r:%NETCF_PATH%\System.Runtime.Serialization.dll"
set REF=%REF% "/r:%NETCF_PATH%\Microsoft.WindowsMobile.DirectX.dll"
set REF=%REF% "/r:%NETCF_PATH%\Microsoft.ServiceModel.Channels.Mail.dll"
set REF=%REF% "/r:%NETCF_PATH%\Microsoft.ServiceModel.Channels.Mail.WindowsMobile.dll"

csc -nostdlib -noconfig %REF% %*
