echo off
echo 'Begin NGROK auto update batch'
START "NGROK" ngrok http -host-header=localhost https://localhost:61839
set DOTNET_ENVIRONMENT=Production
set ASPNETCORE_URLS=https://*:61839
cd ..\bamba\BambaAdminAPI
dotnet publish BambaAdminAPI.csproj -c Release
START "BambaAdminAPI" bin\Release\netcoreapp3.1\publish\BambaAdminAPI.exe
echo 'waiting for process to complete setup'
timeout 10
cd ..\..\ngrok
echo 'delete old tunnels file'
del "..\bamba\bamba-admin-pwa\src\assets\tunnels.json"
echo 'querying ngrok cli to get tunnels info'
powershell "wget http://localhost:4040/api/tunnels -Outfile ..\bamba\bamba-admin-pwa\src\assets\tunnels.json"
timeout 5
echo 'tunnels.json updated'
echo 'pushing changes in tunnels.json, this will trigger a github action'
echo 'git status before committing change'
git -C "..\bamba\bamba-admin-pwa" status
echo 'GIT ADD'
git -C "..\bamba\bamba-admin-pwa" add src/assets/tunnels.json
echo 'GIT COMMIT'
git -C "..\bamba\bamba-admin-pwa" commit -m "updated tunnels file"
echo 'git status after committing change'
git -C "..\bamba\bamba-admin-pwa" status
echo 'GIT PUSH'
git -C "..\bamba\bamba-admin-pwa" push
echo ''
echo 'next ENTER will close all'
pause
taskkill /FI "WindowTitle eq "NGROK" " /T /F
taskkill /IM BambaAdminAPI.exe

echo 'kapish'
