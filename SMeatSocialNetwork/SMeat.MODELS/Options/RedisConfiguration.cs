namespace SMeat.MODELS.Options {
    public class RedisConfiguration {
       public string Host { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public string ConnectionString => $"{Host}:{Port}";
    }
}