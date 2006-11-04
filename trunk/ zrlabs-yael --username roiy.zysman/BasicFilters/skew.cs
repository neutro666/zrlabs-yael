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
//  public class SkewFilter : BasicFilter
//  {
//    #region IFilter Members
//    public const string RIGHT_SHIFT_TOKEN_NAME = "RightShift";
//    public const string UP_SHIFT_TOKEN_NAME = "UpShift";

//    private int _rightShift = 200; //Defualts
//    private int _upShift = 20;


//    public override Image ExecuteFilter(Image rawImage, params filterProperties)
//    {
//      if ((filterProperties != null) && (filterProperties[RIGHT_SHIFT_TOKEN_NAME] != null))
//      {
//        _rightShift = Convert.ToInt32(filterProperties[RIGHT_SHIFT_TOKEN_NAME]);
//      }
//      if ((filterProperties != null) && (filterProperties[UP_SHIFT_TOKEN_NAME] != null))
//      {
//        _upShift = Convert.ToInt32(filterProperties[UP_SHIFT_TOKEN_NAME]);
//      }

//      Bitmap result = new Bitmap(rawImage.Width + Math.Abs(_rightShift), rawImage.Height + Math.Abs(_upShift));
//      Graphics g = Graphics.FromImage(result);
//      g.InterpolationMode = InterpolationMode.HighQualityBicubic;
//      //Rectangle rect = new Rectangle(0+_rightShift,0+_upShift,
//      Point[] points = new Point[3];
//      int horShiftCorrections = 0;
//      if (_rightShift <= 0)
//      {
//        horShiftCorrections = _rightShift * (-1);
//      }
//      points[0] = new Point(horShiftCorrections + _rightShift, 0);
//      points[1] = new Point(horShiftCorrections + _rightShift + rawImage.Width, 0 + _upShift);
//      points[2] = new Point(horShiftCorrections, rawImage.Height + _upShift);

//      //points[3] = new Point(rawImage.Width, rawImage.Height);
//      try
//      {

//        Pen myPen = new Pen(Color.Blue, 1);
//        Pen myPen2 = new Pen(Color.Red, 1);

//        // Create an array of points.
//        Point[] myArray =
//             {
//                 new Point(20, 20),
//                 new Point(120, 50),
//                 new Point(120, 90),
//                 new Point(20, 120),
//                 new Point(20,20)
//             };

//        // Draw the Points to the screen before applying the
//        // transform.
//        //g.DrawLines(myPen, myArray);

//        // Create a matrix and scale it.
//        Matrix myMatrix = new Matrix();
//        //myMatrix.Scale(1.5F, 1.5F, MatrixOrder.Append);
        
//        //myMatrix.TransformPoints(myArray);
//        //myMatrix.Shear(0, 0);

//        // Draw the Points to the screen again after applying the
//        // transform.
//        //g.DrawLines(myPen2, myArray);

//        Matrix mat = new Matrix();

//        mat.Shear(0.0F, -0.2F);
//        //g.DrawRectangle(new Pen(Color.Green), 0, 0, 100, 50);
//        g.ScaleTransform(0.4F, 0.5F);
//        g.MultiplyTransform(mat);
//        //g.DrawRectangle(new Pen(Color.Red), 50, 50, 150, 100);
        
//        //g.DrawEllipse(new Pen(Color.Blue), 0, 0, 100, 50);
//        g.DrawImage(rawImage,new Point(0,100));
//        //mat.Translate(10.0F, 29.0F);
        
//        //mat.TransformPoints(
        
//        //g.MultiplyTransform(myMatrix);
//        //g.DrawImage(rawImage, 0, 0);

//      }
//      catch (Exception e)
//      {

//      }
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
