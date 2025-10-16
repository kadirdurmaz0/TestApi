using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestApi.models;
using TestApi.settings;
using TestApi.utils;
namespace TestApi.service
{
    internal class post_service
    {
        // Genel Fonksiyonlar
        private static async Task<ApiResponseModel> SendPost(string path, object body)
        {
            try
            {

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-Api-Key", config.apiKey);
                client.DefaultRequestHeaders.Add("X-Secret-Key", config.secretKey);
                client.DefaultRequestHeaders.Add("X-Integration-Key", config.integrationKey);

                string jsonBody = JsonConvert.SerializeObject(body);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(config.baseUrl + path, content);

                
                var responseBody = await response.Content.ReadAsStringAsync();

                return new ApiResponseModel
                {
                    StatusCode = (int)response.StatusCode,
                    Body = responseBody
                };
                
            }
            catch (Exception e)
            {
                return new ApiResponseModel
                {
                    StatusCode = 500,
                    Body = $"HATA: {e.Message}"
                };
            }

        }


        // Post Metotları

        // Elektronik belgeyi JSON içinde XML veri olarak gönderir, GİB’e iletir ve PDF linkini döner.
        public static Task<ApiResponseModel> SendDocument(string UUID)
        {
            string documentUUID = UUID; // Rastgele uuid ayarlıyoruz
            string receiverTagString = "1234567891"; // TC                                                             
            string issueDate = time_utils.timeNowTypeDate();
            string issueTime = time_utils.timeNowTypeTimeZone(); 
            string xml = $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Invoice xmlns:ubltr=\"urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents\" xmlns:qdt=\"urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2\" xmlns:cbc=\"urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2\" xmlns:cac=\"urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2\" xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:ccts=\"urn:un:unece:uncefact:documentation:2\" xmlns:ext=\"urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2\" xmlns:xades=\"http://uri.etsi.org/01903/v1.3.2#\" xmlns:udt=\"urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2\" xmlns=\"urn:oasis:names:specification:ubl:schema:xsd:Invoice-2\">\r\n  <ext:UBLExtensions>\r\n    <ext:UBLExtension>\r\n      <ext:ExtensionContent>\r\n        \r\n      </ext:ExtensionContent>\r\n    </ext:UBLExtension>\r\n  </ext:UBLExtensions>\r\n  <cbc:UBLVersionID>2.1</cbc:UBLVersionID>\r\n  <cbc:CustomizationID>TR1.2</cbc:CustomizationID>\r\n  <cbc:ProfileID>TICARIFATURA</cbc:ProfileID>\r\n  <cbc:ID>ARS2024000000001</cbc:ID>\r\n  <cbc:CopyIndicator>false</cbc:CopyIndicator>\r\n  <cbc:UUID>{documentUUID}</cbc:UUID>\r\n  <cbc:IssueDate>{issueDate}</cbc:IssueDate>\r\n  <cbc:IssueTime>{issueTime}</cbc:IssueTime>\r\n  <cbc:InvoiceTypeCode>SATIS</cbc:InvoiceTypeCode>\r\n  <cbc:Note>Yalnız BirTürkLirasıYirmiKuruş&lt;br/&gt;</cbc:Note>\r\n  <cbc:Note>&lt;br&gt;E-Fatura izni kapsamında elektronik ortamda iletilmiştir. &lt;br/&gt;Ödeme Yöntemi: Kredi Kartı İle Ödendi - Sipariş No:_Kopya - Kargo Kampanya Kodu:3321733760237447 - Kargo Şirketi:Aras - Teslimat Bilgileri: TEST EFİRMA Beytepe Mahallesi, Çankaya/Ankara Çankaya Ankara </cbc:Note>\r\n  <cbc:DocumentCurrencyCode>TRY</cbc:DocumentCurrencyCode>\r\n  <cbc:LineCountNumeric>1</cbc:LineCountNumeric>\r\n  <cac:OrderReference>\r\n    <cbc:ID>_Kopya</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n  </cac:OrderReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>88f924da-b4f8-4c22-8db2-8cf9057a8b8d</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>CUST_INV_ID</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>0100</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>OUTPUT_TYPE</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>99</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>TRANSPORT_TYPE</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>ELEKTRONIK</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>EREPSENDT</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>0</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>SendingType</cbc:DocumentTypeCode>\r\n    <cbc:DocumentType>KAGIT</cbc:DocumentType>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>FIT2024000000001</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentType>XSLT</cbc:DocumentType>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>urn:mail:defaultpk@deneme.com</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>recvpk</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:Signature>\r\n    <cbc:ID schemeID=\"VKN_TCKN\">1234567891</cbc:ID>\r\n    <cac:SignatoryParty>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567891</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PostalAddress>\r\n        <cbc:StreetName>Kuşkavağı, Belediye Cd. No:78, 07070 Konyaaltı/Antalya</cbc:StreetName>\r\n        <cbc:CitySubdivisionName>Konyaaltı</cbc:CitySubdivisionName>\r\n        <cbc:CityName>Antalya</cbc:CityName>\r\n        <cac:Country>\r\n          <cbc:Name>TÜRKİYE</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n    </cac:SignatoryParty>\r\n    <cac:DigitalSignatureAttachment>\r\n      <cac:ExternalReference>\r\n        <cbc:URI>#Signature</cbc:URI>\r\n      </cac:ExternalReference>\r\n    </cac:DigitalSignatureAttachment>\r\n  </cac:Signature>\r\n  <cac:AccountingSupplierParty>\r\n    <cac:Party>\r\n      <cbc:WebsiteURI />\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567801</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"TICARETSICILNO\" />\r\n      </cac:PartyIdentification>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"MERSISNO\" />\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Test Firma</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:StreetName>Kuşkavağı, Belediye Cd. No:78, 07070 Konyaaltı/Antalya</cbc:StreetName>\r\n        <cbc:CitySubdivisionName>Konyaaltı</cbc:CitySubdivisionName>\r\n        <cbc:CityName>Antalya</cbc:CityName>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n      <cac:PartyTaxScheme>\r\n        <cac:TaxScheme>\r\n          <cbc:Name>Antalya</cbc:Name>\r\n        </cac:TaxScheme>\r\n      </cac:PartyTaxScheme>\r\n      <cac:Contact>\r\n        <cbc:Telephone>05555555555</cbc:Telephone>\r\n        <cbc:ElectronicMail>info@firma.com</cbc:ElectronicMail>\r\n      </cac:Contact>\r\n    </cac:Party>\r\n  </cac:AccountingSupplierParty>\r\n  <cac:AccountingCustomerParty>\r\n    <cac:Party>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567891</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Test Firması</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:StreetName>Beytepe Mahallesi, Çankaya/ankara</cbc:StreetName>\r\n        <cbc:BuildingName />\r\n        <cbc:CitySubdivisionName>Çankaya</cbc:CitySubdivisionName>\r\n        <cbc:CityName>Ankara</cbc:CityName>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n      <cac:PartyTaxScheme>\r\n        <cac:TaxScheme>\r\n          <cbc:Name>Ankara Kurumlar Vergi Dairesi Müdürlüğü</cbc:Name>\r\n        </cac:TaxScheme>\r\n      </cac:PartyTaxScheme>\r\n      <cac:Contact>\r\n        <cbc:Telephone />\r\n        <cbc:ElectronicMail />\r\n      </cac:Contact>\r\n    </cac:Party>\r\n  </cac:AccountingCustomerParty>\r\n  <cac:Delivery>\r\n    <cac:DeliveryAddress>\r\n      <cbc:StreetName>Talatpaşa Cad.</cbc:StreetName>\r\n      <cbc:CitySubdivisionName>Ümraniye</cbc:CitySubdivisionName>\r\n      <cbc:CityName>İstanbul</cbc:CityName>\r\n      <cac:Country>\r\n        <cbc:Name>Türkiye</cbc:Name>\r\n      </cac:Country>\r\n    </cac:DeliveryAddress>\r\n    <cac:CarrierParty>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567891</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Aras Kargo</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:ID />\r\n        <cbc:StreetName>Rüzgarlıbahçe Mah. Yavuz Sultan Selim Cad.</cbc:StreetName>\r\n        <cbc:BuildingName>Aras Plaza</cbc:BuildingName>\r\n        <cbc:BuildingNumber>2</cbc:BuildingNumber>\r\n        <cbc:CitySubdivisionName>Beykoz</cbc:CitySubdivisionName>\r\n        <cbc:CityName>İstanbul</cbc:CityName>\r\n        <cbc:PostalZone>34000</cbc:PostalZone>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n    </cac:CarrierParty>\r\n    <cac:DeliveryParty>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID />\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Teslimat yapılacak isim</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:ID />\r\n        <cbc:StreetName>Talatpaşa Cad. Park Sok.</cbc:StreetName>\r\n        <cbc:BuildingNumber>35-1</cbc:BuildingNumber>\r\n        <cbc:CitySubdivisionName>Ümraniye</cbc:CitySubdivisionName>\r\n        <cbc:CityName>İstanbul</cbc:CityName>\r\n        <cbc:PostalZone>34000</cbc:PostalZone>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n      <cac:Person>\r\n        <cbc:FirstName>Teslim alacak kişi isim</cbc:FirstName>\r\n        <cbc:FamilyName>Teslim alacak kişi soyisim</cbc:FamilyName>\r\n      </cac:Person>\r\n    </cac:DeliveryParty>\r\n  </cac:Delivery>\r\n  <cac:TaxTotal>\r\n    <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n    <cac:TaxSubtotal>\r\n      <cbc:TaxableAmount currencyID=\"TRY\">1.00</cbc:TaxableAmount>\r\n      <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n      <cbc:Percent>20</cbc:Percent>\r\n      <cac:TaxCategory>\r\n        <cac:TaxScheme>\r\n          <cbc:Name>KDV</cbc:Name>\r\n          <cbc:TaxTypeCode>0015</cbc:TaxTypeCode>\r\n        </cac:TaxScheme>\r\n      </cac:TaxCategory>\r\n    </cac:TaxSubtotal>\r\n  </cac:TaxTotal>\r\n  <cac:LegalMonetaryTotal>\r\n    <cbc:LineExtensionAmount currencyID=\"TRY\">1.00</cbc:LineExtensionAmount>\r\n    <cbc:TaxExclusiveAmount currencyID=\"TRY\">1.00</cbc:TaxExclusiveAmount>\r\n    <cbc:TaxInclusiveAmount currencyID=\"TRY\">1.20</cbc:TaxInclusiveAmount>\r\n    <cbc:AllowanceTotalAmount currencyID=\"TRY\">0.00</cbc:AllowanceTotalAmount>\r\n    <cbc:PayableAmount currencyID=\"TRY\">1.20</cbc:PayableAmount>\r\n  </cac:LegalMonetaryTotal>\r\n  <cac:InvoiceLine>\r\n    <cbc:ID>1</cbc:ID>\r\n    <cbc:InvoicedQuantity unitCode=\"NIU\">1.0000</cbc:InvoicedQuantity>\r\n    <cbc:LineExtensionAmount currencyID=\"TRY\">1.00</cbc:LineExtensionAmount>\r\n    <cac:TaxTotal>\r\n      <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n      <cac:TaxSubtotal>\r\n        <cbc:TaxableAmount currencyID=\"TRY\">1.00</cbc:TaxableAmount>\r\n        <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n        <cbc:Percent>20</cbc:Percent>\r\n        <cac:TaxCategory>\r\n          <cac:TaxScheme>\r\n            <cbc:Name>KDV</cbc:Name>\r\n            <cbc:TaxTypeCode>0015</cbc:TaxTypeCode>\r\n          </cac:TaxScheme>\r\n        </cac:TaxCategory>\r\n      </cac:TaxSubtotal>\r\n    </cac:TaxTotal>\r\n    <cac:Item>\r\n      <cbc:Description />\r\n      <cbc:Name>Cep Telefonu Aksesuarı</cbc:Name>\r\n      <cac:BuyersItemIdentification>\r\n        <cbc:ID />\r\n      </cac:BuyersItemIdentification>\r\n      <cac:SellersItemIdentification>\r\n        <cbc:ID>56898T10Stani1805 </cbc:ID>\r\n      </cac:SellersItemIdentification>\r\n      <cac:AdditionalItemIdentification>\r\n        <cbc:ID />\r\n      </cac:AdditionalItemIdentification>\r\n    </cac:Item>\r\n    <cac:Price>\r\n      <cbc:PriceAmount currencyID=\"TRY\">1.000000</cbc:PriceAmount>\r\n    </cac:Price>\r\n  </cac:InvoiceLine>\r\n</Invoice>";
            byte[] byteArray = zip_utils.IonicZipFile(xml, documentUUID);
            // Örnek: Belge gönderme
            var Documentbody = new
            {
                receiverTag = receiverTagString,   // alıcı tag
                documentBytes = byteArray,   // UBL/XML belgenin Base64 hali
                isDocumentNoAuto = true,        // belge numarası otomatik
                systemTypeCodes = "EFATURA"     // sistem tipi
            };


            return SendPost("/OutEBelgeV2/SendDocument", Documentbody);
        }

