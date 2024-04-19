using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace CSE445ASN5
{
    public partial class form1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookie = Request.Cookies["myCookieId"];                                        //prints cookie word when page is loaded in
            if((myCookie == null) || (myCookie["word"] == ""))
            {
                Label5.Text = "None";
            }
            else { Label5.Text = myCookie["word"]; }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = TextBox1.Text;
            string url = @"http://webstrar32.fulton.asu.edu/page2/Service1.svc/wordFilter?word=" + str;     //change this depending on your localhost

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(responseStream);                         //gets the json from the webservice
            String json = reader.ReadToEnd();

            results p = JsonConvert.DeserializeObject<results>(json);                       //deserializes the webservice
            Label1.Text = p.words;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string str1 = Label1.Text;
            string url1 = @"http://webstrar32.fulton.asu.edu/page2/Service4.svc/definitions?text=" + str1;     //change this depending on your localhost

            HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create(url1);
            WebResponse response1 = request1.GetResponse();
            Stream responseStream1 = response1.GetResponseStream();

            StreamReader reader1 = new StreamReader(responseStream1);                         //gets the json from the webservice
            String json1 = reader1.ReadToEnd();

            defData f = JsonConvert.DeserializeObject<defData>(json1);                       //deserializes the webservice
            string text = " ";

            for (int i = 0; i < f.definitions.Length; i++)                                  //outputs array of synonyms
            {
                text += f.definitions[i];
                if (i != f.definitions.Length - 1)
                {
                    text += "            ";
                }
            }
            if (f.definitions.Length == 0)
            {
                text = "No listed synonyms";
            }
            HttpCookie myCookie = new HttpCookie("myCookieId");
            myCookie["word"] = f.word;
            myCookie.Expires = DateTime.Now.AddDays(3);
            Response.Cookies.Add(myCookie);

            Label2.Text = f.word;
            Label3.Text = text;
            Label4.Text = f.shortestDef;
            Label5.Text =  myCookie["word"];

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string str2 = Label1.Text;
            string url2 = @"http://webstrar32.fulton.asu.edu/page2/Service3.svc/synonymData?text=" + str2;     //change this depending on your localhost

            HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(url2);
            WebResponse response2 = request2.GetResponse();
            Stream responseStream2 = response2.GetResponseStream();

            StreamReader reader2 = new StreamReader(responseStream2);                         //gets the json from the webservice
            String json2 = reader2.ReadToEnd();

            data dat = JsonConvert.DeserializeObject<data>(json2);                       //deserializes the webservice
            string text = " ";
            string text2 = " ";

            for (int i = 0; i < dat.synonyms.Length; i++)                                  //outputs array of synonyms
            {
                text += dat.synonyms[i];
                if (i != dat.synonyms.Length - 1)
                {
                    text += ", ";
                }
            }
            if (dat.synonyms.Length == 0)
            {
                text = "No listed synonyms";
            }

            for (int i = 0; i < dat.antonyms.Length; i++)                             //outputs array of antonyms
            {
                text2 += dat.antonyms[i];
                if (i != dat.antonyms.Length - 1)
                {
                    text2 += ", ";
                }
            }
            if (dat.antonyms.Length == 0)
            {
                text2 = "No listed antonyms";
            }
            Label6.Text = dat.word;
            Label7.Text = text;
            Label8.Text = text2;

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string f = " ";
            if (Cache["cachedDataDepend"] == null)          //utilized from in class example
            {
                string d = File.ReadAllText(Server.MapPath("TextFile1.txt"));
                Label9.Text = d;
                f = Label1.Text;
                Cache.Insert("cachedDataDepend", f, new CacheDependency(Server.MapPath("TextFile1.txt")),
                DateTime.Now.AddSeconds(200), Cache.NoSlidingExpiration);                                               //inserts into cache



            }
            else
            {
                Label9.Text = Cache["cachedDataDepend"].ToString();                                                     //prints what is in teh chache
            }
        }
    }
    public class data                                        //results data contract for results
    {
        public string[] synonyms { get; set; }
        public string[] antonyms { get; set; }
        public string word { get; set; }
    }
    public class results                                        //results data contract for results
    {
        public string words { get; set; }

    }
    public class defData
    {
        public string word { get; set; }
        public string[] definitions { get; set; }
        public string shortestDef { get; set; }
    }
}