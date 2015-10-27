using services.lacage.be.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace api.lacage.be.Controllers
{
    public class ListItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public bool listed { get; set; }
        public bool bought { get; set; }
        public DateTime boughtOn { get; set; }
        public string comment { get; set; }
    }


    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            if (HttpContext.Current.Application["products"]  == null)
            {
                var z = new ListService();
                var y = z.GetAllListItems();
               var listItems = new List<ListItem>();
               listItems.AddRange(new[]{
                new ListItem{id = 0,
                    name= "Coca-Cola",
                    category= "Drank",
                    listed= false,
                    bought= true,
                    boughtOn= DateTime.MinValue,
                    comment= ""
               },
                new ListItem{   id = 1,
                    name= "Cola Zero",
                    category= "Drank",
                    listed= true,
                    bought= true,
                    boughtOn= DateTime.MinValue,
                    comment= ""
                },
                new ListItem{   id = 2,
                    name= "Appels",
                    category= "Fruit en Groenten",
                    listed= true,
                    bought= true,
                    boughtOn= DateTime.MinValue,
                    comment= "Pink ladies"
                },               
                new ListItem{   id = 3,
                    name= "Yoghurt",
                    category= "Zuivel",
                    listed= true,
                    bought= true,
                    boughtOn= DateTime.MinValue,
                    comment= ""
                },              
                new ListItem{   id = 4,
                    name= "Danio",
                    category= "Zuivel",
                    listed= true,
                    bought= true,
                    boughtOn= DateTime.MinValue,
                    comment= ""
                }}             
                );
                HttpContext.Current.Application["products"] = listItems;
            }


            //return products;//new string[] { "value1", "value2" };
            return this.Request.CreateResponse(
                HttpStatusCode.OK, HttpContext.Current.Application["products"]);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post(ListItem listItem)
        {
            //var products = (Array)HttpContext.Current.Session["products"];
            var listItems = (List<ListItem>)HttpContext.Current.Application["products"];
            int ix = listItems.FindIndex(i=>i.id==listItem.id);
            //var item = listItems.FirstOrDefault(i => i.id == listItem.id);
            listItems[ix]=listItem;                
            HttpContext.Current.Application["products"] = listItems;
        }

        // PUT api/values/5
        public void Put(ListItem listItem)
        {
            var listItems = (List<ListItem>)HttpContext.Current.Application["products"];
            listItems.Add(listItem);
            HttpContext.Current.Application["products"] = listItems;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}