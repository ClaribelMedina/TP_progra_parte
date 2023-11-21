using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Entidades
{
    public class Peliculas
    {
        public int PeliculaId { get; set; }  //cambiar a id_pelicula
        public string NombrePelicula { get; set; }

        public Peliculas()
        {
            
        }

        public Peliculas(int peliId, string nombre)
        {
            this.PeliculaId = peliId;
            this.NombrePelicula = nombre;
        }
    }
}
