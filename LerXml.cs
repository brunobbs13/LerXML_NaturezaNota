using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace LerXML_NaturezaTit
{
    internal class LerXml
    {
        //public string NumeroNota {  get; set; }
        //public string OrdemDup { get; set; }
        public string Bordero { get; set; }
        public string Duplicata {  get; set; }
        public string VlDuplicata { get; set; }
        public string VencDuplicata { get; set; }
        public string NaturezaOper {  get; set; }

        public LerXml() 
        {
           
        }

        public List<LerXml> GerarList(string caminhoXML, TextBox txtBor)
        {

            string caminhoXml = caminhoXML;
            LerXml infodup = new LerXml();

            XDocument meuXMLDoc = XDocument.Load(caminhoXml);
            XNamespace ns = "http://www.portalfiscal.inf.br/nfe";


            string numeroNota = (string)meuXMLDoc.Descendants(ns + "nNF").FirstOrDefault();
            string natureza = (string)meuXMLDoc.Descendants(ns + "natOp").FirstOrDefault();


            //var numeroTit = meuXMLDoc.Descendants(ns + "nNF").FirstOrDefault();
            //var duplicataNum = meuXMLDoc.Descendants(ns + "nDup").FirstOrDefault();
            //var valor = meuXMLDoc.Descendants(ns + "vDup").FirstOrDefault();
            //var vencimento = meuXMLDoc.Descendants(ns + "dVenc").FirstOrDefault();
            //var TpNatureza = meuXMLDoc.Descendants(ns + "natOp").First();


            //Numtit.Text = numeroTit.Value;
            //NumDup.Text = duplicataNum.Value;
            //ValorDup.Text = valor.Value;
            //VencDup.Text = vencimento.Value;
            //NaturezaOper.Text = TpNatureza.Value;

          

            var lista = meuXMLDoc.Descendants(ns + "dup")
                .Select(dup => new LerXml
                {
                    //NumeroNota = numeroNota,
                    //OrdemDup = (string)dup.Element(ns + "nDup"),
                    Bordero = txtBor.Text,
                    Duplicata = numeroNota + "/" + (string)dup.Element(ns + "nDup"),
                    VencDuplicata = (string)dup.Element(ns + "dVenc"),
                    VlDuplicata = (string)dup.Element(ns + "vDup"),
                    NaturezaOper = natureza,


                })
                .ToList();      

            return lista;
        }




    }
}
