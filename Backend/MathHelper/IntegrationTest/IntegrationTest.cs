using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using MathHelperr.Data;
using MathHelperr.Model;
using MathHelperr.Model.Db.DTO;
using MathHelperr.Service.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace MyIntegrationTest;


public class IntegrationTest : IClassFixture<MathHelperFactory>
{
    private readonly MathHelperFactory _factory;
    private HttpClient _client;
    private readonly ITestOutputHelper output;
    private readonly IConfiguration _configuration;
    private readonly CookieContainer _cookieContainer;

    public IntegrationTest(MathHelperFactory factory,  ITestOutputHelper output)
    {
  
        this.output = output;
        _factory = factory;
       
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost:8443") // HTTPS URL
        });
    }
    
    
    [Fact]
    public async Task TestUserLoginGodPassword()
    {
    
        var loginRequest = new AuthRequest("a@a.hu", "password");
      
        var loginResponse = await _client.PostAsync(requestUri: "api/authentication/Login",
            new StringContent(JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8, mediaType: "application/json")); // Task<HttpResponseMessage>
        
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
        
        var response = await loginResponse.Content.ReadAsStringAsync();
        output.WriteLine($"Response: {response}"); 
        
        var responseObject = JsonConvert.DeserializeObject<AuthResponse>(response);
        if (loginResponse.StatusCode == HttpStatusCode.OK)
        {
            output.WriteLine($"Status Code: {loginResponse.StatusCode}");
        }

        Assert.Equal(loginRequest.Email,responseObject?.Email);
       
    }
    
    [Fact]
    public async Task TestUserLoginWrongPassword()
    {
        var loginRequest = new AuthRequest("a@sdégljélgjkfs.hu", "password");
        
        var loginResponse = await _client.PostAsync(requestUri: "api/authentication/Login",
            new StringContent(JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8, mediaType: "application/json")); 
        
        Assert.Equal(HttpStatusCode.BadRequest, loginResponse.StatusCode);
        var responseContent = await loginResponse.Content.ReadAsStringAsync();
        Assert.Contains("Invalid email or password", responseContent);
    }

    [Fact]
    public async Task TestAuthorizedGetExerciseLoggedIn()
    {
     
        var loginRequest = new AuthRequest("a@a.hu", "password");
       
        //logging in 
        var loginResponse = await _client.PostAsync(requestUri: "api/authentication/Login",
            new StringContent(JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8, mediaType: "application/json")); 
        
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
        
        //catch cookie
        var cookieHeader = loginResponse.Headers.GetValues("Set-Cookie").FirstOrDefault();
        var cookieContainer = new CookieContainer();
        cookieContainer.SetCookies(new Uri("https://localhost:8443"), cookieHeader);
       
        //to be able to catch 
        var handler = new HttpClientHandler
        {
            CookieContainer = cookieContainer,
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        };
        
        var client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:8443") };

        var request = new HttpRequestMessage(HttpMethod.Get, "api/algebra/TestForDatabase?type=Algebra");
        request.Headers.Add("Level", "1"); 
        
        var response = await client.SendAsync(request);
       
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var responseBody = await response.Content.ReadAsStringAsync();
        output.WriteLine($"Response: {responseBody}");
        var responseJson = JsonConvert.DeserializeObject<ExcerciseResult>(responseBody);
       
        //the lowest ExerciseID is 1, and it decrease on each run
        Assert.InRange(responseJson.ExerciseId, 1, int.MaxValue);
    }
    

    
    [Fact]
    public async Task TestAuthorizedGEtExerciseNotLoggedIn()
    {
        //calling endpoint -httpResponseMessage
        var response = await _client.GetAsync("api/algebra/TestForDatabase?type=Algebra");
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        
    }

 
    [Fact]
    public async Task TestAuthorizedGetSolutionsLoggedIn()
    {
        var loginRequest = new AuthRequest("a@a.hu", "password");
       
        //logging in 
        var loginResponse = await _client.PostAsync(requestUri: "api/authentication/Login",
            new StringContent(JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8, mediaType: "application/json")); 
        
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        //calling endpoint -httpResponseMessage
        var response = await _client.GetAsync("api/solution/userSolutions");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<List<SolutionDto>>(responseBody);
       
        Assert.NotNull(responseObject);
        Assert.NotEmpty(responseObject);
        Assert.Equal(1, responseObject[0].ElapsedTime);

    }

}