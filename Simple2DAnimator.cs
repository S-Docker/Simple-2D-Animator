using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple2DAnimator : MonoBehaviour
{
    public Texture2D spriteTexture;
    private SpriteRenderer spriteRenderer;
    private Sprite[] sprites;
    
    // Default sprite settings
    public int defaultSprite = 0;
    public string sortingLayer = "Default";
    public int sortOrder = 1;

    List<Simple2DAnimation> anims = new List<Simple2DAnimation>();
    Simple2DAnimation currentAnim;
    int currentFrame = 0;
    float timeBetweenAnimChange;
    float timePassed = 0f;

    void OnValidate() 
    {
        LoadSprites();
        SetUpSpriteRenderer();
    }

    void LoadSprites()
    {
        Object[] data = Resources.LoadAll<Sprite>("Sprites/" + spriteTexture.name.ToString());
         = new Sprite[data.Length];

        for (int i = 0; i < data.Length; i++)   
        {
            sprites[i] = (Sprite)data[i];
        }
    }

    void SetUpSpriteRenderer()
    {
        if (this.GetComponent<SpriteRenderer>() != null)
        {
            spriteRenderer = this.GetComponent<SpriteRenderer>();
        } 
        else if (this.GetComponentInChildren<SpriteRenderer>() != null)
        {
             = this.GetComponentInChildren<SpriteRenderer>();
        } 
        else 
        {
            spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
        }

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = sortingLayer;
        spriteRenderer.sortingOrder = sortOrder;
        spriteRenderer.sprite = sprites[defaultSprite];
    }

    public void CreateFixedSpacingAnimation(string animationName, int[] animationFrames, int framesPerSecond = 30, bool loopAnimation = true, bool flippedX = false, bool flippedY = false)
    {
        int[,] temp = ConvertArrayTo2D(animationFrames); // convert 1d array to 2d array for compatibility

        Simple2DAnimation anim = new Simple2DAnimation(animationName, temp, framesPerSecond, loopAnimation, true, flippedX, flippedY);
        anims.Add(anim);
    }
    
    public void CreateVariableSpacingAnimation(string animationName, int[,] animationFrames, int framesPerSecond = 30, bool loopAnimation = true, bool flippedX = false, bool flippedY = false)
    {
        Simple2DAnimation anim = new Simple2DAnimation(animationName, animationFrames, framesPerSecond, loopAnimation, false, flippedX, flippedY);
        anims.Add(anim);
    }

    public int[,] ConvertArrayTo2D(int[] array){
        int[,] temp = new int[array.Length,1];
        Debug.Log(temp.GetLength(0));
        
        for (int i = 0; i < array.Length; i++)
        {
            temp[i,0] = array[i];
        }

        return temp;
    }

    public void StartAnimation(string animationName)
    {
        currentAnim = anims.Find(item => item.animationName.Contains(animationName));

        if (currentAnim != null)
        {
            spriteRenderer.flipX = (currentAnim.flippedX == true) ? true : false;
            spriteRenderer.flipY = (currentAnim.flippedY == true) ? true : false;
            timeBetweenAnimChange = (float)1/currentAnim.framesPerSecond;

            if (!currentAnim.fixedFrameRate)
            {
                timeBetweenAnimChange *= currentAnim.animationFrames[0, 1];
            }
            spriteRenderer.sprite = sprites[currentAnim.animationFrames[0, 0]]; // Set animation to first sprite
        }
    }

    void PlayAnimation()
    {
        timePassed += Time.deltaTime;

        while (timePassed >= timeBetweenAnimChange){
            currentFrame++;

            if (currentFrame > (currentAnim.animationFrames.GetLength(0) - 1))
            {
                if (!currentAnim.loopAnimation)
                {
                    StopAnimation();
                    return;
                } 
                else
                {
                    currentFrame = 0;
                }
            }

            spriteRenderer.sprite = sprites[currentAnim.animationFrames[currentFrame, 0]];
            timePassed -= timeBetweenAnimChange;

            if (!currentAnim.fixedFrameRate)
            {
                timeBetweenAnimChange = ((float)1/currentAnim.framesPerSecond) * currentAnim.animationFrames[currentFrame, 1];
            }
        }
    }

    public void StopAnimation()
    {
        currentAnim = null;
        spriteRenderer.sprite = sprites[defaultSprite];
    }

    void Update() 
    {
        if (currentAnim != null)
        {
            PlayAnimation();
        }
    }

    public class Simple2DAnimation
    {
        public string animationName;
        public int[,] animationFrames;
        public int framesPerSecond;
        public bool loopAnimation;
        public bool fixedFrameRate;
        public bool flippedX;
        public bool flippedY;

        public Simple2DAnimation(string animationName, int[,] animationFrames, int framesPerSecond, bool loopAnimation, bool fixedFrameRate, bool flippedX, bool flippedY)
        {
            this.animationName = animationName;
            this.animationFrames = animationFrames;
            this.framesPerSecond = framesPerSecond;
            this.loopAnimation = loopAnimation;
            this.fixedFrameRate = fixedFrameRate;
            this.flippedX = flippedX;
            this.flippedY = flippedY;
        }
    }
}