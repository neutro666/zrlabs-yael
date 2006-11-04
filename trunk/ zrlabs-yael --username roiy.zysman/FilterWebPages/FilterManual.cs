using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Drawing;
using System.IO;
using ZRLabs.Yael.BasicFilters;
using ZRLabs.Yael.Interfaces;
//using BasicFilters;

namespace FiltersManualPages
{
  public class FilterManual
  {
    BasicFilter _filter = null;
    string _name = "";

    


    #region Private
    
    

    private Image GetTransformedImage(Image examplePic)
    {
      Image bm;
      try
      {
        bm = _filter.ExecuteFilterDemo(examplePic);

      }
      catch
      {
        bm = Bitmap.FromFile("NoDemo.jpg");
      }
      return bm;
    }

    #endregion Private


    #region Public

    public void SaveDemoImage(string path, Image demoImage)
    {
      if (!Directory.Exists(path))
      {
        try
        {
          Directory.CreateDirectory(path);

        }
        catch (Exception e)
        {
          Console.WriteLine(e.ToString());

        }

      }
      GetTransformedImage(demoImage).Save(path.TrimEnd(new char[] { '/' }) + "/" + GetDemoImageFileName(), System.Drawing.Imaging.ImageFormat.Png);
    }

    public FilterManual(ZRLabs.Yael.BasicFilters.BasicFilter myFilter, string name)
    {
      _filter = myFilter;
      _name = name;
    }

    public string  GetDemoImageFileName()
    {
      return _name + ".png";
    }

    public string GetFilterCellHTML()
    {
      string content = "";
      content += "<tr><td><table><tr>";
      content += "<td>" + _name + "</td>" + "<td><img src=\"images/" + GetDemoImageFileName() + "\"></td>";
      content += "</tr></table></td></tr>";
      return content;
    }

    #endregion Public

  }
}
