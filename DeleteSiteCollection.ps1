$AdminUrl = "https://luviz-admin.sharepoint.com/"
$SpURL = ""


Connect-SPOnline -Url $AdminUrl #connect to sp admin
Remove-SPOTenantSite -Url $SpURL -Force  -SkipRecycleBin

