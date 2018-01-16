using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_notas
{

    
    class Nota
    {

        private string titulo, contenido;
        private int id;
        public Nota(int id,string titulo,string contenido)
        {
            this.id = id;
            this.titulo = titulo;
            this.contenido = contenido;
        }

        public string getTitulo()
        {
            return this.titulo;
        }

        public string getContenido()
        {
            return this.contenido;
        }

        public int getId()
        {
            return this.id;
        }


    }
}
