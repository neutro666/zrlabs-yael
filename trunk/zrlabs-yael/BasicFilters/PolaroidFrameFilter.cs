using System;
using System.Drawing;
using ZRLabs.Yael.Interfaces;
using System.Collections.Specialized;

namespace ZRLabs.Yael.BasicFilters
{
  /// <summary>
  /// PolaroidFilterFrameFilter
  /// </summary>
  public class PolaroidFrameFilter : BasicFilter
  {
    #region Private Fields
    private int _width = 100;
    private int _height = 100;
    private int _borderWidth = 20;
    private int _borderHeight = 50;
    private string _caption = "Caption"; //Default
    private Color _captionColor = Color.FromArgb(210, 0, 0, 0);
    private bool _withDropShadow = true;
    #endregion Private Fields

    #region Filter Properties
    public string Caption
    {
      get
      {
        return _caption;
      }
      set
      {
        _caption = value;
      }
    }
    public Color CaptionColor
    {
      get
      {
        return _captionColor;
      }
      set
      {
        _captionColor = value;
      }
    }
    public bool WithDropShadow
    {
      get 
      { 
        return _withDropShadow; 
      }
      set 
      { 
        _withDropShadow = value; 
      }
    }
    #endregion Filter Properties

    #region Public Filter Methods

    /// <summary>
    /// Executes this filter on the input image and returns the result
    /// </summary>
    /// <param name="inputImage">input image</param>
    /// <returns>transformed image</returns>
    public override Image ExecuteFilter(Image rawImage)
    {

      ResizeFilter myFilter = new ResizeFilter();
      myFilter.Width = _width;
      myFilter.Height = _height;

      Image bpSrc = myFilter.ExecuteFilter(rawImage);
      Bitmap bm = new Bitmap(bpSrc.Width + _borderWidth, bpSrc.Height + _borderHeight);

      Graphics g = Graphics.FromImage(bm);
      g.DrawImage(bpSrc, _borderWidth/2, 10);
      g.DrawRectangle(new Pen(Color.Black), 0, 0, bpSrc.Width + _borderWidth - 1, bpSrc.Height + _borderHeight - 1);

      int[] sizes = new int[] { 128, 64, 32, 16, 14, 12, 10, 8, 6, 4 };

      Font crFont = null;
      SizeF crSize = new SizeF();
      string text = _caption;
      int xCenterOfImg = (_width+_borderWidth)/2;
      int yPosFromBottom = 10 + _height;

      //Loop through the defined sizes checking the length of the Copyright string
      //If its length in pixles is less then the image width choose this Font size.
      for (int i = 0; i < sizes.Length; i++)
      {
        //set a Font object to Arial (i)pt, Bold
        crFont = new Font("Lucida Handwriting", sizes[i], FontStyle.Bold);
        //Measure the Copyright string in this Font
        crSize = g.MeasureString(text, crFont);

        if ((ushort)crSize.Width < (ushort)_width)
          break;
      }

      //Define the text layout by setting the text alignment to centered
      StringFormat StrFormat = new StringFormat();
      StrFormat.Alignment = StringAlignment.Center;

      //define a Brush which is semi trasparent black (Alpha set to 153)
      SolidBrush semiTransBrush = new SolidBrush(_captionColor);

      //Draw the Copyright string
      g.DrawString(text,                 //string of text
        crFont,                                   //font
        semiTransBrush,                           //Brush
        new PointF(xCenterOfImg + 1, yPosFromBottom + 1),  //Position
        StrFormat);

      //If drop shadow is turned on..
      if (WithDropShadow)
      {
        DropShadowFilter drFilter = new DropShadowFilter();
        bm = (Bitmap)drFilter.ExecuteFilter((Image)bm);
      }

      return bm;//

    }
    #endregion Public Filter Methods
  }
}
