using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace CommonTestData
{
    public class DataService
    {
        public static bool RestoreDatabase(TestContext testContext, bool isPerformance, bool br)
        {
            var dbServer = TestData.GetDatabaseServer;
            var dbUser = TestData.GetDBUser;
            var dbPassWord = TestData.GetDBAuth();

            string connectionString = @"Data Source='" + dbServer + "';" +
                                       "Initial Catalog=master;" +
                                       "User id='" + dbUser + "';" +
                                       "Password='" + dbPassWord + "';";

            if (isPerformance)
            {
                var dbName = TestData.GetDataBasePerformance + "_1"; 
                string databaseToRestoreFrom = TestData.GetDataBasePerformanceMal + "_1_MAL";
                var retVal1 = Restore(connectionString, dbName, databaseToRestoreFrom, testContext);

                dbName = TestData.GetDataBasePerformance + "_2";
                databaseToRestoreFrom = TestData.GetDataBasePerformanceMal + "_2_MAL";
                var retVal2 = Restore(connectionString, dbName, databaseToRestoreFrom, testContext);

                return retVal1 && retVal2;
            }
            else if (br)
            {
                var databaseToRestoreFrom = TestData.GetMalDataBase + "_BR";
                var dbName = TestData.GetDataBase;
                return Restore(connectionString, dbName, databaseToRestoreFrom, testContext, true);
            }
            else
            {
                var databaseToRestoreFrom = TestData.GetMalDataBase;
                var dbName = TestData.GetDataBase;
                return Restore(connectionString, dbName, databaseToRestoreFrom, testContext);
            }
        }

        private static bool DeleteDatabase(string connectionString, string database, TestContext testContext)
        {
            string strSetDatabaseOffline = @"USE MASTER IF EXISTS(SELECT name FROM sys.databases WHERE name = '" + database + "') ALTER DATABASE " + database + @" SET OFFLINE";
            string strSqlDeleteDatabase = @"USE MASTER IF EXISTS(SELECT name FROM sys.databases WHERE name = '" + database + "') DROP DATABASE " + database;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand(strSetDatabaseOffline, con))
                    {
                        try
                        {
                            command.ExecuteNonQuery();
                            testContext.WriteLine("Database:" + database + " successfully set offline");
                        }
                        catch (Exception e)
                        {
                            testContext.WriteLine("Database:" + database + " error setting offline: " + e.Message);
                        }
                        command.CommandText = strSqlDeleteDatabase;
                        command.ExecuteNonQuery();
                        testContext.WriteLine("Database:" + database + " successfully deleted");

                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                testContext.WriteLine("Failed to delete database: " + database + ", " + e.Message);
                return false;
            }
        }

        private static bool Restore(string connectionString, string dataBase, string dataBaseToRestoreFrom, TestContext testContext, bool isBr = false)
        {
            var logicalNameData = "ASCL_NYTT_FORSKJ";
            var logicalNameLog = "ASCL_NYTT_FORSKJ_log";

            if (isBr)
            {
                logicalNameData = "ASCL_GA_Baseline";
                logicalNameLog = "ASCL_GA_Baseline_log";
            }

            var pathToDBfiles = TestData.GetDataBaseFilLocation;
            var sqlDataLocation = pathToDBfiles.Remove(pathToDBfiles.LastIndexOf(@"\"));

            string strSqlRestoreDatabase = @"USE MASTER RESTORE DATABASE [" + dataBase + @"] 
                                            FROM DISK = '" + pathToDBfiles + @"\" + dataBaseToRestoreFrom + @".bak' 
                                            WITH 
                                            MOVE '" + logicalNameData + @"' TO '" + sqlDataLocation + @"\Data\" + dataBase + @"_Data.mdf',
                                            MOVE '" + logicalNameLog + @"' TO '" + sqlDataLocation + @"\log\" + dataBase + @"_Log.ldf',
                                            REPLACE";

            if (DeleteDatabase(connectionString, dataBase, testContext))
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlCommand command = new SqlCommand(strSqlRestoreDatabase, con))
                        {
                            command.CommandTimeout = 180;
                            command.ExecuteNonQuery();

                            testContext.WriteLine("Database:" + dataBase + " successfully restored");

                            return true;
                        }
                    }
                }
                catch (Exception e)
                {
                    testContext.WriteLine("Failed while restoring database: " + e.Message);
                    return false;
                }

            return false;
        }

        [Obsolete("CompareRosterDataFilesLocked is deprecated", true)]
        public static List<string> CompareRosterDataFilesLocked(string filePath, string fileType, TestContext testContext)
        {
            var headerType = SupportFunctions.HeaderType._LockedFile;
            var errorList = new List<string>();
            var rowToStart = 0;
            var fileName = "";
            try
            {
                DirectoryInfo downloadedMessageInfo = new DirectoryInfo(filePath);
                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {

                    //_Locked
                    if (!file.Extension.Contains(".xls") || !file.Name.Contains(headerType.ToString()) || file.Name.Contains("_Facit"))
                        continue;

                    var report = filePath + file.Name;
                    fileName = report;


                    var name = file.Name.Remove(file.Name.LastIndexOf("_"));
                    var sheetName = "[" + name + "_L$]";


                    var reportFacit = report.Replace(headerType.ToString(), "");
                    reportFacit = reportFacit.Replace(".xls", "_Facit" + fileType);

                    var actualData = new DataTable();
                    //Open file in excell
                    System.Diagnostics.Process.Start(report);
                    actualData = DataSource.GetXLSData(report, testContext);

                    var facit = new DataTable();
                    try
                    {
                        //Open file in excell
                        System.Diagnostics.Process.Start(reportFacit);
                        facit = DataSource.GetXLSData(reportFacit, testContext);

                    }
                    catch
                    {
                        testContext.WriteLine("Unable to find/read facit: " + report);
                        continue;
                    }

                    testContext.WriteLine("Rapport: " + report);
                    for (int i = rowToStart; i < facit.Rows.Count; i++)
                    {
                        var rowNr = i + 2;
                        var colCount = facit.Columns.Count - 1;

                        for (var i2 = 0; i2 < colCount; i2++)
                        {
                            var actualHeader = "";
                            var expectedHeader = "";
                            var actualValue = "";
                            var expectedValue = "";

                            try
                            {
                                actualHeader = "Kolonne " + i2.ToString().Trim();
                                expectedHeader = "Kolonne " + i2.ToString().Trim();

                                actualValue = actualData.Rows[i][i2].ToString().Trim();
                                expectedValue = facit.Rows[i][i2].ToString().Trim();

                                if (String.IsNullOrEmpty(actualValue) && String.IsNullOrEmpty(expectedValue))
                                    continue;

                                var compValue = string.CompareOrdinal(actualValue, expectedValue);

                                if (compValue != 0)
                                    throw new Exception();

                                testContext.WriteLine("Rad nr. " + rowNr + ". Beregnet: " + actualValue + " - Avlest: " + expectedValue);
                            }
                            catch (Exception)
                            {
                                errorList.Add(Environment.NewLine + "Rad nr. " + rowNr + Environment.NewLine +
                                              Environment.NewLine + "Rapport: " + report + ". " + Environment.NewLine +
                                              Environment.NewLine + "Aktuell kolonne = " + actualHeader +
                                              Environment.NewLine + "Forventet kolonne = " + expectedHeader +
                                              Environment.NewLine + "Aktuell verdi = " + actualValue +
                                              Environment.NewLine + "Forventet verdi = " + expectedValue);
                            }
                        }
                    }

                    testContext.WriteLine("Finished: " + report + Environment.NewLine);
                    SupportFunctions.KillExcelProcess(testContext);
                }
            }
            catch (Exception e)
            {
                testContext.WriteLine("An error occured checking files: " + fileName + ", " + e.Message);
                SupportFunctions.KillExcelProcess(testContext);
            }

            SupportFunctions.KillExcelProcess(testContext);
            return errorList;
        }

        public static List<string> CompareReportDataFiles(string reportDir, string fileType, TestContext testContext, int rowToStart = 0, bool isAml = false, bool isUserLog = false)
        {
            //var headerType = SupportFunctions.HeaderType._Common;
            var errorList = new List<string>();
            var fileName = "";

            try
            {
                DirectoryInfo downloadedMessageInfo = new DirectoryInfo(reportDir);
                foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                {

                    //_Common
                    if (!file.Extension.Contains(".xls") && !file.Extension.Contains(".xlsx") ||
                        file.Name.Contains("_Facit"))
                        continue;

                    var report = reportDir + file.Name;
                    fileName = report;
                    var reportFacit = report.ToString();

                    if (file.Extension == ".xls")
                        reportFacit = reportFacit.Replace(".xls", "_Facit" + fileType);
                    else
                        reportFacit = reportFacit.Replace(".xlsx", "_Facit" + fileType + "x");


                    var actualData = new DataTable();
                    actualData = DataSource.GetXLSData(report, testContext);

                    var facit = new DataTable();
                    try
                    {
                        facit = DataSource.GetXLSData(reportFacit, testContext);
                    }
                    catch
                    {
                        testContext.WriteLine("Unable to find/read facit: " + report);
                        continue;
                    }

                    testContext.WriteLine("Rapport: " + report);


                    int rowsExpected = facit.Rows.Count;
                    int rowsActual = actualData.Rows.Count;
                    int rowCount = Math.Max(rowsExpected, rowsActual);

                    for (int i = rowToStart; i < rowCount; i++)
                    {
                        var rowNr = i + 2;

                        var colExpected = facit.Columns.Count - 1;
                        int colActual = actualData.Columns.Count - 1;
                        int colCount = Math.Max(colExpected, colActual);

                        for (var i2 = 0; i2 < colCount; i2++)
                        {
                            var colNo = i2 + 1;
                            var cNameActual = "";
                            var cNameExpected = "";

                            try
                            {
                                cNameActual = actualData.Columns[i2].ColumnName;
                            }
                            catch (Exception)
                            {
                                cNameActual = "N/A";
                            }
                            try
                            {
                                cNameExpected = facit.Columns[i2].ColumnName;
                            }
                            catch (Exception)
                            {
                                cNameExpected = "N/A";
                            }

                            if ((isAml && cNameActual == "Dato") || (isUserLog && cNameActual == "Dato/kl"))
                                continue;

                            var actualHeader = "";
                            var expectedHeader = "";
                            var actualValue = "";
                            var expectedValue = "";

                            var compHeader = string.CompareOrdinal(cNameActual, cNameExpected);

                            if (compHeader != 0)
                                errorList.Add("Column names are not equal, Expected: " + cNameActual + "Actual: " + cNameExpected);

                            actualHeader = cNameActual + "(Kolonne " + colNo.ToString().Trim() + ")";
                            expectedHeader = cNameExpected + "(Kolonne " + colNo.ToString().Trim() + ")";
                            var colStr = Environment.NewLine + "Kolonne: " + cNameActual;

                            try
                            {
                                try
                                {
                                    actualValue = actualData.Rows[i][i2].ToString().Trim();
                                }
                                catch (Exception)
                                {
                                    actualValue = "N/A";
                                }
                                try
                                {
                                    expectedValue = facit.Rows[i][i2].ToString().Trim();
                                }
                                catch (Exception)
                                {
                                    expectedValue = "N/A";
                                }

                                if (String.IsNullOrEmpty(actualValue) && String.IsNullOrEmpty(expectedValue))
                                    continue;

                                var compValue = string.CompareOrdinal(actualValue, expectedValue);

                                if (compValue != 0)
                                    throw new Exception();

                                if (i == rowToStart)
                                    testContext.WriteLine(colStr);

                                var rowStr = "Rad nr. " + rowNr + ". Beregnet: " + actualValue + " - Avlest: " + expectedValue;
                                try
                                {
                                    testContext.WriteLine(rowStr);
                                }
                                catch
                                {
                                    rowStr = rowStr.Replace("{", "(");
                                    rowStr = rowStr.Replace("}", ")");
                                    testContext.WriteLine(rowStr);
                                }
                            }
                            catch
                            {
                                errorList.Add("Rapport: " + report + ". " + Environment.NewLine +
                                              Environment.NewLine + "Aktuell kolonne = " + actualHeader +
                                              Environment.NewLine + "Forventet kolonne = " + expectedHeader +
                                              Environment.NewLine + Environment.NewLine + "Rad nr. " + rowNr +
                                              Environment.NewLine + "Aktuell verdi = " + actualValue +
                                              Environment.NewLine + "Forventet verdi = " + expectedValue + Environment.NewLine);
                            }
                        }
                    }

                    testContext.WriteLine("Finished: " + report + Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                testContext.WriteLine("An error occured checking files: " + fileName + ", " + e.Message);
            }

            return errorList;
        }
    }
}
