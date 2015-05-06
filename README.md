DigitimateSharp
===============
A C# client for digitimate (https://github.com/digitimate/digitimate).

[![Build status](https://ci.appveyor.com/api/projects/status/424l99eatqoihkls/branch/master?svg=true)](https://ci.appveyor.com/project/jacob-ebey/digitimatesharp/branch/master)

DigitimateSharp is avaliable on NuGet [https://www.nuget.org/packages/DigitimateSharp](https://www.nuget.org/packages/DigitimateSharp) or the NuGet Package Manager. DigitimateSharp can also be installed by running the folling command in the Package Manager Console

```
PM> Install-Package DigitimateSharp
```

Example Usage
-------------
```C#
using DigitimateSharp;

...

// Create the validator. Would recommend creating in a static scope.
static Digitimate validator = new Digitimate("your.email@example.com", 6, "Custom message, here is your code: ");

...

// A method that asks for the code that was sent to the user.
async Task<string> PromptForCode()
{ ... }

// Return true if the user recieved, and verified the number sent to their phone.
async Task<bool> ValidatePhone(string mobileNumber)
{
  var promptTask = PromptForCode();
  Result sendResult = await validator.SendCodeAsync(mobileNumber);
    
  if (!sendResult.Successful)
    return false;
  
  string code = await promptTask;
  
  if (code == null)
    return false;
  
  CheckCodeResult checkResult = await validator.CheckCodeAsync(mobileNumber, code);
  
  return checkResult.ValidCode;
}
```
