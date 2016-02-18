"Hello World"
$user = Read-Host "Username"
$pass = Read-Host "Password" -AsSecureString 
$url = "https://luviz.sharepoint.com"

#Getting Scripts Dirctory
$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
cd $dir #changeing to Scriptes Dirctory

$unsafePass = [Runtime.InteropServices.Marshal]::PtrToStringAuto(
                [Runtime.InteropServices.Marshal]::SecureStringToBSTR($pass))


.$dir/Init.Taxonomi/bin/debug/Init.Taxonomi.exe $user $unsafepass $url