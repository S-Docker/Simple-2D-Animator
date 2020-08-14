using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple2DAnimator : MonoBehaviour
{
    public Texture2D spriteTexture;
    private SpriteRenderer _spriteRenderer;
    private Sprite[] _sprites;
    
    // Default sprite settings
    public int defaultSprite = 0;
    public string sortingLayer = "Default";
    public int sortOrder = 1;

    List<Simple2DAnimation> _anims = new List<Simple2DAnimation>();
    Simple2DAnimation _currentAnimation;
    int _currentFrame = 0;
    float _timeBetweenAnimChange;
    float _timePassed = 0f;

    void OnValidate() 
    {
        LoadSprites();
        SetUpSpriteRenderer();
    }

    void LoadSprites()
    {
        Object[] data = Resources.LoadAll<Sprite>("Sprites/" + spriteTexture.name.ToString());
        _sprites = new Sprite[data.Length];

        for (int i = 0; i < data.Length; i++)   
        {
            _sprites[i] = (Sprite)data[i];
        }
    }

    void SetUpSpriteRenderer()
    {
        if (this.GetComponent<SpriteRenderer>() != null)
        {
            _spriteRenderer = this.GetComponent<SpriteRenderer>();
        } 
        else if (this.GetComponentInChildren<SpriteRenderer>() != null)
        {
            _spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        } 
        else 
        {
            _spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
        }

        _spriteRenderer = this.GetComponent<SpriteRenderer>();
        _spriteRenderer.sortingLayerName = sortingLayer;
        _spriteRenderer.sortingOrder = sortOrder;
        _spriteRenderer.sprite = _sprites[defaultSprite];
    }

    public void CreateFixedSpacingAnimation(string animationName, int[] animationFrames, int framesPerSecond = 30, bool loopAnimation = true, bool flippedX = false, bool flippedY = false)
    {
        int[,] temp = ConvertArrayTo2D(animationFrames); // convert 1d array to 2d array for compatibility

        Simple2DAnimation anim = new Simple2DAnimation(animationName, temp, framesPerSecond, loopAnimation, true, flippedX, flippedY);
        _anims.Add(anim);
    }
    
    public void CreateVariableSpacingAnimation(string animationName, int[,] animationFrames, int framesPerSecond = 30, bool loopAnimation = true, bool flippedX = false, bool flippedY = false)
    {
        Simple2DAnimation anim = new Simple2DAnimation(animationName, animationFrames, framesPerSecond, loopAnimation, false, flippedX, flippedY);
        _anims.Add(anim);
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
        _currentAnimation = _anims.Find(item => item.animationName.Contains(animationName));

        if (_currentAnimation != null)
        {
            _spriteRenderer.flipX = (_currentAnimation.flippedX == true) ? true : false;
            _spriteRenderer.flipY = (_currentAnimation.flippedY == true) ? true : false;
            _timeBetweenAnimChange = (float)1/_currentAnimation.framesPerSecond;

            if (!_currentAnimation.fixedFrameRate)
            {
                _timeBetweenAnimChange *= _currentAnimation.animationFrames[0, 1];
            }
            _spriteRenderer.sprite = _sprites[_currentAnimation.animationFrames[0, 0]]; // Set animation to first sprite
        }
    }

    void PlayAnimation()
    {
        _timePassed += Time.deltaTime;

        while (_timePassed >= _timeBetweenAnimChange){
            _currentFrame++;

            if (_currentFrame > (_currentAnimation.animationFrames.GetLength(0) - 1))
            {
                if (!_currentAnimation.loopAnimation)
                {
                    StopAnimation();
                    return;
                } 
                else
                {
                    _currentFrame = 0;
                }
            }

            _spriteRenderer.sprite = _sprites[_currentAnimation.animationFrames[_currentFrame, 0]];
            _timePassed -= _timeBetweenAnimChange;

            if (!_currentAnimation.fixedFrameRate)
            {
                _timeBetweenAnimChange = ((float)1/_currentAnimation.framesPerSecond) * _currentAnimation.animationFrames[_currentFrame, 1];
            }
        }
    }

    public void StopAnimation()
    {
        _currentAnimation = null;
        _spriteRenderer.sprite = _sprites[defaultSprite];
    }

    void Update() 
    {
        if (_currentAnimation != null)
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