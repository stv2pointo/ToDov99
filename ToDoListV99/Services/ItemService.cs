using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using ToDoListV99.Models;

namespace ToDoListV99.Services
{
    public class ItemService : IItemService
    {

        public void deleteItem(int id)
        {

            HttpClient client = new HttpClient();

            // the following gives the protocol, host and port
            client.BaseAddress = new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/api/items/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.DeleteAsync(id.ToString()).Result;


        }
    }
}