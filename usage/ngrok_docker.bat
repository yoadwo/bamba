echo off
IF "%BAMBA_HOME%"=="" (
    echo Error: BAMBA_HOME is not set, please set and run again. Recommend you set it to the parent direcory of 'usage'.
    exit /b 1
)

IF "%GITHUB_BAMBA_ACCESSTOKEN%"=="" (
    echo Error: GITHUB_BAMBA_ACCESSTOKEN is not set, please set and run again. Configure the GitHub Personal Access Token to match it.
    exit /b 1
)

echo 'Begin NGROK auto update batch'
START "NGROK" ngrok http --host-header=localhost https://localhost:5001
START "BambaAdminAPI" docker run ^
 -p 5000:5000 -p 5001:5001 ^
 -v %BAMBA_HOME%\BambaAdminAPI\Assets\Audio\Arya:/app/Assets/Audio ^
 --name BambaAdminAPI ^
 yoadw20/bamba-api:1.0.1
echo 'waiting for process to complete setup'
timeout 15

curl -X POST -L "https://api.github.com/repos/yoadwo/bamba/actions/workflows/deploy.yml/dispatches" ^
 -H "Accept: application/vnd.github+json" ^
 -H "X-GitHub-Api-Version: 2022-11-28" ^
 -H "Content-Type: application/json" ^
 -H "Authorization: Bearer %GITHUB_BAMBA_ACCESSTOKEN%" ^
 -d "{ \"ref\": \"master\" }" ^
 --insecure

echo. && echo. && echo 'next ENTER will close all'
pause
taskkill /FI "WindowTitle eq "NGROK" " /T /F
taskkill /FI "WindowTitle eq "BambaAdminAPI" " /T /F
echo 'Stop and remove the container'
docker stop BambaAdminAPI && docker rm BambaAdminAPI
echo 'kapish'
