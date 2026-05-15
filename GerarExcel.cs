using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace LerXML_NaturezaTit
{
    internal static class GerarExcel
    {
        public static void GerarPlanilha(List<LerXml> lista, string caminhoArquivo)
        {
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Duplicatas");

                // Cabeçalho
                ws.Cell(1, 1).Value = "Duplicata";
                ws.Cell(1, 2).Value = "Valor";
                ws.Cell(1, 3).Value = "Vencimento";
                ws.Cell(1, 4).Value = "Natureza";

                int linha = 2;

                foreach (var item in lista)
                {
                    ws.Cell(linha, 1).Value = item.Duplicata;
                    

                    
                    ws.Cell(linha, 2).Value = Convert.ToDecimal(item.VlDuplicata);
                    ws.Cell(linha, 2).Style.NumberFormat.Format = "R$ #,##0.00";

                   
                    ws.Cell(linha, 3).Value = Convert.ToDateTime(item.VencDuplicata);
                    ws.Cell(linha, 3).Style.DateFormat.Format = "dd/MM/yyyy";

                    ws.Cell(linha, 4).Value = item.NaturezaOper;

                    linha++;
                }


                //BORDAS PRETAS
                var range = ws.Range(1, 1, linha - 1, 4);

                range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                range.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                // Ajustar largura automática
                ws.Columns().AdjustToContents();

                wb.SaveAs(caminhoArquivo);
            }
        }
    }

}