        // Elektronik belgeyi hazır model üzerinden gönderir, GİB’e iletir ve PDF linkini döner.
        public static Task<ApiResponseModel> SendBasicInvoiceFromModel(string uuid)
        {
            var sendBasicInvoiceFromModelBody = new BasicInvoiceModel
            {
                invoice = new InvoiceModel
                {
                    orderCode = "ÖRNEK-1234714141415",
                    orderDate = "2025-10-15",
                    systemTypeCodes = "web",
                    invoiceDate = "2025-10-15",
                    invoiceExplanation = "Deneme faturası",
                    eInvoiceId = uuid,
                    isDocumentNoAuto = true,
                    ettn = uuid,
                    receiverTag = "string",
                    billingName = "Müşteri Adı Soyadı",
                    billingAddress = "Örnek Mah. Deneme Cad. No: 1",
                    billingTown = "Şehir İlçe",
                    billingCity = "İstanbul",
                    billingMobilePhone = "5551234567",
                    billingPhone = "5551234567",
                    billingPhone2 = "string",
                    taxOffice = "Vergi Dairesi",
                    taxNo = "1234567890",
                    email = "musteri@ornek.com",
                    shipCompany = "MNG Kargo",
                    cargoCampaignCode = "string",
                    shippingName = "Alıcı Adı Soyadı",
                    shippingAddress = "Gönderim Adresi",
                    shippingTown = "Gönderim İlçe",
                    shippingCity = "Gönderim Şehir",
                    shippingCountry = "Türkiye",
                    shippingZipCode = "34000",
                    shippingPhone = "5559876543",
                    deliveryFeeType = 2,
                    paymentType = "Kredi Kartı",
                    currency = "TL",
                    currencyRate = 1,
                    totalPaidTaxExcluding = 90,
                    totalPaidTaxIncluding = 108,
                    productsTotalTaxExcluding = 90,
                    productsTotalTaxIncluding = 108,
                    shippingChargeTotalTaxExcluding = 0,
                    shippingChargeTotalTaxIncluding = 0,
                    installmentChargeTotalTaxExcluding = 0,
                    installmentChargeTotalTaxIncluding = 0,
                    bankTransferDiscountTotalTaxExcluding = 0,
                    bankTransferDiscountTotalTaxIncluding = 0,
                    payingAtTheDoorChargeTotalTaxExcluding = 0,
                    payingAtTheDoorChargeTotalTaxIncluding = 0,
                    discountTotalTaxExcluding = 0,
                    discountTotalTaxIncluding = 0,
                    orderDetails = new List<OrderDetailModel>
        {
            new OrderDetailModel
            {
                productCode = "PROD001",
                barcode = "8691234567890",
                productBrand = "Marka Adı",
                productName = "Ürün Adı",
                productNote = "Ek not",
                productQuantityType = "adet",
                productQuantity = 1,
                vatRate = 20,
                productUnitPriceTaxExcluding = 90,
                productUnitPriceTaxIncluding = 108,
                discountIsPercentUnit = 0,
                discountRateUnit = 0,
                discountUnitTaxExcluding = 0,
                discountUnitTaxIncluding = 0
            }
        }
                }
            };

            return SendPost("/OutEBelgeV2/SendBasicInvoiceFromModel", sendBasicInvoiceFromModelBody);
        }

