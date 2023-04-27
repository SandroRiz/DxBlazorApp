
using DxBlazorApp.Models;

namespace DxBlazorApp.Pages;

public partial class ListBox
{
    public IEnumerable<Role> Roles { get; set; } 
    public IEnumerable<Role> RolesSelected { get; set; }

    protected override  void OnInitialized()
    {
        
        Roles = GetAllRoles();
        RolesSelected = GetMyRoles();
       
    }

    private IEnumerable<Role> GetAllRoles()
    {
        List<Role> roles = new List<Role>();
        roles.Add(new Role { Id = "ADM", Name = "Admin" });
        roles.Add(new Role { Id = "SUP", Name = "Power User" });
        roles.Add(new Role { Id = "USR", Name = "User" });
        return roles;
    }

    private IEnumerable<Role> GetMyRoles()
    {
        List<Role> roles = new List<Role>();
        roles.Add(new Role { Id = "ADM", Name = "Admin" });
        return roles;
    }
}