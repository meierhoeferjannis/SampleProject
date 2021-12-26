using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SampleProject;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
var client = new HttpClient();
var authresult = await  client.PostAsync("https://eu-central-1.aws.realm.mongodb.com/api/client/v2.0/app/kiloforkilodev-mtjtu/auth/providers/anon-user/login", null);
var authcontent = await authresult.Content.ReadAsStringAsync();
var result = JsonSerializer.Deserialize<AuthenticatedUser>(authcontent);
builder.Services
    .AddSampleClient()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://eu-central-1.aws.realm.mongodb.com/api/client/v2.0/app/kiloforkilodev-mtjtu/graphql");
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",result.access_token);
    });

await builder.Build().RunAsync();
