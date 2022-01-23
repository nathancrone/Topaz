using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms;
using Topaz.UI.ReportShared;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace Topaz.UI.ReportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream readStream = File.OpenRead(@"report.json"))
            {
                var pageSize = 5;
                var data = JsonSerializer.Deserialize<List<ReportTerritory>>(readStream);

                for (var skip = 0; skip <= data.Count(); skip += pageSize)
                {
                    var reportParams = new List<ReportParameter>();
                    var reportData = new List<ReportLedgerData>();

                    int index1 = 0;
                    foreach (var x in data.Skip(skip).Take(pageSize))
                    {
                        reportParams.Add(new ReportParameter(string.Format("Col{0}_Header", index1 + 1), x.TerritoryCode.ToUpper()));
                        int index2 = 0;
                        foreach (var y in x.Activity.OrderBy(a => a.CheckOutDate))
                        {
                            if (reportData.Count() < (index2 + 1))
                            {
                                reportData.Add(new ReportLedgerData());
                            }

                            switch (index1)
                            {
                                case 0:
                                    reportData[index2].Col1_Publisher = $"{y.FirstName} {y.LastName}";
                                    reportData[index2].Col1_DateOut = $"{y.CheckOutDate:MM/dd/yyyy}";
                                    reportData[index2].Col1_DateIn = $"{y.CheckInDate:MM/dd/yyyy}";
                                    break;
                                case 1:
                                    reportData[index2].Col2_Publisher = $"{y.FirstName} {y.LastName}";
                                    reportData[index2].Col2_DateOut = $"{y.CheckOutDate:MM/dd/yyyy}";
                                    reportData[index2].Col2_DateIn = $"{y.CheckInDate:MM/dd/yyyy}";
                                    break;
                                case 2:
                                    reportData[index2].Col3_Publisher = $"{y.FirstName} {y.LastName}";
                                    reportData[index2].Col3_DateOut = $"{y.CheckOutDate:MM/dd/yyyy}";
                                    reportData[index2].Col3_DateIn = $"{y.CheckInDate:MM/dd/yyyy}";
                                    break;
                                case 3:
                                    reportData[index2].Col4_Publisher = $"{y.FirstName} {y.LastName}";
                                    reportData[index2].Col4_DateOut = $"{y.CheckOutDate:MM/dd/yyyy}";
                                    reportData[index2].Col4_DateIn = $"{y.CheckInDate:MM/dd/yyyy}";
                                    break;
                                case 4:
                                    reportData[index2].Col5_Publisher = $"{y.FirstName} {y.LastName}";
                                    reportData[index2].Col5_DateOut = $"{y.CheckOutDate:MM/dd/yyyy}";
                                    reportData[index2].Col5_DateIn = $"{y.CheckInDate:MM/dd/yyyy}";
                                    break;
                                default:
                                    break;
                            }

                            index2++;
                        }

                        index1++;
                    }

                    var reportDataSource = new ReportDataSource { Name = "DataSet1", Value = reportData };

                    ReportViewer viewer = new ReportViewer();
                    viewer.ProcessingMode = ProcessingMode.Local;
                    viewer.LocalReport.ReportPath = "ReportLedger.rdlc";
                    viewer.LocalReport.SetParameters(reportParams.ToArray());

                    viewer.LocalReport.DataSources.Clear();
                    viewer.LocalReport.DataSources.Add(reportDataSource);
                    viewer.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;
                    byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                    MemoryStream PdfStream = new MemoryStream();

                    try
                    {
                        PdfStream.Write(bytes, 0, bytes.Length);
                        PdfStream.Flush();
                        PdfStream.Position = 0;
                        using (FileStream writeStream = File.Create($@"report_{skip}.pdf"))
                        {
                            PdfStream.CopyTo(writeStream);
                        }
                    }
                    catch
                    {
                        PdfStream.Dispose();
                        throw;
                    }

                }
            }
        }
    }
}
