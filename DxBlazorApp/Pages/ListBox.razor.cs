
using Microgate.Extranet.Models;
using Microgate.Extranet.Services;
using Microsoft.AspNetCore.Components;

namespace DxBlazorApp.Pages;

public partial class ListBox
{
    public IEnumerable<AspNetRole> Roles { get; set; }

    public IEnumerable<AspNetRole> RolesSelected { get; set; }
    [Inject] public RoleService roleService { get; set; }
    protected override  async Task OnInitializedAsync()
    {

        Roles = await roleService.ListRolesAsync();
        RolesSelected = await roleService.GetUserRolesByUserId("2edb20e4-bc5b-4a2b-8fde-af29b67b750e");

    }

    //private IEnumerable<AspNetRole> GetAllRoles()
    //{
    //    List<AspNetRole> roles = new List<AspNetRole>();
    //    roles.Add(new AspNetRole { Id = "ADM", Name = "Admin" });
    //    roles.Add(new AspNetRole { Id = "SUP", Name = "Power User" });
    //    roles.Add(new AspNetRole { Id = "USR", Name = "User" });
    //    return roles;
    //}

    //private IEnumerable<AspNetRole> GetMyRoles()
    //{
    //    List<AspNetRole> roles = new List<AspNetRole>();
    //    roles.Add(new AspNetRole { Id = "ADM", Name = "Admin" });
    //    return roles;
    //}
}