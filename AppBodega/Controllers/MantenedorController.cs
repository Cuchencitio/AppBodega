using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using AppBodega.Models;
using System.ComponentModel;
using System.Security.Policy;

namespace AppBodega.Controllers
{
    public class MantenedorController : Controller
    {
        IFirebaseClient cliente;

        public MantenedorController()
        {
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "GED817lBkiJmvyl5Qad7kJTbufTqOcWiNoopkSCL",
                BasePath = "https://appbodega-1d915-default-rtdb.firebaseio.com/"
            };
            cliente = new FirebaseClient(config);
        }
        // GET: Mantenedor
        public ActionResult Inicio()
        {
            Dictionary<string, Item> lista = new Dictionary<string, Item>();
            FirebaseResponse response = cliente.Get("Item");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)            
                lista = JsonConvert.DeserializeObject<Dictionary<string, Item>>(response.Body);

            List<Item> items = new List<Item>();
            foreach (KeyValuePair<string, Item> elemento in lista)
            {
                items.Add(new Item()
                {
                    Id = elemento.Key,
                    RUT = elemento.Value.RUT,
                    Nombre = elemento.Value.Nombre,
                    Apellido = elemento.Value.Apellido,
                    FechadeNacimiento = elemento.Value.FechadeNacimiento,
                    MedicoTratante = elemento.Value.MedicoTratante,
                    Fechahora = elemento.Value.Fechahora
                });

            }
            
            return View(items);
        }
        public ActionResult Crear()
        {
            return View();
        }
        public ActionResult Editar(string idItem)
        {
            FirebaseResponse response = cliente.Get("Item/" + idItem);
            Item oItem = response.ResultAs<Item>();
            oItem.Id = idItem;
            return View(oItem);
        }
        public ActionResult Eliminar(string idItem)
        {
            FirebaseResponse response = cliente.Delete("Item/" + idItem);

            return RedirectToAction("Inicio", "Mantenedor");
        }
        [HttpPost]
        public ActionResult Crear(Item oItem)
        {
            string idItem = Guid.NewGuid().ToString("N");
            SetResponse response = cliente.Set("Item/" + idItem, oItem);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("RegistroExitoso", "Mantenedor");
            } else
            {
                return View();
            }            
        }
        [HttpPost]
        public ActionResult Editar(Item oitem)
        {
            string idProducto = oitem.Id;
            oitem.Id = null;
            FirebaseResponse response = cliente.Update("Item/" + idProducto, oitem);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Inicio", "Mantenedor");
            }
            else
            {
                return View();
            }
        }
        public ActionResult RegistroExitoso()
        {
            return View();
        }
    }
}