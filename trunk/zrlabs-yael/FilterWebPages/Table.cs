using System;
using System.Collections.Generic;
using System.Text;

namespace FiltersManualPages
{
  abstract class  HTMLObj
  {
    protected string _innerText;
    protected string _innerHtml;
    protected string _class;

    public string InnerText
    {
      get { return _innerText; }
      set { _innerText = value; }
    }


    public string Class
    {
      get { return _class; }
      set { _class = value; }
    }

    public abstract string GetInnerHtml();


	
  }

  class Table : HTMLObj
  {
    private List<Row> _rows = new List<Row>();

    public List<Row> Rows 
    {
      get
      {
        return _rows;
      }
    }

    public override string GetInnerHtml()
    {
      
      _innerHtml = "<Table class=\"" + _class + "\">\r\n";
      if(_rows.Count >0)
      {
        foreach (Row row in _rows)
        {
          _innerHtml += row.GetInnerHtml();
        }
      }
      _innerHtml += "</table>\r\n";
      return _innerHtml;

    }



  }

  class Row :HTMLObj
  {
    private  List<Cell> _cells = new List<Cell>();
    public List<Cell> Cells
    {
      get
      {
        return _cells;
      }
    }
    public override string GetInnerHtml()
    {
      _innerHtml = "<tr class=\"" + _class + "\">";
      if (_cells.Count > 0)
      {
        foreach (Cell cell in _cells)
        {
          _innerHtml += cell.GetInnerHtml();
        }
      }
      _innerHtml += "</tr>\r\n";
      return _innerHtml;
    }

    
  }

  class Cell : HTMLObj
  {
    public override string GetInnerHtml()
    {
      _innerHtml = "<td class=\"" + _class + "\">";
      _innerHtml += _innerText;
      _innerHtml += "</td>\r\n";

      return _innerHtml;
      
      }
  }
}
