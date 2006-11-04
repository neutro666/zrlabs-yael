//using System;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using ZRLabs.Yael.Interfaces;
//using System.Collections.Specialized;

//namespace ZRLabs.Yael.BasicFilters
//{
//  /// <summary>
//  /// Summary description for Class1.
//  /// </summary>
//  public class FloorReflectionFilter : BasicFilter
//  {
//    #region IFilter Members
//    public const string WIDTH_TOKEN_NAME = "WIDTH";
//    public const string HEIGHT_TOKEN_NAME = "HEIGHT";

//    //Defualts
//    private int _width = 200;
//    private int _height = 200;
//    private int _alphaStartValue=110;
//    private double _alphaDecreaseRate = 0.4;


//    public override Image ExecuteFilter(Image rawImage, NameValueCollection filterProperties)
//    {
//      if ((filterProperties != null) && (filterProperties[WIDTH_TOKEN_NAME] != null))
//      {
//        _width = Convert.ToInt32(filterProperties[WIDTH_TOKEN_NAME]);
//        _width = Convert.ToInt32(filterProperties[WIDTH_TOKEN_NAME]);
//      }
//      if ((filterProperties != null) && (filterProperties[HEIGHT_TOKEN_NAME] != null))
//      {
//        _height = Convert.ToInt32(filterProperties[HEIGHT_TOKEN_NAME]);
//      }
//      _width = rawImage.Width;
//      _height = rawImage.Height;
//      int reflectionRows =(int) ((_alphaStartValue)/_alphaDecreaseRate);
//      Bitmap result = new Bitmap(_width, _height+reflectionRows);
//      Graphics g = Graphics.FromImage(result);
//      g.InterpolationMode = InterpolationMode.HighQualityBicubic;
//      g.DrawImage(rawImage, 0, 0, (float)_width, (float)_height);
//      Color pixelColor, newColor;
//      Bitmap raw = (Bitmap)rawImage;
//      int i, j;
//      try
//      {
//        for (i = 0; i < reflectionRows - 1; i++)
//        {
//          for (j = 0; j < _width - 1; j++)
//          {
//            pixelColor = raw.GetPixel(j,_height-i-1);
//            newColor = Color.FromArgb(_alphaStartValue-i, pixelColor.R, pixelColor.G, pixelColor.B);
//            g.DrawRectangle(new Pen(newColor), j, i + _height-1, 1, 1);
//          }
//        }
//      }
//      catch (Exception e)
//      {
//        Console.WriteLine(e.ToString());
//      }
//      //for (int i=1 ToolboxBitmapAttribute 
//      Pen myTestPen = new Pen(Color.FromArgb(100, 5, 5, 5));
//      //g.FillRectangle(new SolidBrush(Color.FromArgb(100, 5, 5, 5)), 0, 375, 150, 100);
//      BasicFilter mat = new MatrixTransformFilter();
      
//      return mat.ExecuteFilter(result, null);
//      //BasicFilter resizeFilter = new ResizeFilter();
//      //return resizeFilter.ExecuteFilter(result,null);

//      //Color myColor = result.GetPixel(1, 1);
//      return result;
//    }


//    public override string GetHelpText(bool extended)
//    {
//      return "";
//    }

//    public Image FilterDemo(Image rawImage, NameValueCollection filterProperties)
//    {
//      return this.ExecuteFilter(rawImage, null);

//    }
//    #endregion
//  }
//}
