#pragma checksum "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a4fbb2f3ae64537c9479fc3abd6cd12522bfa549"
// <auto-generated/>
#pragma warning disable 1591
namespace EmployeeManagement.Web.Pages.EmployeeModule.Details
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using EmployeeManagement.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using EmployeeManagement.Web.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using EmployeeManagement.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\PROJECT\Blazor\EmployeeManagement.Web\_Imports.razor"
using PragimTech.Components;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/employeedetails/{Id}")]
    [Microsoft.AspNetCore.Components.RouteAttribute("/employeedetails")]
    public partial class EmployeeDetails : EmployeeDetailsBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 6 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
 if (Employee == null || Employee.Department == null)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(0, "<div class=\"spinner\"></div>");
#nullable restore
#line 9 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "row justify-content-center m-3");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "col-sm-8");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "card");
            __builder.OpenElement(7, "div");
            __builder.AddAttribute(8, "class", "card-header");
            __builder.OpenElement(9, "h1");
            __builder.AddContent(10, 
#nullable restore
#line 16 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                     Employee.FirstName

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(11, " ");
            __builder.AddContent(12, 
#nullable restore
#line 16 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                                         Employee.LastName

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(13, " ");
            __builder.AddContent(14, 
#nullable restore
#line 16 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                                                            coordinate

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\r\n\r\n            ");
            __builder.OpenElement(16, "div");
            __builder.AddAttribute(17, "class", "card-body text-center");
            __builder.OpenElement(18, "img");
            __builder.AddAttribute(19, "class", "card-img-top");
            __builder.AddAttribute(20, "src", "images/" + (
#nullable restore
#line 20 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                                                       Employee.PhotoName

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(21, "onmousemove", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 21 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                                     e=>coordinate =$"X={e.ClientX} Y={e.ClientY}"

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseElement();
            __builder.AddMarkupContent(22, "\r\n\r\n                ");
            __builder.OpenElement(23, "h4");
            __builder.AddContent(24, "Employee ID : ");
            __builder.AddContent(25, 
#nullable restore
#line 23 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                                   Employee.EmployeeId

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(26, "\r\n                ");
            __builder.OpenElement(27, "h4");
            __builder.AddContent(28, "Email : ");
            __builder.AddContent(29, 
#nullable restore
#line 24 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                             Employee.Email

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(30, "\r\n                ");
            __builder.OpenElement(31, "h4");
            __builder.AddContent(32, "Department : ");
            __builder.AddContent(33, 
#nullable restore
#line 25 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                                  Employee.Gender

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(34, "\r\n                ");
            __builder.OpenElement(35, "h4");
            __builder.AddContent(36, "Department : ");
            __builder.AddContent(37, 
#nullable restore
#line 26 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                                  Employee.Department.DepartmentName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(38, "\r\n                ");
            __builder.OpenElement(39, "button");
            __builder.AddAttribute(40, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 27 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                                  Button_Click

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(41, "class", "btn btn-primary");
            __builder.AddContent(42, 
#nullable restore
#line 27 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                                                                         ButtonText

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(43, "\r\n            ");
            __builder.OpenElement(44, "div");
            __builder.AddAttribute(45, "class", "card-footer" + " text-center" + " " + (
#nullable restore
#line 29 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                                                 CssClass

#line default
#line hidden
#nullable disable
            ));
            __builder.AddMarkupContent(46, "<a href=\"/\" class=\"btn btn-primary\">Back</a>\r\n                ");
            __builder.OpenElement(47, "a");
            __builder.AddAttribute(48, "href", 
#nullable restore
#line 31 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
                           $"EditEmployee/{Employee.EmployeeId}"

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(49, "class", "btn btn-primary");
            __builder.AddContent(50, "Edit");
            __builder.CloseElement();
            __builder.AddMarkupContent(51, "\r\n                ");
            __builder.AddMarkupContent(52, "<a href=\"#\" class=\"btn btn-danger\">Delete</a>");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 37 "D:\PROJECT\Blazor\EmployeeManagement.Web\Pages\EmployeeModule\Details\EmployeeDetails.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
