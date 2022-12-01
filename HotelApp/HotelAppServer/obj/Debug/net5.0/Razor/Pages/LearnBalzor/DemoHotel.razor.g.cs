#pragma checksum "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\DemoHotel.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "14992e0404f01da42b5bd19948149b6388025d69"
// <auto-generated/>
#pragma warning disable 1591
namespace HotelAppServer.Pages.LearnBalzor
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
#line 2 "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\DemoHotel.razor"
using Model;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/demohotel")]
    public partial class DemoHotel : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h3>We will display hotel rooms here!</h3>\r\n\r\n");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "border p-2 mt-2");
            __builder.AddAttribute(3, "style", "background-color:azure");
            __builder.AddMarkupContent(4, "<h2 class=\"text-info\">RoomsList</h2>\r\n\r\n    ");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "row container");
            __builder.AddMarkupContent(7, "<div class=\"col-12\"><h4 class=\"text-info\">Hotel Rooms</h4></div>");
#nullable restore
#line 15 "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\DemoHotel.razor"
         foreach (var room in RoomsList)
        {

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<HotelAppServer.Pages.LearnBalzor.LearnComponents.IndividualRoom>(8);
            __builder.AddAttribute(9, "Room", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<HotelAppServer.Model.Room>(
#nullable restore
#line 17 "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\DemoHotel.razor"
                                  room

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
#nullable restore
#line 18 "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\DemoHotel.razor"

        }

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(10, "<div class=\"col-12 mt-3\"><h4 class=\"text-info\">Hotel Amenities</h4></div>");
#nullable restore
#line 23 "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\DemoHotel.razor"
         foreach (var amenity in AmenitiesList)
        {

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<HotelAppServer.Pages.LearnBalzor.LearnComponents.IndividualAmenity>(11);
            __builder.AddAttribute(12, "Amenity", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<HotelAppServer.Model.Amenity>(
#nullable restore
#line 25 "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\DemoHotel.razor"
                                        amenity

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
#nullable restore
#line 26 "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\DemoHotel.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 32 "D:\PROJECT\HotelApp\HotelAppServer\Pages\LearnBalzor\DemoHotel.razor"
      

    List<Room> RoomsList = new List<Room>();
    List<Amenity> AmenitiesList = new List<Amenity>();

    protected override void OnInitialized()
    {
        base.OnInitialized();
        RoomsList.Add(new Room()
        {
            Id = 101,
            RoomName = "king Suite",
            IsActive = true,
            Price = 1200,
            RoomProps = new List<RoomProp>()
{
                new RoomProp()
                {
                    Id=1,
                    Name="Area",
                    Value="300"
                },
                 new RoomProp()
                {
                    Id=2,
                    Name="Occupancy",
                    Value="1"
                }
            }
        });
        RoomsList.Add(new Room()
        {

            Id = 201,
            RoomName = "Single Room",
            IsActive = true,
            Price = 300,
            RoomProps = new List<RoomProp>()
{
                new RoomProp()
                {
                    Id=1,
                    Name="Area",
                    Value="30"
                },
                new RoomProp()
                {
                    Id=2,
                    Name="Occupancy",
                    Value="1"
                },
            }
        });
        AmenitiesList.Add(new Amenity
        {
            Id = 111,
            AmenityName = "Gym",
            AmenityInfo = "24x7 gmy room is available"
        });
        AmenitiesList.Add(new Amenity
        {
            Id = 222,
            AmenityName = "Swimming Pool",
            AmenityInfo = "Pool room is open from 6am to 10pm"
        });
        AmenitiesList.Add(new Amenity
        {
            Id = 333,
            AmenityName = "Free Breakfast",
            AmenityInfo = "Enjoy free breakfast at out hotel"
        });


    }

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591