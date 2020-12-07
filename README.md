﻿# Confluence Profile Photo Uploader 
Unfortunately, Confluence does not have a way for administrators to bulk upload user profile photos into it. For corporate organizations that want to make sure that user photos are uniform and professional this is somewhat cumbersome. As a result Confluence may have user profiles with Mickey Mouse, Wolverine, or worse as the photo ... don't get me wrong Wolverine is pretty cool, just not professional.

This utility allows you to sweep in photos in a specified folder with a file mask of "%username%.%extension%" into Confluence. The utility will accept .jpg, .jpeg, .tif, .tiff, .png, and .bmp files. Optionally, you can specify a folder to archive the swept-in photos to after they have been uploaded. Confluence profile photos are 48x48 pixels and since most photos are not square the utility will attempt to do face detection and crop the photo to the largest face in the photo. If the photo is already square you can opt to have the utility not perform the face detection. I am personally not smart enough to write face detection algorithms, so I am using [EMGU/OpenCV](http://www.emgu.com/wiki/index.php/Main_Page "EMGU/OpenCV") to do the face detection (which is why the compiled version is so freakin big). Hey, I got no problem standing on the shoulders of giants.

## Configuration ##
The ConfluenceProfilePhotos.exe.Config files contains the configuration for the utility. If you launch the utility without any command line switches you will get a GUI that will allow you to edit the config.
### Passwords ###
The passwords in the config file are encrypted, so you will need to use the GUI to save at least these fields ... the rest are just plain text fields.
### Soap Path ###
The only field that is in the config file but not the GUI is the "soapPath" configuration option. I figured this one is too easy to mess up and then not know what it is supposed to be, so I left that out of the GUI. If your Confluence instances is not installed to the root of the web server then you will need to manually change this field or set the "Confluence Wiki URL" field in the GUI to have the Confluence install folder. For instance, if your Confluence URL is http://mywiki/confluence, then you would need to change the "soapPath" to "/confluence/rpc/soap-axis/confluenceservice-v2?wsdl" or set the "Confluence Wiki URL" to http://mywiki/confluence and leave the "soapPath" as "/rpc/soap-axis/confluenceservice-v2?wsdl". Probably the later approach is the easiest and best option.

## Automation ##
For automation purposes there is a command line switch __/headless__ that will do the upload without any GUI using the configured settings. This works great with Windows Task Scheduler.

## Requirements ##
This uses the standard APIs that come with Confluence. So, in order for it to work you must have "Remote API" turned on in Confluence Admin -> General Configuration.  
[.NET 3.5](http://www.microsoft.com/en-us/download/details.aspx?id=21)  
[Microsoft Visual C++ 2012 Redistributable x86](http://www.microsoft.com/en-us/download/details.aspx?id=30679)  

## Help!! ##
If you run the utility with the command line switch of /h, or if you click the "?" in the program bar of the GUI you will get the help screen. It has some helpful information regarding file masks and some general utility information. Also, each input field has a tooltip that is displayed when you hover over it decribing what that input field does. Finally, feel free to contact me directly at fredclown at gmail if you need other help that isn't in the help screen. If you run across any bugs I would love to know about them. So, feel free to create a bug report on the issues tab and I will attempt to diagnose and fix it. This works great on my installs, but I have not done extensive testing outside of that, so there could very well be bugs.

## Versions ##
v1.4.0.0 (2020-12-07)
Cloned from https://bitbucket.org/fredclown/confluence-profile-photo-uploader (via archive.org)
Support 256x256 profile images
Fix some odd bugs

v1.3.0.0 (2015-03-31)  
Added email option for version updates and failed face detection when running in headless mode  
Added tabbed config interface  
Moved proxy settings to tabbed config  
Removed proxy settings from help screen  
Added help tooltips to each input  
Removed the config help from the help screen  

v1.2.0.0 (2015-03-19)  
Added a path to move images to that failed face detection.  
Added a manual upload form for image that failed face detection.  
Fiddled with the version check message a bit more.  

v1.1.1.0 (2015-02-23)  
Added prerequisite check for Microsoft Visual C++ 2012 Redistributable x86 on program launch  
Fiddled with the version check message a bit.  

v1.1.0.0 (2015-02-21)  
Added version checking  
Revavamped the help dialog  
Enahnced the face detection and cropping to zoom on face for full size images  
Added a test form to see how any given image will be cropped by the utility  
Refactored a bunch of code  
Changed log to be in this format key="value" key="value" key="value" for each entry  

v1.0.0.0 (2013-10-15)  
Initial release