        // Gelen belgeyle ilgili yanıt oluşturur ve gönderir.
        public static Task<ApiResponseModel> SendDocumentAnswer(string UUID)
        {
            var body = new{
                documentUUID= UUID,
                acceptOrRejectCode= "IPTAL",
                acceptOrRejectReason= "string",
                systemTypeCodes= "EARSIV"
            };

            return SendPost("/OutEBelgeV2/SendDocumentAnswer", body);
        }

        // Gelen belgenin durumunu okundu olarak güncellemenizi ve gelen belgelerinizi listelerken filtreleme yapabilmenizi sağlar.
        public static Task<ApiResponseModel> UpdateUnreadedStatus(string UUID)
        {
            var body = new {
                uuid = UUID,
                systemType = "EFATURA",
                documentType = "DOCUMENT",
                inOutCode = "OUT" 
            };

            return SendPost("/OutEBelgeV2/UpdateUnreadedStatus", body);
        }

        // Belirli bir belgeyi istenilen formatta indirir.
        public static Task<ApiResponseModel> DocumentDownloadByUUID(String uuid)
        {
            var body = new
            {
                documentUUID = uuid,
                inOutCode = "OUT",
                systemTypeCodes = "EARSIV",
                fileExtension = "XML"
            };
            return SendPost("/OutEBelgeV2/DocumentDownloadByUUID", body);
        }

