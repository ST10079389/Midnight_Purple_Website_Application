using ST10079389_Kaushil_Dajee_PROG6212_POE.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ST10079389_Kaushil_Dajee_PROG6212_POE.Models.MidnightPurpleWebsiteDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();

/*
 Code Attribution:
1.
https://youtu.be/E7Voso411Vs?si=X5Q7TsUDccjT9TRV from YouTube, Programming with Mosh,
https://youtube.com/@programmingwithmosh?si=-0Saq7LdSpUu-pFj

2.
https://youtu.be/NfUccG5faBQ?si=wSGw_vIDEZ8Ua8o1 from YouTube, tutorialsEUC,
https://youtube.com/@tutorialsEUC?si=0f6ag0Sa53WIIt6Q

3.
https://youtu.be/SnU4Ulee_NI?si=4m7yFHJyiDH9AIbo from YouTube, Computerix,
https://youtube.com/@Computerix?si=0OEID2NXyLZ9YLfy

4.
https://youtu.be/-860xZK5mRg?si=amxCTpZYXk5pjzaL from YouTube, CSharpCodeAcademy,
https://youtube.com/@CSharpCodeAcademy?si=L7NK03qQ26gHF8JJ
 
5.
https://youtu.be/I36SfEBfKD8?si=0Vm55w_8LOlJT_dc from YouTube, Code Galaxy,
https://youtube.com/@CodeGalaxy?si=GW9WQIV1HS8inRNt

6.
https://youtu.be/bzWJOxBR-MY?si=zOYoQTD_zfUw-Nvi from YouTube, Net code,
https://youtube.com/@Netcode-Hub?si=7k0qZGcpYLW-_HlA

7.
https://youtu.be/GlsU9rahttA?si=sVaRv-WXpz6QDARq from YouTube, Anoj Kumudunee,
https://youtube.com/@anojakumudunee?si=x2ktHpO38YXr840v

8.
https://youtu.be/DyFgB878pOI?si=W6brRtBvn3VyhRzp from YouTube, Code With Gopi,
https://youtube.com/@CodeWithGopi?si=t15YeQkS1XrP5d61

9.
https://youtu.be/UA6U2EIN894?si=7cs4zPAs4Nfjsv7F from YouTube, TechSadhna,
https://youtube.com/@techsadhna5445?si=DASxvAUEV4om48Ap

10.
https://youtu.be/yFDc5Fp1LoM?si=_FT647wbV9VczI9i from YouTube, ASP.NET MVC
https://youtube.com/@ASPNETMVCCORE?si=2V9b7j5U4ZogSr4_

11.
https://youtu.be/AopeJjkcRvU?si=AGmISrSWEpN_FAlD from YouTube, DotNetMastery
https://youtube.com/@DotNetMastery?si=r4ZlkNYtnxVrbA8w

12.
https://youtu.be/hfS-UQGVyBk?si=Hc_zQ3GuY5czFB6u from YouTube, Learning Programming Tutorial,
https://youtube.com/@LearningProgrammingTutorial?si=YZg1xvDYuilHMjaz

13.
https://stackoverflow.com/questions/58090526/how-should-we-create-a-login-page-in-mvc from StackOverflow, CodeNiNja,
https://stackoverflow.com/users/12093495/codeninja

14.
https://stackoverflow.com/questions/37803179/cookies-in-asp-net-mvc-5 from StackOverflow, Nikhil Patel
https://stackoverflow.com/users/5680926/nikhil-patel

15.
https://www.aspsnippets.com/Articles/3410/How-does-MVC-application-get-the-Cookies-from-Client-in-ASPNet/ from ASP SNIPPETS, 
https://www.aspsnippets.com/

16.
https://stackoverflow.com/questions/48637648/how-to-search-date-range-asp-net-mvc from StackOverflow, user9144413
https://stackoverflow.com/users/9144413/user9144413

17.
https://stackoverflow.com/questions/16769233/how-to-set-datetime-to-null from StackOverflow, Jon Skeet,
https://stackoverflow.com/users/22656/jon-skeet

18.
https://www.guru99.com/c-sharp-access-database.html from Guru99, Barbara Thompson,
https://www.guru99.com/barbara-thompson-2

19.
https://www.guru99.com/c-sharp-access-database.html#:~:text=operations%20in%20C%23.-,SQL%20Command%20in%20c%23,%2C%20Update%2C%20and%20delete%20commands. from Guru99, Barbara Thompson,
https://www.guru99.com/barbara-thompson-2

20.
https://stackoverflow.com/questions/21709305/how-to-directly-execute-sql-query-in-c from StackOverflow, Kaspars Ozols,
https://stackoverflow.com/users/3070052/kaspars-ozols

 */