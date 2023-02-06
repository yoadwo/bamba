# Dog vs Dev
## Or: How i've used ngrok and GitHub Actions to stop yelling at my dog

I love dogs. Who doesn't? They're cute, they're loyal - they're truly a man's best friend.
The thing they don't tell is - they're not born this way. They require training and discipline. And if you don't maintain they're training exercises - they'll eat your slippers. And this is how my story has begun.

### Keep your friends close, and your shoes closer
The first (and last) time I left my dog completely on its own, she chewed on all of my shoelaces' tips, smashed my computer mouse and left the tv remote eternally scarred.
So we bought a CCTV camera, placed it in the living room, kissed our privacy goodbye (terrible feeling. I hope to never get hacked) and started following our dog's every movement.
More than once we caught her jumping on the sofa, getting dangerously close to our slippers and generally looking to wreak havoc.
So it's a good thing the camera has an intercom, right? we would yell at our phone, at a second later the intercom yells at our dog. Sound like we've got it all covered, right??

### The WOW moment
Image the following scene: you're in a public place. The bus, the mall maybe. You look at your phone and see your dog making trouble again. You open your CCTV's app and order your dog to stop. You know what other people see? Some random crazy person shouting at his phone to "get off that sofa". And at that **precise** moment, you understand that you're better than that, smarter than that, and there must be something you can do.

### Less Shouting, More Coding
The initial idea was fairly simple. I wanted to pre-record several commands, and with a click of a button have my computer play them!
Some platform considerations: 

 - While I wouldn't mind using the app by navigating to some web site, my girlfriend needed a simpler interface - preferably a native app 
 - I'm originally a web developer, so i'm feeling more at home with frameworks like Angular \ Vue \ React than with Flutter or React Native
 - Deploying the server remotely (Cloud, virtual hosting) won't do - the commands needed to be played locally on my computer

All things considered, I chose to develop the UI as a Progressing Web App (PWA) and have my computer run a local server and listen to incoming requests. Using PWA allowed me to develop using Angular and still installing it on my girlfriend's phone; Using .Net Core for the server was pretty arbitrary (language used in recent projects). I guess I had something like this in mind:

![first sketch](https://i.ibb.co/JxskQfr/erd-drawio.png)

**A short note (added midway of writing):** The purpose of this article will be to introduce a cool idea, and how to deploy it using some cool tools. Addressing specific code parts would make reading the article tedious, unmaintainable and not language-agnostic. See the [GitHub repo](https://github.com/yoadwo/bamba) as reference, and perhaps in the future I will add a "prequel" article which describes the code in more depth :)

#### Rok that server!
The above figure isn't quite accurate: It's missing some real-world harsh truths: Our computer can't have a direct connection to our phone. It's behind our local router, which acts as a NAT, which in itself is allocated a dynamic IP from our ISP. Got you confused? follow the numbers:
Inside your home network, your devices are allocated IPs like "192.168.1.10" and "192.168.1.11". To the outside world? You're all one IP provided by your ISP (mine is 77.124.181.120). But that's also temporary! your ISP reassigns your IPs all the time!
![second sketch](https://i.ibb.co/2STGwbZ/erd-Page-2-drawio.png)

This is right where this amazing tool called "ngrok" comes in!
In its simplest form, it allows you to create a "bridge" (actually a **SSH tunnel**) between your computer and the outside world. Allocates you a URL (a permanent one!) and redirects any request to it straight to your computer, where your web server (and your dog) are eagerly waiting!
![third sketch](https://i.ibb.co/cJP9WZ9/erd-Page-3-drawio.png)

To avoid overloading this article with instructions, allow me to refer you to  ngrok's [Getting started](https://ngrok.com/docs/getting-started).
With help from ngrok's cli, I got a "tunnel" linking my local server to the outside world (looking something like ``https://xxx-...-xx.ngrok.io``), and the "voice commands" endpoint just went from ``localhost/api/VoiceCommands`` to ``xx.xx.ngrok.io/api/VoiceCommands``!
I've copied that url into my ``baseUrl`` variable in my client's http requests section, and now the UI interacts directly with my local computer.

So basically that's it! We've got our client hosted somewhere (hey, [github pages](https://pages.github.com/)!), hopefully also installed as a [PWA](https://medium.com/@dhormale/install-pwa-on-windows-desktop-via-google-chrome-browser-6907c01eebe4), and everytime we open the app we can order our dog around without sounding silly :)

![enter image description here](https://i.ibb.co/QNVVW76/web-sample.png)

#### GitHub Actions to the rescue!
In the previous  section, I've shown how to bring your localhost to the web. But coming from a software security background, I just didn't feel safe doing that. My code wasn't built in a very secure way, and neither did I use any of ngrok's security mechanisms (I only later learned about them).

Getting a new URL every run was good enough for me, but I had to make sure the app's baseUrl is updated each time (loaded via ``assets\tunnels.json`` and change the [code read from that file](https://github.com/yoadwo/bamba/tree/master/bamba-admin-pwa/src/app/services/ngrokUrl). Thank god ngrok's CLI is also a web server. And because I kept changing my files, I wanted to automate the whole process with GitHub Actions.
I needed an Action that upon every update, my will build my code and then copy the output folder into the "gh-pages" branch. 
[This Action](https://github.com/s0/git-publish-subdir-action) is doing just that, and you can check my full action [here](https://github.com/yoadwo/bamba/blob/master/.github/workflows/deploy.yml).

Here's the full solution flow (there's a [script](https://github.com/yoadwo/bamba/blob/master/usage/ngrokAndPushTunnels.bat) for it):

 1. Run ngrok
 2. Query the CLI for the tunnel's URL and export it to a file
 3. Update your git repository
 4. Run GitHub Action

<r><r>
Wow! What a journey! This article came out way longer than I planned! 
I hope the instructions were clear, and that you managed to follow them at least partially, but just in case you didn't, here's a cheat-sheet:

 - A quick [readme](https://github.com/yoadwo/bamba#readme) for the main steps to take
 - I uploaded the binaries themselves and added a [no-compile script](https://github.com/yoadwo/bamba/blob/master/usage/ngrokAndPushTunnels_no-compile.bat)

Feel free to contact me for any questions and suggestions!

---
Future To-Do List:

 - Add more in-depth description of the backend and frontend code
 - Step-by-step description the GitHub Action
 - Step-by-step description of the wrapper script
 - Read the audio files dynamically (they are now hard-coded)

