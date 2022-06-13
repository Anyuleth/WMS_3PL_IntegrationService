using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WMS_3PL_IntegrationService.ENTITY
{
    

    public class Articulo
    {
        [XmlElement("empresa")]
        public string Empresa { get; set; }

        [XmlElement("producto")]
        public string Producto { get; set; }

        [XmlElement("barcode")]
        public string Barcode { get; set; }

        [XmlElement("descripcion_1")]
        public string Descripcion_1 { get; set; }

        [XmlElement("categoria")]
        public string Categoria { get; set; }

        [XmlElement("subcategoria")]
        public string SubCategoria { get; set; }

        [XmlElement("uom")]
        public string Uom { get; set; }

        [XmlElement("referencia")]
        public string Referencia { get; set; }

        [XmlElement("estilo")]
        public string Estilo { get; set; }

        [XmlElement("color")]
        public string Color { get; set; }

        [XmlElement("talla")]
        public string Talla { get; set; }

        [XmlElement("marca")]
        public string Marca { get; set; }

        [XmlElement("temporada")]
        public string Temporada { get; set; }

        [XmlElement("familia")]
        public string Familia { get; set; }
    }

    [XmlRootAttribute(ElementName = "lista_articulos", IsNullable = false)]
    public class Articulos
    {
        [XmlElement("articulo")]
        public List<Articulo> Lista_articulos { get; set; }
    }
  

}
