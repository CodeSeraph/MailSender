## Mail Sender NuGet Package

<span id='contents'>

1. [Overview](#1)
2. [Namespace and Instantiation](#2)
3. [Configuration](#3)
4. [Models](#4)
5. [Methods](#5)
6. [Example](#6)

</span>

<a name='1' style="padding-top: 50px">

### Overview

- The MailSender Package is used to easily send emails from .Net
- Minimum Target Framework: 4.6.1

<a name='2' style="padding-top: 50px">

### Namespace and Instantiation

- The namespace for the MailSender class is Mail_Sender and will need to be referenced in your project after installing the NuGet package.
    ```C#
    using Mail_Sender;
    ```
- The **MailSender** class is instatiated using the default contstructor.
- ```MailSender``` inherits from ```IMailSender``` interface which specifies the required properties and methods
    ```C#
    var sender = new MailSender();
    ```

- The **AsyncMailSender** class is instatiated using the default contstructor.
- ```AsyncMailSender``` inherits from ```IMailSender``` interface which specifies the required properties and methods
    ```C#
    var sender = new AsyncMailSender();
    ```

<a name='3' style="padding-top: 50px">

### Configuration

- To use the **MailSender** it must be configured with the necessary mail account parameters.
- These parameters are set as public properties available on the **MailSender** object.

|Property | Type | Accessibility | Default Value |
|---|---|---|---|
|SMTPServer | ```string``` | public | 
|Username | ```string``` | public | |
|Password | ```string``` | public | |
|Port | ```PortNumber``` | public | ```PortNumber.Port587``` |
|SSLEnabled | ```bool``` | public | ```false``` |
|UseTLSEncryption | ```bool``` | public | ```false``` |
|ToAddress | ```List<Person>``` | public | ```new List<Person>``` |
|FromAddress | ```string``` | public |
|FromPersonName | ```string``` | public |
|Subject | ```string``` | public |
|Body | ```string``` | public |
|SuccessMsg | ```string``` | public | Message Sent!|
|FailureMsg | ```string``` | public | Message sending failure |
|IsBodyHtml | ```bool``` | public | ```true```|
|BodyEncoding | ```System.Text.Encoding``` | public | ```Encoding.UTF8``` |
|TokenIdentifier | ```string``` | public |

<a name='4' style="padding-top: 50px">

### Models

|Name | Type | Namespace | Details |
|---|---|---|---|
|PortNumber | ```Enum``` | ```Mail_Sender``` | **Possible Options:** <br/> - Port587<br/> - Port25<br/> - Port2525<br/> - Port465<br/> - Port2526|
|Person | ```Class``` | ```Mail_Sender``` | **Public Propoerties:** <br/> - Name (String)<br/> - Email (String)|

<a name='5' style="padding-top: 50px">

### Methods

#### AddPerson()
- This will add people to the **ToAddress** property which is already instantiated when creating the MailSender object.
    ``` C#
    public void AddPerson(string name, string email)
    ```

#### SendEmail()
- Sending the mail to all recipients synchronously.
- This returns a string with either the Success or Failure message
- Sending mails using the AsyncMailSender requires the ```TokenIdentifier``` to be set
    ``` C#
    public string SendEmail()
    ```

<a name='6' style="padding-top: 50px">

### Example

```C#
using System;
using Mail_Sender;

namespace MyConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var message = new MailSender(){
                SMTPServer = "smtp.myserver.com",
                Port = PortNumber.Port587,
                Username = "mail@myserver.com",
                Password = "mysecretpassword",
                FromAddress = "no-reply@myserver.com",
                FromPersonName = "My Name",
                Subject = "New Email",
                Body = "My Email Template"
            };

            message.AddPerson("Someone's Name", "their@email.com");
            string sendingresult = message.SendEmail();        
        }
    }
}
```
