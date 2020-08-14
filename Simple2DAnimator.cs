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
            Debug.Log("Sprite check");
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

    public void CreateAnimation(string animationName, int[] animationFrames, int framesPerSecond = 30, bool flippedX = false, bool flippedY = false, bool loopAnimation = false)
    {
        Simple2DAnimation anim = new Simple2DAnimation(animationName, animationFrames, framesPerSecond, flippedX, flippedY, loopAnimation);
        _anims.Add(anim);
    }

    public void StartAnimation(string animationName)
    {
        _currentAnimation = _anims.Find(item => item.animationName.Contains(animationName));

        if (_currentAnimation != null)
        {
            _spriteRenderer.flipX = (_currentAnimation.flippedX == true) ? true : false;
            _spriteRenderer.flipY = (_currentAnimation.flippedY == true) ? true : false;
            _timeBetweenAnimChange = (float)1/_currentAnimation.framesPerSecond;

            _spriteRenderer.sprite = _sprites[_currentAnimation.animationFrames[0]]; // Set animation to first sprite
            _currentFrame++;
        }
    }

    void PlayAnimation()
    {
        _timePassed += Time.deltaTime;

        if (_timePassed >= _timeBetweenAnimChange){
            _currentFrame++;
            if (_currentFrame > _currentAnimation.animationFrames.Length - 1)
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

            _spriteRenderer.sprite = _sprites[_currentAnimation.animationFrames[_currentFrame]];
            _timePassed -= _timeBetweenAnimChange;
        }
    }

    public void StopAnimation()
    {
        _currentAnimation = null;
        _spriteRenderer.sprite = _sprites[defaultSprite];
    }

    void Update() {
        if (_currentAnimation != null)
        {
            PlayAnimation();
        }
    }

    public class Simple2DAnimation
    {
        public string animationName;
        public int[] animationFrames;
        public int framesPerSecond;
        public bool flippedX;
        public bool flippedY;
        public bool loopAnimation;


        public Simple2DAnimation(string animationName, int[] animationFrames, int framesPerSecond, bool flippedX, bool flippedY, bool loopAnimation)
        {
            this.animationName = animationName;
            this.animationFrames = animationFrames;
            this.framesPerSecond = framesPerSecond;
            this.flippedX = flippedX;
            this.flippedY = flippedY;
            this.loopAnimation = loopAnimation;
        }
    }
}
