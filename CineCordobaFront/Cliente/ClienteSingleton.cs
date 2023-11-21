using CineCordobaBack.Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaFront.Cliente
{
    public class ClienteSingleton
    {
        private static ClienteSingleton instancia;
        private HttpClient httpCliente;

        private ClienteSingleton()
        {
            httpCliente = new HttpClient();
                
        }

        public static ClienteSingleton ObtenerIntancia() 
        { 
            if (instancia == null)
            {
                instancia = new ClienteSingleton();

            }
            return instancia;
        }



        //get

        public async Task<string> GetAsync(string url)
        {
            var result = await httpCliente.GetAsync(url);
            var content = "";
            if(result.IsSuccessStatusCode)
            {
                content = await result.Content.ReadAsStringAsync();
            }
            return content;
        }

       

        // Post
        public async Task<string> PostAsync(string url, Comprobantes comprobante)
        {
            // Serializar el objeto Comprobantes, incluyendo la lista de detalles
            string comprobanteJson = JsonConvert.SerializeObject(comprobante);

            // Configurar el contenido de la solicitud con el objeto serializado
            StringContent content = new StringContent(comprobanteJson, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST
            var result = await httpCliente.PostAsync(url, content);
            var response = "";

            // Verificar si la solicitud fue exitosa
            if (result.IsSuccessStatusCode)
            {
                // Leer la respuesta del servicio web
                response = await result.Content.ReadAsStringAsync();
            }

            return response;
        }



    }
}
