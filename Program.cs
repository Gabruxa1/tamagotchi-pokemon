using RestSharp;

const string URL_API = "https://pokeapi.co/api/v2/";

var client = new RestClient(URL_API);

var request = new RestRequest("pokemon/eevee", Method.Get);

var response = await client.ExecuteAsync(request);

Console.WriteLine(response.Content);