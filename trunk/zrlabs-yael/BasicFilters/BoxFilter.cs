using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using ZRLabs.Yael.Interfaces;
using System.Collections.Specialized;
namespace ZRLabs.Yael.BasicFilters
{

  /// <summary>
  /// Summary description for Class1.
  /// </summary>
  public class BoxFilter : BasicFilter
  {
    #region TODOs
    //TODO: Add the option to paint images instead
    //of colors on the side of the box
    #endregion TODOs

    #region Private Fields
    private int _boxDepth = 10; //Defualt
    private Color _boxStartColor = Color.DarkBlue; //Defualt
    private Color _boxEndColor =  Color.LightBlue; //Defualt
    #endregion Private Fields

    #region Filter Properties
    /// <summary>
    /// Defines the "3d" depth of the box
    /// </summary>
    public int BoxDepth
    {
      get
      {
        return _boxDepth;
      }
      set
      {
        _boxDepth = value;
      }
    }
    /// <summary>
    /// Sets the starting color of the box gradient brush
    /// </summary>
    public Color BoxStartColor
    {
      get
      {
        return _boxStartColor;
      }
      set
      {
        _boxStartColor = value;
      }
    }
    /// <summary>
    /// Sets the ending color of the box gradient brush
    /// </summary>
    public Color BoxEndColor
    {
      get
      {
        return _boxEndColor;
      }
      set
      {
        _boxEndColor = value;
      }
    }
    #endregion Filter Properties

    #region Public Filter Methods
    /// <summary>
    /// Executes this filter on the input image and returns the result
    /// </summary>
    /// <param name="inputImage">input image</param>
    /// <returns>transformes image into a boxed layout</returns>
    public override Image ExecuteFilter(Image inputImage)
    {

      Bitmap raw = (Bitmap)inputImage;
      double alpha = Math.PI / 6; //30deg
      //Setting up the box 3d depth values.
      int sideWidth = _boxDepth;
      int sideHeight = raw.Height;
      int topWidth = raw.Width;
      int topHeight = sideWidth;
      int totalWidth = (int)(sideWidth * Math.Cos(alpha) + raw.Width * Math.Cos(alpha));
      int totalHeight = (int)(raw.Height + raw.Width * Math.Sin(alpha) + sideWidth * Math.Sin(alpha));

      //Set up the new canvas
      Bitmap result = new Bitmap(totalWidth,totalHeight);
      Graphics g = Graphics.FromImage(result);
      g.InterpolationMode = InterpolationMode.HighQualityBicubic;
      //Set background
      g.FillRectangle(new SolidBrush(BackGroundColor), 0, 0, result.Width, result.Height);
      
      //FrontSide
      //Point rightBottom = new Point((int)(raw.Width * Math.Cos(alpha)) , raw.Height - (int)(raw.Width * Math.Sin(alpha)) + yAlign);
      Point leftTop = new Point((int)(sideWidth*Math.Cos(alpha)),(int)((sideWidth+raw.Width)*Math.Sin(alpha)));
      Point leftBottom = new Point((int)(sideWidth * Math.Cos(alpha)), raw.Height + (int)((sideWidth + raw.Width) * Math.Sin(alpha)));
      Point rightTop = new Point((int)((raw.Width+sideWidth) * Math.Cos(alpha)) , (int)(sideWidth*Math.Sin(alpha)));
      g.DrawImage(raw, new Point[] { leftTop, rightTop, leftBottom });

      
      //TopSide
      Point topUpperRight = new Point(rightTop.X - (int)(topHeight * Math.Cos(alpha)), rightTop.Y - (int)(topHeight * Math.Sin(alpha)));
      Point topLowerRight = new Point(rightTop.X, rightTop.Y);
      Point topLowerLeft = new Point(leftTop.X, leftTop.Y);
      Point topUpperLeft = new Point(leftTop.X - (int)(sideWidth * Math.Cos(alpha)), leftTop.Y - (int)(sideWidth * Math.Sin(alpha)));
      Point[] top = new Point[4];
      top[0] = topUpperLeft;
      top[1] = topUpperRight;
      top[2] = topLowerRight;
      top[3] = topLowerLeft;
      LinearGradientBrush topBrush = new LinearGradientBrush(topLowerRight, topUpperLeft, _boxStartColor, _boxEndColor);
      g.FillPolygon(topBrush, top);

      //LeftSide
      Point sideUpperRight = new Point(leftTop.X, leftTop.Y);
      Point sideLowerRight = new Point(leftBottom.X, leftBottom.Y);
      Point sideLowerLeft = new Point(leftBottom.X - (int)(sideWidth * Math.Cos(alpha)), leftBottom.Y - (int)(sideWidth * Math.Sin(alpha)));
      Point sideUpperLeft = new Point(leftTop.X - (int)(sideWidth * Math.Cos(alpha)), leftTop.Y - (int)(sideWidth * Math.Sin(alpha)));
      Point[] side = new Point[4];
      side[0] = sideUpperLeft;
      side[1] = sideUpperRight;
      side[2] = sideLowerRight;
      side[3] = sideLowerLeft;
      LinearGradientBrush sideBrush = new LinearGradientBrush(sideUpperLeft, sideLowerRight, _boxStartColor, _boxEndColor);
      g.FillPolygon(sideBrush, side);
      return result;
    }
    #endregion Public Filter Methods
  }
}
