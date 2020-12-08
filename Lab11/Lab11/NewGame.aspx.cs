using System;
using System.Web.UI;
using Lab11.Models;

namespace Lab11
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void insertButton_Click(object sender, EventArgs e)
        {
            var newGame = new Table();


            if (ename.Text.Length > 40)
            {
                ModelState.AddModelError("Name", "Название слишком длинное");
            }

            newGame.Name = ename.Text;
            newGame.Type = etype.Text;

            if (Convert.ToInt32(eprice.Text) < 0)
            {
                ModelState.AddModelError("Price", "Стоимость должна быть больше нуля");
            }

            newGame.Price = Convert.ToInt32(eprice.Text);


            if (!ModelState.IsValid) 
	            return;

            using (var db = new GameStoreEntities2())
            {
	            db.Tables.Add(newGame);
	            db.SaveChanges();
            }

            Response.Redirect("~/Games");
        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Games");
        }

        protected void expenceInserted(object sender, EventArgs e)
        {
            
        }
    }
}