# Simple2DAnimator

A simple 2D animator script created for use within the Unity engine.

Allows simple 2D animations to be created quickly and efficiently without the use of Animator or Animation components using only 3 lines of code.

The only requirement is to make sure that any sprites you wish to use are located within "Assets/Resources/Sprites" folder.

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
![image](https://user-images.githubusercontent.com/26844999/90261271-a70cd580-de44-11ea-9b4f-192214049b72.png)

Here you can see that I have created an animation called "Arrow" that uses sprite index 0, 1, 2, 3 playing at 4 frames per second. There are also optional parameters for whether to loop the animation, flipping the sprite on its X axis and flipping the sprite on its Y axis, these values are defaulted to true, false, false.  
Note: You can create and store as many animations as you want at one time and play them when you need them.

## Step 5
Play your animation by passing the animation name you set during creation:  
![image](https://user-images.githubusercontent.com/26844999/90260148-0538b900-de43-11ea-83d1-df1eab133d9d.png)

## Step 6
Stop the current playing animation at any time using the StopAnimation() function:  
![image](https://user-images.githubusercontent.com/26844999/90260352-492bbe00-de43-11ea-9b8e-fde2c427a0c7.png)
