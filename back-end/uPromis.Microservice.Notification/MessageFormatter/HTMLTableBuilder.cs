using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uPromis.Microservice.Notification.MessageFormatter
{
    public class HTMLTableBuilder<TListItem>
    {
        private readonly StringBuilder sb = new StringBuilder();

        /// <summary>
        /// Add a title to the table. Call before creating the table
        /// </summary>
        /// <param name="title"></param>
        public void TableTitle(string title)
        {
            sb.Append(@"<br/>"); //sb.Append(@"<hr/>");
            sb.Append(@"<h3>");
            sb.Append(title);
            sb.Append("</h3>");
        }

        /// <summary>
        /// Starts the table definition
        /// </summary>
        public void StartTable()
        {
            sb.Append(@"<table class=""table table-striped"">");
            sb.Append("<tbody>");
        }

        /// <summary>
        /// End the table definition
        /// </summary>
        public void CloseTable()
        {
            sb.Append("</tbody>");
            sb.Append("</table>");
        }

        /// <summary>
        /// Add a header column
        /// </summary>
        /// <param name="title"></param>
        public void AddHeader(string title)
        {
            sb.Append(@"<th>");
            sb.Append(title);
            sb.Append("</th>");
        }
        /// <summary>
        /// Add a table cell with a string value
        /// </summary>
        /// <param name="value"></param>
        public void AddCell(string value)
        {
            sb.Append(@"<td>");
            sb.Append(value);
            sb.Append("</td>");
        }

        /// <summary>
        /// Add a table cell with a URL to a controller/action
        /// </summary>
        /// <param name="baseURL"></param>
        /// <param name="Controller"></param>
        /// <param name="Action"></param>
        /// <param name="ID"></param>
        /// <param name="title"></param>
        public void AddCell(string baseURL, string URL, string title)
        {
            sb.Append(@"<td>");
            sb.Append(string.Format(@"<a href='{0}/{1}'>{2}</a>", baseURL, URL, title));
            sb.Append("</td>");
        }

        /// <summary>
        /// Add a table cell with a formatted date time value
        /// </summary>
        /// <param name="dt"></param>
        public void AddCell(DateTime? dt)
        {
            sb.Append(@"<td>");
            if (dt.HasValue)
            {
                sb.AppendFormat("{0:dd/MMM/yyyy}", dt.Value);
            }
            else
            {
                sb.Append("(empty)");
            }
            sb.Append("</td>");
        }

        /// <summary>
        /// Add a table cell with a formatted amount value
        /// </summary>
        /// <param name="amount"></param>
        public void AddCell(Decimal? amount)
        {
            sb.Append(@"<td>");
            if (amount.HasValue)
            {
                sb.AppendFormat("{0:€ #,##0.00}", amount.Value);
            }
            else
            {
                sb.Append("€ 0.00");
            }
            sb.Append("</td>");
        }

        /// <summary>
        /// Add a new line to the table
        /// </summary>
        public void OpenHeader()
        {
            sb.Append(@"<tr>");
        }

        /// <summary>
        /// End of line for a table row
        /// </summary>
        public void CloseHeader()
        {
            sb.Append("</tr>");
        }

        /// <summary>
        /// Start a new line to the table, with a css class for that line
        /// </summary>
        /// <param name="CSSclass"></param>
        public void OpenLine()
        {
            sb.Append(@"<tr>");
        }

        /// <summary>
        /// End of line for a table row
        /// </summary>
        public void CloseLine()
        {
            sb.Append("</tr>");
        }

        /// <summary>
        /// Return the HTML that was built up
        /// </summary>
        /// <returns></returns>
        public string GetHTML()
        {
            return sb.ToString();
        }

        /// <summary>
        /// Utility method to create a HTML table in one function call
        /// </summary>
        /// <param name="title">Title of the table</param>
        /// <param name="columntitles">string array with the names of the column headers</param>
        /// <param name="list">IEnumerable list of items to put in the table</param>
        /// <param name="filler">Delegate of lambda that fills each line of the table. Do this by calling AddCell on the ht parameter, using the item that is passed in the delegate/lambda</param>
        /// <returns>The HTML table as a string</returns>
        public string Build(string title, string[] columntitles, IEnumerable<TListItem> list, Action<TListItem, HTMLTableBuilder<TListItem>> filler)
        {
            HTMLTableBuilder<TListItem> ht = this;

            ht.TableTitle(title);

            ht.StartTable();
            ht.OpenHeader();
            foreach (var item in columntitles)
            {
                ht.AddHeader(item);
            }
            ht.CloseHeader();

            foreach (var item in list)
            {
                ht.OpenLine();
                filler(item, ht);
                ht.CloseLine();
            }
            ht.CloseTable();

            return ht.GetHTML();
        }
    }
}
