# Simple Authentication Framework

## Why do I create this?

I was working to a project in WPF and I wanted a simple way to manage authorization and authentication in my project.

## What does it do?

It manages the basics of user management:
- Register/Unregister a user;
- Login/Logout management;
- Authentication of a user;
- Authorization based on roles;
- Validation of key inputs like user name and password.

## How to get started?

Simply implement `IUser` and `IRole` and then use `UserMangerBuilder` to get an instance of `IUserManager`.

The framework come with extension methods to deal with Dependency Injection and Entity Framework Core.

## Example

Check out [the example project](https://github.com/Matt90hz/SimpleAuthenticationFramework) to see the framework in action and the different ways to configure it.