#pragma checksum "D:\PROJECT\ERP\ERPv1\ERPv1\Views\Shared\EditorTemplates\SupplierAccountList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1c135a41a27484c32893df48f16d63837358ff59"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_EditorTemplates_SupplierAccountList), @"mvc.1.0.view", @"/Views/Shared/EditorTemplates/SupplierAccountList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\PROJECT\ERP\ERPv1\ERPv1\Views\_ViewImports.cshtml"
using ERPv1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\PROJECT\ERP\ERPv1\ERPv1\Views\_ViewImports.cshtml"
using ERPv1.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1c135a41a27484c32893df48f16d63837358ff59", @"/Views/Shared/EditorTemplates/SupplierAccountList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"296e1f1e7e985af1e596c25ff18c1e05b69fb24d", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_EditorTemplates_SupplierAccountList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\PROJECT\ERP\ERPv1\ERPv1\Views\Shared\EditorTemplates\SupplierAccountList.cshtml"
   
    var suppliers = _dbcontex.AccountChart
        .Where(x=>x.AccTypeId==13)
        .Where(x=>!x.IsParent)
        .Select(x => new SelectListItem
        {
            Value = x.AccNum.ToString(),
            Text = x.AccountNameAr
        }
    ).OrderBy(x => x.Text).ToList();


#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "D:\PROJECT\ERP\ERPv1\ERPv1\Views\Shared\EditorTemplates\SupplierAccountList.cshtml"
   
    var htmlAtt = ViewData["htmlAtt"] ?? new { };


#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "D:\PROJECT\ERP\ERPv1\ERPv1\Views\Shared\EditorTemplates\SupplierAccountList.cshtml"
Write(Html.DropDownList("",suppliers, "--اختار--",htmlAtt));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public ERPv1.Data.ApplicationDbContext _dbcontex { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591