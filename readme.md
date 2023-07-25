# Welcome to Arya's Admin App

This app allows you to remotely control your PC, and activate pre-recorded audio files.
Main use case: Your dog is at home, you see it on the CCTV, but don't want to shout at it via intercom and risk sounding stupid. What do you do? click the voice command button to play it directly on your computer.

## Prerequisites
1. ngrok installed and a token properly set up; see [ngrok website](https://ngrok.com/)
2. git installed on your computer
3. dotnet core 3.1 SDK installed. The app isn't compiled as self-contanied (oopsy)
4. self signed certificate for localhost, to allow the app to run (try ```dotnet dev-certs https```, or via the browser)
5. github account, and a repo forked from the [AdminApp](https://github.com/yoadwo/bamba/tree/master) github repo.
6. github *pages* set up, and a "deploy to self" github Action configured

## Run using Docker
1. I dockerized the app, but could not get it to run properly. This was probably due to me running a windows machine and linux containers. On Linux, You can try pulling the image `yoadw20/bamba-api:0.2.3-linux` and running it such as `docker run ... --device /dev/snd`. Maybe it will work, maybe it won't :)
2. For the adventurous, you can try and installing _pulse audio_, a socket between the host and guest machines: see this [stack overflow](https://stackoverflow.com/a/51860606) post 



## How this working?
The flow inside the script is as follows:
1. starting ngrok with a predefined port
2. setting local env variables to control the server's configuration
3. launching the server (via the precompiled binary)
4. querying the local ngrok instance, and outputting the result into the "tunnels" file
5. committing the code with the new tunnels file
6. the github Action will take place, rebuilding the PWA app with the current ngrok tunnel into your pc


## Files
1. The *usage* directory is the most basic files you need in order for the app to run: a precompiled binary, a script to call both ngrok and the server precompiled binary, and an icon for the shortcut
2. The *bamba-admin-pwa* contains the web app's source code. No need to compile it, that's what github Actions are for.
3. The  *BambaAdminApi* contains the server's source code. You may use it as is (using the binary in *usage* directory) or compile it again with your own sound files names.