        // Belirli bir belgenin PDF formatında almanızı sağlar.
        public static Task<ApiResponseModel> PreviewDocumentReturnPDF()
        {
            DateTime now = DateTime.Now;   // Tarih ve saat bilgilerini alıyoruz
            string documentUUID = uuid_utils.createUUID(); // Rastgele uuid ayarlıyoruz                                                           
            string issueDate = now.ToString("yyyy-MM-dd");
            string issueTime = now.ToString("HH:mm:ss.fffffffzzz"); // Zaman dilimi bilgisiyle birlikte
            string xml = $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Invoice xmlns:ubltr=\"urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents\" xmlns:qdt=\"urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2\" xmlns:cbc=\"urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2\" xmlns:cac=\"urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2\" xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:ccts=\"urn:un:unece:uncefact:documentation:2\" xmlns:ext=\"urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2\" xmlns:xades=\"http://uri.etsi.org/01903/v1.3.2#\" xmlns:udt=\"urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2\" xmlns=\"urn:oasis:names:specification:ubl:schema:xsd:Invoice-2\">\r\n  <ext:UBLExtensions>\r\n    <ext:UBLExtension>\r\n      <ext:ExtensionContent>\r\n        \r\n      </ext:ExtensionContent>\r\n    </ext:UBLExtension>\r\n  </ext:UBLExtensions>\r\n  <cbc:UBLVersionID>2.1</cbc:UBLVersionID>\r\n  <cbc:CustomizationID>TR1.2</cbc:CustomizationID>\r\n  <cbc:ProfileID>TICARIFATURA</cbc:ProfileID>\r\n  <cbc:ID>ARS2024000000001</cbc:ID>\r\n  <cbc:CopyIndicator>false</cbc:CopyIndicator>\r\n  <cbc:UUID>{documentUUID}</cbc:UUID>\r\n  <cbc:IssueDate>{issueDate}</cbc:IssueDate>\r\n  <cbc:IssueTime>{issueTime}</cbc:IssueTime>\r\n  <cbc:InvoiceTypeCode>SATIS</cbc:InvoiceTypeCode>\r\n  <cbc:Note>Yalnız BirTürkLirasıYirmiKuruş&lt;br/&gt;</cbc:Note>\r\n  <cbc:Note>&lt;br&gt;E-Fatura izni kapsamında elektronik ortamda iletilmiştir. &lt;br/&gt;Ödeme Yöntemi: Kredi Kartı İle Ödendi - Sipariş No:_Kopya - Kargo Kampanya Kodu:3321733760237447 - Kargo Şirketi:Aras - Teslimat Bilgileri: TEST EFİRMA Beytepe Mahallesi, Çankaya/Ankara Çankaya Ankara </cbc:Note>\r\n  <cbc:DocumentCurrencyCode>TRY</cbc:DocumentCurrencyCode>\r\n  <cbc:LineCountNumeric>1</cbc:LineCountNumeric>\r\n  <cac:OrderReference>\r\n    <cbc:ID>_Kopya</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n  </cac:OrderReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>88f924da-b4f8-4c22-8db2-8cf9057a8b8d</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>CUST_INV_ID</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>0100</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>OUTPUT_TYPE</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>99</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>TRANSPORT_TYPE</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>ELEKTRONIK</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>EREPSENDT</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>0</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>SendingType</cbc:DocumentTypeCode>\r\n    <cbc:DocumentType>KAGIT</cbc:DocumentType>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>FIT2024000000001</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentType>XSLT</cbc:DocumentType>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>urn:mail:defaultpk@deneme.com</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>recvpk</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:Signature>\r\n    <cbc:ID schemeID=\"VKN_TCKN\">1234567891</cbc:ID>\r\n    <cac:SignatoryParty>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567891</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PostalAddress>\r\n        <cbc:StreetName>Kuşkavağı, Belediye Cd. No:78, 07070 Konyaaltı/Antalya</cbc:StreetName>\r\n        <cbc:CitySubdivisionName>Konyaaltı</cbc:CitySubdivisionName>\r\n        <cbc:CityName>Antalya</cbc:CityName>\r\n        <cac:Country>\r\n          <cbc:Name>TÜRKİYE</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n    </cac:SignatoryParty>\r\n    <cac:DigitalSignatureAttachment>\r\n      <cac:ExternalReference>\r\n        <cbc:URI>#Signature</cbc:URI>\r\n      </cac:ExternalReference>\r\n    </cac:DigitalSignatureAttachment>\r\n  </cac:Signature>\r\n  <cac:AccountingSupplierParty>\r\n    <cac:Party>\r\n      <cbc:WebsiteURI />\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567801</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"TICARETSICILNO\" />\r\n      </cac:PartyIdentification>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"MERSISNO\" />\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Test Firma</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:StreetName>Kuşkavağı, Belediye Cd. No:78, 07070 Konyaaltı/Antalya</cbc:StreetName>\r\n        <cbc:CitySubdivisionName>Konyaaltı</cbc:CitySubdivisionName>\r\n        <cbc:CityName>Antalya</cbc:CityName>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n      <cac:PartyTaxScheme>\r\n        <cac:TaxScheme>\r\n          <cbc:Name>Antalya</cbc:Name>\r\n        </cac:TaxScheme>\r\n      </cac:PartyTaxScheme>\r\n      <cac:Contact>\r\n        <cbc:Telephone>05555555555</cbc:Telephone>\r\n        <cbc:ElectronicMail>info@firma.com</cbc:ElectronicMail>\r\n      </cac:Contact>\r\n    </cac:Party>\r\n  </cac:AccountingSupplierParty>\r\n  <cac:AccountingCustomerParty>\r\n    <cac:Party>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567891</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Test Firması</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:StreetName>Beytepe Mahallesi, Çankaya/ankara</cbc:StreetName>\r\n        <cbc:BuildingName />\r\n        <cbc:CitySubdivisionName>Çankaya</cbc:CitySubdivisionName>\r\n        <cbc:CityName>Ankara</cbc:CityName>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n      <cac:PartyTaxScheme>\r\n        <cac:TaxScheme>\r\n          <cbc:Name>Ankara Kurumlar Vergi Dairesi Müdürlüğü</cbc:Name>\r\n        </cac:TaxScheme>\r\n      </cac:PartyTaxScheme>\r\n      <cac:Contact>\r\n        <cbc:Telephone />\r\n        <cbc:ElectronicMail />\r\n      </cac:Contact>\r\n    </cac:Party>\r\n  </cac:AccountingCustomerParty>\r\n  <cac:Delivery>\r\n    <cac:DeliveryAddress>\r\n      <cbc:StreetName>Talatpaşa Cad.</cbc:StreetName>\r\n      <cbc:CitySubdivisionName>Ümraniye</cbc:CitySubdivisionName>\r\n      <cbc:CityName>İstanbul</cbc:CityName>\r\n      <cac:Country>\r\n        <cbc:Name>Türkiye</cbc:Name>\r\n      </cac:Country>\r\n    </cac:DeliveryAddress>\r\n    <cac:CarrierParty>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567891</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Aras Kargo</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:ID />\r\n        <cbc:StreetName>Rüzgarlıbahçe Mah. Yavuz Sultan Selim Cad.</cbc:StreetName>\r\n        <cbc:BuildingName>Aras Plaza</cbc:BuildingName>\r\n        <cbc:BuildingNumber>2</cbc:BuildingNumber>\r\n        <cbc:CitySubdivisionName>Beykoz</cbc:CitySubdivisionName>\r\n        <cbc:CityName>İstanbul</cbc:CityName>\r\n        <cbc:PostalZone>34000</cbc:PostalZone>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n    </cac:CarrierParty>\r\n    <cac:DeliveryParty>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID />\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Teslimat yapılacak isim</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:ID />\r\n        <cbc:StreetName>Talatpaşa Cad. Park Sok.</cbc:StreetName>\r\n        <cbc:BuildingNumber>35-1</cbc:BuildingNumber>\r\n        <cbc:CitySubdivisionName>Ümraniye</cbc:CitySubdivisionName>\r\n        <cbc:CityName>İstanbul</cbc:CityName>\r\n        <cbc:PostalZone>34000</cbc:PostalZone>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n      <cac:Person>\r\n        <cbc:FirstName>Teslim alacak kişi isim</cbc:FirstName>\r\n        <cbc:FamilyName>Teslim alacak kişi soyisim</cbc:FamilyName>\r\n      </cac:Person>\r\n    </cac:DeliveryParty>\r\n  </cac:Delivery>\r\n  <cac:TaxTotal>\r\n    <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n    <cac:TaxSubtotal>\r\n      <cbc:TaxableAmount currencyID=\"TRY\">1.00</cbc:TaxableAmount>\r\n      <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n      <cbc:Percent>20</cbc:Percent>\r\n      <cac:TaxCategory>\r\n        <cac:TaxScheme>\r\n          <cbc:Name>KDV</cbc:Name>\r\n          <cbc:TaxTypeCode>0015</cbc:TaxTypeCode>\r\n        </cac:TaxScheme>\r\n      </cac:TaxCategory>\r\n    </cac:TaxSubtotal>\r\n  </cac:TaxTotal>\r\n  <cac:LegalMonetaryTotal>\r\n    <cbc:LineExtensionAmount currencyID=\"TRY\">1.00</cbc:LineExtensionAmount>\r\n    <cbc:TaxExclusiveAmount currencyID=\"TRY\">1.00</cbc:TaxExclusiveAmount>\r\n    <cbc:TaxInclusiveAmount currencyID=\"TRY\">1.20</cbc:TaxInclusiveAmount>\r\n    <cbc:AllowanceTotalAmount currencyID=\"TRY\">0.00</cbc:AllowanceTotalAmount>\r\n    <cbc:PayableAmount currencyID=\"TRY\">1.20</cbc:PayableAmount>\r\n  </cac:LegalMonetaryTotal>\r\n  <cac:InvoiceLine>\r\n    <cbc:ID>1</cbc:ID>\r\n    <cbc:InvoicedQuantity unitCode=\"NIU\">1.0000</cbc:InvoicedQuantity>\r\n    <cbc:LineExtensionAmount currencyID=\"TRY\">1.00</cbc:LineExtensionAmount>\r\n    <cac:TaxTotal>\r\n      <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n      <cac:TaxSubtotal>\r\n        <cbc:TaxableAmount currencyID=\"TRY\">1.00</cbc:TaxableAmount>\r\n        <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n        <cbc:Percent>20</cbc:Percent>\r\n        <cac:TaxCategory>\r\n          <cac:TaxScheme>\r\n            <cbc:Name>KDV</cbc:Name>\r\n            <cbc:TaxTypeCode>0015</cbc:TaxTypeCode>\r\n          </cac:TaxScheme>\r\n        </cac:TaxCategory>\r\n      </cac:TaxSubtotal>\r\n    </cac:TaxTotal>\r\n    <cac:Item>\r\n      <cbc:Description />\r\n      <cbc:Name>Cep Telefonu Aksesuarı</cbc:Name>\r\n      <cac:BuyersItemIdentification>\r\n        <cbc:ID />\r\n      </cac:BuyersItemIdentification>\r\n      <cac:SellersItemIdentification>\r\n        <cbc:ID>56898T10Stani1805 </cbc:ID>\r\n      </cac:SellersItemIdentification>\r\n      <cac:AdditionalItemIdentification>\r\n        <cbc:ID />\r\n      </cac:AdditionalItemIdentification>\r\n    </cac:Item>\r\n    <cac:Price>\r\n      <cbc:PriceAmount currencyID=\"TRY\">1.000000</cbc:PriceAmount>\r\n    </cac:Price>\r\n  </cac:InvoiceLine>\r\n</Invoice>";
            byte[] byteArray = zip_utils.IonicZipFile(xml, documentUUID);
            // Örnek: Belge gönderme
            var body = new
            {  
                documentBytes = byteArray,   // UBL/XML belgenin Base64 hali        
                systemTypeCodes = "EFATURA"     // sistem tipi
            };
            return SendPost("/OutEBelgeV2/PreviewDocumentReturnPDF", body);
        }

