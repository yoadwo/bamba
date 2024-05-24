echo off
echo 'Begin NGROK auto update batch'
START "NGROK" ngrok http --host-header=localhost https://localhost:61839
START "BambaAdminAPI" docker run ^
 -p 61839:61839 ^
 -v C:\Users\yoadw\source\repos\bamba\bamba\BambaAdminAPI\Assets\Audio\Arya:/app/Assets/Audio ^
 --name BambaAdminAPI ^
 yoadw20/bamba-api:1.0.0
echo 'waiting for process to complete setup'
timeout 15

curl -L 'https://api.github.com/repos/yoadwo/bamba/actions/workflows/deploy.yml/dispatches' \
-H 'Accept: application/vnd.github+json' \
-H 'X-GitHub-Api-Version: 2022-11-28' \
-H 'Content-Type: application/json' \
-H 'Authorization: Bearer %GITHUB_BAMBA_ACCESSTOKEN%' \
-d '{
    "ref": "master"    
}'
echo. && echo. && echo 'next ENTER will close all'
pause
taskkill /FI "WindowTitle eq "NGROK" " /T /F
taskkill /FI "WindowTitle eq "BambaAdminAPI" " /T /F
echo 'Stop and remove the container'
docker stop BambaAdminAPI && docker rm BambaAdminAPI
echo 'kapish'
