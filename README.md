FlashMessage
============

### ASP.NET 5 and earlier

Install the [FlashMessage NuGet package](https://www.nuget.org/packages/Vereyon.Web.FlashMessage/) and import the ```Vereyon.Web``` namespace where you need it.

#### Rendering flash messages

Typically you want to render all queued flash messages in your Layout Razor template using the following code:

```C#
@Html.RenderFlashMessages()
```

#### Queuing flash messages

Queuing a confirmation message for display on the next request after for example user login is done as follows:

```C#
// User successfully logged in
FlashMessage.Confirmation("You have been logged in as: {0}", user.Name);
return RedirectToLocal(returnUrl);
```

Different types of messages can be scheduled using different static methods on the FlashMessage object:

```C#
FlashMessage.Info("Your informational message");
FlashMessage.Confirmation("Your confirmation message");
FlashMessage.Warning("Your warning message");
FlashMessage.Danger("Your danger alert");
FlashMessage.Danger("Message title", "Your danger alert");
```

#### Advanced options

Using the FlashMessage.Queue() method advanced options are available:

```C#
FlashMessage.Queue(string.Format("You have been logged in as: {0}", user.Name), "Title", FlashMessageType.Confirmation, false);
```

The ```FlashMessage``` class allows you to queue messages anywhere in your code where a HttpContext is available and the response has not yet been sent out. You can thus also use FlashMessage outside your MVC actions or with WebForms applications.

Tests
-----

Got tests? Yes, see the tests project. It uses xUnit.


More information
-----

 * [FlashMessage NuGet package](https://www.nuget.org/packages/Vereyon.Web.FlashMessage/)
 * [CodeProject article on FlashMessage](http://www.codeproject.com/Articles/987638/Post-Redirect-Get-user-notifications-for-ASP-NET-M)

License
-------

[MIT X11](http://en.wikipedia.org/wiki/MIT_License)
