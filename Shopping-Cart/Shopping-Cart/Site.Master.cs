﻿using AppProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shopping_Cart
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
            {
                if (Session["cart"]!= null)
                {
                    Cart cart = (Cart)Session["cart"];
                    DataListProducts.DataSource = cart.items;
                    DataListProducts.DataBind();
                }
            }
        }

        protected void btnRemoveFromCart_Command(object sender, CommandEventArgs e)
        {
            if(e.CommandName == "removeFromCart")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                Product product = new Product();
                product.Id = id;
                Cart cart = null;
                if (Session["cart"] != null)
                {
                    cart = (Cart)Session["cart"];
                }
                Cart newCart = new Cart(cart);
                if (newCart.RemoveFromCart(product, -1)) 
                {
                    Session["cart"] = newCart;
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    Response.Write(@"<script language='javascript'>alert('Item not Removed');</script>");

                }
            }
        }
    }
}