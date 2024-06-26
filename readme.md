# Welcome to Arya's Admin App
 
 This app allows you to remotely control your PC, and activate pre-recorded audio files.
**What's it for?** Your dog is at home, you see it on the CCTV, but don't want to shout at it via intercom and risk sounding stupid. What do you do? click the voice command button to play it directly on your computer.  

## Prerequisites

1. ngrok installed and a token properly set up; see [ngrok website](https://ngrok.com/)
2. git installed on your computer
3. PulseAudio server for Windows; see [this video](https://www.youtube.com/watch?v=SF_WMBpQ0Qs&t=354s&ab_channel=AgileDevArt).
4. self signed certificate for localhost, to allow the app to run (try ```dotnet dev-certs https```, or via the browser)
5. github account, and a repo forked from the [AdminApp](https://github.com/yoadwo/bamba/tree/master) github repo.
6. github *pages* set up, and a "deploy to self" github Action configured
  
## Run using Docker

1. When running PulseAudio on Windows, use arguments `--use-pid-file=false -D`
2. Specify a mount path for your audio files (mp3 and wav) with `-v host:/app/Assets/Audio`
3. The image uses a self-signed certificate with hard coded password. WIP.

## What's Under the Hood?

The flow inside the script is as follows:
1. launching ngrok for reverse ssh
2. launching the admin API with port matching ngrok's forwarding
3. dispatching a GitHub Action which
    1. queries ngrok's API for allocated ngrok url
    2. saves that url into the client repo as config file (`tunnels.json`)
    3. rebuilds and deploys the updated PWA app

## Files

1. The *usage* directory contains the script and icon files
2. The *bamba-admin-pwa* contains the web app's source code. No need to compile it, that's what github Actions are for.
3. The *BambaAdminApi* contains the server's source code. You may use it as is (using the binary in *usage* directory) or compile it again with your own sound files names.
