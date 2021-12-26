# ContactReport
- ContactReportApp Proje birbirleri ile haberleşen iki microservice'in olduğu bir yapı tasarlayarak, bir telefon rehberi uygulaması oluşturulması sağlanacaktır.

- ContactReportApp Proje İki api, bir web uygulaması apiler çağırmak için ve unit test class library kod test etmek için.

- Uygulama işleri:
    • Rehberde kişi oluşturma <br />
    • Rehberde kişi kaldırma  <br />
    • Rehberdeki kişiye iletişim bilgisi ekleme 
    • Rehberdeki kişiden iletişim bilgisi kaldırma 
    • Rehberdeki kişilerin listelenmesi 
    • Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi 
    • Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi 
    • Sistemin oluşturduğu raporların listelenmesi 
    • Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi 

-  Teknolojiler:
    • .NET Core
    • Git
    • Postgres (pgAdmin)
    • Kafka Message Queue sistemi
    
-  ContactReportApp Proje nasıl çalışır?
   
   1 - DB Migration: bize iki veritabanı vardır "contactdb" ve "reportdb" ikisini için aynı şekilde Migration oluyor.
        - Add-Migration --> Name: contactdb
        - Update-Database

   2 - Kafka Message Queue sistemi kurulum ve çalışması için bir kaç adım vardır.
   


