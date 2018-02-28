# CameraSkills Application


This is an Android app that implements Google Vision's smart image detection features to make a smarter camera. By using this app, your phone's camera turns into a smart image detector. This application was written for CS 480 Mobile Apps as a fun way to implement a webservice into a mobile app. Using Google's API is used in this project for the assignment.

Please enjoy this application! It is not only an entertainment app that shows you cool things that the Google Vision can detect, but it may also serve as a useful tool when trying to detect real-life objects. Though it may not be as specific, it can recognize more general concepts and even ones that may not come into your mind at the time. If you're ever confronted with something obscure that you don't know the label of, try having this app detect and label the image for you. You may use this for anything (or person)!

## System Design 
This is where you specify all of the system's requirements.  This section should accurately portray the complete operation of your application.  Provide scenarios, use cases, system requirements, and diagrams/screenshots of the system.

*This app is designed for Android 5.0 and above. 
*It requires use of the Camera so be ready to be prompted if it happens to ask you for camera permissions. 
* Must have internet connection for the api to work.

I didn't have to "allow" access on my Unihertz Jelly pro. Please note that there are some bugs in this app that I couldn't figure out before I had to cut my losses and turn it in. Instead of reading/writing to a file with an image, I chose to save the image's top five apiResults of its label and corresponding confidence score from google into the Items class. However, I wasn't able to use the class correctly so the app behaves only in regards to the first photo taken. Thus, in order to use the app again, please close and reopen it.




## Usage

This application can be run through many times without fear of crashing. You can get to the main page by either clicking "Play Again" Once the camera returns an image you will be prompted to answer whether or 

![alt text](https://github.com/KalimotxoGood/Teaching-MobileApps/blob/master/projects/project%203/source/Screenshot_20180228-042807.png "The result after the camera returns an image")




Upon a Success or a Google Api response at the end, or simply pressing the "I Don't Know" button when the app asks you if the photo is X. 



![alt text](https://github.com/KalimotxoGood/Teaching-MobileApps/blob/master/projects/project%203/source/Screenshot_20180228-042826.png "Enter Something In and the app will give you its knowledge")
Enter Something In and the app will give you its knowledge


![alt text](https://github.com/KalimotxoGood/Teaching-MobileApps/blob/master/projects/project%203/source/Screenshot_20180228-042807.png "The view Upon the "Yes button")
The view Upon the "Yes" button

![alt text](https://github.com/KalimotxoGood/Teaching-MobileApps/blob/master/projects/project%203/source/Screenshot_20180228-042845.png "Click Play Again to be redirected to the main page!")
Click Play Again to be redirected to the main page!
