using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using Lab11.Models;

namespace Lab11
{
    public partial class About : Page
    {
	    readonly GameStoreEntities2 _db = new GameStoreEntities2();

        public List<string> GameStoreTypes = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            GameStoreTypes = _db.Tables.OrderBy(x => x.Type).Select(x => x.Type).ToList();

            totalGamesCost.InnerText = _db.Tables.Sum(x => x.Price).ToString();
        }

        public IEnumerable<Table> GetGames()
        {
            return _db.Tables;
        }

        public double GetSize(string type)
        {
            var total = _db.Tables.Sum(x => x.Price);
            var typeTotal = _db.Tables.Where(x => x.Type == type).Sum(x => x.Price);
            var percent = (double)typeTotal / (double)total * 200;
            return percent;
        }

        public string Text = "";

        protected void Calculate(object sender, EventArgs e)
        {
            var total = _db.Tables.Sum(x => x.Price);
            var typeTotal = _db.Tables.Where(x => x.Type == "RPG").Sum(x => x.Price);
            var percent = 100 * (double)typeTotal / (double)total;
            Text = Math.Round(percent, 2).ToString(CultureInfo.CurrentCulture);
            percentOfGames.InnerText = Text;
        }
    }
}