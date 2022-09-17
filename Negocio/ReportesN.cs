using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

namespace Negocio
{
    public class ReportesN
    {
        public ReportesE ReporteCotizacionN(string cnn, string strCliente)
        {
            return new ReportesD().ReporteCotizacionD(cnn, strCliente);
        }
        public ReporteLcl ReporteCotizacionLcl(string cnn, string strCliente)
        {
            return new ReportesD().ReporteCotizacionLcl(cnn, strCliente);
        }

        public List<ReportesE> ConsultaCotizacionN(string cnn, DateTime? strFechaInicio, DateTime? strFechaFin, string strUsuario)
        {
            return new ReportesD().ConsultaCotizacionD(cnn,strFechaInicio,strFechaFin, strUsuario);
        }

        public List<ReporteLcl> ConsultaCotizacionLcl(string cnn, DateTime? strFechaInicio, DateTime? strFechaFin, string strUsuario)
        {
            return new ReportesD().ConsultaCotizacionCLcl(cnn, strFechaInicio, strFechaFin, strUsuario);
        }
    }
}