        // Belirli bir belgenin HTML olarak almanızı sağlar.
        public static Task<ApiResponseModel> PreviewDocumentReturnHTML(string uuid)
        {
            string documentUUID = uuid; // Rastgele uuid ayarlıyoruz                                                           
            string issueDate = time_utils.timeNowTypeDate();
            string issueTime = time_utils.timeNowTypeTimeZone(); // Zaman dilimi bilgisiyle birlikte
            string xml = $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<Invoice xmlns:ubltr=\"urn:oasis:names:specification:ubl:schema:xsd:TurkishCustomizationExtensionComponents\" xmlns:qdt=\"urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2\" xmlns:cbc=\"urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2\" xmlns:cac=\"urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2\" xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:ccts=\"urn:un:unece:uncefact:documentation:2\" xmlns:ext=\"urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2\" xmlns:xades=\"http://uri.etsi.org/01903/v1.3.2#\" xmlns:udt=\"urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2\" xmlns=\"urn:oasis:names:specification:ubl:schema:xsd:Invoice-2\">\r\n  <ext:UBLExtensions>\r\n    <ext:UBLExtension>\r\n      <ext:ExtensionContent>\r\n        \r\n      </ext:ExtensionContent>\r\n    </ext:UBLExtension>\r\n  </ext:UBLExtensions>\r\n  <cbc:UBLVersionID>2.1</cbc:UBLVersionID>\r\n  <cbc:CustomizationID>TR1.2</cbc:CustomizationID>\r\n  <cbc:ProfileID>TICARIFATURA</cbc:ProfileID>\r\n  <cbc:ID>ARS2024000000001</cbc:ID>\r\n  <cbc:CopyIndicator>false</cbc:CopyIndicator>\r\n  <cbc:UUID>{documentUUID}</cbc:UUID>\r\n  <cbc:IssueDate>{issueDate}</cbc:IssueDate>\r\n  <cbc:IssueTime>{issueTime}</cbc:IssueTime>\r\n  <cbc:InvoiceTypeCode>SATIS</cbc:InvoiceTypeCode>\r\n  <cbc:Note>Yalnız BirTürkLirasıYirmiKuruş&lt;br/&gt;</cbc:Note>\r\n  <cbc:Note>&lt;br&gt;E-Fatura izni kapsamında elektronik ortamda iletilmiştir. &lt;br/&gt;Ödeme Yöntemi: Kredi Kartı İle Ödendi - Sipariş No:_Kopya - Kargo Kampanya Kodu:3321733760237447 - Kargo Şirketi:Aras - Teslimat Bilgileri: TEST EFİRMA Beytepe Mahallesi, Çankaya/Ankara Çankaya Ankara </cbc:Note>\r\n  <cbc:DocumentCurrencyCode>TRY</cbc:DocumentCurrencyCode>\r\n  <cbc:LineCountNumeric>1</cbc:LineCountNumeric>\r\n  <cac:OrderReference>\r\n    <cbc:ID>_Kopya</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n  </cac:OrderReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>88f924da-b4f8-4c22-8db2-8cf9057a8b8d</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>CUST_INV_ID</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>0100</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>OUTPUT_TYPE</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>99</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>TRANSPORT_TYPE</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>ELEKTRONIK</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>EREPSENDT</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>0</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>SendingType</cbc:DocumentTypeCode>\r\n    <cbc:DocumentType>KAGIT</cbc:DocumentType>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>FIT2024000000001</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentType>XSLT</cbc:DocumentType>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:AdditionalDocumentReference>\r\n    <cbc:ID>urn:mail:defaultpk@deneme.com</cbc:ID>\r\n    <cbc:IssueDate>2024-12-19</cbc:IssueDate>\r\n    <cbc:DocumentTypeCode>recvpk</cbc:DocumentTypeCode>\r\n  </cac:AdditionalDocumentReference>\r\n  <cac:Signature>\r\n    <cbc:ID schemeID=\"VKN_TCKN\">1234567891</cbc:ID>\r\n    <cac:SignatoryParty>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567891</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PostalAddress>\r\n        <cbc:StreetName>Kuşkavağı, Belediye Cd. No:78, 07070 Konyaaltı/Antalya</cbc:StreetName>\r\n        <cbc:CitySubdivisionName>Konyaaltı</cbc:CitySubdivisionName>\r\n        <cbc:CityName>Antalya</cbc:CityName>\r\n        <cac:Country>\r\n          <cbc:Name>TÜRKİYE</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n    </cac:SignatoryParty>\r\n    <cac:DigitalSignatureAttachment>\r\n      <cac:ExternalReference>\r\n        <cbc:URI>#Signature</cbc:URI>\r\n      </cac:ExternalReference>\r\n    </cac:DigitalSignatureAttachment>\r\n  </cac:Signature>\r\n  <cac:AccountingSupplierParty>\r\n    <cac:Party>\r\n      <cbc:WebsiteURI />\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567801</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"TICARETSICILNO\" />\r\n      </cac:PartyIdentification>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"MERSISNO\" />\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Test Firma</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:StreetName>Kuşkavağı, Belediye Cd. No:78, 07070 Konyaaltı/Antalya</cbc:StreetName>\r\n        <cbc:CitySubdivisionName>Konyaaltı</cbc:CitySubdivisionName>\r\n        <cbc:CityName>Antalya</cbc:CityName>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n      <cac:PartyTaxScheme>\r\n        <cac:TaxScheme>\r\n          <cbc:Name>Antalya</cbc:Name>\r\n        </cac:TaxScheme>\r\n      </cac:PartyTaxScheme>\r\n      <cac:Contact>\r\n        <cbc:Telephone>05555555555</cbc:Telephone>\r\n        <cbc:ElectronicMail>info@firma.com</cbc:ElectronicMail>\r\n      </cac:Contact>\r\n    </cac:Party>\r\n  </cac:AccountingSupplierParty>\r\n  <cac:AccountingCustomerParty>\r\n    <cac:Party>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567891</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Test Firması</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:StreetName>Beytepe Mahallesi, Çankaya/ankara</cbc:StreetName>\r\n        <cbc:BuildingName />\r\n        <cbc:CitySubdivisionName>Çankaya</cbc:CitySubdivisionName>\r\n        <cbc:CityName>Ankara</cbc:CityName>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n      <cac:PartyTaxScheme>\r\n        <cac:TaxScheme>\r\n          <cbc:Name>Ankara Kurumlar Vergi Dairesi Müdürlüğü</cbc:Name>\r\n        </cac:TaxScheme>\r\n      </cac:PartyTaxScheme>\r\n      <cac:Contact>\r\n        <cbc:Telephone />\r\n        <cbc:ElectronicMail />\r\n      </cac:Contact>\r\n    </cac:Party>\r\n  </cac:AccountingCustomerParty>\r\n  <cac:Delivery>\r\n    <cac:DeliveryAddress>\r\n      <cbc:StreetName>Talatpaşa Cad.</cbc:StreetName>\r\n      <cbc:CitySubdivisionName>Ümraniye</cbc:CitySubdivisionName>\r\n      <cbc:CityName>İstanbul</cbc:CityName>\r\n      <cac:Country>\r\n        <cbc:Name>Türkiye</cbc:Name>\r\n      </cac:Country>\r\n    </cac:DeliveryAddress>\r\n    <cac:CarrierParty>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID schemeID=\"VKN\">1234567891</cbc:ID>\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Aras Kargo</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:ID />\r\n        <cbc:StreetName>Rüzgarlıbahçe Mah. Yavuz Sultan Selim Cad.</cbc:StreetName>\r\n        <cbc:BuildingName>Aras Plaza</cbc:BuildingName>\r\n        <cbc:BuildingNumber>2</cbc:BuildingNumber>\r\n        <cbc:CitySubdivisionName>Beykoz</cbc:CitySubdivisionName>\r\n        <cbc:CityName>İstanbul</cbc:CityName>\r\n        <cbc:PostalZone>34000</cbc:PostalZone>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n    </cac:CarrierParty>\r\n    <cac:DeliveryParty>\r\n      <cac:PartyIdentification>\r\n        <cbc:ID />\r\n      </cac:PartyIdentification>\r\n      <cac:PartyName>\r\n        <cbc:Name>Teslimat yapılacak isim</cbc:Name>\r\n      </cac:PartyName>\r\n      <cac:PostalAddress>\r\n        <cbc:ID />\r\n        <cbc:StreetName>Talatpaşa Cad. Park Sok.</cbc:StreetName>\r\n        <cbc:BuildingNumber>35-1</cbc:BuildingNumber>\r\n        <cbc:CitySubdivisionName>Ümraniye</cbc:CitySubdivisionName>\r\n        <cbc:CityName>İstanbul</cbc:CityName>\r\n        <cbc:PostalZone>34000</cbc:PostalZone>\r\n        <cac:Country>\r\n          <cbc:Name>Türkiye</cbc:Name>\r\n        </cac:Country>\r\n      </cac:PostalAddress>\r\n      <cac:Person>\r\n        <cbc:FirstName>Teslim alacak kişi isim</cbc:FirstName>\r\n        <cbc:FamilyName>Teslim alacak kişi soyisim</cbc:FamilyName>\r\n      </cac:Person>\r\n    </cac:DeliveryParty>\r\n  </cac:Delivery>\r\n  <cac:TaxTotal>\r\n    <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n    <cac:TaxSubtotal>\r\n      <cbc:TaxableAmount currencyID=\"TRY\">1.00</cbc:TaxableAmount>\r\n      <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n      <cbc:Percent>20</cbc:Percent>\r\n      <cac:TaxCategory>\r\n        <cac:TaxScheme>\r\n          <cbc:Name>KDV</cbc:Name>\r\n          <cbc:TaxTypeCode>0015</cbc:TaxTypeCode>\r\n        </cac:TaxScheme>\r\n      </cac:TaxCategory>\r\n    </cac:TaxSubtotal>\r\n  </cac:TaxTotal>\r\n  <cac:LegalMonetaryTotal>\r\n    <cbc:LineExtensionAmount currencyID=\"TRY\">1.00</cbc:LineExtensionAmount>\r\n    <cbc:TaxExclusiveAmount currencyID=\"TRY\">1.00</cbc:TaxExclusiveAmount>\r\n    <cbc:TaxInclusiveAmount currencyID=\"TRY\">1.20</cbc:TaxInclusiveAmount>\r\n    <cbc:AllowanceTotalAmount currencyID=\"TRY\">0.00</cbc:AllowanceTotalAmount>\r\n    <cbc:PayableAmount currencyID=\"TRY\">1.20</cbc:PayableAmount>\r\n  </cac:LegalMonetaryTotal>\r\n  <cac:InvoiceLine>\r\n    <cbc:ID>1</cbc:ID>\r\n    <cbc:InvoicedQuantity unitCode=\"NIU\">1.0000</cbc:InvoicedQuantity>\r\n    <cbc:LineExtensionAmount currencyID=\"TRY\">1.00</cbc:LineExtensionAmount>\r\n    <cac:TaxTotal>\r\n      <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n      <cac:TaxSubtotal>\r\n        <cbc:TaxableAmount currencyID=\"TRY\">1.00</cbc:TaxableAmount>\r\n        <cbc:TaxAmount currencyID=\"TRY\">0.20</cbc:TaxAmount>\r\n        <cbc:Percent>20</cbc:Percent>\r\n        <cac:TaxCategory>\r\n          <cac:TaxScheme>\r\n            <cbc:Name>KDV</cbc:Name>\r\n            <cbc:TaxTypeCode>0015</cbc:TaxTypeCode>\r\n          </cac:TaxScheme>\r\n        </cac:TaxCategory>\r\n      </cac:TaxSubtotal>\r\n    </cac:TaxTotal>\r\n    <cac:Item>\r\n      <cbc:Description />\r\n      <cbc:Name>Cep Telefonu Aksesuarı</cbc:Name>\r\n      <cac:BuyersItemIdentification>\r\n        <cbc:ID />\r\n      </cac:BuyersItemIdentification>\r\n      <cac:SellersItemIdentification>\r\n        <cbc:ID>56898T10Stani1805 </cbc:ID>\r\n      </cac:SellersItemIdentification>\r\n      <cac:AdditionalItemIdentification>\r\n        <cbc:ID />\r\n      </cac:AdditionalItemIdentification>\r\n    </cac:Item>\r\n    <cac:Price>\r\n      <cbc:PriceAmount currencyID=\"TRY\">1.000000</cbc:PriceAmount>\r\n    </cac:Price>\r\n  </cac:InvoiceLine>\r\n</Invoice>";
            byte[] byteArray = zip_utils.IonicZipFile(xml, documentUUID);
            // Örnek: Belge gönderme
            var body = new
            {
                documentBytes = byteArray,   // UBL/XML belgenin Base64 hali        
                systemTypeCodes = "EFATURA"     // sistem tipi
            };
            return SendPost("/OutEBelgeV2/PreviewDocumentReturnHTML", body);
        }

