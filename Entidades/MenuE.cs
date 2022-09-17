using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{

    public class MenuE
    {
        public int id_nivel { get; set; }
        public string desc { get; set; }
        public int? id_padre { get; set; }
        public string url { get; set; }
    }

    public class MenuNuevoE
    {
        public int? MenuId { get; set; }
        public int? ParentMenuId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string CssFont { get; set; }




    }
    public class ListaPuntos
    {

        public string puntosObtenidos { get; set; }
        public string correo { get; set; }
        public string correoE { get; set; }


    }
    public class ListaCasillero
    {

        public int codiCasillero { get; set; }
    

    }

    public class ListaValorFob
    {

        public double valorAcumulado { get; set; }
      


    }
}
