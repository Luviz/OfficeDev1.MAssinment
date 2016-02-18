#Edit This Filed
##Admin info
$AdminUrl = "https://Luviz-Admin.sharepoint.com"
$AdminCred = "LuvizSharePoint"
$SiteCollectionName = "Assinment"
$SiteCollectionAdmin = "bardiajedi@Luviz.onmicrosoft.com"
$SiteCollectionTitle = "Mandetory Assinment - Bardia Jedi -"

##Site Collection info
$SpURL = "https://Luviz.sharepoint.com/sites/"+$SiteCollectionName
$SpCred = "LuvizSharePoint"

##Username for taxsonomy admin
$SpTaxAdmin = "bardiajedi@luviz.onmicrosoft.com"

##################



#Getting Scripts Dirctory
$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
cd $dir #changeing to Scriptes Dirctory


#Genrate Site Collection
.$dir\initSiteCollection.sp1 


"Taxonomi"
"Loging in as " + $SpTaxAdmin
$pass = Read-Host $SPTaxAdmin "`nPassword" -AsSecureString 
$unsafePass = [Runtime.InteropServices.Marshal]::PtrToStringAuto(
                [Runtime.InteropServices.Marshal]::SecureStringToBSTR($pass))

#Build Taxonomi
.$dir/Init.Taxonomi/bin/debug/Init.Taxonomi.exe $SpTaxAdmin $unsafepass $SpURL

