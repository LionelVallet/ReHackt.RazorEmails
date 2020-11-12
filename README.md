# ReHackt.RazorEmails

ReHackt.RazorEmails allows you to easily create email templates using ASP.NET Core's Razor syntax. The generated HTML will be compatible with all existing email clients. It includes a service based on [MailKit](https://github.com/jstedfast/MailKit) to send them using SMTP.

## Install

Get it on <a href="https://www.nuget.org/packages/ReHackt.RazorEmails"><img src="https://www.nuget.org/Content/gallery/img/default-package-icon.svg" height=18 style="height:18px;" /> NuGet</a>

### Package Manager Console

```
PM> Install-Package ReHackt.RazorEmails
```

### .NET CLI Console

```
> dotnet add package ReHackt.RazorEmails
```

## tl;dr

If you are familiar with ASP.NET Core, this summary may be sufficient.

1) Add the `ReHackt.RazorEmails` NuGet package to your ASP.NET Core application project.

2) Add a configuration section in your `appsettings.json` file containing the required [options](#all-options) (`EmailOptions` class).

3) Register RazorEmails services and configuration to the service collection:

   ``` csharp
   services.AddRazorEmails(Configuration.GetSection("Email"));
   ```

4) Create an **Emails** folder in the **Views** folder and add the following code to a `_ViewImports.cshtml` file in that folder:

   ``` csharp
   @using ReHackt.RazorEmails
   @addTagHelper *, ReHackt.RazorEmails
   ```

//TODO other folder name

5) Create Razor views in the *Emails* folder

   * using [Tag Helpers](#the-email-tag-helpers) instead of raw HTML markup (`<email-a>`, `<email-button>`, `<email-h2>`, `<email-img>`, `<email-p>`)

   * using `EmailViewDataKeys` constants as `ViewData` collection keys to pass the necessary data to the [layout](#the-email-layout)

   Example

    ``` html
    @model MyEmailViewModel
    @{
        ViewData[EmailViewDataKeys.Title] = "My Email Title";
    }
    <email-p>This is my email content.</email-p>
    <email-button href="@Model.Link">Click here</email-button>
    ```

    [See complete examples](#examples)

6) Send an email using `IEmailService`.`SendRazorViewEmailAsync`(`email`, `subject`, `viewName`, `viewModel`)

   Example

    ``` csharp
    await _emailService.SendRazorViewEmailAsync(email, "My Email Subject", "/Views/Emails/MyEmail.cshtml", myEmailViewModel);
    ```

7) That's all. Congratulations!

## Configuration

First of all, you must register *ReHackt.RazorEmails* services to the dependency injection container in the `Startup.ConfigureServices` method. A service collection extension method is available to register all services (and their dependent services): `AddRazorEmails`.

``` csharp
public void ConfigureServices(IServiceCollection services)
{
    // ...
    services.AddRazorEmails();
    // ...
}
```

Then, the configuration is done using [ASP.NET Core's options pattern](https://docs.microsoft.com/aspnet/core/fundamentals/configuration/options).

So you need to add an `EmailOptions` class to the service container and bound to configuration:

* as a parameter of `AddRazorEmails`:

   ``` csharp
   services.AddRazorEmails(Configuration);
   ```

* or, with `Configure`:

   ``` csharp
   services.Configure<EmailOptions>(Configuration);
   ```

### Configuration examples

You can add a new section to your `appsettings.json` file:

``` json
{
    "Email": {
        "Sender": "no-reply@rehackt.com"
    }
}
```

``` csharp
services.AddRazorEmails(Configuration.GetSection("Email"));
```

You can configure simple options with a delegate:

``` csharp
services..AddRazorEmails(options => options.Sender.Name = "no-reply@rehackt.com");
```

### All options

``` json
{
  "EmailOptions": {
    // [Sender]
    "Sender": {
      "Email": "",
      "Name": ""
    },
    // [SMTP server]
    "Smtp": {
      "EnableSsl": true,
      "Host": "",
      "Port": 0,
      "Username": "",
      "Password": ""
    },
    // [Email template]
    "Template": {
      "Address": "",
      "ContactEmail": "",
      "FooterLinks": [
        {
          "Text": "",
          "Url": ""
        }
      ],
      "HeaderLink": "",
      "HeaderLogoUrl": "",
      "SupportLink": "",
      // [Colors (optional)]
      "ButtonBackgroundColor": "",    // (default: "#7FA5DB")
      "ButtonTextColor": "",          // (default: "#FFFFFF")
      "HeaderBackgroundColor": "",    // (default: "#203279")
      "HeadlineColor": "",            // (default: "#111111")
      "LinkColor": "",                // (default: "#203279")
      "SupportBackgroundColor": "",   // (default: "#D8E8FF")
      "SupportLinkColor": "",         // (default: "#203279")
      "TextColor": ""                 // (default: "#666666")
    }
  }
}
```

## Usage

### Send an email

To send an email, use the `IEmailService` from the dependency injection container and call:

* the `SendEmailAsync` method, if your HTML content is already prepared.

* the `SendRazorViewEmailAsync` method, if you want to generate content from a Razor template.

``` csharp
public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
    Task SendRazorViewEmailAsync<TModel>(string email, string subject, string viewName, TModel model);
}
```

### Define a template

// Documentation under construction
