# ContactReport
- ContactReportApp Proje birbirleri ile haberleşen iki microservice'in olduğu bir yapı tasarlayarak, bir telefon rehberi uygulaması oluşturulması sağlanacaktır.

- ContactReportApp Proje İki api, bir web uygulaması apiler çağırmak için ve unit test class library kod test etmek için.

- Uygulama işleri: <br />
    • Rehberde kişi oluşturma <br />
    • Rehberde kişi kaldırma  <br />
    • Rehberdeki kişiye iletişim bilgisi ekleme <br />
    • Rehberdeki kişiden iletişim bilgisi kaldırma <br />
    • Rehberdeki kişilerin listelenmesi <br />
    • Rehberdeki bir kişiyle ilgili iletişim bilgilerinin de yer aldığı detay bilgilerin getirilmesi <br />
    • Rehberdeki kişilerin bulundukları konuma göre istatistiklerini çıkartan bir rapor talebi <br />
    • Sistemin oluşturduğu raporların listelenmesi <br />
    • Sistemin oluşturduğu bir raporun detay bilgilerinin getirilmesi <br />

-  Teknolojiler: <br />
    • .NET Core <br />
    • Git <br />
    • Postgres (pgAdmin) <br />
    • Kafka Message Queue sistemi <br />
    
-  ContactReportApp Proje nasıl çalışır?
   
   1 - DB Migration: bize iki veritabanı vardır "contactdb" ve "reportdb" ikisini için aynı şekilde Migration oluyor. <br />
        - Add-Migration --> Name: contactdb <br />
        - Update-Database <br />

   2 - Kafka Message Queue sistemi kurulum ve çalışması için bir kaç adım vardır. <br />
   


