# Simple Authentication Framework

Simple framework to manage authentication and authorization in your project.

## Why did I create this?

I was working on a WPF project and I needed a way to manage authorization and authentication.

Asking to internet for a solution I found that the most suggested to use ASP.NET authentication and authorization framework.

I soon got lost into all the configurations and the details of this huge framework so I decided to make one of my own with only the few features that i really needed.

## What does it do?

It provides the basics of user management:
- Register/Unregister a user;
- Login/Logout management;
- User authentication;
- Authorization based on roles;
- Validation of key inputs like username and password.

## How to get started?

Simply implement `IUser` and `IRole` and then use `UserManagerBuilder` to get an instance of `IUserManager`.

The framework comes with extension methods to deal with Dependency Injection and Entity Framework Core.

![Image](https://raw.githubusercontent.com/Matt90hz/SimpleAuthenticationFramework/master/Authentication/ClassDiagram.png)

## Example

Check out [the example project](https://github.com/Matt90hz/SimpleAuthenticationFramework) to see the framework in action and the different ways to configure it.