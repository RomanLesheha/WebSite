using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebSite.Models;

namespace WebSite.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public List<FavouriteProducts> FavouriteProducts { get; set; } = new List<FavouriteProducts>();

    public List<LastVisited> LastVisitedProducts { get; set; } = new List<LastVisited>();
    public List<Order> Orders { get; set; } = new List<Order>();
}

public class FavouriteProducts
{
    public int Id { get; set; }
    public string ProductID { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}

public class LastVisited
{
    public int Id { get; set; }
    public string ProductID { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}

