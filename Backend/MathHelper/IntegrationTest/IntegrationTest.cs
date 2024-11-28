using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using MathHelperr.Data;
using MathHelperr.Model;
using MathHelperr.Model.Db.DTO;
using MathHelperr.Service.Authentication;
using Microsoft.AspNetCore.Http.Headers;
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
    private readonly HttpClient _client;
    private readonly ITestOutputHelper output;
    private readonly IConfiguration _configuration;
    private readonly CookieContainer _cookieContainer;

    public IntegrationTest(MathHelperFactory factory,  ITestOutputHelper output)
    {
        this.output = output;
        _factory = factory;
       
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost:5001") // HTTPS URL
        });
      
        var serviceProvider = _factory.Services;
        _configuration = serviceProvider.GetService<IConfiguration>();
    }
    
    
    [Fact]
    public async Task TestUserLoginGodPassword()
    {
    
        var loginRequest = new AuthRequest("a@a.hu", "password");
      
        var loginResponse = await _client.PostAsync(requestUri: "api/authentication/Login",
            new StringContent(JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8, mediaType: "application/json")); // Task<HttpResponseMessage>
        
        var response = await loginResponse.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<AuthResponse>(response);
        if (loginResponse.StatusCode == HttpStatusCode.OK)
        {
            output.WriteLine($"Status Code: {loginResponse.StatusCode}");
        }

        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
        Assert.Equal(responseObject.Email,loginRequest.Email);
       
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
        
        //headers must contain Level key!
        var request = new HttpRequestMessage(HttpMethod.Get, "api/algebra/TestForDatabase?type=Algebra");
        request.Headers.Add("Level", "1");

        var response = await _client.SendAsync(request);
       
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var responseBody = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<ExcerciseResult>(responseBody);
        
       // Assert.False(string.IsNullOrEmpty(responseObject.Question));

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
        Assert.Equal(responseObject[0].ElapsedTime, 1 );

    }
    






}