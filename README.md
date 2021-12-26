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
        JRE8	--->	http://www.oracle.com/technetwork/java/javase/downloads/jre8-downloads-2133155.html

        zookeeper --->	https://zookeeper.apache.org/releases.html

        kafka --->		http://kafka.apache.org/downloads.html 

 JRE8 için Tools klasörde ekldim, indirdikten sonra JAVA_HOME parametre Sistem Ortam Değişkenleri pencerede eklemesi gerekiyor, zookeeper ve kafka için ben Tools klasör içinde indirdim ve ayarladım.
 
- Zookeeper ve Kafka server çalıştırması için bir kaç komut vardır ve topic adı için ben örnek olarak "report-topic" adını verdim. Start Producer ve Start Consumer komut çok önemli değil onlar sadece giden ve gelen veri gelmesi kontrol etmek için.
    
    Zookeeper Start
    C:\apache-zookeeper-3.5.9-bin\bin> zkServer.cmd

    Kafka Start
    C:\kafka_2.12-2.4.1>  .\bin\windows\kafka-server-start.bat .\config\server.properties

    Create Topic
    C:\kafka_2.12-2.4.1\bin\windows>	kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic report-topic

    Start Producer 
    C:\kafka_2.12-2.4.1\bin\windows>	kafka-console-producer.bat --broker-list localhost:9092 --topic report-topic

    Start Consumer
    C:\kafka_2.12-2.4.1\bin\windows>	kafka-console-consumer.bat --bootstrap-server localhost:9092 --topic report-topic --from-beginning




 

