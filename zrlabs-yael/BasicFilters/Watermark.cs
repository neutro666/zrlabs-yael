using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;
using ZRLabs.Yael.Interfaces;

namespace ZRLabs.Yael.BasicFilters
{
  /// <summary>
  /// Summary description for Class1.
  /// </summary>
  public class TextWatermarkFilter : BasicFilter
  {
    #region Public Properties Tokens
    public const string WIDTH_TOKEN_NAME = "Width";
    public const string HEIGHT_TOKEN_NAME = "Height";
    public const string ALPHA_TOKEN_NAME = "ALPHA";
    #endregion Public Properties Tokens
    
    #region Private Fields
    private int _width; //Defualts
    private int _height;
    private int _alpha = 75;
    private Color _captionColor = Color.White;
    private string _caption = "Test";
    private int _textSize = 10; //default
    private bool _automaticTextSizing = false;
    #endregion Private Fields

    #region Filter Properties
    public HAlign Halign = HAlign.Bottom;   
    public VAlign Valign = VAlign.Center;

    public int TextSize
    {
      get { return _textSize; }
      set { _textSize = value; }
    }

    public bool AutomaticTextSize
    {
      get { return _automaticTextSizing; }
      set { _automaticTextSizing = value; }
    }

    public int CaptionAlpha
    {
      get
      {
        return _alpha;
      }
      set
      {
        _alpha = value;
      }
    }


    public Color CaptionColor
    {
      get { return _captionColor; }
      set { _captionColor = value; }
    }



    public string Caption
    {
      get { return _caption; }
      set { _caption = value; }
    }

	  

    #endregion Filter Properties

    #region Public Functions
    public override Image ExecuteFilter(Image rawImage)
    {
      
      _width = rawImage.Width;
      _height = rawImage.Height;

      //create a Bitmap the Size of the original photograph
      Bitmap bmPhoto = new Bitmap(rawImage.Width, rawImage.Height, PixelFormat.Format24bppRgb);

      bmPhoto.SetResolution(rawImage.HorizontalResolution, rawImage.VerticalResolution);

      //load the Bitmap into a Graphics object 
      Graphics grPhoto = Graphics.FromImage(bmPhoto);

      //Set the rendering quality for this Graphics object
      grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

      //Draws the photo Image object at original size to the graphics object.
      grPhoto.DrawImage(
        rawImage,                               // Photo Image object
        new Rectangle(0, 0, _width, _height), // Rectangle structure
        0,                                      // x-coordinate of the portion of the source image to draw. 
        0,                                      // y-coordinate of the portion of the source image to draw. 
        _width,                                // Width of the portion of the source image to draw. 
        _height,                               // Height of the portion of the source image to draw. 
        GraphicsUnit.Pixel);                    // Units of measure 


      //-------------------------------------------------------
      //Set up the automatic font settings
      //-------------------------------------------------------
      int[] sizes = new int[] { 128, 64, 32, 16, 14, 12, 10, 8, 6, 4 };

      Font crFont = null;
      SizeF crSize = new SizeF();


      if (_automaticTextSizing)
      {
        //If automatic sizing is turned on 
        //loop through the defined sizes checking the length of the caption string string
        for (int i = 0; i < sizes.Length; i++)
        {
          //set a Font object to Arial (i)pt, Bold
          crFont = new Font("arial", sizes[i], FontStyle.Bold);
          //Measure the Copyright string in this Font
          crSize = grPhoto.MeasureString(_caption, crFont);
          if ((ushort)crSize.Width < (ushort)_width)
            break;
        }
      }
      else
      {
        crFont = new Font("arial", _textSize, FontStyle.Bold);
        
      }
      crSize = grPhoto.MeasureString(_caption, crFont);

      //Since all photographs will have varying heights, determine a 
      //position 5% from the bottom of the image
      int yPixelsMargin = (int)(_height * .0002);

      //Now that we have a point size use the caption string height 
      //to determine a y-coordinate to draw the string of the photograph
      float yPosFromBottom;
      if (Halign == HAlign.Bottom)
        yPosFromBottom = ((_height) - (crSize.Height));
      else if(Halign == HAlign.Top)
        yPosFromBottom = (yPixelsMargin);
      else //center
        yPosFromBottom = (_height/2-(crSize.Height/2));

      //Determine X position
      float xPositionFromLeft ;
      if (Valign == VAlign.Right)
        xPositionFromLeft = (_width - (crSize.Width));
      else if (Valign == VAlign.Left)
        xPositionFromLeft = 0;//(xPixelsMargin);
      else  //center
        xPositionFromLeft = (_width / 2 - (crSize.Width / 2));


      //Define the text layout by setting the text alignment to centered
      StringFormat StrFormat = new StringFormat();
      //StrFormat.Alignment = StringAlignment.Near;

      //define a Brush which is semi trasparent black (Alpha set to 153)
      SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(_alpha, 0, 0, 0));

      //Draw the Copyright string
      grPhoto.DrawString(_caption,                 //string of text
        crFont,                                   //font
        semiTransBrush2,                           //Brush
        new PointF(xPositionFromLeft + 1, yPosFromBottom + 1),  //Position
        StrFormat);

      //define a Brush which is semi trasparent white (Alpha set to 153)
      
      SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(_alpha, _captionColor.R,_captionColor.G,_captionColor.B));

      //Draw the Copyright string a second time to create a shadow effect
      //Make sure to move this text 1 pixel to the right and down 1 pixel
      grPhoto.DrawString(_caption,                 //string of text
        crFont,                                   //font
        semiTransBrush,                           //Brush
        new PointF(xPositionFromLeft, yPosFromBottom),  //Position
        StrFormat);                               //Text alignment



      grPhoto.Dispose();

      return bmPhoto;
    }

    public override Image ExecuteFilterDemo(Image rawImage)
    {
      this.Caption = "Caption Demo";
      this.TextSize = 18;
      this.AutomaticTextSize = false;
      this.Halign = HAlign.Bottom;
      this.Valign = VAlign.Right;
      return this.ExecuteFilter(rawImage);
    }


    public enum VAlign
    {
      Left,
      Center,
      Right
    }
    public enum HAlign
    {
      Top,
      Middle,
      Bottom
    }

    #endregion Public Functions

    #region Private
    /// <summary>
    /// For ease of use and to avoid the dot net internal's implicit far near center ..
    /// </summary>
    /// <param name="alignment">alignment</param>
    /// <returns>A StringAlignment value</returns>
    private StringAlignment VAlignToStringAlignment(VAlign alignment)
    {
      switch (alignment)
      {
        case VAlign.Right:
          return StringAlignment.Near;
        case VAlign.Center:
          return StringAlignment.Center;
        case VAlign.Left  :
          return StringAlignment.Far;
      }
      return StringAlignment.Center;
    }
    #endregion Private
  }
}
