using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Entidades
{
    public class Funciones
    {
        public int FuncionId { get; set; }
        public Horarios Horario { get; set; }

        //public Horarios HorarioFinal { get; set; }
        public DateTime Fecha { get; set; }
        public int Subtitulo { get; set; }
        public Peliculas Pelicula { get; set; }
        public Salas Sala { get; set; }

        public string FuncionCompleta
        {
            get { return $"{Fecha.ToString("d")} - {Horario.HorarioCompleto} - Sala N°: {Sala.SalaId.ToString()}"; }
        
            
        }


        public Funciones()
        {
            Horario = new Horarios();
            Pelicula = new Peliculas();
            Sala = new Salas();
           

        }

    


    }
}
