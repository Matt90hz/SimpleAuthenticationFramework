# Simple Authentication Framework

## Why did I create this?

I was working on a WPF project and I wanted a simple way to manage authorization and authentication.

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

## Example

Check out [the example project](https://github.com/Matt90hz/SimpleAuthenticationFramework) to see the framework in action and the different ways to configure it.