        // Belirli bir belgenin işlem kayıtlarını verir.
        public static Task<ApiResponseModel> GetDocumentLogs(string UUID)
        {
            var body = new
            {
                documentUUID = UUID
            };

            return SendPost("/OutEBelgeV2/GetDocumentLogs", body);
        }

        // Kullanıcıya ait alıcı etiket bilgisini verir.
        public static Task<ApiResponseModel> GetUserPK(string vergiNo)
        {
            var body = new
            {
                kn = vergiNo
            };
            return SendPost("/OutEBelgeV2/GetUserPK", body);
        }

        // Kullanıcıya ait gönderici etiket bilgisini verir.
        public static Task<ApiResponseModel> GetUserGB(string vergiNo)
        {
            var body = new
            {
                kn = vergiNo
            };
            return SendPost("/OutEBelgeV2/GetUserGB", body);
        }

        // Kullanıcılara ait etiket bilgisini sayfalama ile döner.
        public static Task<ApiResponseModel> GibUserList(string vergiNo)
        {
            var body = new
            {
                pkGbTypeCode = vergiNo,
                pageNumber = 1,
                pageSize = 1

            };
            return SendPost("/OutEBelgeV2/GibUserList", body);
        }

        // GIB'ten zarfın son durumunu sorgulayabilmenizi sağlar .
        public static Task<ApiResponseModel> GetEnvelopeStatusFromGIB(string UUID)
        {
            var body = new
            {
                envelopeID = UUID,
                inOutCode = "OUT"
            };
            return SendPost("/OutEBelgeV2/GetUserPK", body);
        }

