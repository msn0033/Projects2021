#pragma checksum "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\LearnComponents\DeleteEditButton.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "30bcb4088074e1f9885a1e8e9202022acf0b801c"
// <auto-generated/>
#pragma warning disable 1591
namespace HotelAppServer.Pages.LearnBalzor.LearnComponents
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using HotelAppServer;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using HotelAppServer.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using HotelAppServer.Pages.LearnBalzor.LearnComponents;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\PROJECT\HotelApp\HotelAppServer\_Imports.razor"
using Model;

#line default
#line hidden
#nullable disable
    public partial class DeleteEditButton : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
#nullable restore
#line 2 "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\LearnComponents\DeleteEditButton.razor"
 if (IsAdmin)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(0, "<input type=\"button\" class=\"btn btn-warning\" value=\"Delete\">\r\n    <input type=\"button\" class=\"btn btn-success\" value=\"Edit\">");
#nullable restore
#line 6 "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\LearnComponents\DeleteEditButton.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 8 "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\LearnComponents\DeleteEditButton.razor"
      
    [Parameter]
    public bool IsAdmin { get; set; }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591