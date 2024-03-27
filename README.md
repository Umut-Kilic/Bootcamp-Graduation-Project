# Bootcamp-Graduation-Project

Bootcamp Bitirme projemde Şikayet Var benzeri bir site yaptım.
Sitede 2 tür kullanıcı türü vardır. Biri kullanıcı(user) diğeri ise admindir.
1-Kullanıcılar:  ilk başta siteye üye olarak istediği kategoride şikayet yayınlama fırsatı sunuyor.
    Yayınladığı şikayeti istediği gibi güncelleme yapabilmektedir. Ayrıca yayınladığı şikayeti diğer kullanıcılar 
    beğenip altına yorum ekleyerek kullanıcıya destek verebilmektedir. Ayrıca kullancılar birbirlerinin profillerine bakarak
    ne kadar şikayet yayınladığını görebilmektedir.Kendi profil sayfasından kişisel bilgilerini de isteğe bağlı güncelleyebilmektedir.
    Ayrıca olası bir şifre hatırlayamama durumunda  üye olduğu maile şifre sıfırlama linki gönderilmektedir.Bu link 2 saat geçerli olmaktadır.

2-Admin: Admin kullanıcısı ise kullanıclardan farklı olarak kullamnıcların rollerinin eklenmesi,silmesi ve güncellenmesi işlemlerini yapabilmektedir.
    Ayrıca ekstra rol ekleme,silme,güncelleme işlemleri yapabilmektedir. 
    Sitedeki yayınlanan şikayetlerin isActive durumuna göre ana sayfa ve diğer sayfalarda gözüküp gözükmemesini sağlamaktadır.

Sitede kullandığım Mimari ve teknolojiler:
1-Kullandığım Mimari: Katmanlı mmimari(N-Layer) kullanarak tasarlanmıştır. Katmanlarım Core,Repository,Service ve Web katmanıdır.

A- Core katmanı: Bu katmanda entitylerim,Sayfalarda kullanılan viewModeller,Ve sözleşmeler(Service,Repostiry katmanlarının interfaceleri,IunitOfWork pattern interfaceleri) bulunmaktadır.Bu katmanda 
AspNetCore.Identity sınıfından yaralanarak user oluşturduğum için Nuget Manager ileMicrosoft.AspNetCore.Identity.EntityFrameworkCore paketi indirilmiştir.

B- Repository katmanı: Bu katmanda Db bağlantısını sağlayan dbContext nesnem bulunmaktadır.Ayrıca Core katamanındaki sözleşmelerin(interfacelerin) kullanıldığı katmandır.Bu katmanda db ile ilgili işlemler yyapolmaktadır.Atrıca Core katmanından referans almaktadır.Projemde EntityFramework kullanacağım için bu katamana Microsoft.EntityFrameworkCore paketini indridim. Ayrıca bu sınıfta dbContext nesnem bullunduğu için ve bende CodeFirst yaklaşımı benimsediğim için Package-manager-consoledan migration oluşturabilmek için Microsoft.EntityFrameworkCore.Tools paketi ve projemde veritabanı olarak  Microsoft SqlServer kullandığım için Microsoft.EntityFrameworkCore.SqlServer paketini bu katamana indirdim.

C- Service katmanı: Bu katmanda Core katamanındaki sözleşmelerin(interfacelerin) kullanıldığı katmandır.Bu katmanda Business kodları bulunmaktadır. Repository katmanından referans almaktadır boylelıkle dbye gitmeden business kodları burda icra edilecek olası bir hatada dbye gitmeden Web katmanına hata dönecektir.Ayrıca bu katmanda Kullanıcının bilgilerinin belirli bir formatta olmasını(örneğin benim projemde kullanıcı adının türkçe harf içermemesi gerekmekedir.) sağlayan validations sınıflarım vardır.Her biri asp.net core ıdentity teknolojisinden yararlanılarak yazılmıştırç

D- Web katmanı: Bu katmanda Mvc yapısı kullanılmıştır. kullanıcı ve admin controllerını ayırmak için Admin adında bir area oluşturdum. Program.cs çok fazla uzamasını önlemek için Autofac.Extensions.DependencyInjection kütüphanesini bu katmana ındırerek gerekli depency Injection işlemlerini module klasoru altında yaparak kısa bir görünüm kazandırdım.Bu katmanda referans olarak yalnızca Service katmanını almakadır böylece katmanlar arası bağlantıları tamamlamış oldum.

2-Kullandığım Teknolojiler:

Temel olarak Backend için 
-Microsoft.EntityFrameworkCore
-Microsoft.EntityFrameworkCore.Design
-Db için Microsoft.EntityFrameworkCore.SqlServer
-Migration için Microsoft.EntityFrameworkCore.Tools
-Microsoft.EntityFrameworkCore.Design

Ayrıca ajax işlemleri için jquery

Front-end için ise 
-Bootsrap
-Css
kullanılmıştır.