using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CityNoise
{
    public partial class getFeature : System.Web.UI.Page
    {

        double[, ] B = new double[1200, 23];
        int[] rm = new int[1954];
        int[, ] D_weekday = new int[1200, 25];
        int[, ] D_weekend = new int[1200, 25];
        protected void Page_Load(object sender, EventArgs e)
        {
            string method = Request.QueryString["method"];
            readFeature();
            if (method == "roadFeature")  //this method is useless, change it to getRoadFeature.aspx
            {
                string returnString = "0";
                for (int r = 1; r <= 1953; r++)
                {
                    if (rm[r] == 0)
                    {
                        for (int f = 1; f <= 22; f++)
                        {
                            returnString += " 0";
                        }

                    }
                    else
                    {
                        for (int f = 1; f <= 22; f++)
                        {
                            returnString += " " + B[rm[r], f];
                        }
                    }
                }
                Response.Write(returnString);
            }
            else if (method == "checkinData")
            {

                int tID = Convert.ToInt32(Request.QueryString["tID"]);
                int wID = Convert.ToInt32(Request.QueryString["wID"]);
                int[] returnValue = new int[1200];
                if (wID == 0)
                {
                    if (tID == 0)
                    {
                        for (int r = 1; r <= 1199; r++)
                            for (int t = 1; t<= 24; t++)
                            {
                                returnValue[r] += D_weekday[r, t];
                            }
                    }
                    else
                    {
                        for (int r = 1; r <= 1199; r++)
                        {
                            returnValue[r] = D_weekday[r, tID];
                        }
                    }
                }
                else
                {
                    if (tID == 0)
                    {
                        for (int r = 1; r <= 1199; r++)
                            for (int t = 1; t <= 24; t++)
                            {
                                returnValue[r] += D_weekend[r, t];
                            }
                    }
                    else
                    {
                        for (int r = 1; r <= 1199; r++)
                        {
                            returnValue[r] = D_weekend[r, tID];
                        }
                    }
                }

                string returnString = "0";
                for (int r = 1; r <= 1953; r++)
                {
                    if (rm[r] == 0)
                    {
                        returnString += " 0";
                    }
                    else
                    {
                        returnString += " " + returnValue[rm[r]];
                    }
                }
                Response.Write(returnString);

            }
        }

        void readFeature()
        {
            StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "/tensorData/B.txt");
            for (int i = 1; i <= 1199; i++)
                for (int j = 1; j <= 22; j++)
                    {
                        B[i, j] = Convert.ToDouble(sr.ReadLine());
                    }
            sr.Close();
            sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "/tensorData/RM.txt");
            for (int i = 1; i <= 1953; i++)
            {
                rm[i] = Convert.ToInt32(sr.ReadLine().Trim());
            }
            sr.Close();
            sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "/tensorData/D_weekday.txt");
            for (int i = 1; i <= 1199; i++)
                for (int j = 1; j <= 24; j++)
                {
                    D_weekday[i, j] = Convert.ToInt32 (sr.ReadLine());
                }
            sr.Close();
            sr = new StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "/tensorData/D_weekend.txt");
            for (int i = 1; i <= 1199; i++)
                for (int j = 1; j <= 24; j++)
                {
                    D_weekend[i, j] = Convert.ToInt32(sr.ReadLine());
                }
            sr.Close();
        }
    }
}