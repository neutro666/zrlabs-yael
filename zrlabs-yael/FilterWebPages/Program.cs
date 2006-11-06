using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Reflection;
using System.Drawing;
using System.IO;
using ZRLabs.Yael.BasicFilters;
using ZRLabs.Yael.Interfaces;
//using BasicFilters;


namespace FiltersManualPages
{
  class Program
  {

    private const string IMAGES_TABLE_TOKEN = "__IMAGES_TABLE__";
    static void Main(string[] args)
    {
      SamplesTable samplesGenerator = new SamplesTable();
      //Console.WriteLine();
      StreamWriter sr = new StreamWriter("Samples.html", false);
      sr.AutoFlush = true;
      sr.Write(samplesGenerator.GetTable());
      sr.Close();
      string frontPageTemplate = LoadHtmlTemplate();
      string filtersContent = "";
      Assembly myDll = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + @"/zrlabs.Yael.BasicFilters.dll");
      FilterManual manual;
      NameValueCollection filterCells = new NameValueCollection();
      
      Console.WriteLine(myDll.FullName);
      filtersContent += "<table>";
      foreach (Module mod in myDll.GetLoadedModules())
      {
        Console.WriteLine(mod.Name);
      }

      Image myImg = Bitmap.FromFile("1.jpg");
      foreach (Type type in myDll.GetTypes())
      {
        try
        {

          BasicFilter myFilter = (BasicFilter)Activator.CreateInstance(type);
          manual = new FilterManual(myFilter, type.Name);
          Console.WriteLine(type.Name + " " + manual.GetFilterCellHTML());
          filterCells.Add(type.Name, manual.GetFilterCellHTML());
          manual.SaveDemoImage("images/", myImg);
          //manual.
          
          //foreach (MethodInfo prop in type.GetMethods())
          //{
          //  Console.WriteLine(prop.Name);
          //}
          //myFilter.ExecuteFilter(myImg, null).Save("c:/temp/12_" + type.Name + ".jpg");
        }
        catch(Exception e)
        {
          Console.WriteLine(e.ToString());
        }
      }
      foreach (string key in filterCells)
      {
        filtersContent += filterCells[key] + "\r\n";

      }
      string processedHtmlContent = LoadHtmlTemplate().Replace(IMAGES_TABLE_TOKEN,filtersContent);
      SaveFront(processedHtmlContent);
      return;
    }

    public static string LoadHtmlTemplate()
    {
      string htmlTemplate;
      StreamReader sw = new StreamReader("FrontTemplate.html");
      htmlTemplate = sw.ReadToEnd();
      return htmlTemplate;
    }

    private static void SaveFront(string content)
    {
      try
      {
        StreamWriter sw = new StreamWriter("front.html", false);
        sw.Write(content);
        sw.Close();
      }
      catch(Exception e)
      {
        Console.WriteLine(e.ToString());

      }


    }


  }
}
