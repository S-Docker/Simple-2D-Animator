# Simple2DAnimator

A simple 2D animator script created for use within the Unity engine.

Allows simple 2D animations to be created quickly and efficiently without the use of Animator or Animation components using only 3 lines of code.

## Step 1
Add the Simple2DAnimator.cs to your desired GameObject, the script will create a Sprite Renderer if one is not present.

## Step 2
Select the Texture2D that you wish to use:  
![image](https://user-images.githubusercontent.com/26844999/90259596-31a00580-de42-11ea-8235-3277fbf14c45.png)

## Step 3
Store a reference to the Simple2DAnimator component:  
![image](https://user-images.githubusercontent.com/26844999/90259764-788dfb00-de42-11ea-878b-111c45788de9.png)

## Step 4
Create your animation:  
![image](https://user-images.githubusercontent.com/26844999/90260072-e9cdae00-de42-11ea-8720-7e2208f9f950.png)

Here you can see that I have created an animation called "Arrow" that uses sprite index 0, 1, 2, 3 playing at 4 frames per second. I have also set flipX and flipY to false and looped to true. You can create and store as many animations as you want at one time and play them when you need them.

## Step 5
Play your animation by passing the animation name you set during creation:  
![image](https://user-images.githubusercontent.com/26844999/90260148-0538b900-de43-11ea-83d1-df1eab133d9d.png)

## Step 6
Stop the current playing animation at any time using the StopAnimation() function:  
![image](https://user-images.githubusercontent.com/26844999/90260352-492bbe00-de43-11ea-9b8e-fde2c427a0c7.png)
