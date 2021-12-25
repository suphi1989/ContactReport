using Aspose.Cells;
using Confluent.Kafka;
using ContactReportApp.Controllers;
using ContactReportApp.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactReportApp
{
    public class KafkaConsumer
    {
        private readonly ILogger<HomeController> _logger;
        private ConsumerConfig _config;
        public KafkaConsumer(ConsumerConfig config, ILogger<HomeController> logger)
        {
            _config = config;
            _logger = logger;
        }

        public void StartConsumerAsync()
        {
            try
            {
                Task.Run(() => {
                    using (var consumer = new ConsumerBuilder<Null, string>(_config).Build())
                    {
                        consumer.Subscribe("report-topic");
                        while (true)
                        {
                            var cr = consumer.Consume();
                            string data = cr.Message.Value;
                            
                            _logger.LogInformation("Receiver:" + data);

                            if (!string.IsNullOrEmpty(data))
                            {
                                string raporId = data.Split("***")[0];
                                string jsonData = data.Split("***")[1];
                                

                                if (jsonData.Contains("Kişiler bulunamadı.") || jsonData.Contains("404"))//Kişiler bulunamadı
                                {
                                    //RaporStatuGuncelle("Kişiler bulunamadı.", raporId);
                                }
                                else //Başarılı
                                {

                                    var result = JsonConvert.DeserializeObject<ExcelRaporModel>(jsonData);
                                    if (result != null && result.Value != null && result.Value.Detaylar != null && result.Value.Detaylar.Count > 0)
                                    {
                                        string klasor = "ExcelFiles";

                                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                                        var workbook = new Workbook();
                                        workbook.Settings.Encoding = Encoding.UTF8;
                                        Worksheet worksheet = workbook.Worksheets[0];
                                        int cellNo = 0;
                                        worksheet.Name = "İstatistik Raporu";
                                        worksheet.Cells[0, cellNo++].PutValue("Ad");
                                        worksheet.Cells[0, cellNo++].PutValue("Soyad");
                                        worksheet.Cells[0, cellNo++].PutValue("KonumBilgisi");
                                        worksheet.Cells[0, cellNo++].PutValue("Telefon");
                                        
                                        int rowNo = 1;
                                        foreach (var kisi in result.Value.Detaylar)
                                        {
                                            try
                                            {
                                                cellNo = 0;
                                                worksheet.Cells[rowNo, cellNo++].PutValue(kisi.Ad);
                                                worksheet.Cells[rowNo, cellNo++].PutValue(kisi.Soyad);
                                                worksheet.Cells[rowNo, cellNo++].PutValue(kisi.KonumBilgisi);
                                                worksheet.Cells[rowNo, cellNo++].PutValue(kisi.Telefon);
                                            }
                                            catch (Exception ex)
                                            {
                                                _logger.LogError(ex, ex.Message);
                                            }
                                            rowNo++;
                                        }


                                        rowNo++;
                                        rowNo++;
                                        rowNo++;

                                        cellNo = 0;
                                        worksheet.Cells[rowNo, cellNo++].PutValue("Kayıtlı Kişi Sayısı:");
                                        worksheet.Cells[rowNo, cellNo++].PutValue(result.Value.KayitliKisiSayisi);
                                        
                                        rowNo++;
                                        cellNo = 0;
                                        worksheet.Cells[rowNo, cellNo++].PutValue("Kayıtlı Telefon Numarası Sayısı:");
                                        worksheet.Cells[rowNo, cellNo++].PutValue(result.Value.KayitliTelefonNumarasiSayisi);
                                        
                                        rowNo++;


                                        ExcelDosyaOlustur(klasor, "rapor_" + DateTime.Now.ToString("yyyyMMdd") + "_" + raporId + ".xlsx", workbook, raporId);
                                    }

                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        private void ExcelDosyaOlustur(string klasorAdi, string dosyaAdi, Workbook workbook,string raporId)
        {
            //Proje içinde
            var dosyaYolu1 = Path.Combine(klasorAdi, dosyaAdi).ToLower();
            FileInfo file1 = new FileInfo(dosyaYolu1);
            if (!file1.Directory.Exists)
            {
                file1.Directory.Create();
            }
            workbook.Save(dosyaYolu1, SaveFormat.Xlsx);

            //D drive
            var dosyaYolu = Path.Combine(@"D:\logs", klasorAdi, dosyaAdi).ToLower();
            FileInfo file = new FileInfo(dosyaYolu);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            workbook.Save(dosyaYolu, SaveFormat.Xlsx);

            //RaporStatuGuncelle(dosyaYolu, raporId);
        }

    }
}
