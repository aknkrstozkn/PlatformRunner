using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaintingController : MonoBehaviour
{   
    [SerializeField] GameObject gameOverUI = null;
    [SerializeField] GameObject scoreText = null;
    [SerializeField] GameObject paintLabel = null;
    [SerializeField] GameObject countDownText = null;
    [SerializeField] Color brushColor = Color.red;
    [SerializeField] int brushSize = 5;
    [SerializeField] Color wallColor = Color.grey;
    [SerializeField] float paintingCounter = 20f;
    private Texture2D _texture2D;
    private Vector2 _textureSize;
    private RectTransform _rect;    
    private SpriteRenderer _spriteRenderer;
    private Sprite _sprite;
    private Color[] _pixelData;
    private int _pixelCount;
    private Vector2 _pixelCordinate;
    private List<Vector2> _coloredPixels;
    private bool _isPainting = false;
    void Start()
    {   
        transform.parent.GetComponent<WallController>().enabled = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rect = GetComponent<RectTransform>();
        _sprite = _spriteRenderer.sprite;
        _texture2D = _sprite.texture as Texture2D;
        _pixelData = _texture2D.GetPixels();
        _pixelCount = _pixelData.Length;    
        
        _textureSize = new Vector2(((int)_rect.rect.width * 10), ((int)_rect.rect.height * 10));
        _coloredPixels = new List<Vector2>();
        
        for(int i = 0; i < (int)_textureSize.x; ++i)
        {
            for(int j = 0; j <= (int)_textureSize.y; ++j)
            {
                _texture2D.SetPixel(i, j, wallColor);
            }
        }
        _texture2D.Apply();

        paintLabel.GetComponent<TextMeshProUGUI>().text = "You have " + paintingCounter + " seconds to PAINT! befor Wall returns back!";
        paintLabel.SetActive(true);
        
    }

    private bool CalcPixelCordinate()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {   if (hit.transform != null && hit.transform.tag == "Wall")            
            {
               var x = Mathf.Abs(_textureSize.x - (_textureSize.x / 2 - (int)(Mathf.Round(hit.point.x * 10))));
               var tempY = 4 - transform.position.y;              
               var y = Mathf.Abs((_textureSize.y / 2 - (int)Mathf.Round((hit.point.y + tempY) * 10)) - _textureSize.y / 2);
               if (x >= 0 && x < _textureSize.x && y >= 0 && y < _textureSize.y)
               {    
                   _pixelCordinate = new Vector2(x, y);
                   return true;
               }              
            }
        }

        return false;
    }

    private void Draw()
    {
      int x = (int)_pixelCordinate.x;
      int y = (int)_pixelCordinate.y;
      for (int i = (x - brushSize); i <= (x + brushSize); ++i)
      {
          for (int j = (y - brushSize); j <= (y + brushSize); ++j)
          {
              if(Vector2.Distance(new Vector2(x, y), new Vector2(i, j)) <= brushSize)
              {
                var pointVector = new Vector2(i, j);
                if(!_coloredPixels.Contains(pointVector))
                {
                    if (i >= 0 && i < _textureSize.x && j >= 0 && j < _textureSize.y)
                    {
                        _texture2D.SetPixel(i, j, brushColor);
                        _coloredPixels.Add(pointVector); 
                    }                                       
                }
              }
          }
      }
      scoreText.GetComponent<TextMeshProUGUI>().text = ((int)(((decimal)_coloredPixels.Count / (decimal)_pixelCount) * 100m) + " %").ToString();
      _texture2D.Apply();
    }

    private void Update() {
        
        if(_isPainting && paintingCounter > 0)
        {
            paintingCounter -= Time.deltaTime;
            countDownText.GetComponent<TextMeshProUGUI>().text = ((int)paintingCounter).ToString();
        }else if (_isPainting && paintingCounter <= 0)
        {
            transform.parent.GetComponent<WallController>().enabled = true;
            countDownText.SetActive(false);
        }


        // Win
        if (_pixelCount == _coloredPixels.Count)
        {            
            scoreText.SetActive(false);
            gameOverUI.SetActive(true);
            paintLabel.SetActive(false);
            countDownText.SetActive(false);

            transform.parent.GetComponent<WallController>().enabled = false;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Action();                     
        }
        else if(Input.GetMouseButton(0)) 
        {
            if(Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
            {         
                Action();                
            }
        }
    }

    private void Action()
    {
        if(!_isPainting)
        {
            paintLabel.SetActive(false);
            scoreText.SetActive(true);
            countDownText.SetActive(true);
            _isPainting = true;
        }
        if(CalcPixelCordinate())
        {
            Draw();
        }
    }
}
