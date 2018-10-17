using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using conecta.Models;
using Newtonsoft.Json;

namespace conecta.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        async Task<string> GetData(string url, Models.Solicitud Slc)
        {
            HttpClient Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(Slc, Newtonsoft.Json.Formatting.None,new JsonSerializerSettings {NullValueHandling=NullValueHandling.Ignore });
            Debug.WriteLine(json);
            var contenido = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            var resultado = Client.PostAsync(url, contenido).Result;

            var resultadoBody = await resultado.Content.ReadAsStringAsync();
            return resultadoBody;
        }

        // Home/Consultar
        public ActionResult Consultar()
        {
            Models.Solicitud Solicitud = new Models.Solicitud();
            Solicitud.test = true;
            Solicitud.language = "es";
            Solicitud.command = "PING";
            Solicitud.merchant = new Models.DataAcceso { apiLogin = "pRRXKOl8ikMmt9u", apiKey = "4Vj8eK4rloUd272L48hsrarnUA" };


            Task<string> consulta = GetData("https://sandbox.api.payulatam.com/reports-api/4.0/service.cgi", Solicitud);
            var resultado = consulta.Result;
            Debug.WriteLine(resultado);
            Models.Respuesta miRespuesta = JsonConvert.DeserializeObject<Models.Respuesta>(consulta.Result);
            return View(miRespuesta);
        }

        // Home/Tarjeta
        public ActionResult Tarjeta() 
        {
            //Uso: Registrar tarjeta (obtener token)
            Models.Solicitud Solicitud = new Models.Solicitud();
            //Solicitud.test = true;
            Solicitud.language = "es";
            Solicitud.command = "CREATE_TOKEN";
            Solicitud.merchant = new Models.DataAcceso { apiLogin = "pRRXKOl8ikMmt9u", apiKey = "4Vj8eK4rloUd272L48hsrarnUA" };
            Solicitud.creditCardToken = new Models.CreditCardToken
            {
                payerId = 12,
                name = "APPROVED",
                identificationNumber = 9999227,
                paymentMethod = "VISA",
                number = "4715039438621498",
                expirationDate = "2018/12",
            };

            Task<string> consulta = GetData("https://sandbox.api.payulatam.com/payments-api/4.0/service.cgi", Solicitud);           
            Models.Respuesta miRespuesta = JsonConvert.DeserializeObject<Models.Respuesta>(consulta.Result);
            return View(miRespuesta);
        }

        // Home/Cobro
        public ActionResult Cobrar()
        {
            //Usar para autorización y captura (obtiene id de Transacción)
            Models.Solicitud Solicitud = new Models.Solicitud();
            Solicitud.test = true;
            Solicitud.language = "es";
            Solicitud.command = "SUBMIT_TRANSACTION";
            
            Solicitud.merchant = new Models.DataAcceso { apiLogin = "pRRXKOl8ikMmt9u", apiKey = "4Vj8eK4rloUd272L48hsrarnUA" };
            Solicitud.transaction = new Models.Transaction
            {
                order = new Order
                {
                    accountId = 512322,
                    referenceCode = "payment_test_1",
                    description = "Wilson The Duke 2",
                    language = "es",
                    signature = "95d7e92b23cae0cae6a98e34cc54be39",
                    notifyUrl = "www.test.com",
                    additionalValues = new AdditionalValues { TX_VALUE=new TXVALUE { value=100, currency="ARS"} },

                    buyer=new Buyer
                    {
                        merchantBuyerId= 508029,
                        fullName= "APPROVED",
                        emailAddress= "notorious@mail.com",
                        contactPhone= "11111111",
                        dniNumber= "5415668464654",
                        shippingAddress= new Address
                        {
                            street1= "Fighter",
                            street2= "1366",
                            city= "Salta",
                            state= "Salta",
                            country= "AR",
                            postalCode= "000000",
                            phone= "7563126"
                        }
                    },
                },

                
                creditCardTokenId = "f9b2b4ce-6735-4458-b9dd-081c88be77a9",
                creditCard=new CreditCard { securityCode="438"},
                extraParameters=new ExtraParameters { INSTALLMENTS_NUMBER=1},
                type= "AUTHORIZATION_AND_CAPTURE",
                paymentMethod= "VISA",
                paymentCountry= "AR",
                payer= new Payer { fullName="APPROVED"},
                deviceSessionId= "vghs6tvkcle931686k1900o6e5",
                ipAddress= "127.0.0.2",
                cookie="pt1t38347bs6jc9ruv2ecpv7o2",
                userAgent= "Mozilla/5.0 (Windows NT 5.1; rv:18.0) Gecko/20100101 Firefox/18.0"
            };

            Task<string> consulta = GetData("https://sandbox.api.payulatam.com/payments-api/4.0/service.cgi", Solicitud);
            Models.Respuesta miRespuesta = JsonConvert.DeserializeObject<Models.Respuesta>(consulta.Result);
            return View(miRespuesta);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}