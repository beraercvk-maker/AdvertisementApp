# AdvertisementApp

AdvertisementApp, iş ilanları ve başvuru işlemlerini yöneten bir ASP.NET Core MVC projesidir. Kullanıcılar ilanları görüntüleyebilir, başvuru formunu doldurabilir ve başvurularını sisteme kaydedebilir.

## Proje Amacı

- İş ilanlarını listeleme
- İlan detayına ulaşma
- Kullanıcı kaydı ve giriş işlemleri
- Başvuru formu ile CV yükleme ve başvuru kaydı
- Katmanlı mimari ile kod düzeni ve sürdürülebilirlik

## Teknolojiler

- ASP.NET Core MVC
- .NET 9
- Entity Framework Core
- SQL Server / LocalDB
- AutoMapper
- FluentValidation
- Razor View
- Cookie Authentication

## Mimari Yapı

Proje katmanlı bir yapıya sahiptir:

- AdvertisementApp.UI: Görünüm katmanı, MVC controller ve Razor sayfaları
- AdvertisementApp.Business: İş kuralları, servisler, validasyon ve mapping işlemleri
- AdvertisementApp.DataAccess: Veritabanı erişimi, repository ve Unit Of Work yapısı
- AdvertisementApp.Entities: Veritabanı entity sınıfları
- AdvertisementApp.Dtos: DTO sınıfları
- AdvertisementApp.Common: Ortak response sınıfları ve yardımcı yapılar

## Proje Yapısı

```text
AdvertisementApp/
├── AdvertisementApp.Business/
│   ├── DependencyResolvers/
│   ├── Interfaces/
│   ├── Mappings/
│   ├── Services/
│   └── ValidationRules/
├── AdvertisementApp.Common/
├── AdvertisementApp.DataAccess/
│   ├── Context/
│   ├── Interfaces/
│   ├── Repositories/
│   └── UnitOfWork/
├── AdvertisementApp.Dtos/
├── AdvertisementApp.Entities/
├── AdvertisementApp.UI/
│   ├── Controllers/
│   ├── Models/
│   ├── Views/
│   ├── wwwroot/
│   ├── Program.cs
│   └── appsettings.json
├── AdvertisementApp.sln
└── README.md
```

## Ön Koşullar

Aşağıdakilerin kurulu olması gerekir:

- .NET SDK 9.0+
- SQL Server LocalDB ya da erişilebilir bir SQL Server instance
- Visual Studio 2022 veya VS Code

## Veritabanı Ayarı

Projede bağlantı stringi `AdvertisementApp.UI/appsettings.json` içerisinde tanımlıdır:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AdvertisementAppDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

Açıkça belirtmek gerekirse, proje varsayılan olarak `LocalDB` kullanmaktadır. Gerekirse bu bağlantı stringini kendi ortamınıza göre güncelleyin.

## Kurulum

1. Projeyi klonlayın:

```bash
git clone <repo-url>
cd AdvertisementApp
```

2. Çözümü restore edin:

```bash
dotnet restore
```

3. Veritabanını oluşturmak için migration işlemi yapmanız gerekebilir. Proje otomatik migration kullanmıyorsa aşağıdaki komutla veritabanı oluşturulabilir:

```bash
dotnet ef database update --project AdvertisementApp.DataAccess --startup-project AdvertisementApp.UI
```

> Eğer `dotnet ef` komutu kurulu değilse, önce .NET Entity Framework CLI paketini kurmanız gerekebilir.

## Çalıştırma

Proje kök klasöründe aşağıdaki komut ile çalıştırılabilir:

```bash
dotnet run --project AdvertisementApp.UI
```

Tarayıcıda şu adrese giderek uygulamayı görebilirsiniz:

```text
http://localhost:5032
```

## Ana Özellikler

### 1. İlan Listesi
- Ana sayfada veya Human Resources sayfasında aktif ilanlar listelenir.
- Her ilan için başvuru linki bulunur.

### 2. Kullanıcı Kaydı ve Giriş
- Üye kaydı yapılabilir.
- Cookie tabanlı kimlik doğrulama kullanılır.
- Giriş yapıldıktan sonra kullanıcı role bilgisi ile yetkilendirme yapılabilir.

### 3. Başvuru Formu
- Kullanıcı ilan için başvuru formunu doldurur.
- İş tecrübesi, askerlik durumu, tecil tarihi ve CV bilgisi alınır.
- CV dosyası yüklenir ve sunucu tarafında kayıt edilir.

### 4. Başvuru Kaydı
- Başvuru verisi veritabanına kaydedilir.
- Başvuru ile ilgili durum, ilan, kullanıcı ve askerlik bilgileri ilişkilendirilir.

## Geliştirme Notları

- Projede `Repository` ve `UnitOfWork` deseni kullanılmıştır.
- Business katmanı servisler ve validasyonlarla ayrılmıştır.
- UI katmanı sadece görünüm ve kullanıcı etkileşimiyle ilgilenir.
- Proje katmanlı mimariye uygun şekilde tasarlanmıştır.

## Sık Karşılaşılan Sorunlar

### 1. DLL kilitlenmesi hatası
Eğer `dotnet run` sırasında `MSB3021` veya `MSB3027` hataları alırsanız, eski çalışan .NET süreçleri kapatılmalıdır.

```powershell
taskkill /F /IM dotnet.exe /T
```

Sonrasında tekrar çalıştırın.

### 2. Veritabanı bağlantısı sorunu
`appsettings.json` içindeki `DefaultConnection` stringini kontrol edin.

### 3. LocalDB yoksa
Eğer SQL Server LocalDB kurulu değilse, SQL Server instance bağlantısını güncelleyin.

## Katkı

Projeye katkı yapmak isterseniz, öncelikle yeni bir branch oluşturup ardından pull request açabilirsiniz.

## Lisans

Bu proje için özel bir lisans tanımı yapılmamıştır. İhtiyaca göre lisans eklenebilir.
