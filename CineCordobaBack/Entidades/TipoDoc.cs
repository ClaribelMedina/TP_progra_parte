using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineCordobaBack.Entidades
{
    public class TipoDoc
    {
        public int TipoDocId { get; set; }
        public string TipoDocumento { get; set; }

        public TipoDoc()
        {
            //TipoDocId = 0;
            //TipoDocumento = string.Empty;
        }
    }
}
