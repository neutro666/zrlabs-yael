using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ZRLabs.Yael.Interfaces;
using ZRLabs.Yael.BasicFilters;

namespace FiltersManualPages
{
  class SamplesTable
  {

    public static string GetTable()
    {
      Image myImg = Bitmap.FromFile("1.jpg");
      Image transformed;
      Table myTable = new Table();

      //Resize Filter
      ZRLabs.Yael.BasicFilters.ResizeFilter resize = new ResizeFilter();
      transformed = resize.ExecuteFilter(myImg);
      Row resizeFilter = new Row();
      Cell resizeSimpleCell = new Cell();
      resizeSimpleCell.InnerText = "transformed = resize.ExecuteFilter(myImg);";
      resizeFilter.Cells.Add(resizeSimpleCell);
      myTable.Rows.Add(resizeFilter);
      return myTable.GetInnerHtml();

    }
   

  }
}
