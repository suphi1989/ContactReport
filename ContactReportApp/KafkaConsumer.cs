using Confluent.Kafka;
using ContactReportApp.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

                                }
                                else //Başarılı
                                {

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

    }
}
