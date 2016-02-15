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

##################



#Getting Scripts Dirctory
$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
cd $dir #changeing to Scriptes Dirctory

.$dir\test01.ps1 #Debug rem me

#Genrate Site Collection
.$dir\initSiteCollection.sp1 


"Taxonomi"
#Build Taxonomi
.$dir\Init.Taxonomi\bin\Debug\Init.Taxonomi.exe $SpURL "World"