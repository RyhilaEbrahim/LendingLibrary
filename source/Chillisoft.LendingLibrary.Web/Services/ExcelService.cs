using System;
using System.Data;
using System.Web;
using OfficeOpenXml;

namespace Chillisoft.LendingLibrary.Web.Services
{
    public interface IExcelService
    {
        void Export(HttpResponseBase httpResponse, DataTable dataTable, string fileNameWithoutExtension);
    }
    public class ExcelService : IExcelService
    {
        public void Export(HttpResponseBase httpResponse, DataTable dataTable, string fileNameWithoutExtension)
        {
            if (dataTable == null) throw new ArgumentNullException("dataTable");
            if (fileNameWithoutExtension == null) throw new ArgumentNullException("fileNameWithoutExtension");
            if (string.IsNullOrEmpty(fileNameWithoutExtension)) throw new ArgumentException("fileNameWithoutExtension must not be empty");

            BuildExcelWorkSheet(httpResponse, dataTable, fileNameWithoutExtension);
        }

        private void BuildExcelWorkSheet(HttpResponseBase httpResponse, DataTable dataTable, string fileNameWithoutExtension)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(fileNameWithoutExtension);
                worksheet.Cells.Style.Font.Size = 11;
                worksheet.Cells.Style.Font.Name = "Calibri";

                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

                SetHttpResponse(httpResponse, fileNameWithoutExtension, package);
            }
        }

        private void SetHttpResponse(HttpResponseBase httpResponse, string fileNameWithoutExtension, ExcelPackage package)
        {
            httpResponse.ClearContent();
            httpResponse.Buffer = true;

            //Write it back to the client
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", string.Format("attachment;  filename={0}.xlsx", fileNameWithoutExtension));
            httpResponse.BinaryWrite(package.GetAsByteArray());

            httpResponse.Flush();
            httpResponse.End();
        }
    }
}