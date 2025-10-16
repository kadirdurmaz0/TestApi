# TestApi - C# Test API Projesi

Bu proje, çeşitli API çağrılarını gerçekleştirmek için hazırlanmış bir C# test uygulamasıdır. Projede `post_service` sınıfı aracılığıyla farklı API işlemleri asenkron olarak yapılmakta ve sonuçlar konsola yazdırılmaktadır.

---

## Proje Yapısı

- **Settings**  
  `config.cs` dosyasını içerir. API çağrılarında kullanılan temel konfigürasyonlar ve sabitler burada tanımlıdır (örneğin test UUID’leri ve vergi numarası).

- **Models**  
  API çağrılarında kullanılan veri modellerini içerir.

- **Service**  
  `post_service.cs` dosyasını içerir. API'ye HTTP isteklerini yapan servis metotları burada yer alır.

- **Utils**  
  Yardımcı araçlar ve metotlar:  
  - `zip_utils.cs`  
  - `time_utils.cs`

- **Program.cs**  
  Uygulamanın giriş noktasıdır. Asenkron olarak API çağrılarını yapar ve sonuçlarını konsola yazdırır.

---

## Kullanılan Teknolojiler

- C# (.NET 6 veya üzeri önerilir)
- [Newtonsoft.Json](https://www.newtonsoft.com/json) - JSON işlemleri için
- Asenkron programlama (`async` / `await`)

---

## Örnek Kullanım

`Program.cs` içerisinde çeşitli API test çağrıları yapılmıştır. Örneğin:

```csharp
var previewDocumentReturnPDF = await post_service.PreviewDocumentReturnPDF();
printTestResult(previewDocumentReturnPDF, "PreviewDocumentReturnPDF");
