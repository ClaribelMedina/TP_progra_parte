using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Entidades
{
    public class Salas
    {
        public int SalaId { get; set; }
        public TipoSalas TipoSala { get; set; }

        //public string TipoSalaCompleto
        //{
        //    get { return $"{TipoSala.TipoSalasId.ToString()} - {TipoSala.Tipo.ToString()}"; }

        //}

        

        public Salas()
        {
            
            TipoSala = new TipoSalas();
        }

        public Salas(int salaId,TipoSalas tiposala)
        {
            this.SalaId = salaId;
            this.TipoSala = tiposala;
            
        }

    }
}
