using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTestData
{
    public class DataSource
    {

        public static DataTable GetXLSData(string filePath, TestContext testContext)
        {
            //string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Temp\TestProjects\" + fileName + @";Extended Properties='Excel 12.0;HDR=Yes;'";
            //string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + @";Extended Properties='Excel 12.0;HDR=Yes;'";

            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
            String strExtendedProperties = String.Empty;
            sbConnection.DataSource = filePath;
            sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
            strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";  //"Excel 16.0;HDR=Yes;IMEX=1";
            
            sbConnection.Add("Extended Properties", strExtendedProperties);

            var dataTable = new DataTable();
            try
            {

            using (OleDbConnection connection = new OleDbConnection(sbConnection.ToString()))
            {
                connection.Open();
                var dtSheet = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                //if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                var sheetName = dtSheet.Rows[0]["TABLE_NAME"].ToString();

                OleDbCommand command = new OleDbCommand("select * from [" + sheetName + "]", connection);
                //OleDbCommand command = new OleDbCommand("select * from [Sheet$A8:S20]", connection);
                var dataReader = command.ExecuteReader();
                dataTable.Load(dataReader);
            }

            }
            catch (Exception e)
            {
                testContext.WriteLine("An error occured opening excel sheet: " + e.Message);
            }

            return dataTable;
        }

        private static List<string> GetExcelSheetNames(string filePath)
        {
            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
            String strExtendedProperties = String.Empty;
            sbConnection.DataSource = filePath;
            if (Path.GetExtension(filePath).Equals(".xls"))//for 97-03 Excel file
            {
                sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0";
                strExtendedProperties = "Excel 8.0;HDR=Yes;IMEX=1";//HDR=ColumnHeader,IMEX=InterMixed
            }
            else if (Path.GetExtension(filePath).Equals(".xlsx"))  //for 2007 Excel file
            {
                sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
                strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";
            }
            sbConnection.Add("Extended Properties", strExtendedProperties);
            List<string> listSheet = new List<string>();
            using (OleDbConnection conn = new OleDbConnection(sbConnection.ToString()))
            {
                conn.Open();
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow drSheet in dtSheet.Rows)
                {
                    if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
                    {
                        listSheet.Add(drSheet["TABLE_NAME"].ToString());
                    }
                }
            }
            return listSheet;
        }

        private static string GetExcelSheet1Name(string filePath)
        {
            OleDbConnectionStringBuilder sbConnection = new OleDbConnectionStringBuilder();
            String strExtendedProperties = String.Empty;
            sbConnection.DataSource = filePath;
            if (Path.GetExtension(filePath).Equals(".xls"))//for 97-03 Excel file
            {
                sbConnection.Provider = "Microsoft.Jet.OLEDB.4.0";
                strExtendedProperties = "Excel 8.0;HDR=Yes;IMEX=1";//HDR=ColumnHeader,IMEX=InterMixed
            }
            else if (Path.GetExtension(filePath).Equals(".xlsx"))  //for 2007 Excel file
            {
                sbConnection.Provider = "Microsoft.ACE.OLEDB.12.0";
                strExtendedProperties = "Excel 12.0;HDR=Yes;IMEX=1";
            }
            sbConnection.Add("Extended Properties", strExtendedProperties);

            using (OleDbConnection conn = new OleDbConnection(sbConnection.ToString()))
            {
                conn.Open();
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                //if (drSheet["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
              
                return dtSheet.Rows[0]["TABLE_NAME"].ToString();
            }
        }

        //private static String GetExcelSheet1Name_old(string filePath)
        //{
        //    //string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Temp\TestProjects\" + fileName + @";Extended Properties='Excel 12.0;HDR=Yes;'";
        //    string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + @";Extended Properties='Excel 12.0;HDR=Yes;'";

        //    var dataTable = new DataTable();
        //    using (OleDbConnection connection = new OleDbConnection(connString))
        //    {
        //        connection.Open();
        //        var dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        //        if (dt == null)
        //            return "";

        //        return dt.Rows[0]["TABLE_NAME"].ToString();
        //    }
        //}

        //private static String[] GetExcelSheetNames_old(string filePath)
        //{
        //    //string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Temp\TestProjects\" + fileName + @";Extended Properties='Excel 12.0;HDR=Yes;'";
        //    string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + @";Extended Properties='Excel 12.0;HDR=Yes;'";

        //    var dataTable = new DataTable();
        //    using (OleDbConnection connection = new OleDbConnection(connString))
        //    {
        //        connection.Open();
        //        var dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        //        if (dt == null)
        //        {
        //            return null;
        //        }

        //        String[] excelSheets = new String[dt.Rows.Count];
        //        int i = 0;

        //        // Add the sheet name to the string array.
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            excelSheets[i] = row["TABLE_NAME"].ToString();
        //            i++;
        //        }

        //        // Loop through all of the sheets if you want too...
        //        for (int j = 0; j < excelSheets.Length; j++)
        //        {
        //            // Query each excel sheet.
        //        }

        //        return excelSheets;
        //    }
        //}

        public static DataTable GetDataFromDataBase(string InitialCatalog)
        {
            string connString = @"Data Source=win7ascl; Initial Catalog=" + InitialCatalog + ";User Id=sjef;Password=tellus;";

            var dataTable = new DataTable();
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                var command = new SqlCommand("select * from gt_holiday", connection);
                var dataReader = command.ExecuteReader();
                dataTable.Load(dataReader);
            }

            return dataTable;
        }

        public static DataTable GetCSVData(string path)
        {
            DataTable csvData = new DataTable();
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(path))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    //read column names
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Det oppsto en feil ved uthenting av data: " + ex.Message);
            }
            return csvData;
        }
    }
}
