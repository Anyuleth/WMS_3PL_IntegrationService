using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.ENTITY.PedidosCompras
{
    [XmlRootAttribute(ElementName = "pedido_compra", IsNullable = false)]

    [Serializable]
    public class PedidosCompras
    {
        [XmlElement("encabezado")]
        public Encabezaddo Encabezado { get; set; }

        [XmlElement("detalle")]
        public Detalle Detalle { get; set; }

     
    }

    
    public class Detalle
    {
        [XmlElement("linea")]
        public List<Lineas> Lineas { get; set; }
    }

  
    public class Encabezaddo
    {
        [XmlElement("empresa")]
        public string Empresa { get; set; }
        [XmlElement("idpedido")]
        public string IdPedido { get; set; }

        [XmlElement("serie")]
        public string Serie { get; set; }

        [XmlElement("documento")]
        public string Documento { get; set; }

        [XmlElement("tipo")]
        public string Tipo { get; set; }

        [XmlElement("fecha_transmision")]
        public DateTime Fecha_transmision { get; set; }

        [XmlElement("fecha_entrada")]
        public DateTime Fecha_Entrada { get; set; }

     }

   
    public class Lineas
    {
        //[XmlElement("idpedido")]
        //public string IdPedido { get; set; }
        [XmlElement("linea_numero")]
        public int linea_Numero { get; set; }

        [XmlElement("producto")]
        public string Producto { get; set; }

        [XmlElement("bodega")]
        public string Bodega { get; set; }

        [XmlElement("cantidad")]
        public double Cantidad { get; set; }


    }

    


}
