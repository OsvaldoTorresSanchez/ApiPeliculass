using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class GetAll_PController : Controller
    {
        // GET: GetAll_P
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Popular peliculas = new ML.Popular();
            peliculas.populars = new List<Object>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");

                var responseTask = client.GetAsync("movie/popular?api_key=06a108d9592d140077cf03d529b44cfe");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic json = JObject.Parse(readTask.Result.ToString());

                    foreach (var resultItem in json.results)
                    {
                        ML.Popular peliList = new ML.Popular();
                        peliList.id = resultItem.id;
                        peliList.poster_path = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;
                        peliList.original_title = resultItem.original_title;
                        peliList.overview = resultItem.overview;

                        peliculas.populars.Add(peliList);

                    }
                }
            }
            return View(peliculas);

        }

        public ActionResult Favorito( int media_id)
        {
            ML.Popular pelicula = new ML.Popular();
            pelicula.populars = new List<Object>();
            //ML.Result results = new ML.Result();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                string apiKey = "api_key=06a108d9592d140077cf03d529b44cfe";
                string sessionId = "session_id=f556da11090f2bf266c33b01ac0f6307e1269582";
                var value = "{ \"media_type\":\"movie\",\"media_id\": "+ media_id + ", \"favorite\": true}";
                var body= new StringContent(value, System.Text.Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync("account/20430032/favorite?" + apiKey + "&" + sessionId, body);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se ha ingresado correctamente la pelicula";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = "no se ingresado correctemnte la pelicula , Error: " + result.StatusCode;
                }
                //GetFavorites();
            }

            return View("GetAll");
        }

        
        [HttpGet]
        public ActionResult GetFavorites()
        {
            ML.Popular peliculas = new ML.Popular();
            peliculas.populars = new List<Object>();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");

                var responseTask = client.GetAsync("account/20425861/favorite/movies?api_key=06a108d9592d140077cf03d529b44cfe&session_id=f556da11090f2bf266c33b01ac0f6307e1269582");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    dynamic json = JObject.Parse(readTask.Result.ToString());

                    foreach (var resultItem in json.results)
                    {
                        ML.Popular peliList = new ML.Popular();
                        peliList.id = resultItem.id;
                        peliList.poster_path = "https://www.themoviedb.org/t/p/w600_and_h900_bestv2" + resultItem.poster_path;
                        peliList.original_title = resultItem.original_title;
                        peliList.overview = resultItem.overview;

                        peliculas.populars.Add(peliList);

                    }
                }
            }
            return View(peliculas);
        }
        public ActionResult DeleteFavorito(int media_id)
        {
            ML.Popular pelicula = new ML.Popular();
            pelicula.populars = new List<Object>();
            //ML.Result results = new ML.Result();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                string apiKey = "api_key=06a108d9592d140077cf03d529b44cfe";
                string sessionId = "session_id=f556da11090f2bf266c33b01ac0f6307e1269582";
                var value = "{ \"media_type\":\"movie\",\"media_id\": " + media_id + ", \"favorite\": false}";
                var body = new StringContent(value, System.Text.Encoding.UTF8, "application/json");
                var responseTask = client.PostAsync("account/20430032/favorite?" + apiKey + "&" + sessionId, body);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se ha ingresado correctamente la pelicula";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = "no se ingresado correctemnte la pelicula , Error: " + result.StatusCode;
                }
                //GetFavorites();
            }

            return View("GetAll");
        }

    }
}