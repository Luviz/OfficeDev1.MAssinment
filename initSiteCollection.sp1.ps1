#Create Site Colection!

function EnableSideLoading(){
    Connect-SPOnline -Url $SpURL -Credentials $SpCred
    Set-SPOAppSideLoading -On      
}


"--------------- Createing a SiteCollection-------------------"


try{
"Connecting to "+$AdminUrl + " ..."
Connect-SPOnline -Url $AdminUrl -Credentials $AdminCred #connect to sp admin


"--------------- Site Collection Created ---------------------"

$site = Get-SPOTenantSite -Url $SpURL -ErrorAction SilentlyContinue
if($site -eq $null){
    "This Procces May Take a Few Miuntes please hold"
    "Working ..."
    New-SPOTenantSite -Title $SiteCollectionTitle -Url $SpURL -Owner $SiteCollectionAdmin -Template "STS#0" -Lcid 1033 -Wait -TimeZone 4

    #enable sideloading
    ""
    "Site Created!"
    "Enabling sideloading!"
    EnableSideLoading
    "Side loading enabled"
    ""

    #Enable-SPFeature e374875e-06b6-11e0-b0fa-57f5dfd72085 –url $SpURL
    
}else{
    "The Site Colection "+ $SiteCollectionTitle + " Allready Exsites!"
    "skipping this phase!"
}

}catch{

"--------------- The Opration Faild --------------------------"
Break
}
