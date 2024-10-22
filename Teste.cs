namespace NewsApiConsoleApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private const string Url = "https://newsapi.org/v2/top-headlines?country=br&apiKey=";

        static async Task Main(string[] args)
        {
            try
            {
                var articles = await GetNewsArticlesAsync();
                DisplayArticles(articles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro: {ex.Message}");
            }
        }

        private static async Task<JArray> GetNewsArticlesAsync()
        {
            var response = await client.GetStringAsync(Url);
            var jsonResponse = JsonConvert.DeserializeObject<JObject>(response);
            return (JArray)jsonResponse["articles"];
        }

        private static void DisplayArticles(JArray articles)
        {
            Console.WriteLine("Lista de Artigos:\n");
            foreach (var article in articles)
            {
                var author = article["author"]?.ToString() ?? "Desconhecido";
                var title = article["title"]?.ToString() ?? "Sem título";
                var description = article["description"]?.ToString() ?? "Sem descrição";

                Console.WriteLine($"Autor: {author}");
                Console.WriteLine($"Título: {title}");
                Console.WriteLine($"Descrição: {description}\n");
            }
        }
    }
}