        // Tüm vergi dailerine ait bilgileri verir.
        public static Task<ApiResponseModel> GetTaxOfficesAndCodes()
        {
            var body = new {};
            return SendPost("/OutEBelgeV2/GetTaxOfficesAndCodes", body);
        }

        // Kullanıcının kalan kontör sayısını döndürür.
        public static Task<ApiResponseModel> GetNumberOfCredits(string vergiNo)
        {
            var body = new
            {
                kn = vergiNo
            };

            return SendPost("/OutEBelgeV2/GetNumberOfCredits", body);
        }

        // Tarafınızca gönderilen belgeleri sayfalama ile verir.
        public static Task<ApiResponseModel> GetOutBoxDocuments()
        {
            var body = new 
            {
                systemType = "EFATURA",
                startDateTime = "2025-10-01T19:47:12.736Z",
                endDateTime = "2025-10-15T19:47:12.736Z",
                documentType = "INVOICE",
                readUnReadStatus = "UNREADED",
                pageNumber = 0
            };

            return SendPost("/OutEBelgeV2/GetOutBoxDocuments", body);
        }

        // Tarafınıza gelen belgeleri sayfalama ile verir.
        public static Task<ApiResponseModel> GetInBoxDocuments()
        {
            var body = new 
            {
                systemType = "EFATURA",
                startDateTime = "2025-10-01T19:55:47.960Z",
                endDateTime ="2025-10-15T19:55:47.960Z",
                documentType ="INVOICE",
                readUnReadStatus = "UNREADED",
                pageNumber = 0
            };
            return SendPost("/OutEBelgeV2/GetInBoxDocuments", body);
        }

