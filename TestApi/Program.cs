using Ionic.Zip;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestApi.models;
using TestApi.service;
using TestApi.settings;
namespace TestApi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                const string testUUID1 = config.testUUID1;
                const string testUUID2 = config.testUUID2;
                const string testUUID3 = config.testUUID3;
                const string vergiNo   = config.vergiNo;


                //var resultDocument = await post_service.SendDocument();
                //Console.WriteLine("SendDocument Result:\n" + resultDocument);

                //var sendBasicInvoiceModel = await post_service.SendBasicInvoiceFromModel();
                //Console.WriteLine("SendBasicInvoiceModel Result:\n" + sendBasicInvoiceModel);

                //var documentDownloadByUUID = await post_service.DocumentDownloadByUUID(testUUID);
                //Console.WriteLine(documentDownloadByUUID);

                var previewDocumentReturnPDF = await post_service.PreviewDocumentReturnPDF();
                //var previewDocumentReturnHTML = await post_service.PreviewDocumentReturnHTML();
                printTestResult(previewDocumentReturnPDF, "PreviewDocumentReturnPDF");
                //Console.WriteLine(previewDocumentReturnHTML);

                //var sendDocumentAnswer = await post_service.SendDocumentAnswer(testUUID);
                //Console.WriteLine(sendDocumentAnswer);

                //var getDocumentLogs = await post_service.GetDocumentLogs(testUUID1);
                //printTestResult(getDocumentLogs,"GetDocumentLogs");

                //var getUserPK = await post_service.GetUserPK();
                //Console.WriteLine(getUserPK);

                //var getUserGB = await post_service.GetUserGB();
                //Console.WriteLine(getUserGB);

                //var gibUserList = await post_service.GibUserList();
                //Console.WriteLine(gibUserList);

                //(ERROR)//var getEnvelopeStatusFromGIB = await post_service.GetEnvelopeStatusFromGIB("b9393971-fe82-4a94-b2b6-f17a3ca07e1a");
                //Console.WriteLine(getEnvelopeStatusFromGIB);

                //var getTaxOfficesAndCodes = await post_service.GetTaxOfficesAndCodes();
                //Console.WriteLine(getTaxOfficesAndCodes);

                /*(ERROR)*/ //var updateUnreadedStatus = await post_service.UpdateUnreadedStatus(testUUID3);
                //Console.WriteLine(updateUnreadedStatus);

                //var getNumberOfCredits = await post_service.GetNumberOfCredits();//kod çalışıyor kontör 0 olduğu için 0 dönüyor
                //Console.WriteLine(getNumberOfCredits);

                //var getOutBoxDocuments = await post_service.GetOutBoxDocuments();
                //Console.WriteLine(getOutBoxDocuments);

                //var getInBoxDocuments = await post_service.GetInBoxDocuments();
                //Console.WriteLine(getInBoxDocuments);

                //var getOutBoxDocumentByUUID = await post_service.GetOutBoxDocumentByUUID(testUUID);
                //Console.WriteLine(getOutBoxDocumentByUUID);

                //var getCodeListByType = await post_service.GetCodeListByType();
                //Console.WriteLine(getCodeListByType);

                //var getOutBoxDocumentsWithDetail = await post_service.GetOutBoxDocumentsWithDetail();
                //Console.WriteLine(getOutBoxDocumentsWithDetail);

                //var getInBoxDocumentsWithDetail = await post_service.GetInBoxDocumentsWithDetail();
                // Console.WriteLine(getInBoxDocumentsWithDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }



        static void printTestResult(ApiResponseModel result, string testName)
        {
            try
            {
                // Body içindeki "message" ve "result" alanını yakala
                var json = JObject.Parse(result.Body);
                var message = json["Message"]?.ToString() ?? "No message found!";
                var bodyResult = json["Result"]?.ToString() ?? json.ToString();

                // konsol çıktısı
                Console.WriteLine("\n===========================================================================================");
                Console.WriteLine($"[TEST]         : {testName}");
                Console.WriteLine($"[STATUS CODE]  : {result.StatusCode}");
                Console.WriteLine($"[MESSAGE]      : {message}");
                Console.WriteLine("===========================================================================================\n");
                Console.WriteLine($"[RESULT]      : {bodyResult}");
                Console.WriteLine("===========================================================================================\n");
            }
            catch (Exception ex)
            {
                // JSON parse hatası vb. durumlar
                Console.WriteLine("===============================================");
                Console.WriteLine($"[TEST]         : {testName}");
                Console.WriteLine($"[STATUS CODE]  : {result.StatusCode}");
                Console.WriteLine("===============================================\n");
                Console.WriteLine("[MESSAGE]      : Failed to parse JSON.");
                Console.WriteLine($"[ERROR]        : {ex.Message}");
                Console.WriteLine("===============================================\n");
            }
        }

    }
}
