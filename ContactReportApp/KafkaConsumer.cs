using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactReportApp
{
    public class KafkaConsumer
    {
        private ConsumerConfig _config;
        public KafkaConsumer(ConsumerConfig config)
        {
            _config = config;
        }

        public void StartConsumerAsync()
        {
            Task.Run(() => {
                using (var consumer = new ConsumerBuilder<Null, string>(_config).Build())
                {
                    consumer.Subscribe("report-topic");
                    while (true)
                    {
                        var cr = consumer.Consume();
                        string jsonData = cr.Message.Value;
                        //if(string.IsNullOrEmpty(jsonData))
                    }
                }
            });
        }

    }
}
