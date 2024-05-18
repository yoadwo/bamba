echo off
echo 'Begin NGROK auto update batch'
START "NGROK" ngrok http --host-header=localhost https://localhost:61839
START "BambaAdminAPI" docker run ^
 -p 61839 ^
 -v C:\Users\yoadw\source\repos\bamba\bamba\BambaAdminAPI\Assets\Audio\Arya:/app/Assets/Audio ^
 bamba-api:0.2.6
echo 'waiting for process to complete setup'
timeout 10
echo 'delete old tunnels file'
del "..\bamba-admin-pwa\src\assets\tunnels.json"
echo 'querying ngrok cli to get tunnels info'
powershell "wget http://localhost:4040/api/tunnels -Outfile ..\bamba-admin-pwa\src\assets\tunnels.json"
timeout 5
echo 'tunnels.json updated'
echo 'pushing changes in tunnels.json, this will trigger a github action'
echo 'git status before committing change'
git -C "..\bamba-admin-pwa" status
echo 'GIT ADD'
git -C "..\bamba-admin-pwa" add src/assets/tunnels.json
echo 'GIT COMMIT'
git -C "..\bamba-admin-pwa" commit -m "updated tunnels file"
echo 'git status after committing change'
git -C "..\bamba-admin-pwa" status
echo 'GIT PUSH'
git -C "..\bamba-admin-pwa" push
echo ''
echo 'next ENTER will close all'
pause
taskkill /FI "WindowTitle eq "NGROK" " /T /F
taskkill /FI "WindowTitle eq "BambaAdminAPI" " /T /F

echo 'kapish'
