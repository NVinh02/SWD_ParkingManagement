#pragma checksum "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e185b6196a0f155f52a925b83056e863011e96ea"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(ParkingManagementWebApp.Pages.ParkingInfos.Pages_ParkingInfos_History), @"mvc.1.0.razor-page", @"/Pages/ParkingInfos/History.cshtml")]
namespace ParkingManagementWebApp.Pages.ParkingInfos
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
#line 1 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\_ViewImports.cshtml"
using ParkingManagementWebApp;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e185b6196a0f155f52a925b83056e863011e96ea", @"/Pages/ParkingInfos/History.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea45b1815fafa1ec0ddfb71543c7143a646a88f4", @"/Pages/_ViewImports.cshtml")]
    public class Pages_ParkingInfos_History : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "./Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
  
    ViewData["Title"] = "History";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>History</h1>\r\n<br />\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr>\r\n            <th>\r\n                ");
#nullable restore
#line 14 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
           Write(Html.DisplayNameFor(model => model.ParkingInfo[0].CheckInTime));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 17 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
           Write(Html.DisplayNameFor(model => model.ParkingInfo[0].CheckOutTime));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 20 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
           Write(Html.DisplayNameFor(model => model.ParkingInfo[0].IsMonthly));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 23 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
           Write(Html.DisplayNameFor(model => model.ParkingInfo[0].HaveVehicle));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 26 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
           Write(Html.DisplayNameFor(model => model.ParkingInfo[0].Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 29 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
           Write(Html.DisplayNameFor(model => model.ParkingInfo[0].TotalPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 32 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
           Write(Html.DisplayNameFor(model => model.ParkingInfo[0].Cc));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 35 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
           Write(Html.DisplayNameFor(model => model.ParkingInfo[0].Pricing));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 38 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
           Write(Html.DisplayNameFor(model => model.ParkingInfo[0].Slot));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 41 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
           Write(Html.DisplayNameFor(model => model.ParkingInfo[0].Vehicle));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 47 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
         foreach (var item in Model.ParkingInfo)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 51 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
               Write(Html.DisplayFor(modelItem => item.CheckInTime));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n");
#nullable restore
#line 54 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
                     if (item.CheckOutTime.CompareTo(item.CheckInTime) >= 0)
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
                   Write(Html.DisplayFor(modelItem => item.CheckOutTime));

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
                                                                        
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <b>None</b>\r\n");
#nullable restore
#line 61 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 64 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
               Write(Html.DisplayFor(modelItem => item.IsMonthly));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 67 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
               Write(Html.DisplayFor(modelItem => item.HaveVehicle));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 70 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
               Write(Html.DisplayFor(modelItem => item.Status));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 73 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
               Write(Html.DisplayFor(modelItem => item.TotalPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 76 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
               Write(Html.DisplayFor(modelItem => item.Cc.Ccid));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 79 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
               Write(Html.DisplayFor(modelItem => item.Pricing.DurationType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 82 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
               Write(Html.DisplayFor(modelItem => item.Slot.Position));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 85 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
               Write(Html.DisplayFor(modelItem => item.Vehicle.VehicleId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e185b6196a0f155f52a925b83056e863011e96ea12602", async() => {
                WriteLiteral("Details");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 88 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
                                              WriteLiteral(item.ParkingInfoId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 91 "C:\Project\NET\Advanced\Group\Iteration07.5\ParkingManagement\ParkingManagementWebApp\Pages\ParkingInfos\History.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ParkingManagementWebApp.Pages.ParkingInfos.HistoryModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ParkingManagementWebApp.Pages.ParkingInfos.HistoryModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ParkingManagementWebApp.Pages.ParkingInfos.HistoryModel>)PageContext?.ViewData;
        public ParkingManagementWebApp.Pages.ParkingInfos.HistoryModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591