        // Belge detayı
        public static Task<ApiResponseModel> GetOutBoxDocumentByUUID(string UUID)
        {
            var body = new
            {
                uuid=UUID,
                systemType = "EFATURA"
                
            };
            return SendPost("/OutEBelgeV2/GetOutBoxDocumentByUUID", body);
        }

        // Birim kodlarını, Vergi İstisna Kodlarını, Vergi Kodlarını, Tevkifat Kodlarını, Para Birim Kodlarını, Ülke Kodlarını ve Şehir bilgilerini almanızı sağlar.
        public static Task<ApiResponseModel> GetCodeListByType()
        {
            var body = new 
            {
                CodeType = 1
            };
            return SendPost("/OutEBelgeV2/GetCodeListByType", body);
        }

        // Tarafınızca gönderilen belgeleri sayfalama ile verir, faturanın XML içeriğini jsonData değişkeninde döner.
        public static Task<ApiResponseModel> GetOutBoxDocumentsWithDetail()
        {
            var body = new 
            {
                systemType ="EFATURA",
                startDateTime = "2025-10-01T20:13:58.635Z",
                endDateTime = "2025-10-15T20:13:58.635Z",
                documentType = "INVOICE",
                readUnReadStatus = "UNREADED",
                pageNumber = 0
            };
            return SendPost("/OutEBelgeV2/GetOutBoxDocumentsWithDetail", body);
        }

        // Tarafınıza gelen belgeleri sayfalama ile verir, belgenin XML içeriğini jsonData değişkeninde döner.
        public static Task<ApiResponseModel> GetInBoxDocumentsWithDetail()
        {
            var body = new 
            {
                systemType = "EFATURA",
                startDateTime = "2025-10-01T20:19:23.667Z",
                endDateTime = "2025-10-15T20:19:23.667Z",
                documentType = "INVOICE",
                readUnReadStatus = "UNREADED",
                pageNumber = 0
            };
            return SendPost("/OutEBelgeV2/GetInBoxDocumentsWithDetail", body);
        }

    }
}
